using System;
using UnityEngine;
using TMPro;

public class DoctorManager : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private Transform organView;
    [SerializeField] private TMP_Text caption;
    private GameObject[] parts;
    private bool selecting;

    void Start()
    {
        parts = new GameObject[organView.childCount];
        for (int i = 0; i < organView.childCount; i++)
        {
            parts[i] = organView.GetChild(i).gameObject;
        }
        camera = GetComponent<Camera>();
        selecting = false;
        camera.orthographicSize = 5;
        caption.text = "You have failed to meet today's quota.";
    }

    private void Update()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] != null && GameManager.conditions.Contains(parts[i].name))
                GameObject.Destroy(parts[i]);
        }
        if (selecting)
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 1, Time.deltaTime*4);
        if (selecting && camera.orthographicSize<1.1)
        {
            selecting = false;
            organView.gameObject.SetActive(true);
            caption.text = "Choose one.";
        }
    }

    public void transition()
    {
        selecting = true;
    }

    public void select(string part)
    {
        GameManager.conditions.Add(part);

    }
}
