using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;
    public DialogueObject DialogueObject { get => dialogueObject; }
    [SerializeField] private ResponseEvent[] responseEvents;
    public ResponseEvent[] ResponseEvents { get => responseEvents; }

    public void OnValidate()
    {
        if (dialogueObject == null || dialogueObject.Responses == null)
            return;
        if (responseEvents != null && responseEvents.Length == dialogueObject.Responses.Length)
            return;

        if (responseEvents == null)
            responseEvents = new ResponseEvent[dialogueObject.Responses.Length];
        else
            Array.Resize(ref responseEvents, dialogueObject.Responses.Length);

        for (int i = 0; i < dialogueObject.Responses.Length; i++)
        {
            Response response = dialogueObject.Responses[i];
            if (responseEvents[i] != null)
            {
                responseEvents[i].name = response.ResponseText;
                continue;
            }

            responseEvents[i] = new ResponseEvent() { name = response.ResponseText };
        }
    }
}
