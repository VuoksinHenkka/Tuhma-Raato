using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_group : MonoBehaviour
{
    public List<zombie> ourZombies;
    public FormationBase ourFormation;
    public List<Vector3> formationTargets;
    public GameObject toSpawn;
    public NavMeshAgent ourAgent;

    private void Awake()
    {
        ourAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        formationTargets.Clear();
        foreach (var pos in ourFormation.EvaluatePoints())
        {
            formationTargets.Add(pos);
        }
        foreach(Vector3 foundtarget in formationTargets)
        {
            GameObject spawnGuy = Instantiate(toSpawn, null);
            ourZombies.Add(spawnGuy.GetComponent<zombie>());
            spawnGuy.transform.position = transform.position + foundtarget;
        }
    }

    private void Update()
    {

        
        int ourZombiesCount = ourZombies.Count - 1;
        if (ourZombiesCount <= 0) Destroy(gameObject);

        formationTargets.Clear();
        foreach (var pos in ourFormation.EvaluatePoints())
        {
            formationTargets.Add(pos);
        }

        ourAgent.SetDestination(GameManager.Instance.ref_Player.transform.position);

            for (int i = 0; i < ourZombiesCount; i++)
            {
            if (ourZombies[i] == null)
            {
                ourZombies.RemoveAt(i);
                break;
            }
            else if (ourZombies[i].enabled) ourZombies[i].formationTarget = transform.position + formationTargets[i];
            }
    }
}