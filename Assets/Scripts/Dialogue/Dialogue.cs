using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public string[] lines;
    public float textSpeed;

    private int index;

    public delegate void EndOfDialogueDelegate();
    public static event EndOfDialogueDelegate OnEnd;


    void Start()
    {
        textUI.text = string.Empty;
        //StartDialogue();

    }

    void OnEnable()
    {
        textUI.text = string.Empty;
        //StartDialogue();

    }

    private void OnDisable()
    {
        textUI.text = string.Empty;
        StopAllCoroutines();

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            CallDialogue();
        }
    }

    public void CallDialogue()
    {
        if (textUI.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textUI.text = lines[index];
        }
    }

    /// <summary>
    /// Starts the line of Dialogue
    /// </summary>
    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    /// <summary>
    /// Displays each character in a line one by one until the line is complete
    /// </summary>
    /// <returns></returns>
    public IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textUI.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    /// <summary>
    /// Goes to the next line of dialogue if there is one or it
    /// turns off the dialogue box if the player is on the last line
    /// </summary>
    public void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textUI.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            OnEnd();
            gameObject.SetActive(false);
        }
    }

}
