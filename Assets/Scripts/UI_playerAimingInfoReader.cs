using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_playerAimingInfoReader : MonoBehaviour
{

    public UI_playerAiming_Rectangle ourTrackerElement;

    public GameObject ourInteractionTarget_Gameobject;
    private ItemReceiver ourInteractionTarget;
    public Transform ourPlayer;
    public Camera ourCamera;
    public Canvas ourCanvas;




    private void Update()
    {
        if (ourCamera == null)
        {
           if (GameManager.Instance.ref_Camera != null) ourCamera = GameManager.Instance.ref_Camera.ourCamera;
        }
        if (ourCamera == null) return;

        if (ourInteractionTarget_Gameobject)
        {
            ourInteractionTarget = ourInteractionTarget_Gameobject.GetComponent<ItemReceiver>();
        }
        else
        {
            ourInteractionTarget = null;
        }

        if (ourInteractionTarget)
        {

            Vector3 pos = ourCamera.WorldToScreenPoint(ourInteractionTarget.gameObject.transform.position);
            if (ourTrackerElement.transform.position != pos) ourTrackerElement.transform.position = pos;

                float DistanceReading_Inverselerp = Mathf.InverseLerp(0, 50, Vector3.Distance(ourInteractionTarget.gameObject.transform.position, ourPlayer.position));
                float DistanceReading_final = Mathf.Lerp(0, 50, DistanceReading_Inverselerp);
                ourTrackerElement.distancemeter.text = (Mathf.RoundToInt(DistanceReading_final)).ToString();
                if (ourTrackerElement.gameObject.active == false) ourTrackerElement.gameObject.SetActive(true);
                if (ourCanvas.enabled == false) ourCanvas.enabled = true;
            if (ourInteractionTarget is IHaveName)
            {
                ourTrackerElement.NameOfTarget = (ourInteractionTarget as IHaveName).GiveName();
            }
            else ourTrackerElement.NameOfTarget = "";

            if (ourInteractionTarget is ICanDie)
            {
                ourTrackerElement.HPofTarget = (ourInteractionTarget as ICanDie).GiveCurrentHP();
            }
            else ourTrackerElement.HPofTarget = "";
        }
        else
        {
            if (ourTrackerElement.gameObject.active) ourTrackerElement.gameObject.active = false;
            ourCanvas.enabled = false;
        }

    }

}
