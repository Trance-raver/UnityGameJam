using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {


    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public Sprite startSprite;
    public Sprite endSprite;

    public GameObject sparks;

    public float spd = 10f;
    public float bulletDamage;
    public float spriteChangeTime;
    public LayerMask whatIsSolid;

	void Start () {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(changesprite());

        

	}


    void FixedUpdate () {

        rb.velocity = transform.right * spd * 100 * Time.fixedDeltaTime;

	}

    IEnumerator changesprite()
    {
        //transform.localScale = new Vector3(4f, 4f, 0);
        sr.sprite = startSprite;
        
        yield return new WaitForSeconds(spriteChangeTime);

        //transform.localScale = new Vector3(3, 2.5f, 0);
        sr.sprite = endSprite;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy =  collision.GetComponent<Enemy>();
            enemy.health -= bulletDamage;
            enemy.hitByBullet();
            GameObject go = Instantiate(sparks, transform.position, transform.rotation);
            Destroy(go, 0.3f);
            Destroy(this.gameObject);
            
        }

        if(collision.gameObject.layer == 8)
        {
            GameObject go  = Instantiate(sparks, transform.position, transform.rotation);
            Destroy(go, 0.3f);
            Destroy(this.gameObject);
        }

    }


}
