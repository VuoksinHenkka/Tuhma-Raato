using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScaleNColour", menuName = "UI/ScaleNColour", order = 1)]
public class file_ScaleNColour : ScriptableObject
{
    [Header("Scale from AnimationCurve, time of 0-1")]
    [Tooltip("Disable/Enable this modifier, needed for OnLoop behaviours.")]
    public bool useScaleModifier = true;
    [Tooltip("Evaluated at Time 0-1. For longer animations, modify MinMaxSpeed.")]
    public AnimationCurve ScaleModifier;
    [Tooltip("Random.Range(x,y). If you want the speed to be always 2, set both values to 2")]
    public Vector2 Scale_MinMaxSpeed = Vector2.zero;

    [Header("Color from Gradient, time of 0-1")]
    [Tooltip("Disable/Enable this modifier, needed for OnLoop behaviours.")]
    public bool useColorModifier = true;
    [Tooltip("Evaluated at Time 0-1. For longer animations, modify MinMaxSpeed.")]
    public Gradient ColorModifier;
    [Tooltip("Random.Range(x,y). If you want the speed to be always 2, set both values to 2")]
    public Vector2 Color_MinMaxSpeed = Vector2.zero;
}
