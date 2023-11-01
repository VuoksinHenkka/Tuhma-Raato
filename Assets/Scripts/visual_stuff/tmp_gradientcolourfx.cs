using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmp_gradientcolourfx : MonoBehaviour
{

    private float currentFlashValue = 0;
    public Gradient ourGradient;
    public TMPro.TMP_Text ourText;
    public bool loop = false;
    public Color originalColor;
    public bool onenable = true;
    public float speed = 0.5f;

    private void Awake()
    {
        originalColor = ourText.color;
    }

    private void OnEnable()
    {
        if(onenable) ColorFlash();
    }

    public void ColorFlash()
    {
        currentFlashValue = 0;
    }

    private void Update()
    {
        if (currentFlashValue < 1)
        {
            ourText.color = originalColor-ourGradient.Evaluate(currentFlashValue);
            ourText.color = new Color(ourText.color.r, ourText.color.g, ourText.color.b, ourGradient.Evaluate(currentFlashValue).a);
            currentFlashValue += speed * Time.deltaTime;
        }
        else if (loop) currentFlashValue = 0;
        
    }
}
