using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OnEnable_PopUpGFX : MonoBehaviour
{

    public AnimationCurve ScalePattern;
    public float ScaleSpeed = 1;
    private float currentEvaluation = 0.01f;
    private Vector3 originalScale;
    private bool disableWithPopUp_Active = false;
    public Vector2 speedScaleRandom = Vector2.zero;


    private void Awake()
    {
        originalScale = transform.localScale;
        if (speedScaleRandom != Vector2.zero ) { ScaleSpeed = ScaleSpeed * Random.Range(speedScaleRandom.x, speedScaleRandom.y); }
    }
    private void OnEnable()
    {
        currentEvaluation = 0.01f;
        transform.localScale = originalScale * ScalePattern.Evaluate(currentEvaluation);
    }

    public void PopOnce()
    {
        currentEvaluation = 0.01f;
        transform.localScale = originalScale * ScalePattern.Evaluate(currentEvaluation);
    }
    private void Update()
    {
        if (!gameObject.activeInHierarchy || disableWithPopUp_Active) return;
        if (currentEvaluation<1)
        {
            transform.localScale = originalScale * ScalePattern.Evaluate(currentEvaluation);
            currentEvaluation += ScaleSpeed * Time.unscaledDeltaTime;
        }
        else if(transform.localScale != originalScale)
        {
            transform.localScale = originalScale;
        }

    }



    public void DisableWithPopUp()
    {
        if (disableWithPopUp_Active) return;
        StartCoroutine(DisableWithPopUp_Routine());
    }

    IEnumerator DisableWithPopUp_Routine()
    {
        disableWithPopUp_Active = true;
        currentEvaluation = 1f;
        while (currentEvaluation != 0)
        {
            transform.localScale = originalScale * ScalePattern.Evaluate(currentEvaluation);
            currentEvaluation = Mathf.Clamp(currentEvaluation -= 0.01f, 0, 1);
            yield return new WaitForSecondsRealtime(0.005f);
        }

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.localScale = originalScale;
        disableWithPopUp_Active = false;
    }
}