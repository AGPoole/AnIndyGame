using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IndyHealth : Health {

    bool damageable = true;
    public Text healthText;

    public float absoluteMaxHealth;

    public AudioSource hitSound;

    void Update()
    {
        healthText.enabled = true;
        healthText.text = "Health - " + health;
    }

    public Vector2 damageForce;
    protected override void OnDamage(float damage, Vector2 force)
    {
        if (damageable)
        {
            if (force == Vector2.zero)
                force = damageForce;
            base.OnDamage(damage, force);
            StartCoroutine(Immunity());
            hitSound.Play();
        }

    }

    IEnumerator Immunity()
    {
        damageable = false;

        yield return new WaitForSeconds(0.3f);

        damageable = true;
    }

    public void IncreaseMaxHealth(int healthIncrease)
    {
        absoluteMaxHealth = maxHealth + 200;
        maxHealth += healthIncrease;
        if(maxHealth > absoluteMaxHealth)
        {
            maxHealth = absoluteMaxHealth;
        }

    }

    protected override void OnDeath()
    {
        SceneManager.LoadScene(1);
        base.OnDeath();
    }

    //public static void SetLevel(int lev)
    //{
    //    level = lev;
    //}

}
