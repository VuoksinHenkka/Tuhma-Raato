using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerItemUsing : MonoBehaviour
{


    public GameObject currentTarget;
    public UI_playerAimingInfoReader ourTargetInfoReader;
    public float cooldown = 0;
    public LineRenderer ourInteractionLine;
    public LayerMask forInteractionRayCast;
    public characterGFX ourCharacterGFX;

    private float currentInteractionRange = 4;

    private float clickCoolDown = 0.01f;


    void Update()
    {
        GameManager.Instance.cooldownTimer = cooldown;
        if (currentTarget && ourCharacterGFX) ourCharacterGFX.LookToDirection_override = currentTarget.transform.position;
        else if (!currentTarget && ourCharacterGFX) ourCharacterGFX.LookToDirection_override = Vector3.zero;
        if (cooldown != 0) cooldown = Mathf.Clamp(cooldown -= 1 * Time.deltaTime, 0, 100);

        if (Input.GetButton("Fire1"))
        {
            if (clickCoolDown < 0)
            {
                Interact();
                clickCoolDown = 0.25f;
            }
            else clickCoolDown = clickCoolDown -= Time.deltaTime * 1f;
        }
        if (Input.GetButton("Fire2"))
        {
            if (clickCoolDown < 0)
            {
                ThrowItem();
                clickCoolDown = 0.25f;
            }
            else clickCoolDown = clickCoolDown -= Time.deltaTime * 1f;
        }


        if (currentTarget)
        {
            if (currentTarget.GetComponent<IHaveLimitedUseRange>() != null) currentInteractionRange = 5;
            else currentInteractionRange = GameManager.Instance.ref_ItemSolver.currentlyHolding.SendRange;
        }


        if (currentTarget == null) DrawInteractionLine(false);
        else DrawInteractionLine(true);

        if (currentTarget)
        {
            ourTargetInfoReader.ourInteractionTarget_Gameobject = currentTarget;
        }
        else ourTargetInfoReader.ourInteractionTarget_Gameobject = null;

    }



    private void DrawInteractionLine(bool turnOn)
    {
        if (turnOn)
        {
            ourInteractionLine.SetPosition(0, ourInteractionLine.transform.position);
            ourInteractionLine.SetPosition(1, currentTarget.transform.position);
            if (ourInteractionLine.enabled == false) ourInteractionLine.enabled = true;
            if (IsInRange() == false) ourInteractionLine.SetColors(Color.red, Color.red);
            else ourInteractionLine.SetColors(Color.green, Color.green);
        }
        else ourInteractionLine.enabled = false;

    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, forInteractionRayCast))
        {
            currentTarget = hit.collider.gameObject;
        }
        else currentTarget = null;
    }

    private void Interact()
    {
        if (cooldown != 0 || currentTarget == null || IsInRange() == false) return;
        else if (currentTarget.CompareTag("Item"))
        {
            currentTarget.BroadcastMessage("Receive", GameManager.Instance.ref_ItemSolver.currentlyHolding, SendMessageOptions.DontRequireReceiver);
            cooldown = GameManager.Instance.ref_ItemSolver.currentlyHolding.ActionCoolDown;
        }
    }

    private bool IsInRange()
    {
        if (currentTarget == null) return false;

        
        return (Vector3.Distance(GameManager.Instance.ref_Player.transform.position, currentTarget.transform.position) < currentInteractionRange);
    }

    private void ThrowItem()
    {

    }
}
