using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ItemManager : MonoBehaviour
{

    public List<ItemDefiner> PlayerItems;
    public ItemDefiner emptyItem;
    public ItemDefiner currentlyHolding;


    private void Awake()
    {
        PlayerItems = new List<ItemDefiner>();

        for (int i = 0; i < 28; i++)
        {
            PlayerItems.Add(emptyItem);
        }
        currentlyHolding = PlayerItems[0];
    }

    public Global.statusEffect return_statusEffect(string toTest)
    {
        Global.statusEffect toReturn = Global.statusEffect.Nothing;

        switch (toTest)
        {
            case "Torch":
                toReturn = Global.statusEffect.Burn;
                break;
            case "Rock":
                toReturn = Global.statusEffect.Stun;
                break;
            default:
                break;
        }
        return toReturn;
    }

    public bool Try_Pickup(ItemDefiner toPickUp)
    {
        bool toReturn = false;
        for (int i = 0; i < 28; i++)
        {
            if (PlayerItems[i].Name == "Nothing")
            {
                toReturn = true;
                PlayerItems[i] = toPickUp;
                break;
            }
        }

        return toReturn;
    }
}
