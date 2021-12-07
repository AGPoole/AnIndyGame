using UnityEngine;
using System.Collections;

public class Whip : MonoBehaviour {

    DistanceJoint2D joint;

    Vector3 target;

    public Transform whipPosition;

    public Rigidbody2D whipTarget;

    LineRenderer line;

	// Use this for initialization
	void Start () {
        joint = GetComponent<DistanceJoint2D>();

        line = GetComponent<LineRenderer>();

        joint.enabled = false;
	}

    public LayerMask mask;

    RaycastHit2D hit;

    Vector3 whipPoint;
	
    Vector3 whipShot;

    bool forward = true;

    bool breaking = false;

    public float minWhip = 10f;
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space)) {
            for (int i = 0; i < 30; i = (i*-1) + ((i <= 0) ? 1 : 0))
            {
                breaking = false;

                Vector3 vec = whipPosition.position - transform.position;
                var tx = vec.x;
                var ty = vec.y;
                var cos = Mathf.Cos(i * Mathf.Deg2Rad);
                var sin = Mathf.Sin(i * Mathf.Deg2Rad);
                vec.x = (cos * tx) - (sin * ty);
                vec.y = (sin * tx) + (cos * ty);

                hit = Physics2D.Raycast(transform.position, vec, vec.magnitude, mask);

                if (hit.collider != null)
                {
                    joint.enabled = true;

                    whipTarget.transform.position = hit.point;

                    joint.distance = Vector3.Distance(transform.position, whipTarget.transform.position);

                    if (joint.distance < minWhip)
                        joint.distance = minWhip;

                    break;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            joint.enabled = false;
        }

        line.enabled = true;
        if (!Physics2D.OverlapCircle(whipTarget.transform.position, 0.5f, mask))
            joint.enabled = false;
        if (transform.position.y > whipTarget.transform.position.y)
            joint.enabled = false;

        if (joint.enabled)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, whipTarget.transform.position);
        }else if (Input.GetKey(KeyCode.Space) && !breaking)
        { 

            if((whipShot - whipPosition.position).magnitude < 3)
            {
                forward = false;
            }

            whipShot = Vector3.MoveTowards(whipShot, forward ? whipPosition.position : transform.position, 7.5f);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, whipShot);

        }
        else
        {
            line.enabled = false;
            whipShot = transform.position;
            forward = true;
        }
    }

    public bool hanging()
    {
        var playerDistance = Vector3.Distance(transform.position, whipTarget.transform.position);
        var whipLength = joint.distance;
        var isHanging = (Mathf.Abs(playerDistance - whipLength) < 0.5f);
        return (joint.enabled && isHanging);

    }

    public bool Break()
    {
        var ret = joint.enabled;
        joint.enabled = false;
        breaking = true;
        return ret;
    }
}
