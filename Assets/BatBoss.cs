using UnityEngine;
using System.Collections;

public class BatBoss : Enemy
{
    //TODO: Make bat immune to pushers
    public Transform[] path;
    public float normalSpeed = 30f;
    public float chargeSpeed = 60f;
    public float shootingSpeed = 15f;

    enum Mode { STOP, NORMAL, CHARGE, SHOOTING };

    Mode mode = Mode.NORMAL;

    Transform target;
    public int targetNum = 0;

    Transform indyTransform;

    public float shootTime = 10f;

    [Range(0, 100)]
    public int reverseOdds = 30;

    [Range(0, 100)]
    public int shootOdds = 25;

    [Range(0, 100)]
    public int chargeOdds = 25;

    [Range(0, 100)]
    public int normalOdds = 50;

    public Transform chargeTarget;

    public AudioSource chargeWarning;

    // Use this for initialization
    override protected void Start()
    {
        target = path[0];
        base.Start();
        StartCoroutine(turnColOffAndOn());
        StartCoroutine(Stop());
        StartCoroutine(Shoot());

        if (normalOdds + chargeOdds + shootOdds != 100)
            Debug.LogError("Odds do not add to 100");

    }

    public float stopTime = 5f;

    bool travellingForward = true;

    const float CHARGEDIST = 100f;

    bool chasing = false;
    float leaveTime = 0;

    int hitsTillChange = 4;
    float speed;
    // Update is called once per frame
    void Update()
    {
        if (getHealth() <= 0)
        {
            return;
        }
        if (Vector3.Distance(transform.position, target.position) < 10f)
        {
            hitsTillChange -= 1;
            if (hitsTillChange <= 0)
            {
                hitsTillChange = Random.Range(3, 8);
                if(Random.Range(0, 100) < reverseOdds)
                    travellingForward = !travellingForward;
                StartCoroutine(Stop());
            }
            targetNum += (travellingForward) ? 1 : (-1);
            if (targetNum < 0)
            {
                targetNum = path.Length - 1;
            }
        }
        targetNum = targetNum % path.Length;
        if(mode != Mode.CHARGE)
            target = path[targetNum];

        switch (mode) {
            case Mode.STOP:
                speed = 0;
                break;

            case Mode.NORMAL:
                speed = normalSpeed;
                break;

            case Mode.CHARGE:
                speed = chargeSpeed;
                break;

            case Mode.SHOOTING:
                speed = shootingSpeed;
                break;

            default:
                Debug.LogError("WTF!?");
                break;
        }
        Vector3 s;
        s = (target.position - transform.position).normalized;
        s *= speed;

        srend.flipX = (s.x < 0);

        rb2d.velocity = s;
    }

    IEnumerator turnColOffAndOn()
    {
        while (true)
        {
            col.enabled = !col.enabled;
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator Stop()
    {
        if (mode == Mode.CHARGE)
            targetNum = Random.Range(0, path.Length);
        mode = Mode.STOP;
        yield return new WaitForSeconds(stopTime);
        int rand = Random.Range(0, 100);
        if(rand < normalOdds)
        {
            mode = Mode.NORMAL;
        }else if(rand < normalOdds + shootOdds)
        {
            mode = Mode.SHOOTING;
        }else
        {
            mode = Mode.CHARGE;
        }
        if (mode == Mode.SHOOTING)
            hitsTillChange = 1;
        if (mode == Mode.CHARGE)
        {
            if (indyTransform == null)
                indyTransform = IndyController.indy.transform;

            const float CHARGEPREC = 100f;

            hitsTillChange = 1;
            Vector3 dir = (indyTransform.position - transform.position);
            float range = Mathf.Ceil(dir.magnitude / CHARGEPREC) * CHARGEPREC;
            chargeTarget.position = transform.position + (dir.normalized * range);
            target = chargeTarget;

            chargeWarning.Play();
        }
    }

    public GameObject dart;
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (mode != Mode.SHOOTING)
                continue;
            if (GetComponent<Health>().health <= 0)
                break;
            if (indyTransform == null)
                indyTransform = IndyController.indy.transform;
            Vector3 dir = indyTransform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

            Destroy((GameObject)Instantiate(dart, transform.position, rot), 15);
        }
    }
}
