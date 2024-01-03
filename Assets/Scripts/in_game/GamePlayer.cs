using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    private float MoveSpeed_Run = 7;
    private float MoveSpeed_Walk = 2;

    public bool Running = false;
    public float MoveSpeed = 0;

    public Vector3 MoveVector_FromInput = Vector3.zero;
    public CharacterController ourCharacterController;
    public characterGFX ourcharacterGFX;
    public List<Transform> spawnpositions;
    private float movespeed_current = 0;

    private void Awake()
    {
        ourCharacterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        GameManager.Instance.ref_Player = this;
        GameManager.Instance.onGameBegin += Respawn;
    }

    public void Respawn()
    {
        transform.position = spawnpositions[Random.Range(0, spawnpositions.Count)].position;
    }

    private void OnDestroy()
    {
        GameManager.Instance.onGameBegin -= Respawn;

    }

    private void Update()
    {
        if (GameManager.Instance.currentGameSate != GameManager.gamestate.Gameplay) return;
        if (GameManager.Instance.allowMovement == false) return;

        if (Input.GetKeyDown(KeyCode.LeftShift)) Running = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) Running = false;

        if (Running && GameManager.Instance.ref_Stats.Stamina > 25) MoveSpeed = MoveSpeed_Run;
        else MoveSpeed = MoveSpeed_Walk;


        if (GameManager.Instance.ref_Stats.CurrentInsanityFX == Stats.InsanityFX.InvertControls) MoveVector_FromInput = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        else MoveVector_FromInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 MoveVector_Final = (GameManager.Instance.ref_Camera.moveTransform.right * MoveVector_FromInput.x) + (GameManager.Instance.ref_Camera.moveTransform.forward * MoveVector_FromInput.z);

        if (MoveVector_Final != Vector3.zero && Running) GameManager.Instance.ref_Stats.Stamina_Modify(-3 * Time.unscaledDeltaTime);
        else GameManager.Instance.ref_Stats.Stamina_Modify(0.5f * Time.unscaledDeltaTime);
        ourCharacterController.Move(MoveVector_Final * (MoveSpeed * Time.unscaledDeltaTime));


        if (ourcharacterGFX)
        {
            ourcharacterGFX.LookToDirection = transform.position + MoveVector_Final;
            float targetmovespeed = ourCharacterController.velocity.magnitude;
            if (movespeed_current < targetmovespeed) movespeed_current = Mathf.Clamp(movespeed_current += 15f * Time.unscaledDeltaTime, 0, targetmovespeed);
            else if (movespeed_current > targetmovespeed) movespeed_current = Mathf.Clamp(movespeed_current -= 15f * Time.unscaledDeltaTime, targetmovespeed, 10);
            ourcharacterGFX.ourMoveVelocity = movespeed_current;
        }
        if (transform.position.y != 1.1f)
        {
        }

    }
}
