using UnityEngine;
using System.Collections;

public class SpiderHealth : Health {
    DistanceJoint2D dj2d;

    void Start()
    {
        dj2d = GetComponent<DistanceJoint2D>();
    }

    protected override void OnDeath()
    {
        dj2d.enabled = false;

        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<LineRenderer>().enabled = false;
        DropItem();
        StartCoroutine(die());
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(5);
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 500, 0));
        //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(5);
        base.OnDeath();
    }
}
