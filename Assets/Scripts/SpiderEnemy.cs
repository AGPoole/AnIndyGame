using UnityEngine;
using System.Collections;

public class SpiderEnemy : Enemy {

    public Transform webTarget;

    LineRenderer line;
    // Use this for initialization
    protected override void Start()
    {
        line = GetComponent<LineRenderer>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, webTarget.position);
    }

}
