using UnityEngine;
using System.Collections;

public class SnakeEnemy : Enemy{
    public Transform point1;
    public Transform point2;
    Transform[] path;
    //TODO: change to const
    public float SPEED = 10;

    Transform target;

    Collider2D collider;


    public int targetNum = 0;
	// Use this for initialization
	override protected void Start () {
        path = new Transform[] { point1, point2 };
        target = path[0];
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            if (c.isTrigger)
                collider = c;
        }
        base.Start();
	}

    public bool chasing = false;

    public LayerMask lmask;
	// Update is called once per frame
	void Update () {
        float speed;
        if(getHealth() <= 0)
        {
            return;
        }
        if (Mathf.Abs(transform.position.x - target.position.x) < 10f)
        {
            targetNum = (targetNum + 1) % 2;
            target = path[targetNum];

            chasing = false;

        }
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, target.position - transform.position, (target.position - transform.position).magnitude, lmask);
        Debug.DrawLine(transform.position, target.position);
        if ((hit.Length > 1) && (hit[1].transform.gameObject.tag == "Player"))
            chasing = true;
        const float speedMultiplier = 1.6f;
        speed = chasing ? SPEED * speedMultiplier : SPEED;
        anim.SetBool("Angry", chasing);

        Vector3 s;
        s = (target.position.x > transform.position.x) ? transform.right : transform.right * -1;

        {
            float scalex = (s.x < 0) ? -1 : 1;
            transform.localScale = new Vector3(scalex, 1, 1);
        }

        rb2d.AddForce(s*speed);

        collider.isTrigger = chasing;
	}
}
