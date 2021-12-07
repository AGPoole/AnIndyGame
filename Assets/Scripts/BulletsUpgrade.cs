using UnityEngine;
using System.Collections;

public class BulletsUpgrade : Upgrade
{
    public int bullets;

    protected override void upgradeAction()
    {
        indy.CollectGun(bullets);
    }
}
