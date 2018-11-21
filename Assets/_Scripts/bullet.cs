using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {


    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public Sprite startSprite;
    public Sprite endSprite;

    public float spd = 10f;
    public float spriteChangeTime;
    public LayerMask whatIsSolid;

	void Start () {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(changesprite());

        

	}

    private void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, 1.15f,whatIsSolid);

        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {

                Debug.Log("EnemyHit");

            }

            else
            {
                Debug.Log("Hit Solid");
            }

            Destroy(this.gameObject);

        }

    }

    void FixedUpdate () {

        rb.velocity = transform.right * spd * 100 * Time.fixedDeltaTime;

	}

    IEnumerator changesprite()
    {
        transform.localScale = new Vector3(4f, 4f, 0);
        sr.sprite = startSprite;
        
        yield return new WaitForSeconds(spriteChangeTime);

        transform.localScale = new Vector3(3, 2.5f, 0);
        sr.sprite = endSprite;


    }
}
