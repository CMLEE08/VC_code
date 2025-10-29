using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoManage : MonoBehaviour
{
    public GameObject CurrentBull = null;

    public GameObject NowBull = null;

    public Transform firePoint;

    public List<GameObject> Bullets;                  //V,B,F

    public int BulletIndex = 0;

    [Header("Object Script")]
    public KeyPressG KeyPressG;

    public BullSupply bullSupply;

    void Start()
    {
        CurrentBull = Bullets[BulletIndex];
    }
    void Update()                    // Update is called once per frame
    {
        BullChange();         //change Bull

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    void BullChange()
    {

        if (Input.GetKeyDown(KeyCode.I)) 
        {
            if (BulletIndex == 0)
            {
                BulletIndex = 2;
                CurrentBull = Bullets[BulletIndex];
            }
            else
            {
                BulletIndex = BulletIndex - 1;
                CurrentBull = Bullets[BulletIndex];
            }
         }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (BulletIndex == 2)
            {
                BulletIndex = 0;
                CurrentBull = Bullets[BulletIndex];
            }
            else
            {
                BulletIndex = BulletIndex + 1;
                CurrentBull = Bullets[BulletIndex];
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!KeyPressG.isQTEActive)
            {            
                NowBull = CurrentBull;
                StartCoroutine(KeyPressG.GenSQ(5));                 //yield break while process   
                StartCoroutine(KeyPressG.InputSQ());
            }
            else
            {
                Debug.Log("KeyPressG 실행 중");
            }
        }
    }

    void Shoot()
    {
        compareMod();        
    }

    public void compareMod()                                                        //리스트에 해당하는 int 값이 0일시 else
    {
        if (CurrentBull == Bullets[1])
        {
            if (bullSupply.BBint != 0)
            {
                bullSupply.BBint = bullSupply.BBint - 1;
                Instantiate(CurrentBull, firePoint.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("It's 0!");
            }
        }
        else if (CurrentBull == Bullets[0])
        {
            if (bullSupply.VBint != 0)
            {
                bullSupply.VBint = bullSupply.VBint - 1;
                Instantiate(CurrentBull, firePoint.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("It's 0!");
            }
        }
        else if (CurrentBull == Bullets[2])
        {
            if (bullSupply.FBint != 0)
            {
                bullSupply.FBint = bullSupply.FBint - 1;
                Instantiate(CurrentBull, firePoint.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("It's 0!");                
            }
        }
    }
}