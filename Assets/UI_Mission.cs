using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Mission : MonoBehaviour
{
    public TMP_Text ourText;

    private void Start()
    {
        GameManager.Instance.onGameBegin += Reset;
        GameManager.Instance.onEvacZoneActive += EvacText;

    }

    public void Reset()
    {
        ourText.text = "Survive until 4am";
    }
    public void EvacText()
    {
        ourText.text = "Get to the chopper";
    }
}
