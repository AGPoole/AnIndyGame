using UnityEngine;
using System.Collections;

public class CrusherScript : MonoBehaviour {
    public GameObject crusher;
    public float wait = 2f;

    public float startWait = 0f;

    Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
        rb2d = crusher.GetComponent<Rigidbody2D>();
        StartCoroutine(Crush());
	}
	
	IEnumerator Crush()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            yield return new WaitForSeconds(wait);
            rb2d.gravityScale = 1f;
            yield return new WaitForSeconds(1f);
            rb2d.gravityScale = 0f;
            rb2d.velocity = new Vector2(0, 5f);
            while (transform.position.y > crusher.transform.position.y)
            {
                yield return new WaitForSeconds(0.5f);
                rb2d.velocity = new Vector2(0, 10f);
            }
            rb2d.velocity = Vector2.zero;
        }
    }
}
