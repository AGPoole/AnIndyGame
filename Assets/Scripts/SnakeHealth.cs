using UnityEngine;
using System.Collections;

public class SnakeHealth : Health {

    protected override void OnDeath()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 1500, 0));
        GetComponent<Rigidbody2D>().gravityScale = 0.3f;
        StartCoroutine(die());

        foreach(Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }

        GetComponent<Animator>().enabled = false;
        DropItem();
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(5);
        base.OnDeath();
    }
}
