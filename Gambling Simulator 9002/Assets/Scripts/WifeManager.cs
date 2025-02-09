using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WifeManager : MonoBehaviour
{
    [SerializeField] private DialogueObject[] mainBranches;
    [SerializeField] private DialogueUI dialogueUI;

    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Image bgImage;

    private void Start()
    {
        int dayIndex = GameManager.instance.Day - 1;
        bgImage.sprite = backgrounds[dayIndex];
        dialogueUI.ShowDialogue(mainBranches[dayIndex]);
    }

    private void Update()
    {
        if (dialogueUI.IsClosed() && Time.timeSinceLevelLoad > 1)
            GameManager.instance.LoadScene(3);
    }
}
