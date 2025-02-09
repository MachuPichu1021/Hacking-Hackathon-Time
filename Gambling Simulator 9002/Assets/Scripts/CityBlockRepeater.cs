using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBlockRepeater : MonoBehaviour
{
    private float repeatWidth;

    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        repeatWidth = collider.size.x;
    }
    void Update()
    {
        if (Camera.main.transform.position.x > transform.position.x + repeatWidth)
            transform.position = new Vector3(transform.position.x + repeatWidth, transform.position.y, transform.position.z);
        else if (Camera.main.transform.position.x < transform.position.x - repeatWidth)
            transform.position = new Vector3(transform.position.x - repeatWidth, transform.position.y, transform.position.z);
    }
}
