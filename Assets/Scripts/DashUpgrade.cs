using UnityEngine;
using System.Collections;

public class DashUpgrade : Upgrade
{

    protected override void upgradeAction()
    {
        indy.CollectDash();
    }
}
