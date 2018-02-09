using System.Collections;
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

    public Sprite[] sprites;
    public float speed;
    public float jumpForce;
    public bool onGround;
    public bool crouching;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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
    }   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacles")
        {
            SceneManager.LoadScene(0);
        }
    }
}
