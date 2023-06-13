using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHealth : LivingEntity
{
    public int count = 1;
    public override void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPosition, hitNormal);
        StartCoroutine(ShowPaintEffect(hitPosition, hitNormal));
    }

    private IEnumerator ShowPaintEffect(Vector3 hitPosition, Vector3 hitNormal)
    {
        EffectManager.Instance.PlayHitEffect(hitPosition, hitNormal, transform, EffectManager.EffectType.Paint);
        yield return new WaitForSeconds(1.0f);
    }
    public override void Die()
    {
        base.Die();
        GetComponent<GhostControl>().state = GhostControl.State.Die;
        GameManager.instance.AddScore(count);
        SpawnGhost.Instance.fullGhost--;
    }
}
