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

    private audio_manager audio;

    //reload gun Stuff
    public int reloadAfterBullets;
    public float reloadTime;
    private int bulletsLefttoReload;
    [HideInInspector]
    public bool reloading = false;
    private bool startedReloading;

    private Vector3 pos;

    //CameraShake
    public float magnitude = 0.7f;
    public float roughness = 2.5f;
    public float fadeInTime = .1f;
    public float fadeOutTime = 1.3f;

    public int gunNumberForAudio;

    private void Start()
    {
        defaultGun.sr = GetComponent<SpriteRenderer>();
        defaultGun.rb = GetComponent<Rigidbody2D>();
        defaultGun.fallCollider = GetComponent<CapsuleCollider2D>();

        defaultGun.shootPoint = transform.GetChild(0).gameObject;
        defaultGun.rateOfFire = 0;
        bulletsLefttoReload = reloadAfterBullets;

        audio = FindObjectOfType<audio_manager>();

        pos = transform.position;
    }

    private void Update()
    {
        
        if (holdingThisGun)
        {
            defaultGun.sr.sprite = holdingSprite;
            defaultGun.sr.sortingOrder = 2;
            Debug.Log(transform.tag + "_" + gunNumberForAudio);
                 
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
                            audio.Play(transform.tag + "_" + gunNumberForAudio);
                        }

                        else
                        {
                            Instantiate(defaultGun.bulletPrefab, defaultGun.shootPoint.transform.position, transform.rotation);
                            bulletsLefttoReload--;
                            audio.Play(transform.tag + "_" + gunNumberForAudio);
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
            transform.position = pos + new Vector3(0, Mathf.Sin(Time.time * 5f) * 0.3f , 0);

        }



    }

    public void dropGun()
    {       
        defaultGun.rb.bodyType = RigidbodyType2D.Dynamic;
        pos = transform.position;
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
