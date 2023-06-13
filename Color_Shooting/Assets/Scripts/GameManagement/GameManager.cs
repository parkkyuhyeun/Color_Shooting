using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int count;
    public bool isGameOver = false;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Warning : So many GameManager Object");
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        HideCursor(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) HideCursor(false);
        if (Input.GetMouseButtonDown(1)) HideCursor(true);
    }
    private void HideCursor(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !state;
    }
    public void AddScore(int newScore)
    {
        //게임 오버가 아니면 점수를 newScore만큼 증가해라!
        if (!isGameOver)
        {
            count += newScore;
            UIManager.Instance.UpdateScoreText(count);
        }
    }
}
