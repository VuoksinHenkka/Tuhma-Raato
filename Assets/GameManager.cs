using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum gamestate { Gameplay, Menu, GameOver, Inventory}
    public gamestate currentGameSate = gamestate.Gameplay;
    public int Time_Hour = 18;
    public float Time_Minute = 0;
    private float ClockSpeed = 1f;
    public float GameSpeed = 1;

    public Light ourSun;
    public Gradient ourSun_colour_beforemidnight;
    public Gradient ourSun_colour_pastmidnight;
    public NavMeshSurface ref_NavMeshSurface;
    public ItemManager ref_ItemSolver;
    public MessageSpawner ref_messagespawner;
    public Stats ref_Stats;
    private Color ourSun_colour_lerpTarget;
    public GameCamera ref_Camera;
    public GamePlayer ref_Player;

    //peli alkaa klo 18 ja loppuu klo 8.00

    private static GameManager _instance;
    public static GameManager Instance
    {
        get {
            if (_instance == null) Debug.Log("NO GAMEMANAGER INSTANCE FOUND");
            return _instance; 
            }
        set { _instance = value; }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        ourSun.color = ourSun_colour_beforemidnight.Evaluate(0);
    }


    private void Update()
    {
        if (currentGameSate == gamestate.Menu) Time.timeScale = 0;
        else if (currentGameSate == gamestate.Inventory) Time.timeScale = 0.25f;
        else Time.timeScale = GameSpeed;
        if (currentGameSate == gamestate.Gameplay)
        {
            if (Time_Minute != 60) Time_Minute = Mathf.Clamp(Time_Minute += ClockSpeed * Time.deltaTime, 0, 60);
            else
            {
                Time_Minute = 0;
                UpdateClock();

            }
            ourSun_colour_lerpTarget = UpdateSunColour();
            ourSun.color = Color.Lerp(ourSun.color, ourSun_colour_lerpTarget, 0.01f * Time.deltaTime);
            ref_Camera.ourCamera.backgroundColor = ourSun.color * 1.01f;
        }

        if (ref_Stats)
        {
            if (ref_Stats.HP == 0) GameOver();
        }
    }

    private Color UpdateSunColour()
    {
        if (Time_Hour > 17) //pelin alku
        {
            return ourSun_colour_beforemidnight.Evaluate(Mathf.InverseLerp(18,23, Time_Hour));
        }
        else
            return ourSun_colour_pastmidnight.Evaluate(Mathf.InverseLerp(0, 8, Time_Hour));

    }

    private void UpdateClock()
    {

        Time_Minute = 0;
        Time_Hour++;
        if (Time_Hour > 23) Time_Hour = 0;

        if (Time_Hour == 8) GameOver();
    }
    
    private void GameOver()
    {
        print("GAME OVER");
    }
}
