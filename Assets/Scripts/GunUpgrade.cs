using UnityEngine;
using System.Collections;
using System;

public class GunUpgrade : Upgrade {

    protected override void upgradeAction()
    {
        indy.UpgradeGun();
    }
}
