using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static ArrayList conditions = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LegDebuff(float speed)
    {
        speed /= 2;
    }

    public void HandDebuff(float horizontalInput, float verticalInput)
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
            GetComponent<Camera>().transform.localPosition = Vector3.Lerp(GetComponent<Camera>().transform.localPosition, point, Time.deltaTime);
            yield return null;
        }
    }
}
