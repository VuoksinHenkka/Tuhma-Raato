using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie : enemy, IHaveName
{
    public characterGFX ourcharacterGFX;
    public NavMeshAgent ourAgent;
    public bool interestedInPlayer = true;
    private float attackDistance = 2;
    public enum MovementType {straight, chaotic}
    public MovementType currentMovementType = MovementType.straight;
    private float currentDistanceToPlayer = 10;
    private float UpdateCycleForMoveTargets_Current = 0;
    private float UpdateCycleForMoveTargets_Target = 2;
    private float MoveSpeed = 0;
    private float MoveSpeedTarget = 0;
    private float AttackTimer = 0.5f;
    private string ourName = "Zombie";
    private int HP = 10;

    public override string GiveName()
    {
        return ourName;
    }

    public override string GiveCurrentHP()
    {
        return HP.ToString();
    }
    private void Awake()
    {
        ourAgent.avoidancePriority = Random.Range(30, 60);
        MoveSpeed = Random.Range(1, 7);
        PickRandomMovePattern();
        MoveSpeedTarget = Random.Range(0.5f, 7);
        MoveSpeed = MoveSpeedTarget;
    }

    private void PickRandomMovePattern()
    {
        float randomFloat = Random.Range(0, 2);
        if (randomFloat <= 1.5f) currentMovementType = MovementType.straight;
        else currentMovementType = MovementType.chaotic;
    }

    private void Update()
    {
        if (interestedInPlayer)
        {
            if (ourcharacterGFX) ourcharacterGFX.LookToDirection = GameManager.Instance.ref_Player.transform.position;
            currentDistanceToPlayer = DistanceToPlayer();
            MoveSpeed = Mathf.Lerp(MoveSpeed, MoveSpeedTarget, 1f * Time.deltaTime);

            if (currentDistanceToPlayer > 30) ourAgent.speed = MoveSpeed * 2;
            else ourAgent.speed = MoveSpeed;
           
            if (UpdateCycleForMoveTargets_Current < UpdateCycleForMoveTargets_Target) UpdateCycleForMoveTargets_Current += 1 * Time.deltaTime;
            else
            {

                if (currentMovementType == MovementType.chaotic) UpdateCycleForMoveTargets_Target = Random.Range(1f, 3);
                else UpdateCycleForMoveTargets_Target = 0.1f;
                UpdateCycleForMoveTargets_Current = 0;
                PickRandomMovePattern();
                MoveSpeedTarget = Random.Range(0.5f, 7);

                if (currentMovementType == MovementType.straight) ourAgent.SetDestination(GameManager.Instance.ref_Player.transform.position);
                else ourAgent.SetDestination(GameManager.Instance.ref_Player.transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
                       
            }


            if (DistanceToPlayer() < attackDistance)
            {
                if (AttackTimer != 0) AttackTimer = Mathf.Clamp(AttackTimer -= 1 * Time.deltaTime, 0, 2);
                else
                {
                    GameManager.Instance.ref_Stats.HP_Modify(-10);
                    AttackTimer = Random.Range(2,3);

                }
            }
            else if (AttackTimer != 0.5f) AttackTimer = 0.5f;
        }

        ourcharacterGFX.ourMoveVelocity = ourAgent.velocity.magnitude;
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.ref_Player.transform.position);
    }


    public override void Hurt(int _amount)
    {
        GameManager.Instance.ref_messagespawner.SpawnMessage("-" + _amount, Color.white, transform.position);
        HP = HP - _amount;
        if (HP < 1) Die();
        else
        {
            ourcharacterGFX.ourAnimator.SetTrigger("Hurt");
        }
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }
    public override void Burn()
    {
    }
    public override void Wet()
    {
    }
    public override void Electrocute()
    {
    }
    public override void Stun()
    {
    }

}
