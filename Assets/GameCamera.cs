using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    public Camera ourCamera;
    public Transform moveTransform;


    private Vector3 moveTransform_originalposition;
    private void Awake()
    {
        ourCamera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ref_Camera = this;
    }


    public void Update()
    {
        if (GameManager.Instance.currentGameSate != GameManager.gamestate.Gameplay) return;
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        ourCamera.transform.LookAt(GameManager.Instance.ref_Player.transform.position, Vector3.up);
        moveTransform.transform.LookAt(new Vector3(GameManager.Instance.ref_Player.transform.position.x, moveTransform.transform.position.y, GameManager.Instance.ref_Player.transform.position.z), Vector3.up);
    }
}
