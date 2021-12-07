using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour {
    public float damage = 70;
    Animator anim;
    public bool hitting;

    public BoxCollider2D triggerCollider;
    //public BoxCollider2D normalCollider;

    bool upgraded = false;
    public float upgradedDamage;

    public GameObject particles;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void hit()
    {
        anim.SetTrigger("Hit");
        triggerCollider.enabled = true;
        //normalCollider.enabled = false;
    }

    public void SetHitting(bool b)
    {
        hitting = b;
        triggerCollider.enabled = b;
    }

    public float hitForce;
    public AudioSource hitSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();

        if (other.gameObject.tag != "Player" && hitting && health != null)
        {
            health.Damage(upgraded ? upgradedDamage : damage);
        }
        var rb = other.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null) {
            var force = (other.transform.position - IndyController.indy.transform.position).normalized;
            rb.AddForce(force * hitForce);
        }
        hitSound.Play();
        StartCoroutine(changeCollider());
     }

    IEnumerator changeCollider()
    {
        triggerCollider.isTrigger = false;
        yield return new WaitForSeconds(0.5f);
        triggerCollider.isTrigger = true;
    }

    public void Upgrade()
    {
        upgraded = true;
        particles.SetActive(true);
    }
}
