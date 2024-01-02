using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UI_Selectable : Selectable, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    [Header("Replace Colour schemes")]
    public file_SelectableColourTemplate ourColorTemplate;

    [Header("SelectionSettings")]
    public bool OnFocusModifySize = true;
    public float ObjectSizeOnSelect = 1.15f;
    private Vector3 originalSize;
    private Vector3 selectedSize;
    public bool OnFocusMakeSound = true;
    public AudioClip OnSelectSound;
    public bool OnPress_MakeSound = true;
    public AudioClip OnPressSound;

    public AudioSource ourAudioSource;

    [Header("Extended Events")]
    public UnityEvent OnClick;
    public UnityEvent OnFocus;
    public UnityEvent OnUnfocus;

    private bool buttonPressed = false;

    //these are for replacing the colour profiles
    private ColorBlock ourColourBlock;

    private void Awake()
    {
        if (ourColorTemplate) ReplaceButtonColours();


        originalSize = transform.localScale;
        selectedSize = originalSize * ObjectSizeOnSelect;
        if(OnPress_MakeSound || OnFocusMakeSound)
        {
            if (!ourAudioSource) ourAudioSource = gameObject.GetComponent<AudioSource>();
            if (!ourAudioSource) //make a default one
            {
                gameObject.AddComponent<AudioSource>();
                ourAudioSource = gameObject.GetComponent<AudioSource>();
                ourAudioSource.playOnAwake = false;
                ourAudioSource.loop = false;
                ourAudioSource.spatialBlend = 0;
            }
        }
    }

    public void OnEnable()
    {
        if (interactable == false) image.color = colors.disabledColor * colors.colorMultiplier;
        else image.color = colors.normalColor * colors.colorMultiplier;
    }

    public void OnDisable()
    {
        StopAllCoroutines();
        buttonPressed = false;
        image.color = colors.normalColor;
        transform.localScale = originalSize;
    }

    

    private void SetNewImageColor(Color toSet)
    {
        StopAllCoroutines();
        if (gameObject.activeInHierarchy == false) return;

        StartCoroutine(TransitionColourTo(toSet));
    }

    IEnumerator TransitionColourTo(Color transitionTo)
    {
        float lerpValue = 0;
        Color startColor = image.color;
        while(lerpValue != 1)
        {
            image.color = Color.Lerp(startColor, transitionTo, lerpValue);
            lerpValue = Mathf.Clamp(lerpValue += 0.1f, 0, 1);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        image.color = transitionTo;

    }

    public void ReplaceButtonColours()
    {
        ourColourBlock = colors;
        ourColourBlock.normalColor = ourColorTemplate.NormalColor;
        ourColourBlock.highlightedColor = ourColorTemplate.HighlightedColor;
        ourColourBlock.pressedColor = ourColorTemplate.PressedColor;
        ourColourBlock.selectedColor = ourColorTemplate.SelectedColor;
        ourColourBlock.disabledColor = ourColorTemplate.DisabledColor;
        ourColourBlock.colorMultiplier = ourColorTemplate.ColorMultiplier;
        ourColourBlock.fadeDuration = ourColorTemplate.FadeDuration;


        colors = ourColourBlock;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy == false) return;

        if (interactable == false) return;
       if (OnFocusModifySize) transform.localScale = selectedSize;
        if (OnFocusMakeSound) MakeSoundFocus();
        SetNewImageColor(colors.selectedColor * colors.colorMultiplier);
        OnFocus.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy == false) return;

        if (OnFocusModifySize)  transform.localScale = originalSize;
        OnUnfocus.Invoke();
        SetNewImageColor(colors.normalColor * colors.colorMultiplier);
        buttonPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy == false) return;
        if (interactable == false) return;
        if (buttonPressed == false)
        {
            if (OnPress_MakeSound) MakeSoundPress();
            OnClick.Invoke();
            SetNewImageColor(colors.pressedColor * colors.colorMultiplier);
            buttonPressed = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy == false) return;

        buttonPressed = false;
    }

    public void MakeSoundFocus()
    {
        if (gameObject.activeInHierarchy == false) return;

        if (ourAudioSource) ourAudioSource.PlayOneShot(OnSelectSound);
    }

    public void MakeSoundPress()
    {
        if (gameObject.activeInHierarchy == false) return;

        if (ourAudioSource) ourAudioSource.PlayOneShot(OnPressSound);
    }

}
