using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AmmoManage : MonoBehaviour
{
    public GameObject CurrentBull = null;

    public GameObject NowBull = null;

    public Transform firePoint;


    public GameObject bulletBack;
    public GameObject bulletVirus;
    public GameObject bulletFungus;


    [Header("Object Script")]
    public KeyPressG KeyPressG;

    public BullSupply bullSupply;
    






    void Start()
    {
        CurrentBull = bulletVirus;


    }
    // Update is called once per frame
    void Update()
    {
        BullChange(KeyCode.I, bulletVirus);
        BullChange(KeyCode.O, bulletBack);
        BullChange(KeyCode.P, bulletFungus);         //change Bull

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();


    }

    void BullChange(KeyCode key, GameObject bullet)
    {
        if (Input.GetKeyDown(key))
        {
            if (CurrentBull == bullet)
            {
                if (!KeyPressG.isQTEActive)                //yield break while process
                {
                    NowBull = CurrentBull;
                    StartCoroutine(KeyPressG.GenSQ(5));
                    StartCoroutine(KeyPressG.InputSQ());
                }
            }
            else
            {
                CurrentBull = bullet;
                Debug.Log($"{key} 버튼 총알 선택됨");
            }
        }
    }

    void Shoot()
    {
        compareMod();        
    }

    public void compareMod()                                                        //이거 뭐라는겨
    {
        if (CurrentBull == bulletBack)
        {
            if (bullSupply.BBint != 0)
            {
                bullSupply.BBint = bullSupply.BBint - 1;
                Instantiate(CurrentBull, firePoint.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("총알이 없는디?");
            }
        }
        else if (CurrentBull == bulletVirus)
        {
            if (bullSupply.VBint != 0)
            {
                bullSupply.VBint = bullSupply.VBint - 1;
                Instantiate(CurrentBull, firePoint.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("총알이 없는디?");
            }
        }
        else if (CurrentBull == bulletFungus)
        {
            if (bullSupply.FBint != 0)
            {
                bullSupply.FBint = bullSupply.FBint - 1;
                Instantiate(CurrentBull, firePoint.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("총알이 없는디?");                
            }
        }
    }
}

