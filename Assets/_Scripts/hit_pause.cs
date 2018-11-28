using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_pause : MonoBehaviour {


    public float duration = 0.05f;
    float pendingFreezeDuration = 0f;

    private bool isFrozen = false;

	
	void Update () {
		
        if(pendingFreezeDuration > 0 && !isFrozen)
        {

            StartCoroutine(doFreeze());

        }


	}

    public void freezeNow(float duration)
    {
        pendingFreezeDuration = duration;
    }

    IEnumerator doFreeze()
    {

        isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        pendingFreezeDuration = 0;
        isFrozen = false;

    }

}
