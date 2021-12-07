using UnityEngine;
using System.Collections;

public class HammerCollectable : Collectable {

    protected override void CollectedAction()
    {
        IndyController.indy.CollectHammer();
        base.CollectedAction();
    }
}
