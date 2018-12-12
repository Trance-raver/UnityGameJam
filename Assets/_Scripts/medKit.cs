using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medKit : MonoBehaviour {

    public int increaseHealthBy = 20;
    private bool once = true;
    private Vector3 pos;

    private void Start()
    {
        pos = transform.position;

    }

    private void Update()
    {

        transform.position = pos + new Vector3(0, Mathf.Sin(Time.time * 5f) * 0.3f, 0);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && once)
        {
            
            playerHealth ph = collision.gameObject.GetComponent<playerHealth>();
            
            if(ph.health < 100)
            {
                StartCoroutine(increaseHealth(ph));
                once = false;
                GetComponent<SpriteRenderer>().enabled = false;

            }

        }

    }

    IEnumerator increaseHealth(playerHealth ph)
    {
        for (int i = 0; i < increaseHealthBy; i++)
        {
            ph.health += 1;

            if (ph.health > 100)
            {
                ph.health = 100;
            }

            yield return new WaitForSeconds(0.01f);

        }

        Destroy(this.gameObject);

    }


}
