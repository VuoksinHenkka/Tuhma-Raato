using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    public float MoveSpeed = 8;
    public Vector3 MoveVector_FromInput = Vector3.zero;
    public CharacterController ourCharacterController;


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

        MoveVector_FromInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 MoveVector_Final = (GameManager.Instance.ref_Camera.moveTransform.right * MoveVector_FromInput.x) + (GameManager.Instance.ref_Camera.moveTransform.forward * MoveVector_FromInput.z);
        ourCharacterController.Move(MoveVector_Final * (MoveSpeed*Time.deltaTime));

    }
}
