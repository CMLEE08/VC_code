using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
public class Result : MonoBehaviour
{
    public Animator anim;

    public GameSystem gameSystem;
    public TMP_Text resultText;

    public float finaltime;

    public float disDur = 1.2f;

    public void EndResult()
    {
        Time.timeScale = 0f;
        anim.Play("Result");
        StartCoroutine(CountRe());
    }

    public void ReBttn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void HomeBttn()
    {
        SceneManager.LoadScene("StartImage");
        Time.timeScale = 1f;
    }

    IEnumerator CountRe()
    {
        yield return new WaitForSecondsRealtime(1.8f);
        finaltime = gameSystem.timer;
        float t = 0f;
        float current = 0f;

        while (t < disDur)
        {
            t += Time.unscaledDeltaTime;   // TimeScale = 0 상태에서의 unscaled 사용
            float percent = t / disDur;

            current = Mathf.Lerp(0f, finaltime, percent);

            UpCountText(current);

            yield return null;
        }

        // 마지막에 정확히 최종값으로 고정
        UpCountText(finaltime);
    }

    void UpCountText(float timeValue)
    {
        TimeSpan ts = TimeSpan.FromSeconds(timeValue);
        resultText.text = $"{ts.Minutes:00} : {ts.Seconds:00}";
    }
}
