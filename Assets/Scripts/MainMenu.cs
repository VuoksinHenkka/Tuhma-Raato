using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void ClickNewGame()
    {
        GameManager.Instance.Game_NewGame();
        gameObject.SetActive(false);
    }
    public void ClickResume()
    {
        GameManager.Instance.Game_Resume();
        gameObject.SetActive(false);
    }
    public void ClickQuit()
    {
        GameManager.Instance.Game_Quit();
    }
}
