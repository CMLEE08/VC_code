using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
