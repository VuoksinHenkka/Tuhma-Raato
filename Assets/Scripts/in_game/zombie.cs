using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie : enemy, IHaveName
{


    public enum ZombieType
    {
        FAST,
        NORMAL,
        SUPER
    }

    public ZombieType ourZombieType = ZombieType.NORMAL;

    public characterGFX ourcharacterGFX;
    public NavMeshAgent ourAgent;
    public bool interestedInPlayer = true;
    private float attackDistance = 2;
    private float currentDistanceToPlayer = 10;
    private float AttackTimer = 0f;
    private string ourName = "Zombie";
    public float MoveSpeed = 1;
    public int HP = 5;
    public Vector3 formationTarget = Vector3.zero;
    public bool FixingPath = false;

    private OffMeshLinkData ourMeshLinkData;
    public bool AnimationLaunched_Hurdle = false;
    private int Hurdle_NavMeshLayer = 0;
    public bool Delisting = false;

    private void OnEnable()
    {
        FixingPath = false;
        Delisting = false;
        ourAgent.avoidancePriority = Random.Range(20, 60);
        if(GameManager.Instance)GameManager.Instance.SpawnedZombie(ourZombieType);
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
        if(ourZombieType != ZombieType.SUPER) ourAgent.avoidancePriority = Random.Range(30, 60);
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

    private void OnDisable()
    {
        DelistZombie();
        FixingPath = false;
    }

    public void DelistZombie()
    {
        StopAllCoroutines();
        if (Delisting) return;
        Delisting = true;
        GameManager.Instance.RemovedZombie(ourZombieType);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.onGameBegin -= Clean;
        GameManager.Instance.onGameOver -= Clean;
        GameManager.Instance.onGameEnding -= Clean;
        DelistZombie();
    }


    private void Update()
    {
        
            ourAgent.SetDestination(GameManager.Instance.ref_Player.transform.position);

            if (ourAgent.hasPath == false)
            {
                if (FixingPath == false)
                {
                    StartCoroutine(SolveStuck());
                    FixingPath = true;
                }
            }
            else if (FixingPath) FixingPath = false;
            if (ourcharacterGFX) ourcharacterGFX.LookToDirection = GameManager.Instance.ref_Player.transform.position;


            if (DistanceToPlayer() < 10)
            {
                int RandomChance = Random.Range(0, 10);
                if (RandomChance == 0) AudioManager.Instance.SFX_ZombieYell();
            }
            
            if (DistanceToPlayer() < attackDistance)
            {
                if (AttackTimer != 0) AttackTimer = Mathf.Clamp(AttackTimer -= 1 * Time.deltaTime, 0, 2);
                else
                {
                    GameManager.Instance.ref_Stats.HP_Modify(-10);
                    GameManager.Instance.ref_particlespawner.Spawn_Blood(GameManager.Instance.ref_Player.transform.position);
                    AudioManager.Instance.play_sfx(AudioManager.sfxtype.player_hit);
                    AttackTimer = 0.5f;

                }
            }
            else if (AttackTimer != 0.5f) AttackTimer = 0.5f;       

          
        ourcharacterGFX.ourMoveVelocity = ourAgent.velocity.magnitude;

        if (currentDistanceToPlayer < 30) NavMeshLayerReactions();

    }


    IEnumerator SolveStuck()
    {
        FixingPath = true;
        float lastDistanceToTarget = ourAgent.remainingDistance;
        yield return null;

        while (FixingPath)
        {
            if (ourAgent.remainingDistance > ourAgent.stoppingDistance)
            {
                float distanceToTarget = ourAgent.remainingDistance;
                if (lastDistanceToTarget - distanceToTarget < 1f)
                {
                    Vector3 destination = ourAgent.destination;
                    ourAgent.ResetPath();
                    ourAgent.SetDestination(destination);
                    lastDistanceToTarget = distanceToTarget;
                }
                yield return new WaitForSeconds(Random.Range(0.25f,1f));
            }
            yield return null;
        }
        yield return null;
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
        GameManager.Instance.ref_Stats._zombies++;
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
