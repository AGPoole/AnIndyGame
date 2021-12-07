using UnityEngine;
using System.Collections;

public class SetUp : MonoBehaviour {
    public bool doubleJump;
    public bool dash;

    public bool hammer;

    public bool whip;

    public int bullets;
    public int fuel;

    public int money;

	// Use this for initialization
	void Start () {
        StartCoroutine(MakeChanges());
	}

    IEnumerator MakeChanges()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        var indy = IndyController.indy;
        if (doubleJump)
            indy.CollectDoubleJump();
        if (dash)
            indy.CollectDash();
        if (hammer)
            indy.CollectHammer();
        if (whip)
            indy.CollectWhip();
        if (fuel > 0)
            indy.CollectJetPack(fuel);
        if (bullets > 0)
            indy.CollectGun(bullets);
        if (money > 0)
            indy.CollectMoney(money);
    }
}
