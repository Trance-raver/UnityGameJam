using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini_gun : MonoBehaviour {

    public Weapon defaultGun = new Weapon();
    public bool holdingThisGun = false;

    private void Start()
    {
        
        defaultGun.rb = GetComponent<Rigidbody2D>();
        defaultGun.fallCollider = GetComponent<CapsuleCollider2D>();

        defaultGun.shootPoint = transform.GetChild(0).gameObject;
        defaultGun.rateOfFire = 0;

    }


}
