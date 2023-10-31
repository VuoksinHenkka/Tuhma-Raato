using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OnEnable_FlashColor : MonoBehaviour
{
    public Gradient ColorPattern;
    public float ScaleSpeed = 1;
    private float currentEvaluation = 0.01f;
    private Color originalColor;
    private Image ourImage;

    private void Awake()
    {
        ourImage = GetComponent(typeof(Image)) as Image;
        originalColor = ourImage.color;
    }
    private void OnEnable()
    {
        currentEvaluation = 0.01f;
    }


    public void FlashColorOnce()
    {
        currentEvaluation = 0.01f;
    }
    private void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        if (currentEvaluation < 1)
        {
            ourImage.color = ColorPattern.Evaluate(currentEvaluation);
            currentEvaluation += ScaleSpeed * Time.unscaledDeltaTime;
        }
        else if (currentEvaluation != 10)
        {
            ourImage.color = ColorPattern.Evaluate(1);
            currentEvaluation = 10;
        }

    }
}
