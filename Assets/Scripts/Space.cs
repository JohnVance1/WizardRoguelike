using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpaceType
{
    None,
    Black,
    White,

}

public class Space : MonoBehaviour
{
    public int x;
    public int y;
    public List<Space> Edges { get; set; }

    public SpaceType type { get; private set; }

    public bool IsError { get; private set; }

    private void Start()
    {
        Edges = new List<Space>();
    }

    private void Update()
    {
        if (type == SpaceType.Black)
        {

        }
        else if(type == SpaceType.White)
        {

        }
    }

}
