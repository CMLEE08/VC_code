using UnityEngine;

public class Bull_PlayerLine : MonoBehaviour
{
    public Transform pointA;        //Start
    public Transform pointB;        //End

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;

        // 위치 계산
        Vector3 dir = pointB.position - pointA.position;
        float distance = dir.magnitude;

        transform.position = pointA.position + dir / 2f; 
        transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        transform.localScale = new Vector3(distance, sr.transform.localScale.y, 1);     //transform all
    }
}