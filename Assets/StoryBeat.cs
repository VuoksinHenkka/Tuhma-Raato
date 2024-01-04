using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBeat : MonoBehaviour
{
    public GameObject onIntro_objects;
    public GameObject onOutro_objects;
    public GameObject onDeath_objects;

    private void OnEnable()
    {
        GameManager.Instance.currentGameSate = GameManager.gamestate.Menu;

    }

    public void Open_Intro()
    {
        onIntro_objects.SetActive(true);
        onOutro_objects.SetActive(false);
        onDeath_objects.SetActive(false);
        gameObject.SetActive(true);

    }

    public void Open_Outro()
    {
        onIntro_objects.SetActive(false);
        onOutro_objects.SetActive(true);
        onDeath_objects.SetActive(false);

        gameObject.SetActive(true);
    }

    [ContextMenu("Open_OnDeath")]
    public void Open_Death()
    {
        onIntro_objects.SetActive(false);
        onOutro_objects.SetActive(false);
        onDeath_objects.SetActive(true);

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
