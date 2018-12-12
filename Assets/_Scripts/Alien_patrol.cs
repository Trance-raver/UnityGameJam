using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Alien_patrol : MonoBehaviour {

    private Animator anim;
   

    private Rigidbody2D rb;
    private Enemy enemy;
    private Transform player;


    public Transform[] wayPoints;
    private int index;
    private bool waiting = false;   

    public float playerDamage;

    

    

    void Start () {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        
        enemy = GetComponent<Enemy>();
        
        rb = GetComponent<Rigidbody2D>();

        index = 0;

	}
	
	
	void Update () {

        //if(Vector2.Distance(transform.position,player.position) >= 20f) {

            if (!enemy.playingHit)
            {

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(wayPoints[index].position.x, transform.position.y, transform.position.z), enemy.moveSpd * 10 * Time.deltaTime);

                if (Vector3.Distance(transform.position, wayPoints[index].position) <= 0.5f)
                {
                    if (!waiting)
                    {
                        StartCoroutine(wait());
                    }
                }

            }
        //}

        //else
        //{
            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), enemy.moveSpd * 10 * Time.deltaTime);

       // }

	}

    IEnumerator wait()
    {
        if (!enemy.playingHit)
        {
            waiting = true;

            anim.SetBool("idel", true);

            yield return new WaitForSeconds(2f);
            index++;
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);

            anim.SetBool("idel", false);

            if (index >= wayPoints.Length)
            {

                index = 0;

            }

            waiting = false;
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
 
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                collision.gameObject.GetComponent<player_movement>().knockBackFromRight = true;
            }
          

            else
            {
                collision.gameObject.GetComponent<player_movement>().knockBackFromRight = false;
            }

            StartCoroutine(collision.gameObject.GetComponent<playerHealth>().hitEffect(playerDamage));

        }

    }  

}
