using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    [SerializeField]
    private GameObject middleBackground;
    private float repeatWidth;
    private float repeatHeight;

    void Start()
    {
        repeatWidth = middleBackground.GetComponent<Renderer>().bounds.size.x;
        repeatHeight = middleBackground.GetComponent<Renderer>().bounds.size.y;
        Instantiate(middleBackground, middleBackground.transform.position + new Vector3(repeatWidth, 0f), Quaternion.identity, transform);
        Instantiate(middleBackground, middleBackground.transform.position + new Vector3(-repeatWidth, 0f), Quaternion.identity, transform);
        Instantiate(middleBackground, middleBackground.transform.position + new Vector3(0f, repeatHeight), Quaternion.identity, transform);
        Instantiate(middleBackground, middleBackground.transform.position + new Vector3(0f, -repeatHeight), Quaternion.identity, transform);
        Instantiate(middleBackground, middleBackground.transform.position + new Vector3(repeatWidth, repeatHeight), Quaternion.identity, transform);
        Instantiate(middleBackground, middleBackground.transform.position + new Vector3(repeatWidth, -repeatHeight), Quaternion.identity, transform);
        Instantiate(middleBackground, middleBackground.transform.position + new Vector3(-repeatWidth, repeatHeight), Quaternion.identity, transform);
        Instantiate(middleBackground, middleBackground.transform.position + new Vector3(-repeatWidth, -repeatHeight), Quaternion.identity, transform);
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.transform.position.x < transform.position.x - repeatWidth / 2)
        {
            transform.position = new(transform.position.x - repeatWidth, transform.position.y);
        }
        else if (PlayerController.instance.transform.position.x > transform.position.x + repeatWidth / 2)
        {
            transform.position = new(transform.position.x + repeatWidth, transform.position.y);
        }
        if (PlayerController.instance.transform.position.y < transform.position.y - repeatHeight / 2)
        {
            transform.position = new(transform.position.x, transform.position.y - repeatHeight);
        }
        else if (PlayerController.instance.transform.position.y > transform.position.y + repeatHeight / 2)
        {
            transform.position = new(transform.position.x, transform.position.y + repeatHeight);
        }
    }
}
