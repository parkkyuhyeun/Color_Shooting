using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float bulletLineEffectTime = 0.03f; //라인렌더러 유지 시간

    private LineRenderer bulletLineRenderer;
    public float damage = 10;
    public float fireDistance = 50f;
    public int magCapacity = 10; //탄창용량
    public int magAmmo;  //현재 남은 탄알
    public float timeBetFire = 0.12f; //탄알 발사 간격
    public float reloadTime = 1.0f; //재장전 소요시간
    public float lastFireTime; //총을 마지막으로 발사한 시간

    [SerializeField]
    private List<Material> matList;
    [SerializeField]
    private List<ParticleSystem> paints;

    [Header("BulletUI")]
    public Image[] paint;
    public Sprite[] paintStates;
    private int index = 4;
    //오디오 소스
    private AudioSource audioSource;
    public AudioClip shootAudio;
    public AudioClip reloadAudio;

    private void Awake()
    {
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
        audioSource = GetComponent<AudioSource>();
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
            GameObject Enemy = hit.collider.gameObject;
            MeshRenderer meshRenderer = Enemy.GetComponent<MeshRenderer>();
            var target = hit.collider.GetComponent<IDamageable>();
            //총알에 맞았고 해당 충돌체가 데미지를 입을 수 있다면 OnDamage를 통해 데미지 처리
            if (target != null)
            {
                Material mat = meshRenderer.material;
                
                if (bulletLineRenderer.material.name == mat.name)
                {
                    damage = 20;
                    print("ifDamage:" + damage);
                }
                else
                {
                    damage = 10;
                    print("elseDamage:" + damage);
                }
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
        ChangeBulletState();
        //UIManager.Instance.UpdateAmmoText(magAmmo);
        if (magAmmo <= 0)
        {
            state = State.Empty;
        }
    }

    //잔여 총알 여부 표시 UI 설정
    public void ChangeBulletState()
    {
        if (paint[index].sprite == paintStates[0])
        {
            paint[index].sprite = paintStates[1];
        }
        else
        {
            paint[index].sprite = paintStates[2];
            if (index != 0) index--;
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        audioSource.clip = shootAudio;
        StartCoroutine(ShotSound());
        paintEffect.Play();
        bulletLineRenderer.SetPosition(0, firePosition.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(bulletLineEffectTime);
        bulletLineRenderer.enabled = false;
    }

    IEnumerator ShotSound()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        audioSource.Stop();
    }

    public bool Reload()
    {
        //재장전이 가능하지 않은 조건(상태, 탄알)
        if (state == State.Reloading || magAmmo >= magCapacity)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        StartCoroutine(FillBullet());
        return true;
    }

    //총알 메테리얼 바꾸기
    public void ChangeMaterial(int index)
    {
        bulletLineRenderer.material = matList[index];
    }
    //이펙트 컬러 바꾸기
    public void ChangeEffectColor(int index)
    {
        paintEffect = paints[index];
    }

    public IEnumerator ReloadRoutine()
    {
        audioSource.clip = reloadAudio;
        audioSource.Play();

        state = State.Reloading;

        yield return new WaitForSeconds(reloadTime);
        audioSource.Stop();
        magAmmo = magCapacity;
        //UIManager.Instance.UpdateAmmoText(magAmmo);
        state = State.Ready;
    }
    public IEnumerator FillBullet()
    {
        for (int i = index; i < 5; i++)
        {
            paint[i].sprite = paintStates[0];
            yield return new WaitForSeconds(0.1f);
        }
        index = 4;
    }
}
