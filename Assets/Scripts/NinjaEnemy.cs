using UnityEngine;
using System.Collections;

public class NinjaEnemy : Enemy {

    //public Transform start;
    public Transform left;
    public Transform right;

    public Transform target;

    public float speed = 70f;

    bool isGrounded;
    public LayerMask lmask;
    public Transform groundCheck;

    public Vector3 jumpForce;
    public float jumpTime;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(jump());
    }

    IEnumerator jump()
    {
        yield return new WaitForSeconds(Random.Range(0f, jumpTime));
        while (true)
        {
            yield return new WaitForSeconds(jumpTime);
            if (isGrounded && this.enabled)
                rb2d.AddForce(jumpForce);
        }
    }

    // Update is called once per frame
    void Update () {
        if (target == null)
            target = right;

        var dist = Mathf.Abs(transform.position.x - target.position.x);
        if (dist < 3f)
        {
            target = (target == right) ? left : right;
        }
        srend.flipX = (target == left);
        var force = (target == right) ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);

        force *= (isGrounded) ? speed : 0.25f *speed;

        rb2d.AddForce(force);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, lmask);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        StartCoroutine(turnColOffAndOn());

    }

    IEnumerator turnColOffAndOn()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.25f);
        col.enabled = true;
    }
}
