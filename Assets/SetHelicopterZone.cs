using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHelicopterZone : MonoBehaviour
{

    public List<GameObject> ourHelicopters;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onEvacZoneActive += ActivateHelicopter;
        GameManager.Instance.onGameBegin += Reset;
    }

    public void Reset()
    {
        foreach(GameObject found in ourHelicopters)
        {
            found.SetActive(false);
        }
    }

    public void ActivateHelicopter()
    {
        ourHelicopters[Random.Range(0, ourHelicopters.Count)].SetActive(true);
    }

}
