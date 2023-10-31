using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_Text_Typewrite : MonoBehaviour
{

    public TMP_Text ourText;
    private bool typewriting = false;
    private float PopUpLastLetterAmount = 0;
    public bool ShowingAllText = false;
    public AudioSource ourTypingsound;

    public void emptytext()
    {
        StopAllCoroutines();
        typewriting = false;
        ourText.text = null;
        ourText.maxVisibleCharacters = 0;
    }

    private void Update()
    {
        if (ourText.textInfo.characterCount == 0)
        {
            ShowingAllText = false;
            return;
        }
        else
        {
            if (ourText.textInfo.characterCount == ourText.maxVisibleCharacters)
            {
                ShowingAllText = true;
                if(ourTypingsound)ourTypingsound.Stop();
            }
            else ShowingAllText = false;
        }
    }


    public void TypewriteThis(string _text)
    {
        if (ourText.text == _text) return;

        if (typewriting) StopAllCoroutines();
        ourText.maxVisibleCharacters = 0;
        if (_text.Length > 0) StartCoroutine(TypewriteThis_Routine(_text));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        ourText.maxVisibleCharacters = 0;
    }

    IEnumerator TypewriteThis_Routine(string _text)
    {
        typewriting = true;
        int lettercount = _text.Length;
        int currentindex = 0;
        ourText.maxVisibleCharacters = 0;
        ourText.text = _text;
        if (ourTypingsound)
        {
            ourTypingsound.time = Random.Range(0, ourTypingsound.clip.length*0.9f);
            ourTypingsound.Play();
        }
        while (currentindex < lettercount)
        {
            yield return new WaitForSecondsRealtime(0.005f);
            ourText.maxVisibleCharacters++;
            PopUpLastLetterAmount = 1;
            currentindex++;
        }
        ourText.text = _text;
        typewriting = false;
    }

    [ContextMenu("DEBUG_TestDialogue")]
    public void _DEBUG_TestDialogue()
    {
        TypewriteThis("What do I need a vacuum claner for? Besides, I bet there's cheaper ones at the supermarket.");
    }


    public Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 1.25f), Mathf.Cos(time * 1.5f));
    }

}
