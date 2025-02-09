using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerDialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject normalText;
    [SerializeField] private DialogueObject finalDayText;

    private void Start()
    {
        if (GameManager.instance.Day != 5)
            dialogueUI.ShowDialogue(normalText);
        else
            dialogueUI.ShowDialogue(finalDayText);
    }

    private void Update()
    {
        if (dialogueUI.IsClosed() && Time.timeSinceLevelLoad > 1)
            GameManager.instance.LoadScene(4);
    }
}
