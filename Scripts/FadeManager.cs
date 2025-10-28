using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;           
    public float fadeDuration = 1f;    //fade speed

    public static FadeManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);


            if (fadeImage != null)
            {
                Color c = fadeImage.color;
                c.a = 0f;
                fadeImage.color = c;
            }

            SceneManager.sceneLoaded += SceneL;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    private void SceneL(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(Fade(1f, 0f)); 
    }

    public void FadeT(string sceneName)
    {
        StartCoroutine(FadeRout(sceneName));
    }

    private IEnumerator FadeRout(string sceneName)
    {
        yield return StartCoroutine(Fade(0f, 1f));
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator Fade(float from, float to)
    {



        float time = 0f;
        Color c = fadeImage.color;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;              // 계산식이 어떻게 작동하는거지?
            c.a = Mathf.Lerp(from, to, t);
            fadeImage.color = c;

            time += Time.deltaTime;
            yield return null;
        }


        c.a = to;
        fadeImage.color = c;
    }
}
