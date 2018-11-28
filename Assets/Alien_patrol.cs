using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Alien_patrol : MonoBehaviour {

    private Animator anim;
    private SpriteRenderer sr;
    private SpriteMask sm;

    public hit_pause hitPause;

    public Transform[] wayPoints;
    private int index;
    private bool waiting = false;
    private bool playingHit = false;
    public float moveSpd;
    public float playerDamage;

    public GameObject[] DeadParticles;
    public float health = 10;
    

    void Start () {


        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sm = transform.GetChild(0).GetComponent<SpriteMask>();

        index = 0;

	}
	
	
	void Update () {

        if (!playingHit)
        {
            sm.sprite = sr.sprite;

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(wayPoints[index].position.x, transform.position.y, transform.position.z), moveSpd * 10 * Time.deltaTime);

            if (Vector3.Distance(transform.position, wayPoints[index].position) <= 0.5f)
            {
                if (!waiting)
                {
                    StartCoroutine(wait());
                }
            }
        }

	}

    IEnumerator wait()
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

    public IEnumerator gotHit()
    {
        if (!playingHit)
        {
            playingHit = true;
            CameraShaker.Instance.ShakeOnce(1, 3, .1f, 1);
            hitPause.freezeNow(0.05f);

            for (int i = 0; i < 2; i++)
            {
                sm.enabled = true;
                yield return new WaitForSeconds(0.08f);
                sm.enabled = false;
                yield return new WaitForSeconds(0.08f);
                playingHit = false;

            }
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.localScale.x > 0)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(gotHit());
            health -= 5;

            if(health <= 0)
            {
                Destroy(this.gameObject);
                for (int i = 0; i < DeadParticles.Length; i++)
                {
                 
                    GameObject go = Instantiate(DeadParticles[i], transform.position, Quaternion.identity);
                    int force = Random.Range(14000, 15000);                   
                    int sidedecider = Random.Range(0,1);

                    if (sidedecider == 0)
                    {
                        force *= -1;
                    }
                
                    go.GetComponent<Rigidbody2D>().AddForce(new Vector2(force, force),ForceMode2D.Force);

                }
            }
        }

    }
   

}
