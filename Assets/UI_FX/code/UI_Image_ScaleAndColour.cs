using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Image_ScaleAndColour : UI_ScaleAndColour
{

    public Image ourTarget;


    private void Awake()
    {
        
        if (!ourTarget) ourTarget = GetComponent<Image>();
        if (!ourTarget)
        {
            print("WARNING: UI_Juice on " + gameObject.name + " did not find a Image component. Disabling script.");
            this.enabled = false;
        }
        SetOriginalScale(ourTarget.transform.localScale);
        SetOriginalColour(ourTarget.color);
    }
    public override void UpdateTargetScale(Vector3 newScale)
    {
        ourTarget.transform.localScale = newScale;

    }
    public override void UpdateTargetColor(Color newColor)
    {
        ourTarget.color = newColor;
    }

    public override bool CheckifTargetScaleMatches(Vector3 targetScale)
    {
        return ourTarget.transform.localScale.Equals(targetScale);
    }
    public override bool CheckifTargetColorMatches(Color targetColor)
    {
        return ourTarget.color.Equals(targetColor);
    }

    public override void OnDisable_Inheritance()
    {
        ourTarget.color = giveOriginalColour();
    }
    public override void OnEnable_Inheritance()
    {

    }

}
