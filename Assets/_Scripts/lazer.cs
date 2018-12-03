using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour {

    public float lazer_On_for;
    public float lazer_Off_for;

    private Animator anim;
    private GameObject particles;


	void Start () {

        anim = GetComponentInChildren<Animator>();
        particles = gameObject.GetComponentInChildren<ParticleSystem>().gameObject;

        StartCoroutine(lazerOnOff());
	}


    IEnumerator lazerOnOff()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(lazer_Off_for);
            anim.SetBool("canPlay", true);
            anim.SetBool("turnOn", true);
            particles.SetActive(true);

            yield return new WaitForSecondsRealtime(lazer_On_for);

            anim.SetBool("turnOn", false);
            particles.SetActive(false);

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!collision.GetComponent<playerHealth>().playingHit)
            {
                StartCoroutine(collision.gameObject.GetComponent<playerHealth>().hitEffect(10f));

            }
        }

    }

}
