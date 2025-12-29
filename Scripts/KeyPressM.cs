using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class KeyPressM : MonoBehaviour
{
    private int SyMod;
    public Text problems_1;
    public Text problems_2;
    public Text Answers;

    public GameObject Panel;

    private int prob1, prob2, answerM;

    private string NowInput = "";

    public List<GameObject> MathSy;


    [Header("Other Scrpt")]
    public AmmoManage ammoManage;
    public BullSupply bullSupply;

    public GameSystem gameSystem;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ClearM();
    }

    // Update is called once per frame
    void Update()
    {
        if (ammoManage.isQTEActive)
            inputAnswer();
        
    }

    public void StartMP()
    {
        ammoManage.isQTEActive = true;
        GenMP();
        Panel.SetActive(true);
    }
    public void GenMP()
    {

        int probMod = Random.Range(0, 3);      // 0 = + , 1 = - , 2 = *

        switch (probMod)
        {
            case 0:
                ChangeSym(0);
                prob1 = Random.Range(1, 100);
                prob2 = Random.Range(1, 100);

                answerM = prob1 + prob2;

                break;

            case 1:
                ChangeSym(1);
                prob1 = Random.Range(1, 100);
                prob2 = Random.Range(1, prob1);

                answerM = prob1 - prob2;

                break;

            case 2:
                ChangeSym(2);
                prob1 = Random.Range(1, 100);
                prob2 = Random.Range(1, 10);

                answerM = prob1 * prob2;

                break;
        }
        problems_1.text = $"{prob1}";
        problems_2.text = $"{prob2}";
    }

    public void inputAnswer()
    {
        if (ammoManage.isTime == false)
        {
            ammoManage.Times.SetActive(false);
            ammoManage.isQTEActive = false;
            gameSystem.InCorrectC += 1;
            Time.timeScale = 1f;
            ClearM();
        }

        foreach (char c in Input.inputString)
        {
            if (char.IsDigit(c))
            {
                NowInput += c;
                UpdateUI();
            }
            else if (c == '\b')                                 //Backspace
            {
                if (NowInput.Length > 0)
                    NowInput = NowInput.Substring(0, NowInput.Length - 1);
                UpdateUI();
            }
            else if (c == '\r')                                 //Enter
            {
                CheckA();
            }

        }
    }

    public void CheckA()
    {
        if (int.TryParse(NowInput, out int userAnswer))
        {
            if (userAnswer == answerM)
            {
                ammoManage.Times.SetActive(false);
                Debug.Log("Correct");
                bullSupply.BullSupp = true;
                ammoManage.isQTEActive = false;
                gameSystem.CorrectC += 1;
                Time.timeScale = 1f;
                ClearM();
            }
            else
            {
                ammoManage.Times.SetActive(false);
                Debug.Log("Answer Incorrect");
                ammoManage.isQTEActive = false;
                gameSystem.InCorrectC += 1;
                Time.timeScale = 1f;
                ClearM();
            }
        }
        else
        {
            ammoManage.isQTEActive = false;
            ClearM();
            Debug.Log("Something is error");
        }
    }

    public void UpdateUI()
    {
        Answers.text = NowInput;
    }

    public void ClearM()
    {
        problems_1.text = "";
        Answers.text = "";
        Panel.SetActive(false);
        NowInput = "";
    }

    public void ChangeSym(int SyMod)
    {
        switch (SyMod)
        {
            case 0:
                MathSy[0].SetActive(true);
                MathSy[1].SetActive(false);
                MathSy[2].SetActive(false);
                break;
            case 1:
                MathSy[0].SetActive(false);
                MathSy[1].SetActive(true);
                MathSy[2].SetActive(false);
                break;
            case 2:
                MathSy[0].SetActive(false);
                MathSy[1].SetActive(false);
                MathSy[2].SetActive(true);
                break;
        }
    }

}
