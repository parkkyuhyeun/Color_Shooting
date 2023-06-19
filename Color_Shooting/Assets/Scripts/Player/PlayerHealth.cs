using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Slider playerHP_Bar;
    [SerializeField] Image gameOverPanel;
    int maxValue = 10;
    bool coolTime = false;

    private void Start()
    {
        playerHP_Bar.value = maxValue;
        coolTime = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            if(playerHP_Bar.value != 0)
            {
                if (!coolTime)
                {
                    StartCoroutine(OnDamage());
                }
            }
            else if(playerHP_Bar.value == 0)
            {
                Death();
            }
        }
    }

    IEnumerator OnDamage()
    {
        playerHP_Bar.value--;
        coolTime = true;
        yield return new WaitForSeconds(1f);
        coolTime = false;
    }

    public void Death()
    {
        if(playerHP_Bar.value == 0)
        {
            print("Your Die!");
            GameManager.instance.isGameOver = true;
            gameOverPanel.gameObject.SetActive(true);
            GameManager.instance.SetPanelText();
        }
    }
}
