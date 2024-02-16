using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlespawner : MonoBehaviour
{
    [SerializeField] public List<GameObject> particles_blood;


    private void Start()
    {
        GameManager.Instance.ref_particlespawner = this;
    }

    public void Spawn_Blood(Vector3 worldPosition)
    {
        foreach (GameObject foundMessage in particles_blood)
        {
            if (foundMessage.gameObject.activeSelf == false)
            {
                foundMessage.gameObject.transform.position = worldPosition;
                foundMessage.gameObject.SetActive(true);
                break;
            }
        }
    }
}
