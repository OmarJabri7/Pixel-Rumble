using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaFrog : MonoBehaviour
{
    public float playerSpeed;
    public float jumpSpeed;
    private float move;
    private Rigidbody2D rb;
    private bool isJumping;
    private bool isFalling;
    private float distanceToGround;
    private Animator anim;
    private CharacterController controller;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * playerSpeed,rb.velocity.y);
        if (move < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (move>0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed));
            isJumping = true; //Change for double Jumping
        }
        RunAnimations();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isJumping = false;
            isFalling = false;
        }
        else
        {
            isFalling = true;
        }
    }
    void RunAnimations()
    {
        anim.SetFloat("Movement", Mathf.Abs(move));
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFalling", isFalling);
    }
}
