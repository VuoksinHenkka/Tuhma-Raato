using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interactions_door : InteractionsTarget
{

    public Animator ourAnimator;
    public UnityEvent OnUse;
    public bool isLocked = false;

    private void Awake()
    {
        int ourRandom = Random.Range(0, 2);
        if (ourRandom == 0) isLocked = false;
    }

    public override void Receive(ItemDefiner _interactWith)
    {
        ourAnimator.SetTrigger("Use");
        if (isLocked == false) ourAnimator.SetBool("Open", !ourAnimator.GetBool("Open"));
        OnUse.Invoke();

        if (isLocked)
        {
            if (_interactWith.itemType == ItemDefiner.Type.Key)
            {
                isLocked = false;
                GameManager.Instance.ref_ItemSolver.SpendItemInHand();
                GameManager.Instance.ref_messagespawner.SpawnMessage("Unlocked the door.", Color.green, transform.position);
            }
            else
            {
                GameManager.Instance.ref_messagespawner.SpawnMessage("Door is locked.", Color.red, transform.position);

            }
        }
    }
}
