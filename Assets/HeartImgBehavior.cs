using UnityEngine;

public class HeartImgBehavior : MonoBehaviour
{
    public float timer;
    public float randomTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomTime = Random.Range(2f, 6f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= randomTime)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-20f, 20f));

            timer = 0f;
            randomTime = Random.Range(2f, 6f);
        }
    }
}
