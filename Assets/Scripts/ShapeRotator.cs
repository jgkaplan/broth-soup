using UnityEngine;
using System.Collections;

public class ShapeRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Random.Range(-rotationSpeed * 0.66f, rotationSpeed * 2.0f), Random.Range(-rotationSpeed * 0.9f, rotationSpeed * Mathf.PI), 0);
    }
}
