using UnityEngine;
using System.Collections;

public class MoneyBagCollectable : Collectable {
    public int value = 100;

    protected override void CollectedAction()
    {
        IndyController.indy.CollectMoney(value);
        base.CollectedAction();
    }
}
