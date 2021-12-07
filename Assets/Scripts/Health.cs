using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public abstract class Health : MonoBehaviour {

    public float health = 100;
    public float maxHealth;

    public GameObject particles;

    SpriteRenderer srend;
    protected Rigidbody2D rb2d;
	
    protected virtual void Start()
    {
        maxHealth = health*1.5f;
        srend = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Damage(float damage, Vector2 force)
    {
        Assert.IsTrue(damage >= 0);

        OnDamage(damage, force);

        if(health <= 0)
        {
            OnDeath();
        }
    }

    public void Damage(float damage)
    {
        Damage(damage, Vector2.zero);
    }

    protected virtual void OnDamage(float damage, Vector2 force)
    {
        if(rb2d != null)
            rb2d.AddForce(force, ForceMode2D.Impulse);
        health -= damage;
    }

    public GameObject[] spawnable;

    [Range(0, 1)]
    public float dropOdds;

    bool hasDropped = false;

    protected void DropItem()
    {
        int i = spawnable.Length;

        if (Random.value < dropOdds && i != 0)
            Destroy(Instantiate(spawnable[Random.Range(0, i)], transform.position, transform.rotation) as GameObject, 10);

        hasDropped = true;
    }

    protected virtual void OnDeath()
    {
        if (GetComponent<Enemy>() != null)
            GetComponent<Enemy>().enabled = false;
        if(particles != null)
            Destroy((GameObject)Instantiate(particles, transform.position, transform.rotation), 5);

        if (GetComponent<Enemy>() != null)
        {
            GetComponent<Enemy>().enabled = false;
            GetComponent<Enemy>().StopAllCoroutines();
        }

        Destroy(this.gameObject);

        if (hasDropped)
            return;

        int i = spawnable.Length;

        if (Random.value < dropOdds && i != 0)
            Destroy(Instantiate(spawnable[Random.Range(0, i)], transform.position, transform.rotation) as GameObject, 5);

        if(srend != null && srend.isVisible)
            SoundEffects.playDeath();

    }

    public virtual void addHealth(float healthAddition)
    {
        health += healthAddition;
        if (health > maxHealth)
            health = maxHealth;
    }

}
