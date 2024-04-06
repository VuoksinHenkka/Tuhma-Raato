using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class sun_setcookieoffset : MonoBehaviour
{
    private UniversalAdditionalLightData lightExtantion;

    private void Awake()
    {
        lightExtantion = GetComponent<UniversalAdditionalLightData>();
    }

    private void Update()
    {
        lightExtantion.lightCookieOffset += (-Vector2.one+Vector2.left) * Time.deltaTime;
    }
}
