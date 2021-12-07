using UnityEngine;
using System.Collections;

public class JetPackCollectable : Collectable {

	protected override void CollectedAction()
    {
        IndyController.indy.CollectJetPack(5);
        base.CollectedAction();
    }
}
