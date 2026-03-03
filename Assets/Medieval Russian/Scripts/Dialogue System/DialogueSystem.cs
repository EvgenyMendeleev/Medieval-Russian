using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    [SerializeField]
    private DialogueWindow _dialogueWindow;

    private DialogueWindow _spawnedDialogueWindow;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public DialogueWindow SpawnDialogueWindow(Transform spawnPoint)
    {
        _spawnedDialogueWindow = Instantiate(_dialogueWindow, spawnPoint.position, spawnPoint.rotation);
        return _spawnedDialogueWindow;
    }
}