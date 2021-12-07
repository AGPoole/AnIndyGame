using UnityEngine;
using System.Collections;

public class BatEnemy1 : Enemy {
    //TODO: Make bat immune to pushers
    public Transform[] path;
    public const float SPEED = 15;

    Transform target;
    public int targetNum = 0;
	// Use this for initialization
	override protected void Start () {
        target = path[0];
        base.Start();
	}

    bool chasing = false;
    float leaveTime = 0;

	// Update is called once per frame
	void Update () {
        float speed;
        const float CHASETIME = 3f;
        if(getHealth() <= 0)
        {
            return;
        }
        if (Vector3.Distance(transform.position, target.position) < 10f)
        {
            targetNum++;            
        }
        //{
        //    float dist = Vector3.Distance(IndyController.indy.transform.position, transform.position);
        //    if(dist < 20f || (dist < 40f && chasing))
        //    { 
        //        target = IndyController.indy.transform;
        //        chasing = true;
        //        speed = SPEED * 1.5f;
        //        leaveTime = Time.time + CHASETIME;
        //    }else if (chasing)
        //    {
        //        speed = 0;
        //        if(Time.time > leaveTime)
        //        {
        //            chasing = false;
        //        }
        //    }
        //    else{
                targetNum = targetNum % path.Length;
                target = path[targetNum];
                speed = SPEED;
        //    }
        //}
        Vector3 s;
        s = (target.position - transform.position).normalized;
        s *= speed;

        srend.flipX = (s.x < 0);

        rb2d.velocity = s;
	}

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.GetComponent<Health>() != null)
            StartCoroutine(turnColOffAndOn());

    }

    IEnumerator turnColOffAndOn()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.25f);
        col.enabled = true;
    }
}
