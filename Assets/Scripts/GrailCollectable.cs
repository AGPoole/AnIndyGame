using UnityEngine;
using System.Collections;

public class GrailCollectable : Collectable {
    float healthAddition = 50;

    protected override void CollectedAction()
    {
        Health health = IndyController.indy.gameObject.GetComponent<Health>();

        health.addHealth(healthAddition);

        base.CollectedAction();
    }
}
