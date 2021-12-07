using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public abstract class Upgrade : MonoBehaviour {

    public int cost;
    public Text text;

    public string upgradeName;

    protected IndyController indy;
    
    void Start()
    {
        StartCoroutine(changeText());
        indy = IndyController.indy;
    }

    IEnumerator changeText()
    {
        const float WAIT = 1f;
        while (true)
        {
            text.text = upgradeName;
            yield return new WaitForSeconds(WAIT);
            text.text = "Cost: £" + cost;
            yield return new WaitForSeconds(WAIT);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (indy == null)
            indy = IndyController.indy;
        if (other.gameObject.tag == "Player" && indy.GetMoney() >= cost)
        {
            upgradeAction();
            indy.RemoveMoney(cost);
            Destroy(this.gameObject);
        }
    }

    protected abstract void upgradeAction();
}
