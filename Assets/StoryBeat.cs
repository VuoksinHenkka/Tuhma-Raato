using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBeat : MonoBehaviour
{

    private void OnEnable()
    {
        GameManager.Instance.currentGameSate = GameManager.gamestate.Menu;

    }

    public void Open_Intro()
    {
        gameObject.SetActive(true);
    }

    public void Open_Outro()
    {
        gameObject.SetActive(true);
    }
    public void Open_Death()
    {
        gameObject.SetActive(true);
    }

    public void Click_Resume()
    {
        gameObject.SetActive(false);
        GameManager.Instance.currentGameSate = GameManager.gamestate.Gameplay;
    }

    public void Click_Restart()
    {
        GameManager.Instance.Game_NewGame();
    }
}
