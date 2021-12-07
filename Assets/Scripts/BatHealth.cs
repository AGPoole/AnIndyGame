using UnityEngine;
using System.Collections;

public class BatHealth : Health {

    protected override void OnDeath()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 200, 0));
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0.3f;
        GetComponent<Animator>().enabled = false;
        if (GetComponent<BirdEnemy>() != null)
            GetComponent<BirdEnemy>().enabled = false;
        StartCoroutine(die());
        DropItem();
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(5);
        base.OnDeath();
    }
}
