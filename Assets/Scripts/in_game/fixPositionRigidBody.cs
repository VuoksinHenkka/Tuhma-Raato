using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixPositionRigidBody : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
