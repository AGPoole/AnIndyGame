using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    public GameObject bullet;
    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;

    public Text text;

    public GameObject smoke;

    int ammo = 0;

    bool isUpgraded = false;

    const int MAXAMMO = 30;

	// Use this for initialization
	void Start () {
	
	}
    float fireTime = 0;
    public bool fire()
    {
        if (Time.time > fireTime) {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            Destroy((GameObject)Instantiate(smoke, firePoint.position, firePoint.rotation), 5);
            if (isUpgraded)
            {
                Destroy((GameObject)Instantiate(smoke, firePoint2.position, firePoint2.rotation), 5);
                Instantiate(bullet, firePoint2.position, firePoint2.rotation);
                Destroy((GameObject)Instantiate(smoke, firePoint3.position, firePoint3.rotation), 5);
                Instantiate(bullet, firePoint3.position, firePoint3.rotation);
            }
            ammo--;
        }
        
        bool b =  ammo > 0;
        if (text == null)
            return b;
        text.enabled = b;

        text.text = "Ammo - " + ammo;

        return b;
    } 

    public void addAmmo(int ammoAddition)
    {
        ammo += ammoAddition;

        if (ammo > 30)
            ammo = 30;

        text.enabled = true;

        text.text = "Ammo - " + ammo;
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public void ResetAmmo()
    {
        ammo = 0;
    }

    public void Upgrade()
    {
        isUpgraded = true;
    }
}
