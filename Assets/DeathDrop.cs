using UnityEngine;
using System.Collections;

public class DeathDrop : MonoBehaviour {

    GameObject indy;
	// Use this for initialization

    public static DeathDrop deathDrop;

    void Start()
    {
        deathDrop = this;
    }

    void Update()
    {
        if(indy == null)
            indy = IndyController.indy.gameObject;
        if (indy.transform.position.y < transform.position.y)
            indy.GetComponent<Health>().Damage(100);
    }
	
}
