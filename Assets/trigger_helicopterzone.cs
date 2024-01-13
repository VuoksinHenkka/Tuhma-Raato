using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_helicopterzone : MonoBehaviour
{

    public float ladderlerp = 0;
    private bool PlayerNear = false;
    public GameObject LadderInteractionObject;
    public GameObject Ladder;
    public float LadderStartY = 9;
    public float LadderEndY = 0.75f;
    public Light ourSpotLight;


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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerNear = false;
        }
    }


    public void Update()
    {
        if (PlayerNear)
        {
            ourSpotLight.intensity = 4000;
            if (ladderlerp != 1)
            {
                ladderlerp = Mathf.Clamp(ladderlerp += 0.01f * Time.deltaTime, 0, 1);
            }
            else
            {
                if (LadderInteractionObject.activeSelf == false) LadderInteractionObject.SetActive(true);
            }
        }
        else ourSpotLight.intensity = 2000;
        Ladder.transform.localPosition = new Vector3(originalOffset.x, Mathf.Lerp(LadderStartY, LadderEndY, ladderlerp), originalOffset.z);
    }
}
