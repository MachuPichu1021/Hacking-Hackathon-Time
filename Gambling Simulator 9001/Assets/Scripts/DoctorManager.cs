using System;
using UnityEngine;
using TMPro;

public class DoctorManager : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private Transform organView;
    [SerializeField] private TMP_Text caption;
    private bool selecting;

    void Start()
    {
        camera = GetComponent<Camera>();
        selecting = false;
        camera.orthographicSize = 5;
        caption.text = "You have failed to meet today's quota.";
    }

    private void Update()
    {
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
}
