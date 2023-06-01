using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Device : MonoBehaviour
{
    public bool IsActive;
    public bool IsFinished;
    public Herb storedHerb;
    public ProcessType type;

    public delegate void OnFinishedDevice(Herb herb, ProcessType type);
    public OnFinishedDevice onFinishedDevice;

    public void Activate(Herb herb)
    {
        IsFinished = false;
        storedHerb = Object.Instantiate(herb);

        if (storedHerb != null && !IsActive)
        {
            IsActive = true;
            StartCoroutine(UpdateDevice());
        }
    }

    public IEnumerator UpdateDevice()
    {
        //  Play animation here
        yield return new WaitForSeconds(2f);
        IsActive = false;
        IsFinished = true;
        onFinishedDevice(storedHerb, type);
        RemoveOnClick();

    }

    public void ChangeOnClick(Herb herb)
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => Activate(herb));

    }

    public void RemoveOnClick()
    {
        storedHerb = null;
        GetComponent<Button>().onClick.RemoveAllListeners();
        IsFinished = false;

    }


}
