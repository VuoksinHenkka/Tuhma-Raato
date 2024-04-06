using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FreakyOverlays : MonoBehaviour
{


    public bool freakingout = false;

    public List<UI_Image_ScaleAndColour> freakyimages;
    public Image HurtOverlay;


    private void Start()
    {
        GameManager.Instance.ref_Stats.onHurt += Hurt;    
    }

    private void OnDestroy()
    {
       if (GameManager.Instance) GameManager.Instance.ref_Stats.onHurt -= Hurt;

    }

    public void Hurt()
    {
        HurtOverlay.color = Color.white;
    }
    private void Update()
    {
        if (!freakingout && GameManager.Instance.ref_Stats.CurrentInsanityFX != Stats.InsanityFX.None)
        {
            StartCoroutine(FreakOut());
        }

        if (HurtOverlay.color.a != 0)
        {
            float alpha = HurtOverlay.color.a;
            alpha = Mathf.Clamp(alpha -= 2 * Time.deltaTime, 0, 1);
            HurtOverlay.color = new Color(1,1,1,alpha);
        }
    }


    IEnumerator FreakOut()
    {
        freakingout = true;
        freakyimages[Random.Range(0, freakyimages.Count - 1)].Call_ModifyBothIFnotActive();
        yield return new WaitForSeconds(2f);
        freakingout = false;
    }
}
