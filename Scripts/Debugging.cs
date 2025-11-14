using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Debugging : MonoBehaviour
{
    public Text XI;
    public Text YI;
    public Text ZI;

    public bool XB = false;
    public bool YB = false;
    public bool ZB = false;

    public AmmoManage ammoManage;
    public BullSupply bullSupply;

    public SlideWeapon slideWeapon;



    void Start()
    {
        XI.text = "0";
        YI.text = "0";
        ZI.text = "0";

        StartCoroutine(Bullint());
    }

    // Update is called once per frame
    void Update()
    {
        if (XB == true)
        {
            XI.text = "1";
            StartCoroutine(ChangeX());
        }

    }

    public IEnumerator ChangeX()
    {
        if (XB == true)
        {
            yield return new WaitForSeconds(0.1f);
            XI.text = "0";
            XB = false;

            yield break;
        }

        yield return new WaitForSeconds(0.1f);
    }

    public void BullCheck()
    {
        switch (slideWeapon.bullindex)
        {
            case 0: YI.text = "V"; break;
            case 1: YI.text = "B"; break;
            case 2: YI.text = "F"; break;

        }
    }
    
    public IEnumerator Bullint()
    {
        while (true)
        {
            switch (ammoManage.BulletIndex)
            {
                case 0: ZI.text = $"{bullSupply.VBint}"; break;
                case 1: ZI.text = $"{bullSupply.BBint}"; break;
                case 2: ZI.text = $"{bullSupply.FBint}"; break;

            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
