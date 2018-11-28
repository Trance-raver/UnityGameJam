using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_aim : MonoBehaviour
{

    private bool canGrabWeapon;
    private GameObject gunToGrab;
    [HideInInspector]
    public bool holdingWeapon = false;

    private Gun gun;
    public GameObject reloadingObj;

    [HideInInspector]
    public float rotz;

    public SpriteRenderer sr;

    void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotz);  


        if (holdingWeapon)
        {
            if (rotz > 90)
            {
                transform.GetChild(0).localScale = new Vector3(transform.GetChild(0).localScale.x, -1, 1);            
            }

            else if (rotz <= 90)
            {
                transform.GetChild(0).localScale = new Vector3(transform.GetChild(0).localScale.x, 1, 1);             
            }


            if (gun.reloading)
            {
                reloadingObj.SetActive(true);
            }

            else
            {
                reloadingObj.SetActive(false);
            }

            //reloading txt

        }

        if (canGrabWeapon) // trying to grab weapon
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                gunToGrab.transform.SetParent(transform);
                gunToGrab.transform.position = transform.position;
                gunToGrab.gameObject.GetComponent<Collider2D>().enabled = false;
                gunToGrab.GetComponent<Gun>().holdingThisGun = true;
                changeWeapon(gunToGrab);

            }
        }

    }


    public void changeWeapon(GameObject intractingWeapon)
    {
        holdingWeapon = true;
        gun = intractingWeapon.GetComponent<Gun>();
        dropWeapon();
        intractingWeapon.transform.SetParent(transform);
        intractingWeapon.transform.localScale = new Vector3(1, 1, 1);
        intractingWeapon.transform.rotation = transform.rotation;
        sr = intractingWeapon.GetComponent<SpriteRenderer>();

    }

    public void dropWeapon()
    {

        if (transform.childCount > 1)
        {

            GameObject child = transform.GetChild(0).gameObject;
            Gun childsGun = child.GetComponent<Gun>();
            childsGun.holdingThisGun = false;
            child.transform.rotation = Quaternion.identity;

            if (child.transform.localScale.y < 0) //flipping thing here
            {
                child.transform.localScale = new Vector3(-child.transform.localScale.x , -child.transform.localScale.y, 1);
            }

            child.transform.position = this.transform.parent.GetComponent<player_movement>().dropWeaponPoint.transform.position;
            child.gameObject.GetComponent<Collider2D>().enabled = true;
            childsGun.dropGun();
            child.transform.SetParent(null);
            //drop weapon 

        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Weapon"))
        {

            canGrabWeapon = true;            
            gunToGrab = collision.gameObject;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Weapon"))
        {
            canGrabWeapon = false;
            
        }

    }
}
