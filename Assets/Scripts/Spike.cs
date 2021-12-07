using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

    enum directions {up, down, left, right};

    Collider2D col;

    [Range(0f, 5f)]
    public float offTime = 1f;
    [Range(0f, 5f)]
    public float onTime = 0.5f;

    SpriteRenderer srend;

    public bool startOn = true;

    public bool alternating = false;

    bool on;

    void Start()
    {
        col = GetComponent<Collider2D>();
        if (alternating)
        {
            on = startOn;
            srend = GetComponent<SpriteRenderer>();
            srend.enabled = on;
            col.enabled = on;
            StartCoroutine(alternateSpikes());
        }

    }

    IEnumerator alternateSpikes()
    {
        yield return new WaitForSeconds(on ? onTime : offTime);
        on = !on;
        srend.enabled = on;
        col.enabled = on;
        StartCoroutine(alternateSpikes());
    }

    static float force = 70;
	void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health == null)
            return;
        health.Damage(30, transform.up * 70);
        //Rigidbody2D rbody = other.GetComponent<Rigidbody2D>();

        //if (rbody == null)
        //    return;

        //{
        //    Vector3 v = rbody.velocity;
        //    v.y = 0;
        //    rbody.velocity = v;
        //}

        //rbody.AddForce(transform.up * 70, ForceMode2D.Impulse);
        StartCoroutine(TurnColOnAndOff());
    }

    IEnumerator TurnColOnAndOff()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.5f);
        col.enabled = true;
        if (alternating && !on)
            col.enabled = false;
    }

}
