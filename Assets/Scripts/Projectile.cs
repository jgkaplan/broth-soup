using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 0f;
    public int pen = 0;
    public float dmg = 0f;

    private float ttl = 2f;

    LayerMask eMask;

    [SerializeField]
    private Sprite[] popForms;

    private bool popped = false;

    void Start()
    {
        eMask = LayerMask.GetMask("Enemy");
    }

    public void Init(float spd, int pen, float dmg)
    {
        this.speed = spd;
        this.pen = pen;
        this.dmg = dmg;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.IsPlaying()) { return; }
        if (popped) return;
        transform.position += transform.up * speed;

        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.25f, eMask);
        if (hit)
        {
            hit.gameObject.GetComponent<Enemy>().TakeDamage(dmg);
            pen -= 1;
            if (pen <= 0)
            {
                SoundManager.instance.PlayBubblePop();
                popped = true;
                StartCoroutine(PopBubble());
            }
        }

        ttl -= Time.fixedDeltaTime;
        if (ttl <= 1f)
        {
            GetComponent<ShapeRotator>().enabled = true;
        }
        if (ttl <= 0f)
        {
            SoundManager.instance.PlayBubblePop();
            popped = true;
            StartCoroutine(PopBubble());
        }
    }



    IEnumerator PopBubble()
    {
        transform.localScale = transform.localScale * 4;
        GetComponent<SpriteRenderer>().sprite = popForms[0];
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().sprite = popForms[1];
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
