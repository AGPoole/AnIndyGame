using UnityEngine;
using System.Collections;

public class TotemEnemy : Enemy {
    public GameObject dart;
    public Transform dartPosition1;
    public Transform dartPosition2;

    Transform indy;

    const float secondsPerShot = 1.5f;

    // Update is called once per frame
    protected override void Start () {

        base.Start();

        InvokeRepeating("Fire", 1, secondsPerShot);
	}

    public float fireRange = 100f;
    void Fire()
    {

        if (indy == null)
            indy = IndyController.indy.transform;

        Vector3 pos = (Vector3.Distance(dartPosition2.position, indy.position) > Vector3.Distance(dartPosition1.position, indy.position)) ? dartPosition1.position : dartPosition2.position;

        Vector3 dir = indy.position - pos;

        if (dir.magnitude > fireRange)
            return;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

        Destroy((GameObject)Instantiate(dart, pos, rot), 15);
    }
}
