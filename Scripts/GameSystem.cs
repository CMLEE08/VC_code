using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSystem : MonoBehaviour
{
    public List<GameObject> EnemyList;
    public List<Vector3> positionList;

    public List<GameObject> HP_List;
    public int PlayerMod = 1;

    //public float SpawnTime = 2.5f;
    public int PlayerHPs = 2;

    void Start()
    {
        //if (GameManager.Instance.Difficulty == "Hard")
        //{

        //    Debug.Log("하드 모드 값 변경 완료");
        //    SpawnTime = SpawnTime - 1f;
        //    HP_List[PlayerHPs].SetActive(false);
        //    PlayerHPs = PlayerHPs - 1;

        //}

        StartCoroutine(SpawnEnemyCoroutine());

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