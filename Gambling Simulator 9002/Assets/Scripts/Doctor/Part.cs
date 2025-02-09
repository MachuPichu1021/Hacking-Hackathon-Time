using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Part : MonoBehaviour
{
    [SerializeField] private int money;
    public int Money { get => money; }
    [SerializeField] private new string name;
    public string Name { get => name; set => name = value; }
}
