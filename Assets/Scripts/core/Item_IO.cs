using UnityEngine;

public abstract class Item_IO : MonoBehaviour
{
    public Item ourItem;


    public string Name = "DefaultItem";
    public float ActionCoolDown = 1;
    public float SendRange = 5;

    private void Start()
    {
        ourItem = new Item(name, ActionCoolDown, SendRange, 9999, "", 0, Item.Type.Generic);
    }

    public abstract void Receive(Item InteractWith);

}
