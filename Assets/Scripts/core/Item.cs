using System.Collections;
using System.Collections.Generic;


public class Item : System.Object
{

    public Item(string setName, float setActionCoolDown, float setSendRange, int setDurability, string setDescription, int setStaminaConsumption, Type setItemType)
    {
        Name = setName;
        ActionCoolDown = setActionCoolDown;
        SendRange = setSendRange;
        Durability = setDurability;
        Description = setDescription;
        StaminaConsumption = setStaminaConsumption;
        itemType = setItemType;
    }


    public enum Type {Tool, Consumable, Generic}
    public Type itemType = Type.Generic;
    public string Name = "";
    public float ActionCoolDown = 1;
    public float SendRange = 5;
    public int Durability = 1;
    public string Description = "";
    public int StaminaConsumption = 0;
}

/* ALL ITEMS IN THE GAME

Axe, Pistol, Rock, Torch
Gasoline, Crowbar, Barricade,
Food, 

*/
