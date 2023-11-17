using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectableColourTemplate", menuName = "UI/SelectableColourTemplate", order = 1)]
public class file_SelectableColourTemplate : ScriptableObject
{


    public Color NormalColor = Color.white;
    public Color HighlightedColor = Color.white;
    public Color PressedColor = Color.yellow;
    public Color SelectedColor = Color.yellow;
    public Color DisabledColor = Color.grey;
    public float ColorMultiplier = 1;
    public float FadeDuration = 0.1f;
}
