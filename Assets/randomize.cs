using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomize : MonoBehaviour
{

    public List<GameObject> ourObjects;

    public void Randomize()
    {
        foreach(GameObject foundObjects in ourObjects)
        {
            foundObjects.SetActive(false);
        }

        for (int i = 0; i < 2; i++)
        {
            ourObjects[Random.Range(0, ourObjects.Count - 1)].SetActive(true);
        }
    }

    private void Awake()
    {
        foreach (Transform foundTransform in transform)
        {
            ourObjects.Add(foundTransform.gameObject);
        }
    }

    private void Start()
    {
        GameManager.Instance.onGameBegin += Randomize;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance) GameManager.Instance.onGameBegin -= Randomize;
    }
}
