using System.Collections;
using UnityEngine;

public class BlinkCon : MonoBehaviour
{
    public Animator TitleM;
    public Animator BttnM;
    public Animator BttnH;
    public GameObject PrssKey;

    public GameObject BttnUI;

    private bool keyPressed = false;

    void Start()                                //Animate
    {
        BttnUI.SetActive(false);
        StartCoroutine(Blink());
        Debug.Log("깜빡임 코루틴 시작");
    }

    void Update()
    {
        if (!keyPressed && Input.anyKeyDown)
        {
            BttnUI.SetActive(true);
            keyPressed = true;
            PrssKey.SetActive(false);
            Debug.Log("키입력 감지");
            StartCoroutine(PlayAinime());
        }
    }

    IEnumerator Blink()
    {
        while (!keyPressed)
        {
            PrssKey.SetActive(!PrssKey.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator PlayAinime()
    {
        BttnM.Play("BottonMovingN");
        BttnH.Play("BottonMovingH");
        TitleM.Play("TitleMoving");
        yield return new WaitForSeconds(2f);
    }
}