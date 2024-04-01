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
    private float UpdateCycleForMoveTargets_Target = 2;
    private float AttackTimer = 0.5f;
    private string ourName = "Zombie";
    public float MoveSpeed = 1;
    public int HP = 5;
    public Vector3 formationTarget = Vector3.zero;


    private OffMeshLinkData ourMeshLinkData;
    public bool AnimationLaunched_Hurdle = false;
    private int Hurdle_NavMeshLayer = 0;


    private void OnEnable()
    {
        ourAgent.avoidancePriority = Random.Range(20, 60);
    }
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
        ourAgent.speed = MoveSpeed;
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

                if (currentDistanceToPlayer < 10)
                {
                    int RandomChance = Random.Range(0, 10);
                    if (RandomChance == 0) AudioManager.Instance.SFX_ZombieYell();
                }


                Vector3 moveTarget = Vector3.zero;
                
                if (formationTarget != Vector3.zero && currentDistanceToPlayer > 7) moveTarget = formationTarget;
                else
                {
                    moveTarget = GameManager.Instance.ref_Player.transform.position;
                }

                ourAgent.SetDestination(moveTarget);                       
            


            if (DistanceToPlayer() < attackDistance)
            {
                if (AttackTimer != 0) AttackTimer = Mathf.Clamp(AttackTimer -= 1 * Time.deltaTime, 0, 2);
                else
                {
                    GameManager.Instance.ref_Stats.HP_Modify(-10);
                    GameManager.Instance.ref_particlespawner.Spawn_Blood(GameManager.Instance.ref_Player.transform.position);
                    AudioManager.Instance.play_sfx(AudioManager.sfxtype.player_hit);
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
        GameManager.Instance.ref_particlespawner.Spawn_Blood(transform.position+(Vector3.up*0.5f));
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
