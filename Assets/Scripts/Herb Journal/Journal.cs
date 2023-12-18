using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Journal", menuName = "ScriptableObjects/Journal", order = 1)]

public class Journal : ScriptableObject
{
    public Dictionary<string, Herb> herbMap = new Dictionary<string, Herb>();
    public List<Herb> herbOrder = new List<Herb>();


    



    

    
}
