using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FX_Demo_Button : MonoBehaviour
{

    public GameObject Horror;
    public GameObject Robotic;
    public GameObject Cute;

    public void GoToHorror()
    {
        Horror.SetActive(true);
        Robotic.SetActive(false);
        Cute.SetActive(false);
    }

    public void GoToRobotic()
    {
        Horror.SetActive(false);
        Robotic.SetActive(true);
        Cute.SetActive(false);
    }

    public void GoToCute()
    {
        Horror.SetActive(false);
        Robotic.SetActive(false);
        Cute.SetActive(true);
    }
}
