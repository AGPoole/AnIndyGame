using UnityEngine;
using System.Collections;

public class HollowBlock : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
