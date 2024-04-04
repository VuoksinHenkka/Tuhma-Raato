using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_ending : MonoBehaviour
{
    public TMPro.TMP_Text ourText;


    private void OnEnable()
    {
        string toBuild = "The nightmare is finally over. ";
        if (GameManager.Instance.ref_Stats.Sanity < 20) toBuild = toBuild + "By pushing your last pieces of sanity";
        else toBuild = toBuild + "Under the orange searchlight";

        toBuild = toBuild + " you climb into the helicopter. ";

        if (GameManager.Instance.ref_Stats._vodka != 0)
        {
            toBuild = toBuild + "Quite a feat with " + GameManager.Instance.ref_Stats._vodka.ToString() + " bottles of vodka in your system. ";
        }
        else if (GameManager.Instance.ref_Stats._water != 0)
        {
            toBuild = toBuild + "You celebrate by letting out the " + (GameManager.Instance.ref_Stats._water + GameManager.Instance.ref_Stats._vodka).ToString() + " litres of consumed liquids in your bladder. ";
        }
        toBuild = toBuild + "The military looks at you with respect. Thru the night, you managed to neutralize " + GameManager.Instance.ref_Stats._zombies.ToString() + " zombies. ";

        if (GameManager.Instance.ref_Stats._liquorice != 0)
        {
            toBuild = toBuild + "You also consumed " + GameManager.Instance.ref_Stats._liquorice.ToString() + " bags of salty liquorice, which is truly noteworthy. ";
        }
        if (GameManager.Instance.ref_Stats._money != 0)
        {
            toBuild = toBuild + "Freedom smells good. Almost as good as the " + GameManager.Instance.ref_Stats._money.ToString() + "0 bucks you collected from corpses. ";
        }
        if (GameManager.Instance.ref_Stats._lint != 0)
        {
            toBuild = toBuild + "You think of the " + GameManager.Instance.ref_Stats._lint.ToString() + " clumps of random pocket lint you stole, and how someday you can look back at them and think of this night. ";
        }
        toBuild = toBuild + "Congratulations for your survival.";
        ourText.text = toBuild;
    }
}
