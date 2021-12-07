using UnityEngine;
using System.Collections;

public class NativeEnemy : Enemy {
    Vector3 target;
    public Transform start;
    public Transform end;

    public Transform firePoint;

    public GameObject dart;

    Vector3 scale;

    public float speed;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(SetTarget());
        StartCoroutine(shootDart());
        scale = transform.localScale;
        StartCoroutine(turnColOffAndOn());
    }

    IEnumerator SetTarget()
    {
        while (true)
        {
            target = start.position + (Random.value * (end.position - start.position));
            yield return new WaitForSeconds(Random.Range(3f, 10f));
        }
    }

    enum direction { LEFT, RIGHT};
    direction dir = direction.RIGHT;

    bool walking;

    IEnumerator shootDart()
    {
        const float WAIT = 3f;
        while (true)
        {
            yield return new WaitForSeconds(WAIT);
            var fireDirection = (dir == direction.RIGHT) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 0, 180);
            if (walking)
                continue;
            for (int i = 0; i < 3; i++)
            {
                Destroy((GameObject)Instantiate(dart, firePoint.position, fireDirection), 20f);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

	// Update is called once per frame
	void Update () {
        var dist = Mathf.Abs(transform.position.x - target.x);
        walking = (dist > 3f);
        anim.SetBool("Walking", walking);
        if (walking)
        {
            var temp = scale;
            dir = (target.x < transform.position.x) ? direction.LEFT : direction.RIGHT;
            temp.x *= (dir == direction.LEFT) ? (-1) : 1;
            transform.localScale = temp;
            var force = (dir == direction.LEFT) ? new Vector3(-1, 0, 0) : new Vector3(1, 0, 0);

            force *= speed;

            rb2d.AddForce(force);
        }
    }

    IEnumerator turnColOffAndOn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            col.enabled = !col.enabled;
        }
    }
}
