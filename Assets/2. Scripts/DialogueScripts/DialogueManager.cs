using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private readonly List<char> punCharas = new List<char>
    {
        ',',
        '.',
        '!',
        '?'
    };

    //singleton
    public static DialogueManager instance;

    //DIALOGUE
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public Image dialoguePortrait;
    public float delay = 0.001f;
    
    //FIFO COLLECTION ; FIRST IN FIRST OUT
    public Queue<DialogueBase.DialogueInfo> dialogueInfo = new Queue<DialogueBase.DialogueInfo>();

    //OPTIONS
    private bool isDialogueOption;
    public GameObject dialogueOptionsUI;
    public bool inDialogue;
    public GameObject[] optionButtons;
    private int optionsCount;
    public TextMeshProUGUI questionText;

    private bool isCurrentlyTyping;
    private string completeText;


    private void Awake()
    {
        //SINGLETON
        if (instance != null)
            Debug.LogWarning("fix this: " + gameObject.name);
        else
            instance = this;

        dialogueBox.SetActive(false);
        dialogueOptionsUI.SetActive(false);
    }
    public void EnqueDialogue(DialogueBase db)
    {
        if (inDialogue) return;
        inDialogue= true;

        dialogueBox.SetActive(true);
        dialogueInfo.Clear();

        OptionsParser(db);

        foreach (DialogueBase.DialogueInfo info in db.dialogueInfo) 
            dialogueInfo.Enqueue(info);

        DequeDialogue();
    }

    public void DequeDialogue()
    {
        if (isCurrentlyTyping)
        {
            CompleteText();
            StopAllCoroutines(); //stops typetext coroutine //StopCoroutine(TypeText)
            isCurrentlyTyping = false;
            return;
        }

        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        DialogueBase.DialogueInfo info = dialogueInfo.Dequeue();
        completeText = info.c_text;


        dialogueName.text = info.character.c_name;
        dialogueText.text = info.c_text;
        dialogueText.font = info.character.c_font;
        info.ChangeEmotion(); //changes emotion before calling portrait
        dialoguePortrait.sprite = info.character.C_portrait;

        dialogueText.text = "";
        StartCoroutine(TypeText(info));
    }

    private bool CheckPunctuation(char c)
    {
        if (punCharas.Contains(c))
            return true;
        else
            return false;
        
    }

    //types one letter at a time
    IEnumerator TypeText(DialogueBase.DialogueInfo info)
    {
        isCurrentlyTyping = true;
        foreach(char c in info.c_text.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text+= c;
            AudioManager.instance.PlayClip(info.character.c_voice);

            if (CheckPunctuation(c))
            {
                yield return new WaitForSeconds(0.25f);
            }
        }
        isCurrentlyTyping = false;
    }

    private void CompleteText()
    {
        dialogueText.text = completeText;
    }
    public void EndOfDialogue()
    {
        dialogueBox.SetActive(false);
        OptionsLogic();
    }
    private void OptionsLogic()
    {
        if (isDialogueOption)
        {
            dialogueOptionsUI.SetActive(true);
            
        }
        else
        {
            inDialogue = false;
        }
    }

    public void CloseOptions()
    {
        dialogueOptionsUI.SetActive(false);
    }

    private void OptionsParser(DialogueBase db)
    {
        if (db is DialogueOptions)
        {
            isDialogueOption = true;
            DialogueOptions dialogueOptions = db as DialogueOptions;
            optionsCount = dialogueOptions.dialogueInfo.Length + 1;
            questionText.text = dialogueOptions.questionText;

            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(false);

            }
            for (int i = 0; i < optionsCount; i++)
            {
                optionButtons[i].SetActive(true);
                optionButtons[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text =
                    dialogueOptions.optionsInfo[i].buttonName;
                UnityEventHandler myEventHandler = optionButtons[i].GetComponent<UnityEventHandler>();
                myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;
                if (dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;

                }
                else
                {
                    myEventHandler.myDialogue = null;
                }
            }

        }
        else
        {
            isDialogueOption = false;
        }
    }
}
