using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Enemy : MonoBehaviour {

    public hit_pause hitPause;
    public float health;
    public float moveSpd;
    public bool playingHit = false;

    public GameObject[] DeadParticles;
    public GameObject explosionEffect;

    private SpriteMask sm;
    private SpriteRenderer sr;
    private audio_manager audio;



    private void Start()
    {
        sm = transform.GetChild(0).GetComponent<SpriteMask>();
        sr = GetComponent<SpriteRenderer>();
        hitPause = FindObjectOfType<hit_pause>();
        audio = FindObjectOfType<audio_manager>();

    }

    private void Update()
    {
        sm.sprite = sr.sprite;
    }

    public void hitByBullet()
    {

        StartCoroutine(gotHit());
        
        if (health <= 0)
        {
            Destroy(this.gameObject);
            audio.Play("Explosion");
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);

            for (int i = 0; i < DeadParticles.Length; i++)
            {

                Vector3 randomPos = new Vector3(Random.Range(transform.position.x - 5f, transform.position.x), transform.position.y, 0); 
                GameObject go = Instantiate(DeadParticles[i], randomPos, Quaternion.identity);
                int force = Random.Range(14, 20);
                int sidedecider = Random.Range(0, 1);

                if (sidedecider == 0)
                {
                    force *= -1;
                }

                //go.GetComponent<Rigidbody2D>().AddForce(new Vector2(force, force), ForceMode2D.Force);
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(force, force);
            }
        }
    }

    public IEnumerator gotHit()
    {
        if (!playingHit)
        {
            playingHit = true;
            audio.Play("EnemyHit");
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
}
