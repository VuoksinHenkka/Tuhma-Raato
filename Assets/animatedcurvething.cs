using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatedcurvething : MonoBehaviour
{


    public AnimationCurve curve_x;
    public AnimationCurve curve_y;
    public AnimationCurve curve_z;

    public float speed = 1.0f;
    public float clampvalue = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (clampvalue > 1) clampvalue = 0;
        else clampvalue = clampvalue += Time.deltaTime * speed;

        transform.position = (transform.position += new Vector3(curve_x.Evaluate(clampvalue), curve_y.Evaluate(clampvalue), curve_z.Evaluate(clampvalue)));
    }
}
