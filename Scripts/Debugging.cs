using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Debugging : MonoBehaviour
{
    public Text XI;
    public Text YI;
    public Text ZI;

    public bool XB = false;
    public bool YB = false;
    public bool ZB = false;



    void Start()
    {
        XI.text = "0";
        YI.text = "0";
        ZI.text = "0";

        
    }

    // Update is called once per frame
    void Update()
    {
        if (XB == true)
        {
            XI.text = "1";
            StartCoroutine(ChangeX());
        }

    }

    public IEnumerator ChangeX()
    {
        if (XB == true)
        {
            XI.text = "0";
            XB = false;

            yield break;
        }

        yield return new WaitForSeconds(.1f);
    }  
}
