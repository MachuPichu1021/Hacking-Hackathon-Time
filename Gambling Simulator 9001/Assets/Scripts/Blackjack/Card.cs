using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private int _value;
    public int Value { get => _value; set => _value = value; }

    [SerializeField] private GameObject cardBackPrefab;
    private GameObject cardBack;

    private bool isHidden;
    public bool IsHidden { get => isHidden; private set => isHidden = value; }

    public void Hide()
    {
        if (!isHidden)
        {
            cardBack = Instantiate(cardBackPrefab, transform.position + new Vector3(0, 0.01f, 0), cardBackPrefab.transform.rotation);
            isHidden = true;
        }
    }

    public void Show()
    {
        if (isHidden)
        {
            Destroy(cardBack);
            isHidden = false;
        }
    }

    private void OnDestroy()
    {
        Destroy(cardBack);
    }
}
