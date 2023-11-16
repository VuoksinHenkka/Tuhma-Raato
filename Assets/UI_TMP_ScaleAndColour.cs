using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_TMP_ScaleAndColour : UI_ScaleAndColour
{
    public TMP_Text ourTarget;
    private void Awake()
    {

        if (!ourTarget) ourTarget = GetComponent<TMP_Text>();
        if (!ourTarget)
        {
            print("WARNING: UI_Juice on " + gameObject.name + " did not find a Image component. Disabling script.");
            this.enabled = false;
        }
        originalScale = ourTarget.transform.localScale;
        originalColor = ourTarget.color;
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
        ourTarget.color = originalColor;
    }
    public override void OnEnable_Inheritance()
    {

    }
}
