using UnityEngine;
using System.Collections;

public class GunCollectable : Collectable {
    public int ammo = 10;

	protected override void CollectedAction()
    {
        IndyController.indy.CollectGun(ammo);
        base.CollectedAction();
    }

}
