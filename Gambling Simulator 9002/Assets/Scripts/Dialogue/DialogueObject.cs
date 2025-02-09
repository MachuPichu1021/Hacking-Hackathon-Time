using System;
using UnityEngine;
using TMPro;


[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] string[] dialogue;
    public string[] Dialogue { get => dialogue; }

    [SerializeField] private TMP_FontAsset[] fonts;
    public TMP_FontAsset[] Fonts { get => fonts; }

    [SerializeField] private Response[] responses;
    public Response[] Responses { get => responses; }

    private void OnValidate()
    {
        Array.Resize(ref fonts, dialogue.Length);
    }

    public bool HasResponses()
    {
        return responses != null && responses.Length > 0;
    }
}
