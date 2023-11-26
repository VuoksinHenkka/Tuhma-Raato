using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{

    public List<ItemPickUp> Cheap;
    public List<ItemPickUp> Mid;
    public List<ItemPickUp> Expensive;

    private static ItemSpawnManager _instance;
    public static ItemSpawnManager Instance
    {
        get
        {
            if (_instance == null) Debug.Log("NO GAMEMANAGER INSTANCE FOUND");
            return _instance;
        }
        set { _instance = value; }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
    }



    public void SpawnCheap(Vector3 spawnPos)
    {
        RunThruList(Cheap, spawnPos);
    }

    public void SpawnMid(Vector3 spawnPos)
    {
        RunThruList(Mid, spawnPos);
    }
    
    public void SpawnExpensive(Vector3 spawnPos)
    {
        RunThruList(Expensive, spawnPos);
    }

    private void RunThruList(List<ItemPickUp> toCheck, Vector3 spawnPos)
    {
        bool hasSpawned = false;

        for (int i = 0; i < 5; i++)
        {
            ItemPickUp tryToSpawn = toCheck[Random.Range(0, toCheck.Count - 1)];
            if (tryToSpawn.Spawned == false)
            {
                hasSpawned = true;
                PlaceHere(tryToSpawn.gameObject, spawnPos);
                break;
            }
        }
        if (hasSpawned == false)
        {
            foreach (ItemPickUp foundObject in toCheck)
            {
                if (foundObject.Spawned == false)
                {
                    hasSpawned = true;
                    PlaceHere(foundObject.gameObject, spawnPos);
                    break;
                }
            }
        }
    }


    private void PlaceHere(GameObject toSpawn, Vector3 spawnPos)
    {
        toSpawn.transform.position = spawnPos;
        toSpawn.SetActive(true);
    }
}
