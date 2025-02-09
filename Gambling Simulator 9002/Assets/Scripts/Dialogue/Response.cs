using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;

    public string ResponseText { get => responseText; }

    [SerializeField] private DialogueObject dialogueObject;
    public DialogueObject DialogueObject { get => dialogueObject; }
}
