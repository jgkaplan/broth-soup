using UnityEngine;

public class ProjectileEnemy : Enemy
{
    public GameObject proj;
    public GameObject player;
    private Vector3 toPlayer;
    public bool inPosition = false;
    public bool recharging = false;
    public float fireTime = 2f;
    public float chargeTimer = 0f;
    public float resetTime = 2f;
    public float resetTimer = 0f;

    void Start()
    {
        player = GameObject.Find("Player");
        inPosition = false;
        toPlayer = player.transform.position - transform.position;
    }

    
    public override void InitEnemy(int id=-1)
    {
        base.InitEnemy(id);

        recharging = false;
        inPosition = false;
        chargeTimer = 0f;
        resetTimer = 0f;
    }


    void FixedUpdate()
    {
        toPlayer = player.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0f, 0f, -90f+Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x));

        if (recharging)
        {
            resetTimer += Time.fixedDeltaTime;
            if (resetTimer >= resetTime)
            {
                recharging = false;
                resetTimer = 0f;
            }
        }

        if (!inPosition)
        {
            if (!recharging)
            {
                if (toPlayer.sqrMagnitude > 64f)
                {
                    transform.position += (speed*Vector3.Normalize(toPlayer));
                }
                else
                {
                    inPosition = true;
                }
            }
        }
        else
        {
            if (chargeTimer >= fireTime)
            {
                for (int i = 0; i < 3; i ++)
                {
                    Instantiate(proj, transform.position, Quaternion.Euler(0f, 0f, 40f-40f*i+Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x)));
                }

                chargeTimer = 0f;
                inPosition = false;
                recharging = true;
            }
            else
            {
                chargeTimer += Time.fixedDeltaTime;
            }
        }
    }
}
