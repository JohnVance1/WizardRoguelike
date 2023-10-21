using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputEvents
{
    public event Action onSubmitPressed;

    public void SubmitPressed()
    {
        if (onSubmitPressed != null)
        {
            onSubmitPressed();
        }

    }

    public event Action onHerbSelected;

    public void HerbSelected()
    {
        if (onHerbSelected != null)
        {
            onHerbSelected();
        }

    }


}
