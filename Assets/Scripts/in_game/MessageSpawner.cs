using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSpawner : MonoBehaviour
{
    public List<popupMessage> ourMessages;
    public GameManager ourgameManager;


    private void Start()
    {
        GameManager.Instance.ref_messagespawner = this;
    }

    public void SpawnMessage(string Message, Color messagecolor, Vector3 worldPosition)
    {
        foreach(popupMessage foundMessage in ourMessages)
        {
            if (foundMessage.gameObject.activeSelf == false)
            {
                foundMessage.gameObject.transform.position = worldPosition+(Vector3.up*1.25f);
                foundMessage.gameObject.SetActive(true);
                foundMessage.SetMessage(Message, messagecolor);
                break;
            }
        }
                }
}
