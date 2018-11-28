using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Gun : MonoBehaviour {

    public Weapon defaultGun = new Weapon();
    public bool holdingThisGun = false;
    public bool doBloom;
    public float accuracy;
    public Sprite idelSprite;
    public Sprite holdingSprite;

    //reload gun Stuff
    public int reloadAfterBullets;
    public float reloadTime;
    private int bulletsLefttoReload;
    [HideInInspector]
    public bool reloading = false;
    private bool startedReloading;

    //CameraShake
    public float magnitude = 0.7f;
    public float roughness = 2.5f;
    public float fadeInTime = .1f;
    public float fadeOutTime = 1.3f;

    private void Start()
    {
        defaultGun.sr = GetComponent<SpriteRenderer>();
        defaultGun.rb = GetComponent<Rigidbody2D>();
        defaultGun.fallCollider = GetComponent<CapsuleCollider2D>();

        defaultGun.shootPoint = transform.GetChild(0).gameObject;
        defaultGun.rateOfFire = 0;
        bulletsLefttoReload = reloadAfterBullets;

        
    }

    private void Update()
    {
        
        if (holdingThisGun)
        {
            defaultGun.sr.sprite = holdingSprite;
            defaultGun.sr.sortingOrder = 2;
                 
            if (Input.GetMouseButton(0))
            {
                if (!reloading)
                {
                    
                    if (defaultGun.rateOfFire <= 0)
                    {                       
                        CameraShaker.Instance.ShakeOnce(magnitude,roughness,fadeInTime,fadeOutTime);
                        if (doBloom)
                        {
                            Instantiate(defaultGun.bulletPrefab, defaultGun.shootPoint.transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(-accuracy, accuracy)));
                            bulletsLefttoReload--;
                        }

                        else
                        {
                            Instantiate(defaultGun.bulletPrefab, defaultGun.shootPoint.transform.position, transform.rotation);
                            bulletsLefttoReload--;
                        }

                        defaultGun.rateOfFire = defaultGun.startRateofFire;
                    }

                    if(bulletsLefttoReload <= 0 && !startedReloading)
                    {
                        reloading = true;
                        startedReloading = true;
                        StartCoroutine(reload());                      
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if(bulletsLefttoReload < reloadAfterBullets)
                {
                    reloading = true;
                    startedReloading = true;
                    StartCoroutine(reload());

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

    IEnumerator reload()
    {
        startedReloading = true;
        yield return new WaitForSeconds(reloadTime);       
        bulletsLefttoReload = reloadAfterBullets;
        startedReloading = false;
        reloading = false;
    }

}
