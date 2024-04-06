using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class interactions_door : InteractionsTarget
{

    public Animator ourAnimator;
    public UnityEvent OnUse;
    public bool isLocked = false;
    public GameObject NavMeshCarve;
    public List<ItemDefiner> UnlocksWith;
    public enum locktype { Key, RFID};
    public locktype ourlocktype = locktype.RFID;

    public void Start()
    {
        GameManager.Instance.onGameBegin += Randomise;
    }

    public void OnDestroy()
    {
        if (GameManager.Instance) GameManager.Instance.onGameBegin -= Randomise;
    }
    private void Randomise()
    {
        ourAnimator.SetTrigger("Reset");
        int ourRandom = Random.Range(0, 2);
        if (ourRandom == 0) isLocked = false;
    }

    public override void Receive(ItemDefiner _interactWith)
    {
        ourAnimator.SetTrigger("Use");
        if (isLocked == false)
        {
            AudioManager.Instance.play_sfx(AudioManager.sfxtype.opendoor);
            ourAnimator.SetBool("Open", !ourAnimator.GetBool("Open"));
        }
        else
        {
            AudioManager.Instance.play_sfx(AudioManager.sfxtype.lockeddoor);

        }
        OnUse.Invoke();

        if (isLocked)
        {

                if (ourlocktype == locktype.RFID)
                {
                    if (GameManager.Instance.ref_Stats.Use_RFIDs() == true)
                    {
                        isLocked = false;
                        GameManager.Instance.ref_messagespawner.SpawnMessage("Door unlocked.", Color.green, transform.position);
                        return;
                    }
                    else
                    {
                        GameManager.Instance.ref_messagespawner.SpawnMessage("Needs an RFID tag to unlock.", Color.red, transform.position);
                        return;
                    }
                }
                if (ourlocktype == locktype.Key)
                {
                    if (GameManager.Instance.ref_Stats.Use_Key() == true)
                    {
                        isLocked = false;
                        GameManager.Instance.ref_messagespawner.SpawnMessage("Door unlocked.", Color.green, transform.position);
                        return;
                    }
                    else
                    {
                        GameManager.Instance.ref_messagespawner.SpawnMessage("Need a key to unlock.", Color.red, transform.position);
                        return;
                    }

                }

            GameManager.Instance.ref_messagespawner.SpawnMessage("Door is locked.", Color.red, transform.position);
        }
    }

    public void ReceiveZombie()
    {
        ourAnimator.SetTrigger("Use");
        if (isLocked == false) ourAnimator.SetBool("Open", !ourAnimator.GetBool("Open"));
        OnUse.Invoke();

        if (isLocked)
        {
            int tryToBreakDoor = Random.Range(0, 10);
            if (tryToBreakDoor < 4)
            {
                isLocked = false;
                ourAnimator.SetBool("Open", !ourAnimator.GetBool("Open"));
                GameManager.Instance.ref_particlespawner.Spawn_Splinters(transform.position);
            }
            GameManager.Instance.ref_messagespawner.SpawnMessage("<scratching>", Color.red, transform.position);
        }
    }

    private void Update()
    {
        if (isLocked && NavMeshCarve.activeSelf == false) NavMeshCarve.SetActive(true);
        else if (isLocked == false && NavMeshCarve.activeSelf == true) NavMeshCarve.SetActive(false);
    }
}