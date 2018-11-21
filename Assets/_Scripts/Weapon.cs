using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon  {

    public string weaponName;

    public GameObject bulletPrefab;
    public GameObject shootPoint;

    public float rateOfFire;
    public float startRateofFire;
    public float damage;

    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public CapsuleCollider2D fallCollider;
    

}
