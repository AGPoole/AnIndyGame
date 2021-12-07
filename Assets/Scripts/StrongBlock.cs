using UnityEngine;
using System.Collections;

public class StrongBlock : MonoBehaviour {
    Collider2D coll;
    //TODO: IMPROVE WHOLE SCRIPT!!!

    void Start()
    {
        foreach (Collider2D c in GetComponents<Collider2D>())
            if (c.isTrigger)
                coll = GetComponent<Collider2D>();
        //StartCoroutine(switchCollider());
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) {
        Health health = other.GetComponent<Health>();
        if (health == null)
            return;

        health.Damage(20);
	}

    IEnumerator noDamage()
    {
        coll.enabled = false;
        yield return new WaitForSeconds(2);
        coll.enabled = true;
    }
}
