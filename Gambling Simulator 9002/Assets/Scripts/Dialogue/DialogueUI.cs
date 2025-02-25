using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private GameObject nextTextHint;

    private ResponseHandler handler;
    private TypeWriterEffect typeWriter;

    [SerializeField] private DialogueObject testDialogue;

    private void Start()
    {
        handler = GetComponent<ResponseHandler>();
        typeWriter = GetComponent<TypeWriterEffect>();

        CloseDialogueBox();

        if (testDialogue)
            ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        handler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            nextTextHint.SetActive(false);
            string dialogue = dialogueObject.Dialogue[i];
            textLabel.font = dialogueObject.Fonts[i];
            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses())
                break;

            nextTextHint.SetActive(true);
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses())
            handler.ShowResponses(dialogueObject.Responses);
        else
            CloseDialogueBox();
    }
    
    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriter.Run(dialogue, textLabel);

        while (typeWriter.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
                typeWriter.Stop();
        }
    }

    public void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = "";
    }

    public bool IsClosed()
    {
        return !dialogueBox.activeInHierarchy;
    }
}
