using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfficeManager : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Image bgImage;

    private void Start()
    {
        int dayIndex = GameManager.instance.Day - 1;
        bgImage.sprite = backgrounds[dayIndex];
    }
}
