using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    [SerializeField]
    private DialogueWindow _dialogueWindow;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnDialogueWindow(Transform spawnPoint)
    {
        DialogueWindow window = Instantiate(_dialogueWindow, spawnPoint);
    }
}