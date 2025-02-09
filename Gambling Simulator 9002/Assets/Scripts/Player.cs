using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!PartManager.instance.HasCondition("hand"))
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            horizontalInput = Input.GetAxisRaw("Vertical");
            verticalInput = Input.GetAxisRaw("Horizontal");
        }
        
        Vector2 moveInput = new Vector2(horizontalInput, verticalInput).normalized;
        rb.velocity = moveInput * speed;
    }
}
