using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie : MonoBehaviour
{
    public NavMeshAgent ourAgent;
    public bool interestedInPlayer = true;
    public float attackDistance = 1;
    private void Update()
    {
        if (interestedInPlayer)
        {
            ourAgent.SetDestination(GameManager.Instance.ref_Player.transform.position);
            if (Vector3.Distance(transform.position, GameManager.Instance.ref_Player.transform.position) < attackDistance)
            {
                print("hurt player");
                GameManager.Instance.HurtPlayer();
            }
        }
        
    }
}
