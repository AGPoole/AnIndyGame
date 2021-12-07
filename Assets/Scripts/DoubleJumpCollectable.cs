using UnityEngine;
using System.Collections;

public class DoubleJumpCollectable : Collectable
{

    protected override void CollectedAction()
    {
        IndyController.indy.CollectDoubleJump();
        base.CollectedAction();
    }
}
