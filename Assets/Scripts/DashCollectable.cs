using UnityEngine;
using System.Collections;

public class DashCollectable : Collectable {

    protected override void CollectedAction()
    {
        IndyController.indy.CollectDash();
        base.CollectedAction();
    }
}
