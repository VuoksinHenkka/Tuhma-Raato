using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoorifZombieNearDoor : MonoBehaviour
{

    public interactions_door ourDoor;
    public ItemDefiner emptyItem;
    private void OnTriggerEnter(Collider other)
    {
        print("collider reacts");
        if (other.CompareTag("Zombie"))
        {
            print("Zombie enters door");
            if (ourDoor.ourAnimator.GetBool("Open") == false) ourDoor.ReceiveZombie();
        }
    }
}
