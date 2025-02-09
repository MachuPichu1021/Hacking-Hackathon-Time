using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using TMPro;

public class DoctorManager : MonoBehaviour
{
    private Camera camera;
    private GameManager instance;
    [SerializeField] private GameObject confirmation;
    [SerializeField] private Transform organView;
    [SerializeField] private TMP_Text caption;
    private GameObject[] parts;
    private string daPart;
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
                if (parts[i].name.ToLower() == "hand")
                    instance.isHandDebuff = true;
                else if (parts[i].name.ToLower() == "kidney")
                    instance.isKidneyDebuff = true;
                else if (parts[i].name.ToLower() == "leg")
                    instance.isLegDebuff = true;
                GameObject.Destroy(parts[i]);
        }
        if (selecting)
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 1, Time.deltaTime*4);
        if (selecting && camera.orthographicSize<1.1)
        {
            selecting = false;
            organView.gameObject.SetActive(true);
            caption.text = "Choose one to sacrifice.";
        }
    }

    public void transition()
    {
        selecting = true;
    }

    public void select(string part)
    {
        daPart = part;
        confirmation.SetActive(true);
    }

    public void cancel()
    {
        confirmation.SetActive(false);
    }

    public void confirm()
    {
        GameManager.conditions.Add(daPart);
        GameManager.money += 4000;
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].SetActive(false);
        }
        confirmation.SetActive(false);
    }
}
