using UnityEngine;
using System.Collections;

public class HammerCollectUpgrade : Upgrade
{

    protected override void upgradeAction()
    {
        indy.CollectHammer();
    }
}
