using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public enum gamestate { Gameplay, Menu, GameOver, Inventory}
    public gamestate currentGameSate = gamestate.Gameplay;
    public int Time_Hour = 19;
    public float Time_Minute = 0;
    private float ClockSpeed = 1f;
    public float GameSpeed = 1;
    public float GameSpeedInsanityModifier = 0;
    public float cooldownTimer = 0;
    public float MaxCooldown = 1;
    public Light ourSun;
    public Gradient ourSun_colour_beforemidnight;
    public Gradient ourSun_colour_pastmidnight;
    public NavMeshSurface ref_NavMeshSurface;
    public ItemManager ref_ItemSolver;
    public MessageSpawner ref_messagespawner;
    public particlespawner ref_particlespawner;
    public gameSpawnZombies ourzombiespawner;
    public Stats ref_Stats;
    private Color ourSun_colour_lerpTarget;
    public GameCamera ref_Camera;
    public GamePlayer ref_Player;

    public int activeNormalZombies = 0;
    public int activeSuperzombies = 0;
    public int activeFastZombies = 0;

    public int PlayersLightAmount = 0;

    public MainMenu ourMainMenu;
    public StoryBeat ourStoryBeats;


    public delegate void OnGameBegin();
    public event OnGameBegin onGameBegin;

    public delegate void OnGameOver();
    public event OnGameOver onGameOver;

    public delegate void OnGameEnding();
    public event OnGameEnding onGameEnding;

    
    public delegate void OnEvacZoneActive();
    public event OnEvacZoneActive onEvacZoneActive;
    //peli alkaa klo 19 ja loppuu klo 4am, kun helikopteri tulee

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

        ourMainMenu.gameObject.SetActive(true);
        currentGameSate = gamestate.Menu;
    }

    private void Start()
    {
        ourSun.color = ourSun_colour_beforemidnight.Evaluate(0);
    }


    private void Update()
    {
        if (ref_Stats.CurrentInsanityFX == Stats.InsanityFX.SpeedUpGame) GameSpeedInsanityModifier = 1;
        else GameSpeedInsanityModifier = 0;

        if (currentGameSate == gamestate.Menu) Time.timeScale = 0;
        else if (currentGameSate == gamestate.Inventory) Time.timeScale = 0.25f + GameSpeedInsanityModifier;
        else Time.timeScale = GameSpeed + GameSpeedInsanityModifier;
        if (currentGameSate == gamestate.Gameplay)
        {
            if (Time_Hour != 4)
            {
                if (Time_Minute != 60) Time_Minute = Mathf.Clamp(Time_Minute += ClockSpeed * Time.deltaTime, 0, 60);
                else
                {
                    Time_Minute = 0;
                    UpdateClock();

                }
            }
            ourSun_colour_lerpTarget = UpdateSunColour();
            ourSun.color = Color.Lerp(ourSun.color, ourSun_colour_lerpTarget, 0.1f * Time.deltaTime);
        }

        if (ref_Stats)
        {
            if (ref_Stats.HP == 0)
            {
                if (currentGameSate == gamestate.Gameplay || currentGameSate == gamestate.Inventory)
                Game_GameOver();
            }
        }

        if (Time_Hour > 5 && Time_Hour < 8) ref_Stats.Sanity_ItsNight = false;
        else if (Time_Hour > 12 && Time_Hour < 21) ref_Stats.Sanity_ItsNight = false;
        else ref_Stats.Sanity_ItsNight = true;


        if (ref_Stats.Sanity_ItsNight)
        {
            if (PlayersLightAmount == 0) ref_Stats.Sanity_Modify(-(1.25f * Time.deltaTime));
            else ref_Stats.Sanity_Modify(8f * Time.deltaTime);
        }

        if (ref_Stats.CurrentInsanityFX == Stats.InsanityFX.EatHP) ref_Stats.HP_ModifyNoMessage(-3 * Time.deltaTime);
        if (ref_Stats.CurrentInsanityFX == Stats.InsanityFX.EatStamina) ref_Stats.Stamina_Modify(-4 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           if(currentGameSate == gamestate.Gameplay)
            {
                ourMainMenu.gameObject.SetActive(true);
                currentGameSate = gamestate.Menu;
            }
        }

    }

    public void ModifyPlayersLightAmount(int byAmount)
    {
        PlayersLightAmount = PlayersLightAmount + byAmount;
        if (PlayersLightAmount < 0) PlayersLightAmount = 0;
    }

    private Color UpdateSunColour()
    {
        if (Time_Hour > 17) //pelin alku, peli alkaa klo 19
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

        if (Time_Hour == 4) EvacZoneActive();
    }

    public void EvacZoneActive()
    {
        if (onEvacZoneActive != null) onEvacZoneActive.Invoke();

    }

    public void Game_Finished()
    {
        if (onGameEnding != null) onGameEnding.Invoke();
        ourStoryBeats.Open_Outro();
    }

    public void Game_Resume()
    {
        currentGameSate = gamestate.Gameplay;
    }

    public void Game_GameOver()
    {
        if(onGameOver != null)onGameOver.Invoke();
        ourStoryBeats.Open_Death();

    }

    public void Game_Quit()
    {
        Application.Quit();
    }

    public void Game_NewGame()
    {
        if (onGameBegin != null) onGameBegin.Invoke();
        Time_Hour = 19;
        Time_Minute = 0;
        ClockSpeed = 1.25f;
        GameSpeed = 1;
        GameSpeedInsanityModifier = 0;
        cooldownTimer = 0;
        MaxCooldown = 1;
        ourSun.color = ourSun_colour_beforemidnight.Evaluate(0);
        ourStoryBeats.Open_Intro();

        ref_Stats.Reset();
    }
}
