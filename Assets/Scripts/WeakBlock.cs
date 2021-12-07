using UnityEngine;
using System.Collections;

public class WeakBlock : MonoBehaviour {
    Animator anim;
    Collider2D bc2d;
    SpriteRenderer sRenderer;

    void Start()
    {
        anim = GetComponent<Animator>();

        sRenderer = GetComponent<SpriteRenderer>();

        Collider2D[] cols = GetComponents<Collider2D>();

        foreach (Collider2D c in cols)
        {
            if (!c.isTrigger)
            {
                bc2d = c;
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger && other.GetComponent<Hammer>() == null)
            return;
        StartCoroutine(Break());
    }

    IEnumerator Break()
    {
        const float BREAKTIME = 1.5f;
        anim.SetBool("Damaged", true);

        yield return new WaitForSeconds(BREAKTIME);

        bc2d.enabled = false;
        sRenderer.enabled = false;

        yield return new WaitForSeconds(4);

        bc2d.enabled = true;
        sRenderer.enabled = true;

        anim.SetBool("Damaged", false);
    }
}
