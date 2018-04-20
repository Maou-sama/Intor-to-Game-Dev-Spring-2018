using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Collider2D[] colliders;

    public Transform[] groundChecks;
    public LayerMask groundLayer;
    public BoxCollider2D c2d;
    public Animator anim;

    public bool dead;
    public AudioClip jumpSound;
    public float speed;
    public float jumpForce;
    public bool onGround;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        c2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        onGround = false;
        dead = false;
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
       
        
        //Change to crouch animation if pressed C
        if (onGround)
        {
            anim.SetBool("Jump", false);
        }

        else
        {
            anim.SetBool("Jump", true);
        }

        rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb2d.velocity.y);

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

    public void Die()
    {
        if (!dead)
        {
            sr.enabled = false;
            Camera.main.gameObject.GetComponent<ShakeScreen>().Screenshake(0.8f, 1.6f);
            StartCoroutine(Restart());
            dead = true;
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }
    
}
