using UnityEngine;


//Base class for things that can receive Items
public abstract class ItemReceiver : MonoBehaviour
{
    public abstract void Receive(ItemDefiner InteractWith);

}
