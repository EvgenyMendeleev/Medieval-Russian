using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

namespace Tour_Novgorod_MedievalRussia
{
    public class DialogueWindow : MonoBehaviour 
    {
        [Header("Dialogue fields")]

        [SerializeField]
        private TMP_Text _characterNameBox;

        [SerializeField]
        private TMP_Text _characterWordsBox;

        [Header("Dialogue controlling")]

        [SerializeField]
        private Button _continueDialogueButton;

        [SerializeField]
        private Button _cancelDialogueButton;

        private Stack<string> _currentDialogue = new Stack<string>();

        public UnityEvent OnDialogueEnded;

        public void StartDialogue(ScenarioContainer scenario)
        {
            _characterNameBox.text = scenario.CharacterName;
            _currentDialogue = new Stack<string>(scenario.CharacterWords.Reverse<string>());
            _cancelDialogueButton.gameObject.SetActive(false);
            _continueDialogueButton.gameObject.SetActive(true);
            _continueDialogueButton.onClick.AddListener(DisplayNextPhrase);

            DisplayNextPhrase();
        }

        private void DisplayNextPhrase()
        {
            if(_currentDialogue == null)
            {
            return; 
            }

            if(_currentDialogue.Count > 1)
            {
                _characterWordsBox.text = _currentDialogue.Pop();
            }
            else if(_currentDialogue.Count == 1)
            {
                _cancelDialogueButton.gameObject.SetActive(true);
                _continueDialogueButton.gameObject.SetActive(false);
                _continueDialogueButton.onClick.RemoveAllListeners();
                _cancelDialogueButton.onClick.AddListener(CancelDialogue);

                _characterWordsBox.text = _currentDialogue.Pop();
            }
        }

        private void CancelDialogue()
        {
            OnDialogueEnded?.Invoke();
            Destroy(gameObject);
        }
    }
}
