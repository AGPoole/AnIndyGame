using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
    public int damage = 30;

    Health health;
    protected Rigidbody2D rb2d;
    protected SpriteRenderer srend;
    protected Animator anim;
    protected Collider2D col;

    protected virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        srend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            if (c.isTrigger)
                col = c;
        }
    }

    protected float getHealth() {
        return health.health;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null && other.GetComponent<Enemy>() == null)
            health.Damage(damage);
    }
}
