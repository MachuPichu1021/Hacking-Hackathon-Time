using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager instance;
    [SerializeField] private float speed;
    [SerializeField] private GameObject camera;
    [SerializeField] private float timeRemaining;
    [SerializeField] private float shakeCooldown = 7f;
    private float horizontalInput;
    private float verticalInput;
    private bool isHeartDebuff = true;
    private Rigidbody2D rb;
    //private Animator animator;

    private void Start()
    {
        instance.LegDebuff(speed);
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isHeartDebuff)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            instance.HandDebuff(horizontalInput, verticalInput);
        }
        
        Vector2 moveInput = new Vector2(horizontalInput, verticalInput).normalized;
        rb.velocity = moveInput * speed;
        
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = shakeCooldown;
            instance.KidneyDebuff();
        }

        //animator.SetFloat("Vertical Input", verticalInput);
        //animator.SetFloat("Horizontal Input", horizontalInput);
    }
}
