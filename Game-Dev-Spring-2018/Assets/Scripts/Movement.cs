using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Collider2D[] colliders;
    private Animator anim;

    [Header("Player's Ground Check")]
    [SerializeField] private Transform[] groundChecks;
    [SerializeField] private LayerMask groundLayer;

    [Header("Player's Properties")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private bool onGround;
    private bool dead;

    // Use this for initialization
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        onGround = false;
        dead = false;
    }

    //Check if player is in contact with object designated as ground with fixed update
    private void FixedUpdate()
    {
        onGround = false;

        //Loop through each of the ground check on the player and if they collide with ground set onGround to true
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
    private void Update()
    {
        //Change to jump animation depends on whether player is on ground or not
        if (onGround)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }

        //Set velocity according to directional input
        rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb2d.velocity.y);

        //Change to move animation if velocity is not 0
        if(rb2d.velocity != new Vector2(0, 0))
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }

        //Only Jump if player is on ground
        //Jump using instantaneous force upward
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            Sound.me.PlaySound(jumpSound);
            anim.SetBool("Jump", true);
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        //Flip the sprite depends on the direction moving
        if(Input.GetAxis("Horizontal") < 0)
        {
            sr.flipX = true;
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            sr.flipX = false;
        }
    }

    //Die method that invoke whenever player touch enemy
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

    private IEnumerator Restart()
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
