using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    public int Mod = 0;                 
    public float moveStep = 0.5f;       
    public float moveDelay = 0.2f;   

    private float Health = 1f;         
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image HealthBack;

    private Transform target;
    public Debugging Compare;

    private bool isMoving = false;     

    void Start()
    {
        HealthBar.enabled = false;     
        HealthBack.enabled = false;

        // 적 타입 설정
        if (CompareTag("Virus")) { Mod = 1; moveStep = 0.6f; }
        if (CompareTag("Fungus")) { Mod = 2; }
        if (CompareTag("Backteria")) { Mod = 3; }

        GameObject player = GameObject.FindGameObjectWithTag("Finish");
        if (player != null) target = player.transform;
    }

    void Update()
    {
        if (!isMoving && target != null)
        {
            StartCoroutine(MoveStep());
        }
    }

    IEnumerator MoveStep()
    {
        isMoving = true;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveStep;

        yield return new WaitForSeconds(moveDelay);

        isMoving = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BullV") || other.CompareTag("BullB") || other.CompareTag("BullF"))
        {
            if (IsBullEffective(other.tag))
            {
                EnableHealthBar();
                HealthCheck();
                Compare.XB = true;
            }

            Destroy(other.gameObject);
        }
    }

    bool IsBullEffective(string bullTag)
    {
        return (Mod == 1 && bullTag == "BullV") ||
               (Mod == 2 && bullTag == "BullF") ||
               (Mod == 3 && bullTag == "BullB");
    }

    void EnableHealthBar()
    {
        if (!HealthBar.enabled)
            HealthBar.enabled = true;
            HealthBack.enabled = true;
    }

    void HealthCheck()
    {
        Health -= 0.5f;
        HealthBar.fillAmount = Health;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}