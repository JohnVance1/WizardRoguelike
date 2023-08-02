using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Quests/AskForPotion", order = 1)]
public class Quest : ScriptableObject
{
    public string Name;
    public bool Completed;
    public bool Active;

}
