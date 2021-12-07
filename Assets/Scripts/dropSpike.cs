using UnityEngine;
using System.Collections;

public class dropSpike : MonoBehaviour {
    public float damage = 30f;
    public GameObject particles;

    float startTime;
    void Start()
    {
        startTime = Time.time;
    }

	void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Hammer>() != null)
            return;

        Health health = other.gameObject.GetComponent<Health>();
        if (health != null)
            health.Damage(damage);

        if (Time.time - startTime < 0.5f)
            return;
        Instantiate(particles, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
