using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public int playerSpeed = 10;
    public int playerJump;
    public int extraJumpsValue;
    public bool isGrounded;
    public dialogueTrigger trigger;
    public Animator dialogueAnimator;

    private int extraJumps;
    private bool facingRight = false;
    private bool isClimbing;
    private float moveX;
    private float inputVertical;
    private Rigidbody2D rb;

    public Quest quest;
    public playerStats stats;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");

        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }
            
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            Jump();
        }
        else if (Input.GetButtonUp("Jump") && extraJumps == 0 && isGrounded == true)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJump);
        }

        if (moveX != 0)
        {
            GetComponent<Animator>().SetBool("isMoving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isMoving", false);
        }


        //PLAYER DIRECTION
        if (moveX < 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == true)
        {
            FlipPlayer();
        }

        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }


    void Jump()
    {
        //JUMPING
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJump);
        extraJumps--;
        GetComponent<Animator>().SetBool("isJumping", true);
    }


    void FlipPlayer()
    {
        //FLIP SPRITE
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("Player has collided with " + col.collider.name);
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "spikes")
        {
            isGrounded = true;
        }
        if (col.gameObject.name.Equals("platform"))
        {
            this.transform.parent = col.transform;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "NPC") 
        {
            dialogueAnimator.SetBool("isOpen", false);
        }

    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("platform"))
        {
            this.transform.parent = null;
        }
        if (col.gameObject.tag == ("ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "ladder")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isClimbing = true;
               // GetComponent<Animator>().SetBool("isClimbing", true);
            }
            else if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown(KeyCode.D)))
            {
                rb.gravityScale = 1;
                isClimbing = false;
                //GetComponent<Animator>().SetBool("isClimbing", false);
            }
        }
        else if (col.gameObject.tag != "ladder" || col.gameObject.tag == null)
        {
            rb.gravityScale = 1;
            isClimbing = false;
            //GetComponent<Animator>().SetBool("isClimbing", false);
        }

        if (isClimbing == true)
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * playerSpeed);
            rb.gravityScale = 0;

        }
        else if (isClimbing == false)
        {
            rb.gravityScale = 1;
            //GetComponent<Animator>().SetBool("isClimbing", false);
        }

        if (col.gameObject.tag == "portal")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene("Arena");
            }
        }

        if (col.gameObject.tag == "portal2")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene("Main");
                //stats.Experience = PlayerPrefs.GetInt("Experience");
            }
        }

                if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }

    }
}
