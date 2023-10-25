using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie : MonoBehaviour
{
    public NavMeshAgent ourAgent;
    public bool interestedInPlayer = true;
    public float attackDistance = 1;
    public enum MovementType {straight, chaotic}
    public MovementType currentMovementType = MovementType.straight;
    private float currentDistanceToPlayer = 10;
    private float UpdateCycleForMoveTargets_Current = 0;
    private float UpdateCycleForMoveTargets_Target = 2;
    private float MoveSpeed = 0;
    private float MoveSpeedTarget = 0;

    private void Awake()
    {
        ourAgent.avoidancePriority = Random.Range(30, 60);
        MoveSpeed = Random.Range(2, 5);
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
                print("hurt player");
                GameManager.Instance.HurtPlayer();
            }
        }

    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.ref_Player.transform.position);
    }
}
