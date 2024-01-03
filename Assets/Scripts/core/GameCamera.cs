using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameCamera : MonoBehaviour
{


    public Transform PlayerPosition_Dummy;
    public Camera ourCamera;
    public Transform moveTransform;
    public Vector2 playerPositionInScreenSpace;

    private Vector3 moveTransform_originalposition;
    private void Awake()
    {
        ourCamera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ref_Camera = this;
        PlayerPosition_Dummy.position = GameManager.Instance.ref_Player.transform.position;

    }


    public void Update()
    {
        if (GameManager.Instance.currentGameSate == GameManager.gamestate.Gameplay) PlayerPosition_Dummy.position = Vector3.Lerp(PlayerPosition_Dummy.position, GameManager.Instance.ref_Player.transform.position, 4 * Time.unscaledDeltaTime);
        else PlayerPosition_Dummy.position = Vector3.Lerp(PlayerPosition_Dummy.position, (GameManager.Instance.ref_Player.transform.position + (Vector3.down*3)), 4 * Time.unscaledDeltaTime);

        playerPositionInScreenSpace = ourCamera.WorldToScreenPoint(PlayerPosition_Dummy.position);
        ourCamera.transform.LookAt(PlayerPosition_Dummy.position, Vector3.up);
        moveTransform.transform.LookAt(new Vector3(PlayerPosition_Dummy.position.x, moveTransform.transform.position.y, PlayerPosition_Dummy.position.z), Vector3.up);
    }

}
