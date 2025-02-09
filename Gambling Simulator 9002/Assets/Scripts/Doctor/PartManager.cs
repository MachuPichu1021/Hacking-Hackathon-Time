using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartManager : MonoBehaviour
{
    public static PartManager instance;

    [SerializeField] private List<Part> conditions = new List<Part>();
    public List<Part> Conditions { get => conditions; }

    private const float kidneyCooldown = 7.5f;
    private float kidneyTimer = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (HasCondition("kidney"))
        {
            if (kidneyTimer <= 0)
            {
                StartCoroutine(Shake());
                kidneyTimer = kidneyCooldown;
            }
            kidneyTimer -= Time.deltaTime;
        }
    }

    public void AddCondition(Part part)
    {
        GameObject child = Instantiate(new GameObject(), transform);
        Part p = child.AddComponent<Part>();
        p.Name = part.Name;

        conditions.Add(p);
    }

    public bool HasCondition(string partName)
    {
        return conditions.Any(part => part.name.ToLower() == partName);
    }

    private IEnumerator Shake()
    {
        float shakeDuration = 3;
        float shakeMagnitude = 7.5f;

        while (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            Vector2 point = Random.insideUnitCircle * shakeMagnitude;
            Camera.main.transform.localPosition = Vector3.Lerp(GetComponent<Camera>().transform.localPosition, point, Time.deltaTime);
            yield return null;
        }
    }
}
