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

    //for any additive behaviour, called from our OnEnable/OnDisable/Awake
    public virtual void OnDisable_Inheritance()
    {

    }
    public virtual void OnEnable_Inheritance()
    {

    }

    [Header("OnEnable")]
    public file_ScaleNColour OnEnable_ScaleNColour;

    [Header("OnLoop")]
    public file_ScaleNColour OnLoop_ScaleNColour;
    private bool Use_Color_Loop = false;
    private bool Use_Scale_Loop = false;
    private AnimationCurve OnLoop_ScaleModifier;
    private Gradient OnLoop_ColorModifier;
    private Vector2 ScaleB_MinMaxSpeedScale = Vector2.zero;
    private Vector2 ColorB_MinMaxSpeedScale = Vector2.zero;

    [Header("OnCall")]
    public file_ScaleNColour OnCall_ScaleNColour;


    //internal stuffs
    private float OnUpdate_Color_Speed = 1;
    private float OnUpdate_Scale_Speed = 1;
    private float OnUpdate_Color_Progress = 0;
    private float OnUpdate_Scale_Progress = 0;
    private Gradient OnUpdate_ColorTarget;
    private AnimationCurve OnUpdate_ScaleCurve_Target;
    private Color originalColor = Color.white;
    private Vector3 originalScale = Vector3.one;
    private bool OnUpdate_Color = false;
    private bool OnUpdate_Scale = false;
    private float OnUpdate_Scale_Progress_OnLoop = 0;
    private float OnUpdate_Color_Progress_OnLoop = 0;
    private float OnUpdate_Scale_Speed_OnLoop = 1;
    private float OnUpdate_Color_Speed_OnLoop = 1;


    public void SetOriginalScale(Vector3 toSet)
    {
        originalScale = toSet;
    }

    public void SetOriginalColour(Color toSet)
    {
        originalColor = toSet;
    }
    public Color giveOriginalColour()
    {
        return originalColor;
    }
    public Vector3 giveOriginalScale()
    {
        return originalScale;
    }


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

        OnEnableModifiers();
        OnEnable_Inheritance();
    }


    private void OnEnableModifiers()
    {
        if (OnEnable_ScaleNColour)
        {
            if (OnEnable_ScaleNColour.useColorModifier)
            {
                UpdateTargetColor(OnEnable_ScaleNColour.ColorModifier.Evaluate(0));

                OnUpdate_ColorTarget = OnEnable_ScaleNColour.ColorModifier;
                if (OnEnable_ScaleNColour.Color_MinMaxSpeed != Vector2.zero)
                {
                    OnUpdate_Color_Speed = Random.Range(OnEnable_ScaleNColour.Color_MinMaxSpeed.x, OnEnable_ScaleNColour.Color_MinMaxSpeed.y);
                    if (OnUpdate_Color_Speed == 0) print("WARNING: UI ScaleAndColour on " + gameObject.name + " OnEnable MinMaxSpeed of Color outputs to speed 0 or below");
                }
                else OnUpdate_Color_Speed = 1;

                OnUpdate_Color_Progress = 0;
                OnUpdate_Color = true;
            }

            if (OnEnable_ScaleNColour.useScaleModifier)
            {
                UpdateTargetScale(originalScale * OnEnable_ScaleNColour.ScaleModifier.Evaluate(0));

                OnUpdate_ScaleCurve_Target = OnEnable_ScaleNColour.ScaleModifier;
                if (OnEnable_ScaleNColour.Scale_MinMaxSpeed != Vector2.zero)
                {
                    OnUpdate_Scale_Speed = Random.Range(OnEnable_ScaleNColour.Scale_MinMaxSpeed.x, OnEnable_ScaleNColour.Scale_MinMaxSpeed.y);
                    if (OnUpdate_Scale_Speed <= 0) print("WARNING: UI ScaleAndColour on " + gameObject.name + " OnEnable MinMaxSpeed of Scale outputs to speed 0 or below.");
                }
                else OnUpdate_Scale_Speed = 1;

                OnUpdate_Scale_Progress = 0;
                OnUpdate_Scale = true;
            }
        }

        if (OnLoop_ScaleNColour)
        {
            if (OnLoop_ScaleNColour.useColorModifier && OnLoop_ScaleNColour.Color_MinMaxSpeed != Vector2.zero) OnUpdate_Color_Speed_OnLoop = Random.Range(OnLoop_ScaleNColour.Color_MinMaxSpeed.x, OnLoop_ScaleNColour.Color_MinMaxSpeed.y);
            if (OnLoop_ScaleNColour.useScaleModifier && OnLoop_ScaleNColour.Scale_MinMaxSpeed != Vector2.zero) OnUpdate_Scale_Speed_OnLoop = Random.Range(OnLoop_ScaleNColour.Scale_MinMaxSpeed.x, OnLoop_ScaleNColour.Scale_MinMaxSpeed.y);
        }
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

        else if (OnLoop_ScaleNColour)
        {
            if (OnLoop_ScaleNColour.useColorModifier)
            {
                if (OnUpdate_Color_Progress_OnLoop != 1) OnUpdate_Color_Progress_OnLoop = Mathf.Clamp(OnUpdate_Color_Progress_OnLoop += OnUpdate_Color_Speed_OnLoop * Time.unscaledDeltaTime, 0, 1);
                else OnUpdate_Color_Progress_OnLoop = 0;
                UpdateTargetColor(OnLoop_ScaleNColour.ColorModifier.Evaluate(OnUpdate_Color_Progress_OnLoop));
            }
            else if (CheckifTargetColorMatches(originalColor) == false) UpdateTargetColor(originalColor);
        }



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
        else if (OnLoop_ScaleNColour)
        {
            if (OnLoop_ScaleNColour.useScaleModifier)
            {
                if (OnUpdate_Scale_Progress_OnLoop != 1) OnUpdate_Scale_Progress_OnLoop = Mathf.Clamp(OnUpdate_Scale_Progress_OnLoop += OnUpdate_Scale_Speed_OnLoop * Time.unscaledDeltaTime, 0, 1);
                else OnUpdate_Scale_Progress_OnLoop = 0;
                UpdateTargetScale(originalScale * OnLoop_ScaleNColour.ScaleModifier.Evaluate(OnUpdate_Scale_Progress_OnLoop));
            }
            else if (CheckifTargetScaleMatches(originalScale) == false) UpdateTargetScale(originalScale);
        }

    }


    [ContextMenu("DEBUG_CallScaleModify")]
    public void Call_ModifyScale()
    {
        if (OnCall_ScaleNColour == null) return;
        if (OnCall_ScaleNColour.useScaleModifier == false)
        {
            print("WARNING: UI ScaleAndColour on " + gameObject.name + " tried to OnCall ModifyScale, but asset is set to useScale = false");
            return;
        }
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
        if (OnCall_ScaleNColour == null) return;
        if (OnCall_ScaleNColour.useColorModifier == false)
        {
            print("WARNING: UI ScaleAndColour on " + gameObject.name + " tried to OnCall ModifyColour, but asset is set to useColor = false");
            return;
        }
        StopCoroutine(DisableOtherModifiersAndStartCallableColor());
        StartCoroutine(DisableOtherModifiersAndStartCallableColor());
    }




    //Using coroutines to fix race conditions between Update loop reading the target data while we might be modifying it
    IEnumerator DisableOtherModifiersAndStartCallableColor()
    {
        OnUpdate_Color = false;
        OnUpdate_Color_Progress = 0;

        yield return null;
        OnUpdate_ColorTarget = OnCall_ScaleNColour.ColorModifier;
        if (OnCall_ScaleNColour.Color_MinMaxSpeed != Vector2.zero)
        {
            OnUpdate_Color_Speed = Random.Range(OnCall_ScaleNColour.Color_MinMaxSpeed.x, OnCall_ScaleNColour.Color_MinMaxSpeed.y);
            if (OnUpdate_Color_Speed <= 0) print("WARNING: UI_Juice on " + gameObject.name + " Callable Color Modify MinMaxSpeed outputs to speed 0 or below.");
        }
        else OnUpdate_Color_Speed = 1;
        OnUpdate_Color = true;
    }


    IEnumerator DisableOtherModifiersAndStartCallableScale()
    {
        OnUpdate_Scale = false;
        OnUpdate_Scale_Progress = 0;
        yield return null;

        OnUpdate_Scale = false;
        OnUpdate_ScaleCurve_Target = OnCall_ScaleNColour.ScaleModifier;
        if (OnCall_ScaleNColour.Scale_MinMaxSpeed != Vector2.zero)
        {
            OnUpdate_Scale_Speed = Random.Range(OnCall_ScaleNColour.Scale_MinMaxSpeed.x, OnCall_ScaleNColour.Scale_MinMaxSpeed.y);
            if (OnUpdate_Scale_Speed <= 0) print("WARNING: UI_Juice on " + gameObject.name + " Callable Scale Modify MinMaxSpeed outputs to speed 0 or below.");
        }
        else OnUpdate_Scale_Speed = 1;
        OnUpdate_Scale_Progress = 0;
        OnUpdate_Scale = true;
    }
}
