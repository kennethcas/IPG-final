using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class NPCBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject triggerSprite;
    [SerializeField]
    GameObject dialogueBox;
    [SerializeField]
    GameObject dialogueOptionsUI;

    [SerializeField]
    DialogueBase dialogue; //dialogue to be queued

    void Update()
    {
        CheckHideSprite(triggerSprite);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialogue();
        }
    }
    public void ShowDialogue()
    {
        DialogueManager.instance.EnqueDialogue(dialogue);

    }

    public void CheckHideSprite(GameObject sprite)
    {
        //if in dialogue, hide trigger sprite. when out of dialogue, show it again.

        if (dialogueOptionsUI.activeInHierarchy || dialogueBox.activeInHierarchy)
        {
            sprite.SetActive(false);
        }
        else
        {
            sprite.SetActive(true);
        }
    }
}
