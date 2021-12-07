using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatBossHealth : Health
{

    public Text healthText;

    float startHealth;

    static int numLeft = 2;

    void Start()
    {
        base.Start();
        startHealth = health;
        healthText.text = startHealth + " / " + startHealth;
        numLeft = 2;
    }

    protected override void OnDamage(float damage, Vector2 force)
    {
        base.OnDamage(damage, force);
        healthText.text = health + " / " + startHealth;
    }

    public GameObject enemies;
    public GameObject idol;

    bool dead = false;
    protected override void OnDeath()
    {
        if (!dead)
        {
            numLeft--;
            dead = true;
        }
        print(numLeft);
        if (numLeft != 0)
            enemies.SetActive(true);
        else
            Instantiate(idol, transform.position, transform.rotation);

        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 200, 0));
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0.3f;
        GetComponent<Animator>().enabled = false;
        if (GetComponent<BirdEnemy>() != null)
            GetComponent<BirdEnemy>().enabled = false;
        StartCoroutine(die());
        DropItem();
        GetComponent<BatBoss>().enabled = false;
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(5);
        base.OnDeath();
    }
}
