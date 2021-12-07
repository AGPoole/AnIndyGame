using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour {
    public Transform start;
    public Transform end;

    Transform target;

    public float speed;
    public float damage;

    Rigidbody2D rb2d;
    public Collider2D col;

    public float wait = 3f;

	// Use this for initialization
	void Start () {
        target = start;
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(Travel());
        StartCoroutine(turnColOffAndOn());
    }

    IEnumerator Travel()
    {
        const float WAIT = 0.1f;
        while (true)
        {
            var temp = (target.position - transform.position).normalized;
            rb2d.velocity = temp * speed;
            //while (!(Vector3.Distance(target.position, transform.position) < 5f))
            //{

            //}
            if (Vector3.Distance(target.position, transform.position) < 5f)
            {
                rb2d.velocity = Vector3.zero;
                target = (target == start) ? end : start;
                yield return new WaitForSeconds(wait);
            }else
                yield return new WaitForSeconds(WAIT);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
            other.gameObject.GetComponent<Health>().Damage(damage);

    }

    IEnumerator turnColOffAndOn()
    {
        while (true)
        {
            col.enabled = !col.enabled;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
