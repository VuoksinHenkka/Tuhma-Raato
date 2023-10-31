using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    private float MoveSpeed_Run = 8;
    private float MoveSpeed_Walk = 3;

    public bool Running = false;
    public float MoveSpeed = 0;

    public Vector3 MoveVector_FromInput = Vector3.zero;
    public CharacterController ourCharacterController;
    public characterGFX ourcharacterGFX;


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

        if (Running) MoveSpeed = MoveSpeed_Run;
        else MoveSpeed = MoveSpeed_Walk;


        MoveVector_FromInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 MoveVector_Final = (GameManager.Instance.ref_Camera.moveTransform.right * MoveVector_FromInput.x) + (GameManager.Instance.ref_Camera.moveTransform.forward * MoveVector_FromInput.z);
        ourCharacterController.Move(MoveVector_Final * (MoveSpeed*Time.deltaTime));

        if (ourcharacterGFX) ourcharacterGFX.LookToDirection = transform.position + MoveVector_Final;


    }
}
