using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Image[] panels;

    public void StartBtn(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void GuideBtn(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
    public void BeforeNextBtn(string command)
    {
        if(command == "before")
        {
            panels[0].gameObject.SetActive(true);
            panels[1].gameObject.SetActive(false);
        }
        else if (command == "after")
        {
            panels[2].gameObject.SetActive(true);
            panels[1].gameObject.SetActive(false);
        }
    }
    public void GoBack(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OpenClosePanel(Image panel)
    {
        panel.gameObject.SetActive(!panel.gameObject.activeSelf);
        Time.timeScale = 1f;
        GameManager.instance.HideCursor(true);
    }
    public void Restart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
