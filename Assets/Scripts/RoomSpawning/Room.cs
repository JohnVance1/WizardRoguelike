using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum OpenDir
{
    None = 0,
    L = 1,
    R = 2,
    U = 4,
    D = 8

}

public class Room : MonoBehaviour
{
    public OpenDir openDirections;

}
