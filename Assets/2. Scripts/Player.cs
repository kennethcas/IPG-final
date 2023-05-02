using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private ThirdPersonController playerController;
    private PlayerInput playerInput;
    Animator anim;
    private bool hit = false; //has the player been hit
    private bool isAttacking = false;

    private float speed;

    [SerializeField]
    GameObject dialogueBox;
    [SerializeField]
    GameObject dialogueOptionsUI;

    bool attackTrigger;
    private void Awake()
    {
        playerController = gameObject.GetComponent<ThirdPersonController>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        anim = gameObject.GetComponent<Animator>();
        
        if (dialogueBox || dialogueOptionsUI == null)
        {
            Debug.LogWarning("MISSING COMPONENTS ASSIGNED HERE: " + gameObject.name);
        }
    }

    private void Start()
    {
        attackTrigger = anim.GetParameter(2).defaultBool;
    }

    void Update()
    {
        CheckDialogue();
        CheckAttack();
        /*if (attackTrigger)
        {
            
        }*/
    }

    private void FixedUpdate()
    {
        speed = GetComponent<NavMeshAgent>().velocity.magnitude;
        anim.SetFloat("Speed", speed);
    }

    public void CheckDialogue()
    {
        if (dialogueBox.activeSelf || dialogueOptionsUI.activeSelf)
        {
            playerController.enabled = false;
            playerInput.enabled = false;
            anim.SetTrigger("Idle");

        }
        else
        {
            playerController.enabled = true;
            playerInput.enabled = true;
        }

    }

    public void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(); 
        }

        if (hit)
        {
            anim.SetTrigger("Hurt");
        }
    }

    public void Attack()
    {
        isAttacking = true;

        if (isAttacking)
        {
            anim.SetTrigger("Attack");
        }
        else
        {
            anim.ResetTrigger("Attack");
            isAttacking= false;
        }
    }
}
