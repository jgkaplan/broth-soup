using UnityEngine;
using System.Collections;

public class CrabEnemy : Enemy
{
    public GameObject proj;
    public GameObject player;
    private Vector3 toPlayer;

    public float maxHp;

    public bool firingLaser = false;
    public bool catchingUp = false;
    public float catchUpBoost = 2f;
    public float chargeTimer = 0f;
    public float chargeTime = 5f;

    public bool isAscended = false;

    public Sprite[] walkSprites = new Sprite[4];
    public int walkAnimCt = 0;

    public Sprite[] laserSprites = new Sprite[8];

    public Sprite[] divineSprites = new Sprite[6];

    public Sprite[] divineLaserSprites = new Sprite[6];

    void Start()
    {
        player = GameObject.Find("Player");
        toPlayer = player.transform.position - transform.position;
        maxHp = hp;
    }

    public override void InitEnemy(int id = -1)
    {
        if (id >= 0) { this.id = id; }
        hp = basehp;

        baseScale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        toPlayer = player.transform.position - transform.position;
        if (!isAscended && hp <= maxHp / 2f)
        {
            SoundManager.instance.PlayCrabScream();
            isAscended = true;
        }

        if (!firingLaser)
        {
            if (toPlayer.sqrMagnitude >= 400f && !catchingUp)
            {
                speed += catchUpBoost;
                catchingUp = true;
            }
            else if (catchingUp && toPlayer.sqrMagnitude <= 400f)
            {
                speed -= catchUpBoost;
                catchingUp = false;
            }

            transform.rotation = Quaternion.Euler(0f, 0f, -90f + Mathf.Rad2Deg * Mathf.Atan2(toPlayer.y, toPlayer.x));
            transform.position += speed * Vector3.Normalize(toPlayer);

            if (!isAscended)
            {
                walkAnimCt++;
                GetComponent<SpriteRenderer>().sprite = walkSprites[walkAnimCt / 25];
                if (walkAnimCt >= 99) { walkAnimCt = 0; }
            }
            else
            {
                walkAnimCt++;
                GetComponent<SpriteRenderer>().sprite = divineSprites[walkAnimCt / 25];
                if (walkAnimCt >= 149) { walkAnimCt = 0; }
            }

            chargeTimer += Time.fixedDeltaTime;
            if (chargeTimer > chargeTime)
            {
                firingLaser = true;
                StartCoroutine("FireLaser");
            }
        }
    }

    IEnumerator FireLaser()
    {
        if (!isAscended)
        {
            //Animation time
            for (int i = 0; i < 4; i ++)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, (-180f/4f)*(i+1) - 90f+Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x));
                GetComponent<SpriteRenderer>().sprite = laserSprites[i];
                yield return new WaitForSeconds(0.5f);
            }
            GetComponent<SpriteRenderer>().sprite = laserSprites[5];
            for (int i = 0; i < 48; i ++)
            {
                
                GameObject beamObj = Instantiate(proj, transform.position, Quaternion.Euler(0f, 0f, Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x)));
                yield return new WaitForSeconds(0.05f);
            }

            for (int i = 6; i < 7; i ++)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180f - 90f+Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x));
                GetComponent<SpriteRenderer>().sprite = laserSprites[i];
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            for (int i = 0; i < 6; i ++)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, (-180f/6f)*(i+1) - 90f+Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x));
                GetComponent<SpriteRenderer>().sprite = divineLaserSprites[i];
                yield return new WaitForSeconds(0.5f);                
            }

            for (int i = 0; i < 48; i ++)
            {
                Instantiate(proj, transform.position, Quaternion.Euler(0f, 0f, Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x)));
                toPlayer = player.transform.position - transform.position+transform.right*7f+transform.up*-9f;
                Instantiate(proj, transform.position+transform.right*7f+transform.up*-9f, Quaternion.Euler(0f, 0f, -62f+Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x)));
                toPlayer = player.transform.position - transform.position+transform.right*-7f+transform.up*-9f;
                Instantiate(proj, transform.position+transform.right*-7f+transform.up*-9f, Quaternion.Euler(0f, 0f, 62f+Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x)));
                if (i%4 == 0) 
                {
                    Instantiate(proj, transform.position+transform.right*12f+transform.up*-3f, Quaternion.Euler(0f, 0f, (120f/48f)*i + -60f +  Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x)));
                    Instantiate(proj, transform.position+transform.right*-12f+transform.up*-3f, Quaternion.Euler(0f, 0f, -(120f/48f)*i + +60f +  Mathf.Rad2Deg*Mathf.Atan2(toPlayer.y, toPlayer.x)));
                }
                yield return new WaitForSeconds(0.05f);
            }
        }

        yield return new WaitForSeconds(1f);
        firingLaser = false;
        chargeTimer = 0f;
    }

    protected override void RotAndDie()
    {
        GameManager.instance.Win();
        base.RotAndDie();
    }

    public bool GetAscended()
    {
        return isAscended;
    }
}
