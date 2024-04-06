using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_playerAiming_Rectangle : MonoBehaviour
{

    public TMP_Text distancemeter;
    public GameObject distancemeter_root;

    public TMP_Text targetName;
    public TMP_Text targetHP;
    public GameObject targetHP_root;

    public string NameOfTarget;
    public string HPofTarget;


    private void Update()
    {

        if (distancemeter.text != "" && distancemeter_root.activeSelf == false) distancemeter_root.SetActive(true);
        else if (distancemeter.text == "" && distancemeter_root.activeSelf == true) distancemeter_root.SetActive(false);

        if (NameOfTarget != "")
        {
            if (targetName.text != NameOfTarget) targetName.text = NameOfTarget;
            targetName.enabled = true;
        }
        else if (targetName.enabled) targetName.enabled = false;

        if (HPofTarget != "")
        {
            if (targetHP.text != HPofTarget) targetHP.text = HPofTarget;
            if(targetHP_root.activeSelf == false) targetHP_root.SetActive(true);
        }
        else if (HPofTarget == "" && targetHP_root.activeSelf == true) targetHP_root.SetActive(false);

    }
}
