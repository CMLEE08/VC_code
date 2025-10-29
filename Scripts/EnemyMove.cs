using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Linq.Expressions;

public class EnemyMove : MonoBehaviour
{
    public int Mod = 0;
    public float speed = 2f;       
    private Transform target;
    public GameObject VB;
    public GameObject BB;
    public GameObject FB;
    public Debugging Compare;

    void Start()
    {
        if (this.CompareTag("Virus"))
        {
            Mod = 1;
            speed = 3f;   
        }

        if (this.CompareTag("Fungus"))           //To compare with Bull
        {
            Mod = 2;
        }
        if (this.CompareTag("Backteria"))
        {
            Mod = 3;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Finish");           //direction player?
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("BullV") || other.CompareTag("BullB") || other.CompareTag("BullF"))               //Change bull require
        {

            if (Mod == 1)
            {
                if (other.CompareTag("BullV"))
                {
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                    Compare.XB = true;
                }
                else
                {
                    Destroy(other.gameObject);
                }          

            }
            else if (Mod == 2)
            {
                if (other.CompareTag("BullF"))
                {
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                    Compare.XB = true;
                }
                else
                {
                    Destroy(other.gameObject);
                }          
            }
            else if (Mod == 3)
            {
                if (other.CompareTag("BullB"))
                {
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                    Compare.XB = true;
                }
                else
                {
                    Destroy(other.gameObject);
                }             
            }

        }
    }
}