using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayerItemUsing : MonoBehaviour
{


    public GameObject currentTarget;
    public UI_playerAimingInfoReader ourTargetInfoReader;
    public float cooldown = 0;
    public Image ourInteractionLine;
    public LayerMask forInteractionRayCast;
    public characterGFX ourCharacterGFX;
    public ingamecursor ourCursor;
    private float currentInteractionRange = 4;

    private float clickCoolDown = 0.01f;
    public Color cursor_notarget = new Color(1, 1, 1, 0.5f);
    public Color cursor_targetnoreach = new Color(0.75f, 0, 0, 0.5f);
    public Color cursor_targetandreach = new Color(0, 0.75f, 0, 0.5f);

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


        CheckCursorColour();

        if (currentTarget)
        {
            ourTargetInfoReader.ourInteractionTarget_Gameobject = currentTarget;
        }
        else ourTargetInfoReader.ourInteractionTarget_Gameobject = null;

    }



    private void CheckCursorColour()
    {
        if (!currentTarget)
        {
            if (ourInteractionLine.color != cursor_notarget) ourInteractionLine.color = cursor_notarget;
        }
        else
        {
            if (IsInRange())
            {
                if (ourInteractionLine.color != cursor_targetandreach) ourInteractionLine.color = cursor_targetandreach;
            }
            else
            {
                if (ourInteractionLine.color != cursor_targetnoreach) ourInteractionLine.color = cursor_targetnoreach;
            }
        }

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
            ourCursor.currentScaleLerp = 1;
            currentTarget.BroadcastMessage("TryAttackSound", GameManager.Instance.ref_ItemSolver.currentlyHolding, SendMessageOptions.DontRequireReceiver);
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
