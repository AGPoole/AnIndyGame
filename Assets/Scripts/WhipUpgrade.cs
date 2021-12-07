using UnityEngine;
using System.Collections;

public class WhipUpgrade : Upgrade
{

    protected override void upgradeAction()
    {
        indy.CollectWhip();
    }
}
