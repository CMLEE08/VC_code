using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameSystem : MonoBehaviour
{
    [Header("List")]
    public List<GameObject> EnemyList;
    public List<Vector3> positionList;
    [Header("Image")]
    public Image Time1;
    public Image Time2;
    //public List<GameObject> HP_List;
    [Header("System_int")]
    public int PlayerMod = 1;
    public int KCount = 0;
    public int Vkill = 0;
    public int Bkill = 0; 
    public int Fkill = 0;
    public int CorrectC = 0;
    public int InCorrectC = 0;
    public TMP_Text timeUI;  
    public float timer = 0f;
    public float currentTime = 6f;
    
    [Header("bools")]
    public bool isOver = false;
    public bool PuzzleT = true;
    [Header("Other Scripts")]
    public AmmoManage ammoManage;
    
    /*
    public float SpawnTime = 2.5f;
    public int PlayerHPs = 2;
    */
    void Start()
    {
        timer = 0f;
        currentTime = 6f;
        /*
        if (GameManager.Instance.Difficulty == "Hard")
        {

            Debug.Log("하드 모드 값 변경 완료");
            SpawnTime = SpawnTime - 1f;
            HP_List[PlayerHPs].SetActive(false);
            PlayerHPs = PlayerHPs - 1;

        }
        */
        StartCoroutine(SpawnEnemyCoroutine());

    }
    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        Uptime();
        if (PuzzleT)
        {
            currentTime -= Time.unscaledDeltaTime;
            Time1.fillAmount = currentTime / 6;
            Time2.fillAmount = currentTime / 6;
            if (currentTime <= 0)
            {
                PuzzleT = false;
                ammoManage.isTime = false;
            }
            
        }
    }
    void Uptime()
    {
        int min = (int)(timer / 60);
        int sec = (int)(timer % 60);

        timeUI.text = $"{min:00} : {sec:00}";
    }

    public string ReTime()
    {
        int min = (int)(timer / 60);
        int sec = (int)(timer % 60);

        return $"{min:00} : {sec:00}";
    }

    void SpawnE()
    {
        GameObject enemy = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], positionList[Random.Range(0, positionList.Count)], Quaternion.identity);

        enemy.GetComponent<EnemyMove>().enabled = true;
        Debug.Log("복제 완료, Enemy 태그 :" + enemy.tag);
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            SpawnE();  // 적 생성
            float randomDelay = Random.Range(0.5f, 4.5f);
            yield return new WaitForSeconds(randomDelay);
        }
    }
}