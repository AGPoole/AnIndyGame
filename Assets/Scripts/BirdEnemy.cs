using UnityEngine;
using System.Collections;

public class BirdEnemy : Enemy {
    public Transform right;
    public Transform left;
    public Transform target;
    public float speed;

    public GameObject dart;
    public Transform dropPoint;

    protected override void Start()
    {
        if(target == null)
            target = right;
        base.Start();
        StartCoroutine(drop());
    }

    IEnumerator drop()
    {
        const float wait = 4f;
        while (true)
        {
            yield return new WaitForSeconds(wait);
            Destroy(Instantiate(dart, dropPoint.position, Quaternion.Euler(0, 0, -90)), 5);
        }
    }

    // Update is called once per frame
    void Update () {
        var dist = (transform.position - target.position).magnitude;
        if(dist < 20f)
        {
            target = (target == right) ? left : right;
        }
        srend.flipX = (target.position.x < transform.position.x);
        var force = (target.position - transform.position).normalized * speed;
        rb2d.AddForce(force);
	}
}
