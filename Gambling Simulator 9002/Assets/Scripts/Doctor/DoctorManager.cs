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
    private Part[] parts;
    Part daPart;
    private bool selecting;

    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject text1;
    [SerializeField] private DialogueObject text2;

    void Start()
    {
        parts = new Part[organView.childCount];
        for (int i = 0; i < organView.childCount; i++)
        {
            parts[i] = organView.GetChild(i).GetComponent<Part>();
        }
        camera = GetComponent<Camera>();
        selecting = false;
        camera.orthographicSize = 5;
        dialogueUI.ShowDialogue(text1);
    }

    private void Update()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i] != null && PartManager.instance.Conditions.Contains(parts[i]))
                Destroy(parts[i].gameObject);
        }
        if (selecting)
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 1, Time.deltaTime * 4);
        if (selecting && camera.orthographicSize < 1.1f)
        {
            selecting = false;
            organView.gameObject.SetActive(true);
            dialogueUI.ShowDialogue(text2);
        }
    }

    public void transition()
    {
        selecting = true;
    }

    public void select(Part part)
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
        PartManager.instance.AddCondition(daPart);
        GameManager.instance.Money += daPart.Money;
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].gameObject.SetActive(false);
        }
        confirmation.SetActive(false);
        GameManager.instance.LoadScene(7);
    }
}
