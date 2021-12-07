using UnityEngine;
using System.Collections;

public class ZombieHealth : Health
{

    protected override void OnDeath()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 1500, 0));
        StartCoroutine(die());

        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }

        GetComponent<Animator>().enabled = false;
        GetComponent<ZombieEnemy>().enabled = false;
        DropItem();
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(5);
        base.OnDeath();
    }
}
