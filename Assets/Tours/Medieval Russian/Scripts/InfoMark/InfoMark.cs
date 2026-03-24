using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMark : MonoBehaviour
{
    [SerializeField]
    private ScenarioContainer characterInfo;

    [SerializeField]
    private Transform _spawnPoint;

    private void Start()
    {
        var button = GetComponentInChildren<Button>();
        button.onClick.AddListener(StartDialogue);
    }

    public void StartDialogue()
    {
        var dialogueWindow = DialogueSystem.Instance.SpawnDialogueWindow(_spawnPoint);
        dialogueWindow.OnDialogueEnded.AddListener(() =>
        {
            gameObject.SetActive(true);
        });
        dialogueWindow.StartDialogue(characterInfo);
        gameObject.SetActive(false);
    }
}
