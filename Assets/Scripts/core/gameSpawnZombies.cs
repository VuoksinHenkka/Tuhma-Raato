using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class gameSpawnZombies : MonoBehaviour
{
    private float spawnFrequency = 5;
    private int spawnAmount = 2;
    public GameObject Zombie_Basic;
    public GameObject Zombie_Fast;
    public GameObject Zombie_Super;

    public List<Transform> spawnPositions;
    private float currenttime = 0;


    private void Start()
    {
        currenttime = spawnFrequency;
        GameManager.Instance.ourzombiespawner = this;
    }

    void Update()
    {

        if (currenttime < spawnFrequency) currenttime += 1 * Time.deltaTime;
        else
        {
            currenttime = 0;
            spawnFrequency = 5;
            StartSpawning();
        }
    }

    private void StartSpawning()
    {
        SpawnFastZombie();

        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnZombies();
            if (GameManager.Instance.Time_Hour < 5) SpawnSuperZombie();


        }
    }

    private void SpawnZombies()
    {
        if (GameManager.Instance.activeNormalZombies == 10) return;
        Vector3 newspawnPosition = getSpawnPosition();
        if (Vector3.Distance(newspawnPosition, GameManager.Instance.ref_Player.transform.position) < 8)
        {
            return;
        }
        GameObject newInstance = Instantiate(Zombie_Basic, newspawnPosition, Quaternion.identity);
    }
    private void SpawnSuperZombie()
    {
        if (GameManager.Instance.activeSuperzombies == 10) return;
        Vector3 newspawnPosition = getSpawnPosition();
        if (Vector3.Distance(newspawnPosition, GameManager.Instance.ref_Player.transform.position) < 8)
        {
            return;
        }
        GameObject newInstance = Instantiate(Zombie_Super, newspawnPosition, Quaternion.identity);
    }
    private void SpawnFastZombie()
    {
        if (GameManager.Instance.activeFastZombies == 10) return;
        Vector3 newspawnPosition = getSpawnPosition();
        if (Vector3.Distance(newspawnPosition, GameManager.Instance.ref_Player.transform.position) < 8)
        {
            return;
        }
        GameObject newInstance = Instantiate(Zombie_Fast, newspawnPosition, Quaternion.identity);
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
