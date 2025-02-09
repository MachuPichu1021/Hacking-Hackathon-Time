using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
            HandDebuff();
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
            KidneyDebuff();
        }

        //animator.SetFloat("Vertical Input", verticalInput);
        //animator.SetFloat("Horizontal Input", horizontalInput);
    }

    public void LegDebuff()
    {
        speed /= 2;
    }

    public void HandDebuff()
    {
        horizontalInput = Input.GetAxisRaw("Vertical");
        verticalInput = Input.GetAxisRaw("Horizontal");
    }

    public void KidneyDebuff()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float shakeDuration = 3f;
        float shakeMagnitude = 75f;
        
        while (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            Vector2 point = Random.insideUnitCircle * shakeMagnitude;
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, point, Time.deltaTime);
            yield return null;
        }
    }
}
