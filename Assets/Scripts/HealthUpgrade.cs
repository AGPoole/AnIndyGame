using UnityEngine;
using System.Collections;

public class HealthUpgrade : Upgrade
{
    public int health;

    protected override void upgradeAction()
    {
        indy.GetComponent<Health>().addHealth(health);
    }
}
