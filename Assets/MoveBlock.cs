using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveBlock : MonoBehaviour {
    public float speed;
    public Transform start;
    public Transform end;

    public float wait;

    Rigidbody2D rb2d;
	
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        target = start;
        StartCoroutine(Go());
    }

    Transform target;

    IEnumerator Go()
    {
        while (true)
        {
            var direction = target.position - transform.position;
            rb2d.velocity = speed * direction.normalized;
            yield return new WaitForSeconds(direction.magnitude / speed);
            rb2d.velocity = Vector2.zero;
            yield return new WaitForSeconds(wait);
            target = (target == start) ? end : start;
        }
    }
}
