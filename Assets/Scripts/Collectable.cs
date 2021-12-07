using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
abstract public class Collectable : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            CollectedAction();
        }
    }

    protected virtual void CollectedAction()
    {
        SoundEffects.playCollect();
        Destroy(this.gameObject);
    }

}
