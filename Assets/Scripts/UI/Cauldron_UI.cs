using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cauldron_UI : MonoBehaviour
{
    public GameObject selected;

    public Herb storedHerb;

    public void SetStoredHerb()
    {

    }

    public void Activate()
    {
        if (storedHerb != null)
        {
            StartCoroutine(UpdateDevice());
        }
    }

    public IEnumerator UpdateDevice()
    {
        yield return new WaitForSeconds(2f);

    }



    public void UseDevice(Herb herb)
    {

    }



}
