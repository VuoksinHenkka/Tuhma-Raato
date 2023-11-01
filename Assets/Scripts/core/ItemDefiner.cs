using UnityEngine;

[CreateAssetMenu(fileName = "ItemDefiner", menuName = "AAA/ItemDefiner", order = 1)]
public class ItemDefiner : ScriptableObject
{

    public enum Type { Tool, Consumable, Generic };

    [Header("ITEM PROPERTIES")]
    public Type itemType = Type.Generic;
    public string Name = "";
    public float ActionCoolDown = 1;
    public float SendRange = 5;
    public string Description = "";
    public int StaminaConsumption = 0;
    public int DamageOutput = 1;
    public int GiveHealth = 0;
    public int GiveStamina = 0;

    [Header("AUDIOVISUALS")]
    public Sprite Icon;

}