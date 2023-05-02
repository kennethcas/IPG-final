using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetNextLine();
        }
    }
    public void GetNextLine()
    {
        DialogueManager.instance.DequeDialogue();
    }
}
