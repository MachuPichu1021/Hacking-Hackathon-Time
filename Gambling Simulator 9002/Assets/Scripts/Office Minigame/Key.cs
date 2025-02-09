using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour
{
    private string key = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    [SerializeField] private char character;
    private KeyCode keyToPress;
    public KeyCode KeyToPress { get => keyToPress; }
    
    [SerializeField] private TMP_Text KeyText;
    void Start()
    {
        character = key[Random.Range(0, key.Length)];
        keyToPress = (KeyCode)System.Enum.Parse(typeof(KeyCode), character.ToString());
        KeyText.text = character.ToString();
    }
}
