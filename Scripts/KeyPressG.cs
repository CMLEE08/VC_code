using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

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
    public GameSystem gameSystem;



    public List<KeyCode> ArrowList = new List<KeyCode>();
    public List<GameObject> ArrowObject = new List<GameObject>();

    void Start()
    {
        arrowPanel.GetComponent<HorizontalLayoutGroup>().enabled = true;
        PanelTF.SetActive(false);
    }


    public IEnumerator InputSQ()                                  // QTE 실행
    {

        while (ArrowList.Count > 0)
        {
            if (ammoManage.isTime == false)
            {
                Debug.Log("퍼즐 실패");
                ammoManage.Times.SetActive(false);
                clearAll();
                ammoManage.isQTEActive = false;
                PanelTF.SetActive(false);
                gameSystem.InCorrectC += 1;
                Time.timeScale = 1f;
                yield break;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||         //플레이어 키 입력
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
                        ammoManage.Times.SetActive(false);
                        ArrowObject.Clear();
                        ammoManage.isQTEActive = false;
                        bullSupply.BullSupp = true;
                        PanelTF.SetActive(false);
                        gameSystem.CorrectC += 1;
                        Time.timeScale = 1f;
                        


                        yield break;

                    }

                }
                else
                {
                    Debug.Log("퍼즐 실패");
                    ammoManage.Times.SetActive(false);
                    clearAll();
                    ammoManage.isQTEActive = false;
                    PanelTF.SetActive(false);
                    gameSystem.InCorrectC += 1;
                    Time.timeScale = 1f;
                    yield break;


                }

            }
            yield return null;
        }
    }

    public IEnumerator GenSQ(int count)                  //SQ 선택                   
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
                case 2: key = KeyCode.LeftArrow; chosenSprite = LeftAr; break;           //리스트를 케이스를 통해 이미지로 변환
                case 3: key = KeyCode.RightArrow; chosenSprite = RightAr; break;
            }

            ArrowList.Add(key);

            GameObject arrow = Instantiate(ArPre, arrowPanel);

            RectTransform rt = arrow.GetComponent<RectTransform>();     
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;

            arrow.GetComponent<Image>().sprite = chosenSprite;

            ArrowObject.Add(arrow);

            yield return null;

        }
        arrowPanel.GetComponent<HorizontalLayoutGroup>().enabled = false; 
    }

    public void clearAll()
    {
        foreach (var arrow in ArrowObject)       //초기화
        {
            Destroy(arrow);
        }
        ArrowObject.Clear();
        ArrowList.Clear();
        //inputQTE = false;     
    }
}