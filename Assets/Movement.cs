using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //declare 
    private float Horizontal;
    private float DashTimer;
    private float DashCooldown = 1.5f;
    public float DashForce;
    public float Speed;
    public float JumpForce;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    void Start()
    {
        //RigidBody
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //move
        Horizontal = Input.GetAxis ("Horizontal");
        rb.AddForce (new Vector2(Horizontal * Speed, 0));
        
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce (new Vector2(0, JumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }

        //dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && DashTimer <= 0)
        {
            rb.AddForce(new Vector2(Horizontal * DashForce, 0), ForceMode2D.Impulse);
            DashTimer = DashCooldown;
        }
        //dash_cd
        if (DashTimer >= 0)
        {
            DashTimer -= Time.deltaTime;
        }
    }

    // cek ground untuk jump
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}