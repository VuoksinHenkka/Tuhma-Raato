using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPunch : MonoBehaviour
{

    [SerializeField] public Camera PunchCamera;
    [SerializeField]public float FOV_original;

    private void Awake()
    {
        FOV_original = PunchCamera.fieldOfView;
    }

    private void Start()
    {
        GameManager.Instance.ref_Stats.onHurt += Hurt;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance) GameManager.Instance.ref_Stats.onHurt -= Hurt;

    }

    public void Hurt()
    {
        PunchCamera.fieldOfView = FOV_original + 0.5f;
    }
    private void Update()
    {

        if (PunchCamera.fieldOfView > FOV_original)
        {
            PunchCamera.fieldOfView = Mathf.Clamp(PunchCamera.fieldOfView -= 0.5f * Time.deltaTime, FOV_original, 100);
        }
        else if (PunchCamera.fieldOfView < FOV_original)
        {
            PunchCamera.fieldOfView = Mathf.Clamp(PunchCamera.fieldOfView += 0.5f * Time.deltaTime, 0.1f, FOV_original);
        }
        
    }

}
