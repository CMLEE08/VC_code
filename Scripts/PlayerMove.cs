using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSize = 1f;
    public float moveINT = 0.2f;

    private float moveTI = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveTI -= Time.fixedDeltaTime;

        Vector2 moveDir = Vector2.zero;

        if (Input.GetKey(KeyCode.D)) moveDir += Vector2.right;        //Bull transform
        if (Input.GetKey(KeyCode.A)) moveDir += Vector2.left;
        if (Input.GetKey(KeyCode.W)) moveDir += Vector2.up;
        if (Input.GetKey(KeyCode.S)) moveDir += Vector2.down;

        if (moveDir != Vector2.zero && moveTI <= 0f)
        {
            moveDir = moveDir.normalized;
            Vector2 newPos = rb.position + moveDir * moveSize;        //vector normalize
            rb.MovePosition(newPos);
            moveTI = moveINT;
        }
    }
}
