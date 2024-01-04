using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction_target_container : InteractionsTarget
{
    public enum ItemSpawnType {Cheap, Mid, Expensive}
    public ItemSpawnType WhatWeSpawn = ItemSpawnType.Cheap;
    public List<Transform> SpawnPositions;

    public Animator myAnimator;
    public List<ItemDefiner> OpenWithItems;
    public bool OpenWithAnything = false;
    public bool Opened = false;
    public bool ConsumeStamina = false;
    public int ChanceOfDisappearing = 0;
    public string InteractionAnimation = "Interact";


    public void Start()
    {
        GameManager.Instance.onGameBegin += Respawn;
    }

    public void Respawn()
    {
        Opened = false;
        if (myAnimator) myAnimator.SetBool("Open", false);

        if (ChanceOfDisappearing != 0)
        {
            int Chance = Random.Range(0, 101);
            if (Chance < ChanceOfDisappearing) gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance) GameManager.Instance.onGameBegin -= Respawn;

    }

    public override void Receive(ItemDefiner _interactWith)
    {
        GameManager.Instance.ref_Player.ourcharacterGFX.TriggerAnimation(InteractionAnimation);
        if (Opened) return;

        if (OpenWithAnything)
        {
            Open(_interactWith);
            return;
        }
        foreach(ItemDefiner foundDefiner in OpenWithItems)
        {
            if (foundDefiner.name == _interactWith.name)
            {
                Open(_interactWith);
                return;
            }
        }
        GameManager.Instance.ref_messagespawner.SpawnMessage("No fitting tool for opening this.", Color.red, transform.position);


    }


    public void Open(ItemDefiner _interactWith)
    {
        if (ConsumeStamina)
        {
            if (GameManager.Instance.ref_Stats.Stamina < _interactWith.StaminaConsumption)
            {
                GameManager.Instance.ref_messagespawner.SpawnMessage("Not enough stamina!", Color.red, GameManager.Instance.ref_Player.transform.position);
                return;
            }
            else GameManager.Instance.ref_Stats.Stamina_Modify(-_interactWith.StaminaConsumption);
        }

        if (myAnimator)myAnimator.SetBool("Open", true);
        Opened = true;
        foreach(Transform SpawnPosition in SpawnPositions)
        {
            if (WhatWeSpawn == ItemSpawnType.Cheap) ItemSpawnManager.Instance.SpawnCheap(SpawnPosition.position);
            else if (WhatWeSpawn == ItemSpawnType.Mid) ItemSpawnManager.Instance.SpawnMid(SpawnPosition.position);
            else if (WhatWeSpawn == ItemSpawnType.Expensive) ItemSpawnManager.Instance.SpawnExpensive(SpawnPosition.position);
        }


        onInteract.Invoke();
    }
}
