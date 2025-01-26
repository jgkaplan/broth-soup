using UnityEngine;

public class BasicFollowerEnemy : Enemy
{
    public GameObject player;
    private LayerMask eMask;



    void Start()
    {
        player = GameObject.Find("Player");
        eMask = LayerMask.GetMask("Enemy");
    }

    public override void InitEnemy(int id = -1)
    {
        base.InitEnemy(id);

        GetComponent<SpriteRenderer>().color = new Color(1f - Random.Range(0f, 0.5f), 1f - Random.Range(0f, 0.5f), 1f - Random.Range(0f, 0.5f));
        transform.localScale = new Vector3(1.25f + Random.Range(-0.1f, 0.1f), 1.25f + Random.Range(-0.1f, 0.1f), 1.25f + Random.Range(-0.1f, 0.1f));
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.IsPlaying()) { return; }
        if (isDead) return;

        Vector3 moveVec = speed * Vector3.Normalize(player.transform.position - transform.position);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, eMask);
        for (int i = 1; i < hits.Length; i++)
        {
            Vector3 avoidOther = transform.position - hits[i].transform.position;
            moveVec += avoidOther * 0.01f;
        }

        moveVec = new Vector3(moveVec.x, moveVec.y, 0f);

        transform.position += moveVec;

        if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = baseScale;
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else
        {
            transform.localScale = new Vector3(-1 * baseScale.x, baseScale.x, baseScale.z);
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
    }
}
