using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_movement : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    public GameObject jumpPoint;

    public float spd;
    public float jumpSpd;
    public float jumpPointRadius;
    public LayerMask whatIsGround;

    private float horizontalMove;
    private bool grounded;
    private bool jump = false;

	void Start () {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

	}
	

	void Update () {

        horizontalMove = Input.GetAxis("Horizontal");

        grounded = Physics2D.OverlapCircle(jumpPoint.transform.position, jumpPointRadius, whatIsGround);

        anim.SetFloat("move", Mathf.Abs(horizontalMove));
        anim.SetBool("grounded", grounded);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {

            jump = true;

        }

        if(horizontalMove > 0)
        {
            sr.flipX = false;
        }

        else if(horizontalMove < 0)
        {
            sr.flipX = true;
        }

	}

    private void FixedUpdate()
    {

        rb.velocity = new Vector2(horizontalMove * spd * 100 * Time.fixedDeltaTime, rb.velocity.y);

        if (jump)
        {
            jump = false;
            jumpNow(jumpSpd);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(jumpPoint.transform.position, jumpPointRadius);

    }

    private void jumpNow(float jumpForce)
    {

        rb.velocity = new Vector2(rb.velocity.x, jumpForce * 100 * Time.fixedDeltaTime);

    }
}
