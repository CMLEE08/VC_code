using UnityEngine;

public class DifficultDebug : MonoBehaviour
{
    void Start()
    {
                Debug.Log("현재 난이도 : " + GameManager.Instance.Difficulty);
    }
}
