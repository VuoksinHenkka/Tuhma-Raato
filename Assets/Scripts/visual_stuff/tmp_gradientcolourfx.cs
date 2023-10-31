using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmp_gradientcolourfx : MonoBehaviour
{

    private float currentFlashValue = 0;
    public Gradient ourGradient;
    public TMPro.TMP_Text ourText;
    public bool loop = false;

    public void ColorFlash()
    {
        currentFlashValue = 0;
    }

    private void Update()
    {
        if (currentFlashValue < 1)
        {
            ourText.color = ourGradient.Evaluate(currentFlashValue);
            currentFlashValue += 1 * Time.deltaTime;
        }
        else if (loop) currentFlashValue = 0;
        
    }
}
