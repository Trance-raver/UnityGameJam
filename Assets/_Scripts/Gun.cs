using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Weapon defaultGun = new Weapon();
    public bool holdingThisGun = false;
    public bool doBloom;
    public Sprite idelSprite;
    public Sprite holdingSprite;

    private hit_pause hitPause;

    private void Start()
    {
        defaultGun.sr = GetComponent<SpriteRenderer>();
        defaultGun.rb = GetComponent<Rigidbody2D>();
        defaultGun.fallCollider = GetComponent<CapsuleCollider2D>();

        defaultGun.shootPoint = transform.GetChild(0).gameObject;
        defaultGun.rateOfFire = 0;

        hitPause = FindObjectOfType<hit_pause>();
        
    }

    private void Update()
    {
        

        if (holdingThisGun)
        {
            defaultGun.sr.sprite = holdingSprite;
            defaultGun.sr.sortingOrder = 1;
                 
            if (Input.GetMouseButton(0))
            {
                if (defaultGun.rateOfFire <= 0)
                {
                    if (doBloom)
                    {

                        Instantiate(defaultGun.bulletPrefab, defaultGun.shootPoint.transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-10, 10)));
                        //hitPause.freezeNow();
                    }

                    else
                    {
                        Instantiate(defaultGun.bulletPrefab, defaultGun.shootPoint.transform.position, transform.rotation);
                        //hitPause.freezeNow();
                    }

                    defaultGun.rateOfFire = defaultGun.startRateofFire;
                }
            }

            if (defaultGun.rateOfFire > 0)
            {
                defaultGun.rateOfFire -= Time.deltaTime;
            }

        }

        else
        {
            defaultGun.sr.sprite = idelSprite;
            defaultGun.sr.sortingOrder = 2;
        }



    }

    public void dropGun()
    {       
        defaultGun.rb.bodyType = RigidbodyType2D.Dynamic;
        defaultGun.fallCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.layer == 8)
        {
            defaultGun.rb.bodyType = RigidbodyType2D.Kinematic;
            defaultGun.rb.velocity = Vector3.zero;
            defaultGun.fallCollider.enabled = false;
        }

    }

}
