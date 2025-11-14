using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using JetBrains.Annotations;

public class KeyPressG : MonoBehaviour
{
    [Header("Prefab & Panel")]
    public GameObject ArPre;  
    public RectTransform arrowPanel;

    public GameObject PanelTF;

    [Header("Arrow Sprites")]
    public Sprite UpAr;
    public Sprite DownAr;
    public Sprite LeftAr;
    public Sprite RightAr;

    [Header("Arrow Sprites")]
    public BullSupply bullSupply;
    public AmmoManage ammoManage;




    public List<KeyCode> ArrowList = new List<KeyCode>();
    public List<GameObject> ArrowObject = new List<GameObject>();

    void Start()
    {
        arrowPanel.GetComponent<HorizontalLayoutGroup>().enabled = true;
        PanelTF.SetActive(false);
    }


    public IEnumerator InputSQ()                                  //Puzzle Process and Result
    {



        while (ArrowList.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||         //Compare with player pressed
                Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Input.GetKeyDown(ArrowList[0]))
                {
                    ArrowList.RemoveAt(0);
                    Destroy(ArrowObject[0]);
                    ArrowObject.RemoveAt(0);

                    if (ArrowList.Count == 0)
                    {
                        Debug.Log("모든 퍼즐 성공");
                        ArrowObject.Clear();
                        ammoManage.isQTEActive = false;
                        bullSupply.BullSupp = true;
                        PanelTF.SetActive(false);


                        yield break;

                    }

                }
                else
                {
                    Debug.Log("퍼즐 실패");
                    clearAll();
                    ammoManage.isQTEActive = false;
                    PanelTF.SetActive(false);
                    
                    yield break;


                }

            }
            yield return null;
        }
    }

    public IEnumerator GenSQ(int count)                  //select SQ                   
    {
        if (ammoManage.isQTEActive) yield break;
        ammoManage.isQTEActive = true;

        PanelTF.SetActive(true);
        ArrowList.Clear();
        arrowPanel.GetComponent<HorizontalLayoutGroup>().enabled = true; 


        for (int i = 0; i < count; i++)
        {
            int dir = Random.Range(0, 4);
            KeyCode key = KeyCode.UpArrow;
            Sprite chosenSprite = UpAr;

            switch (dir)
            {
                case 0: key = KeyCode.UpArrow; chosenSprite = UpAr; break;
                case 1: key = KeyCode.DownArrow; chosenSprite = DownAr; break;
                case 2: key = KeyCode.LeftArrow; chosenSprite = LeftAr; break;           //Selected list ㅡ> Sprite List
                case 3: key = KeyCode.RightArrow; chosenSprite = RightAr; break;
            }

            ArrowList.Add(key);

            GameObject arrow = Instantiate(ArPre, arrowPanel);

            RectTransform rt = arrow.GetComponent<RectTransform>();      //chosen RectTransform arrange
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;

            arrow.GetComponent<Image>().sprite = chosenSprite;

            ArrowObject.Add(arrow);

            yield return null;

        }
        arrowPanel.GetComponent<HorizontalLayoutGroup>().enabled = false;   //Horizon unable
    }

    public void clearAll()
    {
        foreach (var arrow in ArrowObject)       //Clear all List and Sprite 
        {
            Destroy(arrow);
        }
        ArrowObject.Clear();
        ArrowList.Clear();
        //inputQTE = false;     
    }
}