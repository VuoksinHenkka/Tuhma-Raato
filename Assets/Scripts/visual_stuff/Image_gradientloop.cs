using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_gradientloop : MonoBehaviour
{

    public Gradient ourGradient;
    private float lifetime = 0;
    public Image ourImage;
    private float alpha = 0;
    public bool FadeIn = true;

    private void OnEnable()
    {
        alpha = 0;
        lifetime = Random.Range(0, 0.75f);
    }
    void Update()
    {
        if (lifetime < 1) lifetime += 1 * Time.deltaTime;
        else lifetime = 0;
        ourImage.color = ourGradient.Evaluate(lifetime);
        if (FadeIn && alpha != 1)
        {
            alpha += 1 * Time.deltaTime;
            ourImage.color = new Color(ourImage.color.r, ourImage.color.g, ourImage.color.b, alpha);
        }

    }
}
