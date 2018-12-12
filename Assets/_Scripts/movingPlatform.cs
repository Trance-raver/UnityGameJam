using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour {

    public float moveSpd;
    public Transform[] movePoints;
    private int index;

	void Start () {
		
	}
	

	void Update () {

        transform.position = Vector2.MoveTowards(transform.position, movePoints[index].position, moveSpd * 10 * Time.deltaTime);

        if(Vector2.Distance(transform.position, movePoints[index].position) <= 0.2f)
        {

            index++;
            if(index >= movePoints.Length)
            {
                index = 0;
            }

        }


	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {


            collision.gameObject.transform.SetParent(transform);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }

    }
}
