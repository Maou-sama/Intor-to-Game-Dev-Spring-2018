﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Collider2D[] colliders;

    [Range(0, 1)] public float crouchSpeed = 0.5f;
    public Transform[] groundChecks;
    public LayerMask groundLayer;
    public BoxCollider2D c2d;
    public Animator anim;

    public AudioClip jumpSound;
    //public Sprite[] sprites;
    public float speed;
    public float jumpForce;
    public bool onGround;
    public bool crouching;

    private bool canCrouch = true;

    private Vector3 standColliderSize;
    private Vector3 crouchColliderSize;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        //standColliderSize = c2d.transform.localScale;
        //crouchColliderSize = new Vector3(c2d.transform.localScale.x * 2.0f, c2d.transform.localScale.y * 0.5f, c2d.transform.localScale.z);
        onGround = false;
        //sr.sprite = sprites[0];
        crouching = false;
    }

    void FixedUpdate()
    {
        onGround = false;
        foreach (Transform groundCheck in groundChecks)
        {
            colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.135f, groundLayer);
            if (colliders.Length >= 1)
            {
                onGround = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (crouching)
        {
            c2d.enabled = false;
        }

        else
        {
            c2d.enabled = true;
        }
        
        //Change to crouch animation if pressed C
        if (onGround)
        {
            anim.SetBool("Jump", false);
            if (Input.GetKey(KeyCode.C) && canCrouch)
            {
                StartCoroutine(CrouchTimer());
                //sr.sprite = sprites[1];
                crouching = true;
            }

            else
            {
                canCrouch = true;
                //sr.sprite = sprites[0];
                crouching = false;
            }
        }

        else
        {
            anim.SetBool("Jump", true);
            crouching = false;
        }

        rb2d.velocity = (crouching ? new Vector2(Input.GetAxis("Horizontal") * speed * crouchSpeed, rb2d.velocity.y) : new Vector2(Input.GetAxis("Horizontal") * speed, rb2d.velocity.y));

        if(rb2d.velocity != new Vector2(0, 0))
        {
            anim.SetBool("Moving", true);
        }

        else
        {
            anim.SetBool("Moving", false);
        }

        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            Sound.me.PlaySound(jumpSound);
            anim.SetBool("Jump", true);
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

    IEnumerator CrouchTimer()
    {
        yield return new WaitForSeconds(1.5f);
        if (canCrouch == true)
        {
            canCrouch = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            collision.gameObject.GetComponent<MovingPlatform>().startMoving = true;
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if (!crouching)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    
}
