using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class NPCBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject triggerSprite;

    [SerializeField]

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowDialogue();
        }
        
    }

    public void ShowDialogue()
    {
        if (triggerSprite != null)
        {
            triggerSprite.SetActive(true);
        }

    }
}
