using System.Collections;
using System.Collections.Generic;


public class Item : System.Object
{
    public Item(string setName, float setActionCoolDown, float setSendRange)
    {
        Name = setName;
        ActionCoolDown = setActionCoolDown;
        SendRange = setSendRange;
    }


    public string Name = "";
    public float ActionCoolDown = 1;
    public float SendRange = 5;
    public int Durability = 1;
}

/* ALL ITEMS IN THE GAME

Axe, Pistol, Rock, Torch
Gasoline, Crowbar, Barricade,
Food, 

*/
