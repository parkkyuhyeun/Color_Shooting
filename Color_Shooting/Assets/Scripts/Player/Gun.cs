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
    public ParticleSystem paintEffect; //�ѱ� ȭ��
    public float bulletLineEffectTime = 0.03f; //���η����� ���� �ð�

    private LineRenderer bulletLineRenderer;
    public float damage = 10;
    public float fireDistance = 50f;
    public int magCapacity = 10; //źâ�뷮
    public int magAmmo;  //���� ���� ź��
    public float timeBetFire = 0.12f; //ź�� �߻� ����
    public float reloadTime = 1.0f; //������ �ҿ�ð�
    public float lastFireTime; //���� ���������� �߻��� �ð�

    [SerializeField]
    private List<Material> matList;
    [SerializeField]
    private List<ParticleSystem> paints;

    [Header("BulletUI")]
    public Image[] paint;
    public Sprite[] paintStates;
    private int index = 4;
    //����� �ҽ�
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
        //�߻� ������ ����
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
        //����ĳ��Ʈ
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        //��� ��ġ, ��� ����, out hit
        if (Physics.Raycast(firePosition.position, fireDirection, out hit, fireDistance))
        {
            GameObject Enemy = hit.collider.gameObject;
            MeshRenderer meshRenderer = Enemy.GetComponent<MeshRenderer>();
            var target = hit.collider.GetComponent<IDamageable>();
            //�Ѿ˿� �¾Ұ� �ش� �浹ü�� �������� ���� �� �ִٸ� OnDamage�� ���� ������ ó��
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

    //�ܿ� �Ѿ� ���� ǥ�� UI ����
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
        //�������� �������� ���� ����(����, ź��)
        if (state == State.Reloading || magAmmo >= magCapacity)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        StartCoroutine(FillBullet());
        return true;
    }

    //�Ѿ� ���׸��� �ٲٱ�
    public void ChangeMaterial(int index)
    {
        bulletLineRenderer.material = matList[index];
    }
    //����Ʈ �÷� �ٲٱ�
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
