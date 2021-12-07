using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {

    public float damage = 50;

    Health health;

    Rigidbody2D rb2d;

    public Vector3 startForce;

    public bool destructor = false;

    Collider2D col;

    void Start()
    {
        health = GetComponent<Health>();
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.AddForce(startForce, ForceMode2D.Impulse);

        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            if (c.isTrigger)
                col = c;
        }
        if (!destructor)
            StartCoroutine(activateTrigger());
        else
            col.enabled = true;
    }

    IEnumerator activateTrigger()
    {
        const float WAIT = 0.25f;
        while (true)
        {
            yield return new WaitForSeconds(WAIT);
            if (alternateCol)
                col.enabled = !col.enabled;
            else
                col.enabled = false;
        }
    }

    float speed;
    float timeToDie = 1;

    bool alternateCol;

    void Update()
    {
        Vector3 velocity = rb2d.velocity;
        speed = velocity.magnitude;

        alternateCol = speed > 10;

        if(velocity.magnitude > 10)
        {
            timeToDie = Time.time + 5;
        }
        if (Time.time > timeToDie)
            health.Damage(200);

        if(transform.position.y < DeathDrop.deathDrop.transform.position.y)
        {
            GetComponent<Health>().Damage(500);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Health otherHealth = other.GetComponent<Health>();

        if (otherHealth == null)
            return;

        otherHealth.Damage(damage);
    }
}
