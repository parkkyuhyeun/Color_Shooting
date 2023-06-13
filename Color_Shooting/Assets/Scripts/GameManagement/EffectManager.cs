using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static EffectManager m_Instance;
    public static EffectManager Instance
    {
        get
        {
            if (m_Instance == null) m_Instance = FindAnyObjectByType<EffectManager>();
            return m_Instance;
        }
    }

    public enum EffectType
    {
        Paint,
    }

    public ParticleSystem paintHitEffectPrefab;


    public void PlayHitEffect(Vector3 pos, Vector3 normal, Transform parent = null, EffectType effectType = EffectType.Paint)
    {
        var targetPrefab = paintHitEffectPrefab;

        var effect = Instantiate(targetPrefab, pos, Quaternion.LookRotation(normal));

        if (parent != null) effect.transform.SetParent(parent);
        effect.Play();

    } //parent 사용 이유: 움직이는 물체에  effect를 단다면 effect도 따라가야하니 자식으로 만들어 effect실행 
}