using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UIManager>();
            return instance;
        }
    }

    private void Update()
    {
        UpDownPaint();
    }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image[] paints;
    [SerializeField] private GameObject waterGun;
    [SerializeField] private AudioSource[] audioes;

    Gun gun;

    private void Start()
    {
        gun = waterGun.GetComponent<Gun>();
    }
    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "잡은 마릿수 : " + newScore + "마리";
    }
    public void VolumeSet(float sound)
    {
        audioes[0].volume = sound;
    }
    public void MusicSet(float sound)
    {
        audioes[1].volume = sound;
    }

    public void UpDownPaint()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun.ChangeMaterial(0);
            gun.ChangeEffectColor(0);
            EffectManager.Instance.ChangeEffectColor(0);
            for(int i = 0; i < 5; i++)
            {
                gun.paint[i].color = Color.red;
            }
            paints[0].rectTransform.localPosition = new Vector3(-360, -120, 0);
            paints[1].rectTransform.localPosition = new Vector3(-305, -160, 0);
            paints[2].rectTransform.localPosition = new Vector3(-250, -160, 0);
            paints[3].rectTransform.localPosition = new Vector3(-195, -160, 0);
        }                                                           
        else if (Input.GetKeyDown(KeyCode.Alpha2))                  
        {
            gun.ChangeMaterial(1);
            gun.ChangeEffectColor(1);
            EffectManager.Instance.ChangeEffectColor(1);
            for (int i = 0; i < 5; i++)
            {
                gun.paint[i].color = Color.yellow;
            }
            paints[0].rectTransform.localPosition = new Vector3(-360, -160, 0);
            paints[1].rectTransform.localPosition = new Vector3(-305, -120, 0);
            paints[2].rectTransform.localPosition = new Vector3(-250, -160, 0);
            paints[3].rectTransform.localPosition = new Vector3(-195, -160, 0);
        }                                                           
        else if (Input.GetKeyDown(KeyCode.Alpha3))                  
        {
            gun.ChangeMaterial(2);
            gun.ChangeEffectColor(2);
            EffectManager.Instance.ChangeEffectColor(2);
            for (int i = 0; i < 5; i++)
            {
                gun.paint[i].color = Color.blue;
            }
            paints[0].rectTransform.localPosition = new Vector3(-360, -160, 0);
            paints[1].rectTransform.localPosition = new Vector3(-305, -160, 0);
            paints[2].rectTransform.localPosition = new Vector3(-250, -120, 0);
            paints[3].rectTransform.localPosition = new Vector3(-195, -160, 0);
        }                                                           
        else if (Input.GetKeyDown(KeyCode.Alpha4))                  
        {
            gun.ChangeMaterial(3);
            gun.ChangeEffectColor(3);
            EffectManager.Instance.ChangeEffectColor(3);
            for (int i = 0; i < 5; i++)
            {
                gun.paint[i].color = Color.white;
            }
            paints[0].rectTransform.localPosition = new Vector3(-360, -160, 0);
            paints[1].rectTransform.localPosition = new Vector3(-305, -160, 0);
            paints[2].rectTransform.localPosition = new Vector3(-250, -160, 0);
            paints[3].rectTransform.localPosition = new Vector3(-195, -120, 0);
        }
    }
}
