using UnityEngine;
using System.Collections;
using System;

public class HammerUpgrade : Upgrade
{

    protected override void upgradeAction()
    {
        indy.UpgradeHammer();
    }
}
