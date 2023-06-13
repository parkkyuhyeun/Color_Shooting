using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [SerializeField] private TextMeshProUGUI scoreText;

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }
}
