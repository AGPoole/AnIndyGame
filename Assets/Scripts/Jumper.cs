using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

    public float force = 80;

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rbody = other.GetComponent<Rigidbody2D>();

        if (rbody == null)
            return;

        {
            Vector3 v = rbody.velocity;
            v.y = 0;
            rbody.velocity = v;
        }

        rbody.AddForce(new Vector3(0, force, 0), ForceMode2D.Impulse);
    }

}
