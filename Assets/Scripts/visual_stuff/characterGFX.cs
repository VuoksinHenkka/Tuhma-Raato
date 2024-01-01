using System.Collections.Generic;
using UnityEngine;

public class characterGFX : MonoBehaviour
{
    public Vector3 LookToDirection = Vector3.zero;
    public Vector3 LookToDirection_override = Vector3.zero;
    public Animator ourAnimator;
    public float ourMoveVelocity = 0;


    public float leanAngle = 40f; 
    public float leanSmoothness = 5f;


    public void TriggerAnimation(string triggername)
    {
        if (ourAnimator) ourAnimator.SetTrigger(triggername);
    }

    private void Awake()
    {
        if (ourAnimator) ourAnimator.SetFloat("Offset", Random.Range(0, 2f));
    }
    private void LateUpdate()
    {
        if (LookToDirection == Vector3.zero) return;

        if (LookToDirection_override != Vector3.zero)
        {
            transform.LookAt(new Vector3(LookToDirection_override.x, transform.position.y, LookToDirection_override.z), Vector3.up);
            if (ourAnimator) ourAnimator.SetFloat("Velocity", ourMoveVelocity);
            return;
        }

        if (ourAnimator) ourAnimator.SetFloat("Velocity", ourMoveVelocity);
        //smoothly rotate towards direction
        if ((LookToDirection - transform.position) == Vector3.zero) return;
        Quaternion lookDirection = Quaternion.LookRotation(LookToDirection - transform.position);


        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 5f);
    }
}