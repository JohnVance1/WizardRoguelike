using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Journal", menuName = "ScriptableObjects/Journal", order = 1)]

public class Journal : ScriptableObject
{
    public List<Herb> foundHerbs = new List<Herb>();
    public Journal_UI ui;

    public void AddHerb(Herb herb)
    {
        if (!foundHerbs.Contains(herb))
        {
            foundHerbs.Add(herb);
        }
    }

    public bool Contains(Herb herb)
    {
        return foundHerbs.Contains(herb);
    }
}
