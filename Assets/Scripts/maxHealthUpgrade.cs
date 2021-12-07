using UnityEngine;
using System.Collections;

public class maxHealthUpgrade : Upgrade
{
    public int health;

    protected override void upgradeAction()
    {
        indy.GetComponent<IndyHealth>().IncreaseMaxHealth(health);
    }
}
