using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BullSupply : MonoBehaviour
{
    public Text Vtext;
    public Text Btext;
    public Text Ftext;

    public int VBint = 55;
    public int BBint = 55;
    public int FBint = 55;

    public Text BullT;

    public bool BullSupp = false;

    public KeyPressG keyPressG;
    public AmmoManage ammoManage;

    void Start()
    {
        Vtext.text = "0";
        Btext.text = "0";
        Ftext.text = "0";
        StartCoroutine(BText());
    }

    void Update()
    {
        Vtext.text = VBint.ToString();
        Btext.text = BBint.ToString();
        Ftext.text = FBint.ToString();

        if (BullSupp == true)                        //keyPressG에 있는 BullSupp bool 값을 가져옴
        {
            Debug.Log("탄약 충전");
            DemonsIFbullet();

        }

    }

    public void DemonsIFbullet()                            //bool 값을 거짓으로 변경 후에 CurrentBull 속성에 따라 값 +5
    {
        BullSupp = false;

        if (ammoManage.NowBull == ammoManage.Bullets[1])
        {
            BBint = BBint + 5;

        }
        else if (ammoManage.NowBull == ammoManage.Bullets[0])
        {
            VBint = VBint + 5;
        }
        else if (ammoManage.NowBull == ammoManage.Bullets[2])
        {
            FBint = FBint + 5;
        }
    }
    
    public IEnumerator BText()
    {
        while (true)
        {
            switch (ammoManage.BulletIndex)
            {
                case 0: BullT.text = $"{VBint}"; break;
                case 1: BullT.text = $"{BBint}"; break;
                case 2: BullT.text = $"{FBint}"; break;

            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}