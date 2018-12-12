using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour {

    public Transform cam;
    public Transform[] backgrounds;

    private float[] parralaxScale;

    public float smotthing = 1f;

    private Vector3 previousCamPos;

    public Transform player;

    private void Awake()
    {

        cam = Camera.main.transform;

    }


    void Start () {

        previousCamPos = cam.position;

        parralaxScale = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parralaxScale[i] = backgrounds[i].position.z * -1f;


        }

	}
	
	
	void Update () {

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parralaxScale[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smotthing * Time.deltaTime);
        }

        previousCamPos = cam.position;      

	}
}
