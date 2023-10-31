using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OnEnable_RandomRotation : MonoBehaviour
{
    private Image ourImage;
    public float RangeOfDegrees = 90;
    private float rotateBuffer = 0;

    private void Awake()
    {
        ourImage = GetComponent(typeof(Image)) as Image;
    }
    private void OnEnable()
    {
        ourImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-RangeOfDegrees, RangeOfDegrees)));
        rotateBuffer = 1;
    }

    public void RotateOnce()
    {
        if (rotateBuffer != 0) return;
        ourImage.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-RangeOfDegrees, RangeOfDegrees)));
        rotateBuffer = 1;
    }

    private void Update()
    {
        if (rotateBuffer == 0) return;
        rotateBuffer = Mathf.Clamp(rotateBuffer - 1 * Time.deltaTime, 0, 2);
    }
}
