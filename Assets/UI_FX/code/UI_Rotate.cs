using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Rotate : MonoBehaviour
{

    public Vector3 Degrees = Vector3.zero;

    public enum rotationType {RandomPositiveNegative, AddOnCurrentRotation, None}
    public rotationType rotationOnEnable = rotationType.None;
    public rotationType rotationOnCall = rotationType.None;
    public bool loop = false;

    private Vector3 currentQuaternionEuler;

    private void OnEnable()
    {
        if (rotationOnEnable == rotationType.RandomPositiveNegative)
        {
            transform.localRotation = Quaternion.Euler(Random.Range(-Degrees.x, Degrees.x), Random.Range(-Degrees.y, Degrees.y), Random.Range(-Degrees.z, Degrees.z));
        }
        else if (rotationOnEnable == rotationType.AddOnCurrentRotation)
        {
            currentQuaternionEuler = transform.localEulerAngles;
            transform.localRotation = Quaternion.Euler(currentQuaternionEuler += Degrees);
        }
    }

    public void Call_Rotate()
    {
        if (rotationOnEnable == rotationType.RandomPositiveNegative)
        {
            transform.localRotation = Quaternion.Euler(Random.Range(-Degrees.x, Degrees.x), Random.Range(-Degrees.y, Degrees.y), Random.Range(-Degrees.z, Degrees.z));
        }
        else if (rotationOnEnable == rotationType.AddOnCurrentRotation)
        {
            currentQuaternionEuler = transform.localEulerAngles;
            transform.localRotation = Quaternion.Euler(currentQuaternionEuler += Degrees);
        }
    }

    private void Update()
    {
        if (loop)
        {
            currentQuaternionEuler = transform.localEulerAngles;
            transform.localRotation = Quaternion.Euler(currentQuaternionEuler += (Degrees * Time.unscaledDeltaTime));
        }
    }
}
