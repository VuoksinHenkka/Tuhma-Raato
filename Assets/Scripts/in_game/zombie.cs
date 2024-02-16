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
    private float currentDistanceToPlayer = 10;
    private float UpdateCycleForMoveTargets_Current = 0;
    private float UpdateCycleForMoveTargets_Target = 2;
    private float MoveSpeed = 0;
    private float MoveSpeedTarget = 0;
    private float AttackTimer = 0.5f;
    private string ourName = "Zombie";
    public int HP = 5;
    public Vector3 formationTarget = Vector3.zero;


    private OffMeshLinkData ourMeshLinkData;
    public bool AnimationLaunched_Hurdle = false;
    private int Hurdle_NavMeshLayer = 0;

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
        MoveSpeed = Random.Range(2, 7);
        MoveSpeedTarget = Random.Range(2f, 9);
        MoveSpeed = MoveSpeedTarget;
        Hurdle_NavMeshLayer = NavMesh.GetAreaFromName("Hurdle");
    }

    private void Start()
    {
        GameManager.Instance.onGameBegin += Clean;
        GameManager.Instance.onGameOver += Clean;
        GameManager.Instance.onGameEnding += Clean;
    }

    private void Clean()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.onGameBegin -= Clean;
        GameManager.Instance.onGameOver -= Clean;
        GameManager.Instance.onGameEnding -= Clean;
    }


    private void Update()
    {
        if (ourAgent.isOnNavMesh == false) Destroy(gameObject);
        if (interestedInPlayer)
        {
            if (ourcharacterGFX) ourcharacterGFX.LookToDirection = GameManager.Instance.ref_Player.transform.position;
            currentDistanceToPlayer = DistanceToPlayer();
            MoveSpeed = Mathf.Lerp(MoveSpeed, MoveSpeedTarget, 2f * Time.deltaTime);
            if (currentDistanceToPlayer > 50) ourAgent.speed = 9f;
            else ourAgent.speed = MoveSpeed;
            
            if (UpdateCycleForMoveTargets_Current < UpdateCycleForMoveTargets_Target) UpdateCycleForMoveTargets_Current += 1 * Time.deltaTime;
            else
            {
                if (currentDistanceToPlayer < 10)
                {
                    int RandomChance = Random.Range(0, 10);
                    if (RandomChance == 0) AudioManager.Instance.SFX_ZombieYell();
                }

                UpdateCycleForMoveTargets_Current = 0;
                MoveSpeedTarget = Random.Range(1.5f, 3f);

                Vector3 moveTarget = Vector3.zero;
                
                if (formationTarget != Vector3.zero && currentDistanceToPlayer > 7) moveTarget = formationTarget;
                else
                {
                    moveTarget = GameManager.Instance.ref_Player.transform.position;
                }

                ourAgent.SetDestination(moveTarget);                       
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

        if (currentDistanceToPlayer < 30) NavMeshLayerReactions();

    }

    private void NavMeshLayerReactions()
    {

        if (ourAgent.isOnOffMeshLink)
        {
            if (ourAgent.currentOffMeshLinkData.offMeshLink.area == Hurdle_NavMeshLayer)
            {
                if (AnimationLaunched_Hurdle == false)
                {
                    ourcharacterGFX.ourAnimator.SetTrigger("Hurdle");
                    AnimationLaunched_Hurdle = true;
                }
            }
        }

        else AnimationLaunched_Hurdle = false;
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
