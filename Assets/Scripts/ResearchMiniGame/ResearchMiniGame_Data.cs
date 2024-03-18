using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResearchMiniGame", menuName = "ScriptableObjects/ResearchMiniGame", order = 1)]
public class ResearchMiniGame_Data : ScriptableObject
{
    public Herb herb;
    public Vector2 start;
    public Vector2[] end;
    public Vector2[] turn;
    public Vector2[] straight;

}
