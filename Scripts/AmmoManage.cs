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

    public int randomO = 1;

    public bool isQTEActive = false;

    [Header("Object Script")]
    public KeyPressG KeyPressG;

    public KeyPressM KeyPressM;

    public BullSupply bullSupply;

    public SlideWeapon slideWeapon;

    void Start()
    {
        CurrentBull = Bullets[0];
    }
    void Update()                    // Update is called once per frame
    {
        SupB();

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    void SupB()
    {
        if (slideWeapon.isDot) return;
        if (slideWeapon.isSeting) return;



        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!isQTEActive)
            {
                NowBull = CurrentBull;
                ActiveO();
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

    public void ActiveO()
    {
        randomO = Random.Range(0, 100);
        if (randomO < 60)
        {
            StartCoroutine(KeyPressG.GenSQ(5));
            StartCoroutine(KeyPressG.InputSQ());
        }
        else
        {
            KeyPressM.StartMP();
        }
    }
    
    public void CheckBull()
    {
        switch (slideWeapon.bullindex)
        {
            case 0:
                CurrentBull = Bullets[0];
                break;
            case 1:
                CurrentBull = Bullets[1];
                break;
            case 2:
                CurrentBull = Bullets[2];
                break;
        }
    }
}