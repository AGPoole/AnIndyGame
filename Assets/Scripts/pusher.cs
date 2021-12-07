using UnityEngine;
using System.Collections;

public class pusher : MonoBehaviour {
    public Vector3 force;
    Collider2D collider;
    //TODO: IMPROVE WHOLE SCRIPT!!!

    void Start()
    {
        foreach(Collider2D c in GetComponents<Collider2D>())
            if(c.isTrigger)
                collider = GetComponent<Collider2D>();
        //StartCoroutine(switchCollider());
    }

    IEnumerator switchCollider()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            collider.enabled = !collider.enabled;
        }
    }

	void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb2d = other.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.AddForce(force);
            print("INDY");
        }
    }
}
