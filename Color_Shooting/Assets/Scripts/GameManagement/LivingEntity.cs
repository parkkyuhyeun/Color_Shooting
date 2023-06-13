using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float initHealth = 100f;

    public float health { get; protected set; }
    public bool dead { get; protected set; }

    public UnityEvent onDeath;

    //����ü�� Ȱ��ȭ�� �� ���¸� ����
    protected virtual void OnEnable()
    {
        dead = false;
        health = initHealth;
    }

    //������ �Դ� ���
    public virtual void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        health -= damage;
        if (!dead && health <= 0)
        {
            Die();
        }
    }

    //ü�� ȸ�� ���
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead) return;
        health += newHealth;
    }

    //��� ó��
    public virtual void Die()
    {
        onDeath?.Invoke();
        dead = true;
    }
}
