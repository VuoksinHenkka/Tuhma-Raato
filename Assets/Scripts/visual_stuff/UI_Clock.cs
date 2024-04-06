using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Clock : MonoBehaviour
{
    public Image fill;
    public TMP_Text text;
    public int currentTime = 0;
    public float fillAmount = 0;
    public string AM = "AM";

    public UI_Image_ScaleAndColour ourScaleFX;
    void Update()
    {
        if (currentTime != GameManager.Instance.Time_Hour)
        {
            currentTime = GameManager.Instance.Time_Hour;
            if (currentTime > 12) AM = "PM";
            else AM = "AM";
            text.text = currentTime + AM;
            ourScaleFX.Call_ModifyScale();
        }
        fillAmount = Mathf.InverseLerp(59, 0, GameManager.Instance.Time_Minute);
        if (fill.fillAmount != fillAmount) fill.fillAmount = fillAmount;
    }
}
