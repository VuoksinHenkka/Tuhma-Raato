using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_IO : MonoBehaviour
{
    public Item ourItem;


    public string Name = "DefaultItem";
    public float ActionCoolDown = 1;
    public float SendRange = 5;

    private void Awake()
    {
        ourItem = new Item(name, ActionCoolDown, SendRange);
    }

    public void Receive(Item InteractWith)
    {
        print(InteractWith.Name);
    }

}
