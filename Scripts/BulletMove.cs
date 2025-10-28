using Unity.VisualScripting;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 5f;       //Bull speed
    private Vector2 MDirect;     //Bull Direction

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); 
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            MDirect = direction;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;                //find BullSight and move
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Update()
    {
        transform.Translate(MDirect * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))           // Enemy compare and Wall
        {
            Destroy(gameObject);
        }
    }
}