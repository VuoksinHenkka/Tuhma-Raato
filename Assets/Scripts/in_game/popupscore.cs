using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupscore : MonoBehaviour
{
    public Canvas ourCanvas;
    public TMPro.TMP_Text ourText;
    public float lifetime = 2;
    private float currentlifetime = 2;
    public Vector3 originalScale;
    public float currentscalemultiplier = 1;

    private void Awake()
    {
        originalScale = transform.localScale;
    }
    private void OnEnable()
    {
        ourCanvas.worldCamera = GameManager.Instance.ref_Camera.GetComponent<Camera>();
        currentlifetime = lifetime;
    }
    public void SetScore(int score)
    {
        ourText.text = score.ToString();
    }

    private void Update()
    {
        if (!isActiveAndEnabled) return;

        transform.Translate(Vector3.up * 4f * Time.deltaTime);
        if (currentlifetime > 0) currentlifetime -= 1 * Time.deltaTime;
        else gameObject.SetActive(false);

        float distance = Vector3.Distance(transform.position, GameManager.Instance.ref_Camera.transform.position);
        float inverselerp = Mathf.InverseLerp(0, 600, distance);
        currentscalemultiplier = Mathf.Lerp(1, 8, inverselerp);
        transform.localScale = originalScale * currentscalemultiplier;
    }
}
