using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlespawner : MonoBehaviour
{
    [SerializeField] public List<GameObject> particles_blood;
    [SerializeField] public List<GameObject> particles_splinters;


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

    public void Spawn_Splinters(Vector3 worldPosition)
    {
        foreach (GameObject foundMessage in particles_splinters)
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
