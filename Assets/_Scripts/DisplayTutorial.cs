using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayTutorial : MonoBehaviour {

    private bool canGrabWeapon;
    private bool holdingWeapon;
    public Text text;
    private float horizontalmove;
    private bool check;
    private bool check1;
    private bool check2;

	// Use this for initialization
	void Start () {
        canGrabWeapon = GetComponentInChildren<gun_aim>();
        holdingWeapon = GetComponentInChildren<gun_aim>();
        check = true;
        check1 = true;
        check2 = true;
    }
	
	// Update is called once per frame
	void Update () {
        horizontalmove = Input.GetAxis("Horizontal");


        if(horizontalmove > 0 && check == true)
        {
            text.text = "Press space to jump";
            check = false;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && check1 == true)
        {
            text.text = "Press E to pick up gun";
            check1 = false;
        }
		else if(check2 == true && Input.GetKeyDown(KeyCode.E))
        {
            text.text = "Press LMB to shoot";
            check2 = false;
        }

        
	}
}
