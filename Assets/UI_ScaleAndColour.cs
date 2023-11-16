using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UI_ScaleAndColour : MonoBehaviour
{



    //BASECLASS. Inherit, then add a Target variable (TMP_Text,Image, whatever), then connect it to the virtual voids/bools

    public virtual void UpdateTargetScale(Vector3 newScale)
    {

    }
    public virtual void UpdateTargetColor(Color newColor)
    {

    }


    //default returns as false since we have no Target defined in this baseclass
    public virtual bool CheckifTargetScaleMatches(Vector3 targetScale)
    {
        return false;
    }
    public virtual bool CheckifTargetColorMatches(Color targetColor)
    {
        return false;
    }

    //for resetting or setting any behaviour
    public virtual void OnDisable_Inheritance()
    {

    }
    public virtual void OnEnable_Inheritance()
    {

    }

    [Header("OnEnable")]
    public AnimationCurve OnEnable_ScaleModifier;
    public Gradient OnEnable_ColorModifier;
    public Vector2 Scale_MinMaxSpeed = Vector2.zero;
    public Vector2 Color_MinMaxSpeed = Vector2.zero;

    [Header("OnLoop")]
    public bool Use_Color_Loop = false;
    public bool Use_Scale_Loop = false;
    public AnimationCurve OnLoop_ScaleModifier;
    public Gradient OnLoop_ColorModifier;
    public Vector2 ScaleB_MinMaxSpeedScale = Vector2.zero;
    public Vector2 ColorB_MinMaxSpeedScale = Vector2.zero;

    [Header("OnCall")]
    public AnimationCurve OnCall_ScaleModifier;
    public Gradient OnCall_ColorModifier;
    public Vector2 ScaleC_MinMaxSpeed = Vector2.zero;
    public Vector2 ColorC_MinMaxSpeed = Vector2.zero;


    [Header("DEBUG Read Only")]
    public float OnUpdate_Color_Speed = 1;
    public float OnUpdate_Scale_Speed = 1;
    public float OnUpdate_Color_Progress = 0;
    public float OnUpdate_Scale_Progress = 0;
    public Gradient OnUpdate_ColorTarget;
    public AnimationCurve OnUpdate_ScaleCurve_Target;
    public Color originalColor = Color.white;
    public Vector3 originalScale = Vector3.one;
    public bool OnUpdate_Color = false;
    public bool OnUpdate_Scale = false;
    public float OnUpdate_Scale_Progress_OnLoop = 0;
    public float OnUpdate_Color_Progress_OnLoop = 0;
    public float OnUpdate_Scale_Speed_OnLoop = 1;
    public float OnUpdate_Color_Speed_OnLoop = 1;



    public void OnDisable()
    {
        StopAllCoroutines();
        OnUpdate_Color = false;
        OnUpdate_Scale = false;
        transform.localScale = originalScale;
        OnUpdate_Color_Progress = 0;
        OnUpdate_Scale_Progress = 0;
        OnDisable_Inheritance();
    }


    private void OnEnable()
    {
        OnUpdate_ColorTarget = OnEnable_ColorModifier;
        if (Color_MinMaxSpeed != Vector2.zero)
        {
            OnUpdate_Color_Speed = Random.Range(Color_MinMaxSpeed.x, Color_MinMaxSpeed.y);
            if (OnUpdate_Color_Speed == 0) print("WARNING: UI_Juice on " + gameObject.name + " OnEnable MinMaxSpeed of Color outputs to speed 0 or below");
        }
        else OnUpdate_Color_Speed = 1;

        OnUpdate_Color_Progress = 0;
        OnUpdate_Color = true;

        OnUpdate_ScaleCurve_Target = OnEnable_ScaleModifier;
        if (Scale_MinMaxSpeed != Vector2.zero)
        {
            OnUpdate_Scale_Speed = Random.Range(Scale_MinMaxSpeed.x, Scale_MinMaxSpeed.y);
            if (OnUpdate_Scale_Speed <= 0) print("WARNING: UI_Juice on " + gameObject.name + " OnEnable MinMaxSpeed of Scale outputs to speed 0 or below.");
        }
        else OnUpdate_Scale_Speed = 1;

        OnUpdate_Scale_Progress = 0;
        OnUpdate_Scale = true;

        if (ColorB_MinMaxSpeedScale != Vector2.zero) OnUpdate_Color_Speed_OnLoop = Random.Range(ColorB_MinMaxSpeedScale.x, ColorB_MinMaxSpeedScale.y);
        if (ScaleB_MinMaxSpeedScale != Vector2.zero) OnUpdate_Scale_Speed_OnLoop = Random.Range(ScaleB_MinMaxSpeedScale.x, ScaleB_MinMaxSpeedScale.y);

        OnEnable_Inheritance();
    }

    private void Update()
    {

        //OnUpdateColor = linear, used for OnEnable and Callables.
        ///Use_OnUpdate_Color_Loop is only used if its TRUE and OnUpdateColor is not running
        if (OnUpdate_Color)
        {
            if (OnUpdate_Color_Progress != 1)
            {
                UpdateTargetColor(OnUpdate_ColorTarget.Evaluate(OnUpdate_Color_Progress));
                OnUpdate_Color_Progress = Mathf.Clamp((OnUpdate_Color_Progress += (OnUpdate_Color_Speed * Time.unscaledDeltaTime)), 0, 1);
            }
            else
            {
                //if (ourTarget.color != OnUpdate_ColorTarget.Evaluate(1)) 
                UpdateTargetColor(OnUpdate_ColorTarget.Evaluate(1));
                OnUpdate_Color = false;
            }
        }
        else if (Use_Color_Loop)
        {
            if (OnUpdate_Color_Progress_OnLoop != 1) OnUpdate_Color_Progress_OnLoop = Mathf.Clamp(OnUpdate_Color_Progress_OnLoop += OnUpdate_Color_Speed_OnLoop * Time.unscaledDeltaTime, 0, 1);
            else OnUpdate_Color_Progress_OnLoop = 0;
            UpdateTargetColor(OnLoop_ColorModifier.Evaluate(OnUpdate_Color_Progress_OnLoop));
        }
        else if (CheckifTargetColorMatches(originalColor) == false) UpdateTargetColor(originalColor);


        //as above so below, ofc
        if (OnUpdate_Scale)
        {
            if (OnUpdate_Scale_Progress != 1)
            {
                UpdateTargetScale(originalScale * OnUpdate_ScaleCurve_Target.Evaluate(OnUpdate_Scale_Progress));
                OnUpdate_Scale_Progress = Mathf.Clamp((OnUpdate_Scale_Progress += (OnUpdate_Scale_Speed * Time.unscaledDeltaTime)), 0, 1);
            }
            else
            {
                if (CheckifTargetScaleMatches(originalScale * OnUpdate_ScaleCurve_Target.Evaluate(1)) == false) UpdateTargetScale(originalScale * OnUpdate_ScaleCurve_Target.Evaluate(1));
                UpdateTargetScale(originalScale * OnUpdate_ScaleCurve_Target.Evaluate(1));
                OnUpdate_Scale = false;
            }
        }
        else if (Use_Scale_Loop)
        {
            if (OnUpdate_Scale_Progress_OnLoop != 1) OnUpdate_Scale_Progress_OnLoop = Mathf.Clamp(OnUpdate_Scale_Progress_OnLoop += OnUpdate_Scale_Speed_OnLoop * Time.unscaledDeltaTime, 0, 1);
            else OnUpdate_Scale_Progress_OnLoop = 0;
            UpdateTargetScale(originalScale * OnLoop_ScaleModifier.Evaluate(OnUpdate_Scale_Progress_OnLoop));
        }
        else if (CheckifTargetScaleMatches(originalScale) == false) UpdateTargetScale(originalScale);

    }


    [ContextMenu("DEBUG_CallScaleModify")]
    public void Call_ModifyScale()
    {
        StopCoroutine(DisableOtherModifiersAndStartCallableScale());
        StartCoroutine(DisableOtherModifiersAndStartCallableScale());

    }

    [ContextMenu("DEBUG_CallModifyBoth")]
    public void Call_ModifyBoth()
    {
        Call_ModifyColor();
        Call_ModifyScale();
    }
    [ContextMenu("DEBUG_CallColorModify")]
    public void Call_ModifyColor()
    {
        StopCoroutine(DisableOtherModifiersAndStartCallableColor());
        StartCoroutine(DisableOtherModifiersAndStartCallableColor());
    }




    //Using coroutines to fix race conditions between Update loop reading the target data while we might be modifying it
    IEnumerator DisableOtherModifiersAndStartCallableColor()
    {
        OnUpdate_Color = false;
        OnUpdate_Color_Progress = 0;

        yield return null;
        OnUpdate_ColorTarget = OnCall_ColorModifier;
        if (ColorC_MinMaxSpeed != Vector2.zero)
        {
            OnUpdate_Color_Speed = Random.Range(ColorC_MinMaxSpeed.x, ColorC_MinMaxSpeed.y);
            if (OnUpdate_Color_Speed <= 0) print("WARNING: UI_Juice on " + gameObject.name + " Callable Color Modify MinMaxSpeed outputs to speed 0 or below.");
        }
        else OnUpdate_Color_Speed = 1;
        OnUpdate_Color = true;
        print("reached end");
    }


    IEnumerator DisableOtherModifiersAndStartCallableScale()
    {
        OnUpdate_Scale = false;
        OnUpdate_Scale_Progress = 0;
        yield return null;

        OnUpdate_Scale = false;
        OnUpdate_ScaleCurve_Target = OnCall_ScaleModifier;
        if (ScaleC_MinMaxSpeed != Vector2.zero)
        {
            OnUpdate_Scale_Speed = Random.Range(ScaleC_MinMaxSpeed.x, ScaleC_MinMaxSpeed.y);
            if (OnUpdate_Scale_Speed <= 0) print("WARNING: UI_Juice on " + gameObject.name + " Callable Scale Modify MinMaxSpeed outputs to speed 0 or below.");
        }
        else OnUpdate_Scale_Speed = 1;
        OnUpdate_Scale_Progress = 0;
        OnUpdate_Scale = true;
    }
}
