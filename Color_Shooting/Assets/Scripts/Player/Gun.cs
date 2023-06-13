using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }
    public State state { get; private set; }
    public Transform firePosition;
    public ParticleSystem paintEffect; //총구 화염
    public float bulletLineEffetTime = 0.03f; //라인렌더러 유지 시간

    private LineRenderer bulletLineRenderer;
    public float damage = 25;
    public float fireDistance = 50f;
    public int magCapacity = 10; //탄창용량
    public int magAmmo;  //현재 남은 탄알
    public float timeBetFire = 0.12f; //탄알 발사 간격
    public float reloadTime = 1.0f; //재장전 소요시간
    public float lastFireTime; //총을 마지막으로 발사한 시간


    //오디오 소스
    /*private AudioSource audioSource;
    public AudioClip shootAudio;
    public AudioClip reloadAudio;*/

    private void Awake()
    {
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;

        //audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        magAmmo = magCapacity;
        state = State.Ready;
        lastFireTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Fire(Vector3 aimPoint)
    {
        //발사 가능한 조건
        if (state == State.Ready && Time.time >= lastFireTime + timeBetFire)
        {
            var fireDirection = aimPoint - firePosition.transform.position;

            lastFireTime = Time.time;
            Shot(fireDirection);
            return true;
        }
        return false;
    }

    private void Shot(Vector3 fireDirection)
    {
        //레이캐스트
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        //쏘는 위치, 쏘는 방향, out hit
        if (Physics.Raycast(firePosition.position, fireDirection, out hit, fireDistance))
        {
            var target = hit.collider.GetComponent<IDamageable>();
            //총알에 맞았고 해당 충돌체가 데미지를 입을 수 있다면 OnDamage를 통해 데미지 처리
            if (target != null)
            {
                target.OnDamage(damage, hit.point, hit.normal);
            }
            else
            {
                EffectManager.Instance.PlayHitEffect(hit.point, hit.normal, hit.transform);
                Debug.Log("This is not Ghost!");
            }
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = firePosition.position + fireDirection * fireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));
        magAmmo--;
        //UIManager.Instance.UpdateAmmoText(magAmmo);
        if (magAmmo <= 0)
        {
            state = State.Empty;
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        //audioSource.clip = shootAudio;
        //audioSource.Play();
        paintEffect.Play();
        bulletLineRenderer.SetPosition(0, firePosition.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(bulletLineEffetTime);
        bulletLineRenderer.enabled = false;
    }

    public bool Reload()
    {
        //재장전이 가능하지 않은 조건(상태, 탄알)
        if (state == State.Reloading || magAmmo >= magCapacity)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        return true;
    }


    public IEnumerator ReloadRoutine()
    {
        //audioSource.clip = reloadAudio;
        //audioSource.Play();

        state = State.Reloading;

        yield return new WaitForSeconds(reloadTime);
        magAmmo = magCapacity;
        //UIManager.Instance.UpdateAmmoText(magAmmo);
        state = State.Ready;
    }
}
