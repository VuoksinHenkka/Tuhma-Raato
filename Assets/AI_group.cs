using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_group : MonoBehaviour
{
    public List<zombie> ourZombies;
    public FormationBase ourFormation;
    public List<Vector3> formationTargets;

    private void Update()
    {

        
        int ourZombiesCount = ourZombies.Count - 1;
        if (ourZombiesCount <= 0) Destroy(gameObject);

        formationTargets.Clear();
        foreach (var pos in ourFormation.EvaluatePoints())
        {
            formationTargets.Add(pos);
        }


            for (int i = 0; i < ourZombiesCount; i++)
            {
            if (ourZombies[i] == null) continue;
                if (ourZombies[i].enabled) ourZombies[i].formationTarget = transform.position+formationTargets[i];
            }
    }
}
