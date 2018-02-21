﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    [Range(0, 1)] public float crouchSpeed = 0.5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public BoxCollider2D c2d;

    public Sprite[] sprites;
    public float speed;
    public float jumpForce;
    public bool onGround;
    public bool crouching;

    private Vector3 standColliderSize;
    private Vector3 crouchColliderSize;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c2d = GetComponent<BoxCollider2D>();
        standColliderSize = c2d.transform.localScale;
        crouchColliderSize = new Vector3(c2d.transform.localScale.x * 2.0f, c2d.transform.localScale.y * 0.5f, c2d.transform.localScale.z);
        onGround = false;
        sr.sprite = sprites[0];
        crouching = false;
    }

    void FixedUpdate()
    {
        onGround = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayer);
        if(colliders.Length >= 1)
        {
            onGround = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (crouching)
        {
            c2d.transform.localScale = crouchColliderSize;
        }

        else
        {
            c2d.transform.localScale = standColliderSize;
        }
        */
        //Change to crouch animation if pressed C
        if (onGround)
        {
            if (Input.GetKey(KeyCode.C))
            {
                sr.sprite = sprites[1];
                crouching = true;
            }

            else
            {
                sr.sprite = sprites[0];
                crouching = false;
            }
        }

        else
        {
            sr.sprite = sprites[2];
            crouching = false;
        }

        rb2d.velocity = (crouching ? new Vector2(Input.GetAxis("Horizontal") * speed * crouchSpeed, rb2d.velocity.y) : new Vector2(Input.GetAxis("Horizontal") * speed, rb2d.velocity.y));
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            sr.sprite = sprites[2];
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if(Input.GetAxis("Horizontal") < 0)
        {
            sr.flipX = true;
        }

        else if(Input.GetAxis("Horizontal") > 0)
        {
            sr.flipX = false;
        }
    }   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacles")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
