using UnityEngine;

public class MeatSpike : MonoBehaviour
{
    public int dmg;
    public float speed;

    public float ttl = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right*speed;

        if (ttl <= 0f)
        {
            Destroy(gameObject);
        }
        ttl -= Time.fixedDeltaTime;
    }
}
