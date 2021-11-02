using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 8f;
    public Rigidbody2D rb;

    public bool IsMove { get; private set; }
    void FixedUpdate()
    {
        UpdatePlayerPosition();
    }

    void UpdatePlayerPosition()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 destination = Vector2.zero;

        destination += (Vector2.right * horizontal * speed * Time.deltaTime);
        destination += (Vector2.up * vertical * speed * Time.deltaTime);
        rb.MovePosition((Vector2)transform.parent.position + destination);

        IsMove = (destination == Vector2.zero) ? false : true;
    }
}
