using UnityEngine;
using System.Collections;

public class ZombieEnemy : Enemy {
    Transform target;

    public Transform attackPoint;
    public Transform left;
    public Transform right;

    const float speed = 100f;

    Vector3 scale;

    protected override void Start()
    {
        scale = transform.localScale;
        base.Start();
    }
    // Update is called once per frame
    void Update () {
        if(target == null)
            target = IndyController.indy.transform;
        bool chasing = (target.position.x > left.position.x && target.position.x < right.position.x);
        anim.SetBool("Chasing", chasing);
        if (chasing)
        {
            if (target.position.x < transform.position.x)
            {
                rb2d.AddForce(new Vector2(-1, 0) * speed);
                Vector3 temp = scale;
                temp.x *= -1;
                transform.localScale = temp;
            }
            if (target.position.x > transform.position.x)
            {
                rb2d.AddForce(new Vector2(1, 0) * speed);
                transform.localScale = scale;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        StartCoroutine(turnColOffAndOn());
        
    }

    IEnumerator turnColOffAndOn()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.5f);
        col.enabled = true;
    }
}
