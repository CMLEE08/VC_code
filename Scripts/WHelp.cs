using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class WHelp : MonoBehaviour
{
    public Animator AnHelp;
    public void HelpStart(int a)
    {
        switch (a)
        {
            case 1: AnHelp.Play("WeaponHelp"); break;
            case 2: AnHelp.Play("WeaponHelpS"); break;
        }

    }
}
