using UnityEngine;
using System.Collections;

public class WhipCollectable : Collectable {

    protected override void CollectedAction()
    {
        IndyController.indy.CollectWhip();
        base.CollectedAction();
    }

}
