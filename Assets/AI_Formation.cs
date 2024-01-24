using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Formation : MonoBehaviour
{
    public NavMeshAgent ourAgent;
    // Update is called once per frame
    void Update()
    {
    if (ourAgent && GameManager.Instance) ourAgent.SetDestination(GameManager.Instance.ref_Player.transform.position);
    }
}
