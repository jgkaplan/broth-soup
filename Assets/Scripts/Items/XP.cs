using UnityEngine;

public class XP : MonoBehaviour
{
    public bool isCollected = false;
    public int value = 1;
    public float collectSpeed = 0.01f;

    public Sprite[] sprites = new Sprite[4];

    public GameObject player;
    public GameObject gameManager;

    void Start()
    {
        isCollected = false;
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager");
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.IsPlaying()) { return; }
        
        if (isCollected)
        {
            Vector3 moveDir = player.transform.position - transform.position;

            if (moveDir.magnitude < 1f)
            {
                gameManager.GetComponent<GameManager>().CollectXP(value);
                Destroy(gameObject);
            }

            moveDir = collectSpeed*Vector3.Normalize(moveDir);
            transform.position += moveDir;
        }
    }

    public void RandomizeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,4)];
    }

    public void Collect()
    {
        isCollected = true;
    }
}
