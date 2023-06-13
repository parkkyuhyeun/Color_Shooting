using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
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
}
