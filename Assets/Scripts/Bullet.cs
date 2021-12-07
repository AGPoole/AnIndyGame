using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject particles;

    public float speed;

    public float damage = 10;

    public Collider2D triggerCollider;
    // Use this for initialization

    static float force = 10;

    public AudioSource hitSound;

    bool shot = false;
    void Start () {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector3(speed * (IndyController.flipped ? -1 : 1), 0, 0);
        GetComponent<SpriteRenderer>().flipX = IndyController.flipped;

        IndyController.indy.GetComponent<Rigidbody2D>().AddForce(-1 * rbody.velocity * 10);

        Destroy(this.gameObject, 5);
	}

    void OnTriggerEnter2D (Collider2D hit)
    {
        if ((hit.isTrigger && hit.GetComponent<Enemy>() == null) && hit.GetComponent<Hammer>() == null && hit.tag != "Enemy")
            return;
        if (shot)
            return;
        shot = true;
        Health health = hit.gameObject.GetComponent<Health>();
        if(health != null)
        {
            health.Damage(damage);
        }
        hitSound.Play();
        triggerCollider.enabled = false;
        Destroy((GameObject) Instantiate(particles, transform.position, transform.rotation), 5f);
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject, 0.2f);

        //GetComponent<SpriteRenderer>().enabled = false;
    }

}
