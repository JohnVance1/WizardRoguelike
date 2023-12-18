using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    public event Action onHerbCollected;

    public void HerbCollected()
    {
        if (onHerbCollected != null)
        {
            onHerbCollected();
        }

    }

    


}
