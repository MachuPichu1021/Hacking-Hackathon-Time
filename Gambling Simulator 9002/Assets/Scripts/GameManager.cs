using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static ArrayList conditions = new ArrayList();
    public bool isHandDebuff = false;
    public bool isLegDebuff = false;
    public bool isKidneyDebuff = false;
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

    public void HandDebuff(ref KeyCode hit, ref KeyCode stand, ref KeyCode Doubled)
    {
        hit = KeyCode.Space;
        stand = KeyCode.D;
        Doubled = KeyCode.E;
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
