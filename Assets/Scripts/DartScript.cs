using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class DartScript : MonoBehaviour {
    public int damage = 30;
    public GameObject particles;

    public float speed;

    public AudioSource spawnSound;

    void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        rb2d.velocity = speed * transform.right;

        StartCoroutine(PlaySound());

    }

    IEnumerator PlaySound()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (GetComponent<SpriteRenderer>().isVisible)
            spawnSound.Play();
    }

    bool shot = false;

    public AudioSource hitSound;

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.isTrigger && hit.GetComponent<Hammer>() == null && hit.tag != "Enemy")
            return;
        if (shot)
            return;
        shot = false;
        Health health = hit.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.Damage(damage);
        }
        if (GetComponent<SpriteRenderer>().isVisible)
            hitSound.Play();
        GetComponent<Collider2D>().isTrigger = false;
        Destroy((GameObject)Instantiate(particles, transform.position, transform.rotation), 5);
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject, 0.2f);

        //GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnWillRenderObject()
    {

    }
}
