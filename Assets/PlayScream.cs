using UnityEngine;
using System.Collections;

public class PlayScream : MonoBehaviour {
    public AudioSource sound;

    public GameObject target;

    bool used = false;
	
	// Update is called once per frame
	void Update () {
	    if(!used && Vector2.Distance(transform.position, target.transform.position) > 10f)
        {
            used = true;
            sound.Play();
        }
	}
}
