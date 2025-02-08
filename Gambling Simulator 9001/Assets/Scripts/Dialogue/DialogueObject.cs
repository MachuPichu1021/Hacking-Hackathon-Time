using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] string[] dialogue;
    public string[] Dialogue { get => dialogue; }

    [SerializeField] private Response[] responses;
    public Response[] Responses { get => responses; }

    public bool HasResponses()
    {
        return responses != null && responses.Length > 0;
    }
}
