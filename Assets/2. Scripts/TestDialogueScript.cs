using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogueScript : MonoBehaviour
{
    public DialogueBase dialogue;

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.EnqueDialogue(dialogue);
    }


}
