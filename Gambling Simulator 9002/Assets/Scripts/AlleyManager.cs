using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyManager : MonoBehaviour
{
    [SerializeField] private DialogueObject finalDayText;
    [SerializeField] private DialogueObject normalText;
    [SerializeField] private DialogueUI dialogueUI;

    private void Start()
    {
        StartCoroutine(WaitToLoad());
    }

    private IEnumerator WaitToLoad()
    {
        yield return new WaitForEndOfFrame();
        if (GameManager.instance.Day == 5)
            dialogueUI.ShowDialogue(finalDayText);
        else
            dialogueUI.ShowDialogue(normalText);
    }

    private void Update()
    {
        if (dialogueUI.IsClosed() && Time.timeSinceLevelLoad > 1)
            GameManager.instance.LoadScene(7);

    }
}
