using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public TextMeshProUGUI bestScoreText;
    [SerializeField] public TextMeshProUGUI NowScoreText;
    [SerializeField] public GameObject escMenu;
    PlayerMove playerMove;
    public int count;
    public bool isGameOver = false;
    private int bestScore = 0;
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
        playerMove = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideCursor(false);
            escMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        if (Input.GetMouseButtonDown(1)) HideCursor(true);
    }
    public void HideCursor(bool state)
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
            if (bestScore < count)
            {
                bestScore = count;
            }
        }
    }
    public void SetPanelText()
    {
        bestScoreText.text = "Your Best Score : " + bestScore;
        NowScoreText.text = "Now Score : " + count;
    }
}
