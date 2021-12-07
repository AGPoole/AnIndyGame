using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class IndyController : MonoBehaviour {

    public static IndyController indy;

    [SerializeField]
    float speed, jumpSpeed;

    Animator anim;

    Rigidbody2D rbody;

    public Transform groundCheck;

    SpriteRenderer sRenderer;

    float scale;

    Whip whip;

    public Gun gun;

    public Hammer hammer;

    [HideInInspector]
    public static bool flipped = false;

    bool hasHammer;

    //Jetpack values
    int fuel;
    public Text fuelText;
    public GameObject jetpackParticles;
    public GameObject jetpack;
    Animator jetpackAnim;
    public Transform firePoint;

    int money;
    public Text moneyText;

    public AudioSource jumpSound;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();

        rbody = GetComponent<Rigidbody2D>();

        sRenderer = GetComponent<SpriteRenderer>();

        scale = transform.localScale.x;

        whip = GetComponent<Whip>();

        whip.enabled = false;

        if(indy == null)
        {
            indy = this;
        }

        hasHammer = false;

        hammer.gameObject.SetActive(hasHammer);

        gun.gameObject.SetActive(false);

        jetpackAnim = jetpack.GetComponent<Animator>();

        jetpack.SetActive(false);

        moneyText.enabled = true;
	}

    bool isGrounded;

    public LayerMask lmask;
    // Update is called once per frame
    void Update() {

        Move();

        Dash();

        Jump();

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            DropGun();

    }

    void Move() {

        anim.SetBool("isWalking", Mathf.Abs(rbody.velocity.x) > 5);

        if (Input.GetAxis("Horizontal") > 0.5f)
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        else if (Input.GetAxis("Horizontal") < -0.5f)
            transform.localScale = new Vector3(-scale, transform.localScale.y, transform.localScale.z);

        flipped = (transform.localScale.x < 0);

        if (dashing)
            return;

        rbody.AddForce((isGrounded ? 1 : 0.75f) * speed * Input.GetAxis("Horizontal") * transform.right * 100 * Time.deltaTime);

        UseWeapon();

    }

    bool hasDoubleJump = true;
    float lastGroundTime = 0;
    bool canDoubleJump = false;

    void Jump() {
        const float GROUNDWAIT = 0.2f;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, lmask);

        lastGroundTime = isGrounded ? Time.time : lastGroundTime;

        if (Time.time < lastGroundTime + GROUNDWAIT)
            isGrounded = true;

        hasDoubleJump = (isGrounded || (whip.enabled && whip.hanging())) ? true : hasDoubleJump;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(isGrounded)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, jumpSpeed);
                jumpSound.Play();
                isGrounded = false;
                lastGroundTime = Time.time - GROUNDWAIT;
            }
            else if (hasDoubleJump && canDoubleJump)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, jumpSpeed);
                jumpSound.Play();

                hasDoubleJump = whip.Break();

            }else if(fuel > 0)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, jumpSpeed * 1.5f);
                jumpSound.Play();

                fuel -= 1;
                jetpackAnim.SetTrigger("Fly");

                fuelText.text = "Fuel - " + fuel;

                GameObject burst = (GameObject)Instantiate(jetpackParticles, firePoint.position, firePoint.rotation);
                burst.transform.parent = firePoint;
                Destroy(burst, 5);

                if(fuel < 1)
                {
                    jetpack.SetActive(false);
                    fuelText.enabled = false;
                }
            }
                
        }
    }

    bool dashing = false;
    bool readyToDash = true;

    public float dashSpeed;
    public float dashTime;

    bool canDash = false;
    void Dash() {
        if (!canDash)
            return;
        if (!dashing && Input.GetKeyDown(KeyCode.A) && readyToDash) {
            dashing = true;
            StartCoroutine(ResetDash());
        }

        if (dashing)
        {
            rbody.AddForce((isGrounded ? 1 : 0.75f) * dashSpeed * transform.right * (flipped ? -1 : 1) * 100 * Time.deltaTime);
        }

        rbody.gravityScale = dashing ? 0 : 1;

    }

    bool upgradedHammer = false;
    void UseWeapon()
    {
        const float HAMMERFORCE = 1000f;
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (gun.gameObject.activeSelf)
            {
                gun.gameObject.SetActive(gun.fire());
                if (hasHammer)
                {
                    hammer.gameObject.SetActive(!gun.gameObject.activeSelf);
                    hammer.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }else if (hammer.gameObject.activeSelf)
            {
                hammer.hit();
                rbody.AddForce((isGrounded ? 1 : 0.75f) * HAMMERFORCE * (upgradedHammer ? 2 : 1) * transform.right * (flipped ? -1 : 1));
            }
        }
    }

    IEnumerator ResetDash() {
        readyToDash = false;

        yield return new WaitForSeconds(dashTime);

        dashing = false;

        yield return new WaitForSeconds(dashTime*3);

        readyToDash = true;

    }

    const int gunSpawnDist = 7;
    public GameObject gunCollectable;

    void DropGun()
    {
        if(gun.GetAmmo() > 0)
        {
            GameObject gunCollect = (GameObject)Instantiate(gunCollectable, transform.position + new Vector3(0, gunSpawnDist, 0), transform.rotation);
            gunCollect.GetComponent<GunCollectable>().ammo = gun.GetAmmo();

            gun.ResetAmmo();
            gun.gameObject.SetActive(false);
            if (hasHammer)
            {
                hammer.gameObject.SetActive(!gun.gameObject.activeSelf);
                hammer.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            
        }
    }

    public void CollectWhip()
    {
        whip.enabled = true;
    }

    public void CollectHammer()
    {
        hasHammer = true;
        if (!gun.gameObject.activeSelf)
            hammer.gameObject.SetActive(true);
    }

    public void CollectGun(int ammoAddition)
    {
        gun.gameObject.SetActive(true);
        hammer.gameObject.SetActive(false);
        gun.addAmmo(ammoAddition);
    }

    const int MAXFUEL = 15;
    public void CollectJetPack(int fuelAddition)
    {
        jetpack.SetActive(true);
        fuel += fuelAddition;
        if (fuel > 15)
            fuel = 15;
        if (fuelText != null)
        {
            fuelText.enabled = true;
            fuelText.text = "Fuel - " + fuel;
        }
    }

    public void CollectMoney(int moneyAddition)
    {
        if(moneyAddition < 0)
        {
            Debug.LogError("Money Addition Less Than 0");
            return;
        }
        StartCoroutine(AddMoney(moneyAddition));
    }

    IEnumerator AddMoney(int moneyAddition)
    {
        const float WAIT = 0.01f;
        for(int i = 0; i < moneyAddition; i++)
        {
            yield return new WaitForSeconds(WAIT);
            money++;
            moneyText.text = "Money: $" + money;
        }
    }

    public void RemoveMoney(int moneyReduction)
    {
        if (moneyReduction < 0)
        {
            Debug.LogError("Money Reduction Less Than 0");
            return;
        }
        money -= moneyReduction;
        moneyText.text = "Money: $" + money;
    }

    public void CollectDash()
    {
        canDash = true;
    }

    public void CollectDoubleJump()
    {
        canDoubleJump = true;
    }

    public int GetMoney()
    {
        return money;
    }

    public void UpgradeGun()
    {
        gun.Upgrade();
    }

    public void UpgradeHammer()
    {
        hammer.Upgrade();
        upgradedHammer = true;
    }
}
