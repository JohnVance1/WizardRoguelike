using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalEvents
{
    public event Action<Herb> onFirstherbCollected;

    public void FirstHerbCollected(Herb herb)
    {
        if (onFirstherbCollected != null)
        {
            onFirstherbCollected(herb);
        }

    }
}
