using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnim : MonoBehaviour
{
    private float speed;
    private float initSpeed;
    private int dir;
    private float counter;
    private void Start()
    {
        if (Random.Range(0, 2) == 0)
            dir = -1;
        else
            dir = 1;
        counter = 0;
        initSpeed = Random.Range(15f, 20f);
        speed = initSpeed;
    }
    void Update()
    {
        speed *= 1f-(0.01f*Time.deltaTime);
        if (dir == -1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - speed * Time.deltaTime));
            counter -= speed*Time.deltaTime;
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + speed * Time.deltaTime));
            counter += speed*Time.deltaTime;
        }

        if (counter < -15)
        {
            dir = 1;
            speed = initSpeed;
        }
        else if (counter > 15)
        {    
            dir = -1;
            speed = initSpeed;
        }
    }
}
