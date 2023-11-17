using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class gameSpawnZombies : MonoBehaviour
{
    private float spawnFrequency = 40;
    private int spawnAmount = 3;
    public GameObject Zombie_Basic;
    public List<Transform> spawnPositions;
    private float currenttime = 0;


    private void Start()
    {
        currenttime = spawnFrequency;
    }

    void Update()
    {

        if (currenttime < spawnFrequency) currenttime += 1 * Time.deltaTime;
        else
        {
            currenttime = 0;
            spawnFrequency = Mathf.Clamp(spawnFrequency--, 10, 40);
            StartSpawning();
        }
    }

    private void StartSpawning()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnZombies();
        }
        if (spawnAmount < 10) spawnAmount++;
    }

    private void SpawnZombies()
    {
        Vector3 newspawnPosition = getSpawnPosition();
        if (Vector3.Distance(newspawnPosition, GameManager.Instance.ref_Player.transform.position) < 10)
        {
            SpawnZombies();
            return;
        }
        GameObject newInstance = Instantiate(Zombie_Basic, newspawnPosition, Quaternion.identity);
    }

    private Vector3 getSpawnPosition()
    {
        Transform currentSpawnPos = spawnPositions[0];
        float distancetoPlayer = Vector3.Distance(currentSpawnPos.position, GameManager.Instance.ref_Player.transform.position);

        for(int i = 0; i < 6; i++)
        {
            Transform foundTransform = spawnPositions[Random.Range(0, spawnPositions.Count-1)];
            if (Vector3.Distance(foundTransform.position, GameManager.Instance.ref_Player.transform.position) > distancetoPlayer) currentSpawnPos = foundTransform;

        }
        return currentSpawnPos.position;
    }
}
