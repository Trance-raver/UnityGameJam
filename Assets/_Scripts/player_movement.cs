using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_movement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    public gun_aim aimer;
    public GameObject dropWeaponPoint;

    public float spd;  //Move Spd
    public float backSpd;
    private bool movingBack = true;
    private bool isShooting = false;

    //Jump Stuff
    public GameObject jumpPoint;
    public float jumpSpd;
    public float fallMultplier = 6.5f;
    public float lowJumpMultplier = 6f;
    public float jumpPointRadius;
    public LayerMask whatIsGround;

    private float horizontalMove;
    private bool grounded;
    private bool jump = false;
    private Vector3 startGunDropPoint;

    //Land & jump particels stuff
    private bool spawnParticle = false;
    public GameObject landedDust;
    public GameObject jumpDust;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        aimer = GetComponentInChildren<gun_aim>();

        StartCoroutine(changeIdelStates());

        backSpd = spd - 3.3f;

    }


    void Update()
    {
        Debug.Log(isShooting);
        horizontalMove = Input.GetAxis("Horizontal");

        grounded = Physics2D.OverlapCircle(jumpPoint.transform.position, jumpPointRadius, whatIsGround);

        anim.SetFloat("move", Mathf.Abs(horizontalMove));
        anim.SetBool("grounded", grounded);

        if (Input.GetKeyDown(KeyCode.Space) && grounded) //checking if on ground 
        {

            jump = true;
            GameObject dust = Instantiate(jumpDust, jumpPoint.transform.position, Quaternion.identity);
            Destroy(dust, 1f);

        }

        if (aimer.holdingWeapon)
        {

            if (aimer.rotz <= 90) // Flipping player according to mouse pos
            {

                sr.flipX = false;
                dropWeaponPoint.transform.localPosition = new Vector3(1.031f, -0.219f, 0);

            }

            else if (aimer.rotz > 90)
            {
                sr.flipX = true;
                dropWeaponPoint.transform.localPosition = new Vector3(-1.031f, -0.219f, 0);
            }


            if ((horizontalMove > 0 && aimer.rotz > 90) || (horizontalMove < 0 && aimer.rotz < 90))
            {
                movingBack = false;
            }

            else
            {
                movingBack = true;
            }


            if (Input.GetMouseButton(0))
            {
                isShooting = true;

            }

            else if(Input.GetMouseButtonUp(0))
            {
                isShooting = false;
            }

        }

        else
        {
            if (horizontalMove > 0) // Fliping player according to keys hold
            {
                sr.flipX = false;
            }

            else if (horizontalMove < 0)
            {
                sr.flipX = true;
            }
        }

        if(grounded)
        {
            if (spawnParticle)
            {
                GameObject go = Instantiate(landedDust, jumpPoint.transform.position, Quaternion.identity);
                spawnParticle = false;
                Destroy(go, 1f);               
            }
        }

        else
        {
            spawnParticle = true;
        }


    }

    private void FixedUpdate()
    {
        if (movingBack)
        {
            rb.velocity = new Vector2(horizontalMove * spd * 100 * Time.fixedDeltaTime, rb.velocity.y);
        }

        else
        {
            if (isShooting)
            {
                rb.velocity = new Vector2(horizontalMove * backSpd * 100 * Time.fixedDeltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(horizontalMove * spd * 100 * Time.fixedDeltaTime, rb.velocity.y);
            }
        }

        // Jump Stuff
        if (jump)
        {
            jump = false;
            jumpNow(jumpSpd);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultplier - 1) * Time.fixedDeltaTime;
        }

        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultplier - 1) * Time.fixedDeltaTime;
        }
        //Jump Stuff Ends

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

    IEnumerator changeIdelStates()
    {
        while (true)
        {
            anim.SetBool("idel_1", false);

            int animDecider = Random.Range(0, 5);
            yield return new WaitForSeconds(2f);
            if (animDecider == 4)
            {
                anim.SetBool("idel_1", true);
                yield return new WaitForSeconds(4.5f);
            }
        }
    }
}
