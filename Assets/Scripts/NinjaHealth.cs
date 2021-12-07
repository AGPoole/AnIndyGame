using UnityEngine;
using System.Collections;

public class NinjaHealth : Health
{

    protected override void OnDeath()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 1500, 0));
        GetComponent<Rigidbody2D>().gravityScale = 0.3f;
        StartCoroutine(die());

        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }

        GetComponent<Animator>().SetTrigger("Dead");
        GetComponent<Animator>().enabled = false;
        GetComponent<NinjaEnemy>().enabled = false;
        DropItem();
        base.OnDeath();
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(5);
        base.OnDeath();
    }
}
