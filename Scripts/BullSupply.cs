using UnityEngine;
using UnityEngine.UI;

public class BullSupply : MonoBehaviour
{
    public Text Vtext;
    public Text Btext;
    public Text Ftext;

    public int VBint = 0;
    public int BBint = 0;
    public int FBint = 0;




    public GameObject bulletBack;
    public GameObject bulletVirus;
    public GameObject bulletFungus;

    public KeyPressG keyPressG;
    public AmmoManage ammoManage;


    void Start()
    {


        Vtext.text = "0";
        Btext.text = "0";
        Ftext.text = "0";
    }

    void Update()
    {
        Vtext.text = VBint.ToString();
        Btext.text = BBint.ToString();
        Ftext.text = FBint.ToString();

        if (keyPressG.BullSupp == true)                        //keyPressG에 있는 BullSupp bool 값을 가져옴
        {
            DemonsIFbullet();

        }

    }

    public void DemonsIFbullet()                            //bool 값을 거짓으로 변경 후에 CurrentBull 속성에 따라 값 +5
    {
        keyPressG.BullSupp = false;
 
        if (ammoManage.NowBull == bulletBack)       
        {
            BBint = BBint + 5;

        }
        else if (ammoManage.NowBull == bulletVirus)
        {
            VBint = VBint + 5;
        }
        else if (ammoManage.NowBull == bulletFungus)
        {
            FBint = FBint + 5;
        }
    }

}
