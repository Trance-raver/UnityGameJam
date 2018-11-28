using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class playerHealth : MonoBehaviour {

    public Image healthBar;
    private float maxHealth = 100f;
    public float health;

    private SpriteRenderer sr;
    private player_movement pm;
    public hit_pause hitPause;
    public SpriteMask sm;

    [HideInInspector]
    public bool playingHit = false;

    public void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        pm = GetComponent<player_movement>();
       
    }


    void Update () {

        healthBar.fillAmount = health / maxHealth;

        sm.sprite = sr.sprite;

        if (sr.flipX == false)
        {
            sm.gameObject.transform.localScale = new Vector2(1, 1);
        }

        else
        {
            sm.gameObject.transform.localScale = new Vector2(-1, 1);
        }

        if(health <= 0)
        {

            Debug.Log("dead");

        }

    }

    public IEnumerator hitEffect(float damage)
    {
        if (!playingHit)
        {
            playingHit = true;
            health -= damage;
            CameraShaker.Instance.ShakeOnce(2,3,.1f,1);
            hitPause.freezeNow(0.05f);
            healthBar.gameObject.GetComponent<Animator>().SetTrigger("hit");
            pm.knockBackCount = pm.knockBackLength;

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
