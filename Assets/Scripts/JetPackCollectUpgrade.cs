using UnityEngine;
using System.Collections;

public class JetPackCollectUpgrade : Upgrade
{
    public int fuel;

    protected override void upgradeAction()
    {
        indy.CollectJetPack(fuel);
    }
}
