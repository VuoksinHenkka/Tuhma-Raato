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

    private float movespeed_current = 0;

    private void Awake()
    {
        ourCharacterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        GameManager.Instance.ref_Player = this;
    }

    private void Update()
    {
        if (GameManager.Instance.currentGameSate != GameManager.gamestate.Gameplay) return;

        if (Input.GetKeyDown(KeyCode.LeftShift)) Running = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) Running = false;

        if (Running && GameManager.Instance.ref_Stats.Stamina > 25) MoveSpeed = MoveSpeed_Run;
        else MoveSpeed = MoveSpeed_Walk;


        MoveVector_FromInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 MoveVector_Final = (GameManager.Instance.ref_Camera.moveTransform.right * MoveVector_FromInput.x) + (GameManager.Instance.ref_Camera.moveTransform.forward * MoveVector_FromInput.z);

        if (MoveVector_Final != Vector3.zero && Running) GameManager.Instance.ref_Stats.Stamina_Modify(-3 * Time.deltaTime);
        else GameManager.Instance.ref_Stats.Stamina_Modify(0.5f * Time.deltaTime);
        ourCharacterController.Move(MoveVector_Final * (MoveSpeed * Time.deltaTime));

        
        if (ourcharacterGFX)
        {
            ourcharacterGFX.LookToDirection = transform.position + MoveVector_Final;
            float targetmovespeed = ourCharacterController.velocity.magnitude;
            if (movespeed_current < targetmovespeed) movespeed_current = Mathf.Clamp(movespeed_current += 15f * Time.deltaTime,0, targetmovespeed);
            else if (movespeed_current > targetmovespeed) movespeed_current = Mathf.Clamp(movespeed_current -= 15f * Time.deltaTime, targetmovespeed, 10);
            ourcharacterGFX.ourMoveVelocity = movespeed_current;
        }

        if (transform.position.y != 1.1f)
        {
            transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
        }
    }
}
