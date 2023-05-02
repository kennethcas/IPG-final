using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Dialogue Text", menuName = "Dialogue Texts")]

public class DialogueBase : ScriptableObject
{
    [System.Serializable]
    public class DialogueInfo
    {
        public CharacterProfile character;
        [TextArea(4, 8)]
        public string c_text;

        public EmotionType characterEmotion;

        public void ChangeEmotion()
        {
            character.Emotion = characterEmotion;
        }
    }

    [Header("Insert Dialogue Info Below")]
    public DialogueInfo[] dialogueInfo;
    
    
}
