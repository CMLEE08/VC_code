using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSystem : MonoBehaviour
{
    public List<GameObject> EnemyList;
    public List<Vector3> positionList;

    public List<GameObject> HP_List;
    public int PlayerMod = 1;
    public float SpawnTime = 2.5f;
    public int PlayerHPs = 2;
    public GameObject PS_B;
    public GameObject PS_A;


    void Start()
    {

        

        PS_B.SetActive(true);
        PS_A.SetActive(false);
        
        if (GameManager.Instance.Difficulty == "Hard")
        {

            Debug.Log("하드 모드 값 변경 완료");
            SpawnTime = SpawnTime - 1f;
            HP_List[PlayerHPs].SetActive(false);
            PlayerHPs = PlayerHPs - 1;


        }
    
        InvokeRepeating("SpawnE", 3f, SpawnTime);


    }
    // Update is called once per frame
    void Update()
    {

    }

    void SpawnE()
    {
        GameObject enemy = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], positionList[Random.Range(0, positionList.Count)], Quaternion.identity);

        enemy.GetComponent<EnemyMove>().enabled = true;  
        Debug.Log("복제 완료, Enemy 태그 :" + enemy.tag);
    }
}
