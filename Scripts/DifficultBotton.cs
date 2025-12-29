using UnityEngine;


public class DifficultBotton : MonoBehaviour
{

    public void SetNormal()
    {
        GameManager.Instance.Difficulty = "Normal";
        FadeManager.Instance.FadeT("GameScene");
    }

    public void SetHard()
    {
        GameManager.Instance.Difficulty = "Hard";
        FadeManager.Instance.FadeT("GameScene");
    }
}
