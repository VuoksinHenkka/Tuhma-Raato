using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupMessage : MonoBehaviour
{
    public Canvas ourCanvas;
    public TMPro.TMP_Text ourText;
    private float lifetime = 3;
    private float currentlifetime = 2;
    public Vector3 originalScale;
    public float currentscalemultiplier = 1;
    public float RandomSpeed = 2;
    public tmp_gradientcolourfx ourgradientFX;

    private void Awake()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.y);
        originalScale = transform.localScale;
    }
    private void OnEnable()
    {
        RandomSpeed = Random.Range(1, 2);
        ourCanvas.worldCamera = GameManager.Instance.ref_Camera.GetComponent<Camera>();
        currentlifetime = lifetime;
    }
    public void SetMessage(string Message, Color colour)
    {
        ourText.text = Message;
        if(ourgradientFX) ourgradientFX.originalColor = colour;
        else ourText.color = colour;
    }

    private void Update()
    {
        if (!isActiveAndEnabled) return;
        if (GameManager.Instance.currentGameSate != GameManager.gamestate.Gameplay && GameManager.Instance.currentGameSate != GameManager.gamestate.Inventory) ourCanvas.enabled = false;
        else if (ourCanvas.enabled == false) ourCanvas.enabled = true;
        transform.Translate(Vector3.up * RandomSpeed * Time.deltaTime);
        if (currentlifetime > 0) currentlifetime -= 1 * Time.deltaTime;
        else gameObject.SetActive(false);

        float distance = Vector3.Distance(transform.position, GameManager.Instance.ref_Camera.transform.position);
        float inverselerp = Mathf.InverseLerp(0, 600, distance);
        currentscalemultiplier = Mathf.Lerp(1, 8, inverselerp);
        transform.localScale = originalScale * currentscalemultiplier;
    }

    private void LateUpdate()
    {
        transform.LookAt(GameManager.Instance.ref_Camera.transform.position, Vector3.up);
    }
}
