using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    InputAction blow;
    InputAction pos;
    public float impulseConst = 1.0f;
    public float maxVelocity = 5f;
    Rigidbody2D rigidBody;
    Camera cam;

    public int hp = 6;
    public int maxHp = 6;
    private float recharge = 0;

    public GameObject projParent;
    public GameObject bubble;
    public int projCt = 5;
    public float projDmg = 2.5f;
    public float projSize = 1f;
    public float projVelMult = 1f;
    public int projPen = 0;
    public float spread = 0f;
    public float cd = 1f;
    public float pickupRange = 3f;

    public float invulnPeriod = 3f;
    public float invulnTimer = 0f;

    private bool isInvulnBlinking = false;

    LayerMask eMask;
    LayerMask eProjMask;
    LayerMask xpMask;

    // Sprites
    public Sprite[] bubbleSprites = new Sprite[12];

    public static PlayerController instance;

    void Start()
    {
        instance = this;
        blow = InputSystem.actions.FindAction("Bubble");
        pos = InputSystem.actions.FindAction("Position");
        blow.Enable();
        pos.Enable();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        eMask = LayerMask.GetMask("Enemy");
        xpMask = LayerMask.GetMask("XP");
        eProjMask = LayerMask.GetMask("EnemyProjectile");
    }

    void OnDestroy()
    {
        blow.Disable();
        pos.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.PlayerHPChange(hp); // TODO: move this
        if (!GameManager.instance.IsPlaying()) { return; }
        if (recharge > 0)
        {
            recharge -= Time.deltaTime;
        }
        invulnTimer -= Time.deltaTime;

        Vector3 mPos = pos.ReadValue<Vector2>();
        Vector2 dir = SettingsStuff.invertControls ? gameObject.transform.position - cam.ScreenToWorldPoint(mPos) : cam.ScreenToWorldPoint(mPos) - gameObject.transform.position;
        dir.Normalize();
        dir = -dir;
        transform.rotation = Quaternion.Euler(0f, 0f, -90f + Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x));

        if (blow.triggered && recharge <= 0)
        {
            recharge += cd;
            Blow();
        }

        CheckCollisions();
    }

    IEnumerator InvulnFlash()
    {
        isInvulnBlinking = true;
        Color c = GetComponent<SpriteRenderer>().color;
        while (invulnTimer > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 0.3f);
            yield return new WaitForSeconds(0.15f);
            GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1f);
            yield return new WaitForSeconds(0.15f);
        }
        isInvulnBlinking = false;
    }

    void Blow()
    {
        if (!GameManager.instance.IsPlaying()) { return; }
        Vector3 mPos = pos.ReadValue<Vector2>();
        Vector2 dir = SettingsStuff.invertControls ? gameObject.transform.position - cam.ScreenToWorldPoint(mPos) : cam.ScreenToWorldPoint(mPos) - gameObject.transform.position;
        dir.Normalize();
        StartCoroutine(ShootBubbles(dir));
        dir *= -impulseConst * Mathf.Sqrt(projVelMult * projSize * projCt);
        rigidBody.AddForce(dir, ForceMode2D.Impulse);
        rigidBody.linearVelocity = Vector2.ClampMagnitude(rigidBody.linearVelocity, maxVelocity);
        SoundManager.instance.PlayShootBubble();
    }

    // Accepts vector relative to player position
    IEnumerator ShootBubbles(Vector2 dir)
    {
        for (int i = 0; i < projCt; i++)
        {
            GameObject newBub;
            if (projParent) { newBub = Instantiate(bubble, projParent.transform); }
            else { newBub = Instantiate(bubble); }
            newBub.GetComponent<Projectile>().Init((0.03f + Random.Range(0f, 0.03f)) * projVelMult, projPen, projDmg);

            newBub.GetComponent<SpriteRenderer>().sprite = bubbleSprites[Random.Range(0, 12)];

            float spreadVar = Random.Range(-0.5f, 0.5f);

            newBub.transform.SetPositionAndRotation(transform.position - 1f * transform.up + spreadVar * transform.right, Quaternion.Euler(0f, 0f, -90f + spreadVar * spread * 45f + Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x)));
            newBub.transform.localScale *= projSize;
            if (i % 2 == 1) { yield return new WaitForSeconds(0.05f); }
        }
    }

    private void CheckCollisions()
    {
        Collider2D[] xpHits = Physics2D.OverlapCircleAll(transform.position, pickupRange, xpMask);
        Collider2D[] eHits = Physics2D.OverlapCircleAll(transform.position, 1f, eMask);
        Collider2D[] eProjHits = Physics2D.OverlapCircleAll(transform.position, 1f, eProjMask);

        if (xpHits.Length > 0)
        {
            foreach (Collider2D xp in xpHits)
            {
                xp.GetComponent<XP>().Collect();
            }
        }
        if (invulnTimer <= 0f && eProjHits.Length > 0)
        {
            foreach (Collider2D p in eProjHits)
            {
                hp -= p.gameObject.GetComponent<MeatSpike>().dmg;
                Destroy(p.gameObject);
                SoundManager.instance.PlayPlayerHurt();
                invulnTimer = invulnPeriod;
                if (hp <= 0)
                {
                    GameManager.instance.Lose();
                }
                else if (!isInvulnBlinking)
                {
                    StartCoroutine(InvulnFlash());
                }
            }
        }
        if (invulnTimer <= 0f && eHits.Length > 0)
        {
            hp -= eHits[0].gameObject.GetComponent<Enemy>().GetDamage();
            SoundManager.instance.PlayPlayerHurt();
            invulnTimer = invulnPeriod;
            if (hp <= 0)
            {
                GameManager.instance.Lose();
            }
            else if (!isInvulnBlinking)
            {
                StartCoroutine(InvulnFlash());
            }
        }
    }
}
