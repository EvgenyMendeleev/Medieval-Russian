using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario Container", menuName = "Dialogue System/Scenario Container", order = 0)]
public class ScenarioContainer : ScriptableObject
{
    [SerializeField]
    private string _characterName;

    //TODO: Сделать массив объектов, которые хранят выборы ответов и переход к другим диалогам
    [SerializeField] 
    [Multiline]
    private List<string> _characterWords;

    public string CharacterName { get { return _characterName; } }
    public List<string> CharacterWords { get { return _characterWords; } }
}