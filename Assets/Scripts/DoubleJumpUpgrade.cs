using UnityEngine;
using System.Collections;
using System;

public class DoubleJumpUpgrade : Upgrade {

    protected override void upgradeAction()
    {
        indy.CollectDoubleJump();
    }
}
