using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class trigger_helicopterzone : MonoBehaviour
{

    public Image ourGroundCircle;
    public float ladderlerp = 0;
    private bool PlayerNear = false;
    public GameObject LadderInteractionObject;
    public GameObject Ladder;
    public float LadderStartY = 9;
    public float LadderEndY = 0.75f;
    public Light ourSpotLight;
    public Color groundciclecolor_off;
    public Color groundciclecolor_on;
    public TMP_Text EvacStatusText;
    public GameObject EvacPromptText;
    public GameObject ourRootObject;

    private Vector3 originalOffset = Vector3.zero;

    private void Awake()
    {
        originalOffset = transform.localPosition;
    }

    private void Start()
    {
        GameManager.Instance.onGameBegin += Reset;    
    }


    private void Reset()
    {
        PlayerNear = false;
        LadderInteractionObject.SetActive(false);
        ladderlerp = 0;
        Ladder.transform.localPosition = new Vector3(originalOffset.x, Mathf.Lerp(LadderStartY, LadderEndY, ladderlerp), originalOffset.z);
        ourSpotLight.intensity = 2000;
        ourGroundCircle.color = groundciclecolor_off;
        EvacStatusText.text = "0%";
        EvacStatusText.color = Color.yellow;
        EvacPromptText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerNear = true;
            GameManager.Instance.PlayersLightAmount += 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerNear = false;
            GameManager.Instance.PlayersLightAmount -= 1;
        }
    }


    public void Update()
    {
        if (PlayerNear)
        {
            if (ourGroundCircle.color != groundciclecolor_on) ourGroundCircle.color = groundciclecolor_on;
            ourSpotLight.intensity = 4000;
            if (ladderlerp != 1)
            {
                EvacStatusText.color = Color.yellow;
                ladderlerp = Mathf.Clamp(ladderlerp += 0.025f * Time.deltaTime, 0, 1);
                EvacStatusText.text = Mathf.RoundToInt(Mathf.Lerp(0, 100, ladderlerp)).ToString() + "%";
            }
            else
            {
                EvacStatusText.text = "Ready";
                EvacStatusText.color = Color.green;
                if (LadderInteractionObject.activeSelf == false) LadderInteractionObject.SetActive(true);
                EvacPromptText.SetActive(true);
            }
        }
        else
        {
            if (ourGroundCircle.color != groundciclecolor_off) ourGroundCircle.color = groundciclecolor_off;
            ourSpotLight.intensity = 2000;
        }
        Ladder.transform.localPosition = new Vector3(originalOffset.x, Mathf.Lerp(LadderStartY, LadderEndY, ladderlerp), originalOffset.z);
    }


    public void UseLadder()
    {
        GameManager.Instance.Game_Finished();
        ourRootObject.SetActive(false);
    }

}
