using UnityEngine;
using System.Collections;

public class NativeHealth : Health {
    protected override void OnDeath()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 2500, 0));
        StartCoroutine(die());

        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }

        GetComponent<Animator>().enabled = false;
        if (GetComponent<Enemy>() != null)
        {
            GetComponent<Enemy>().enabled = false;
            GetComponent<Enemy>().StopAllCoroutines();
        }
        DropItem();
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(5);
        base.OnDeath();
    }
}
