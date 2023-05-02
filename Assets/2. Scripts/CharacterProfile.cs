using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[CreateAssetMenu(fileName = "New Profile", menuName = "Character Profile")]
public class CharacterProfile : ScriptableObject
{
    public string c_name;
    private Sprite c_portrait;
    public AudioClip c_voice;
    public TMP_FontAsset c_font;
    
    public Sprite C_portrait
    {
        get
        {
            SetEmotionType(Emotion);
            return c_portrait; //returns private sprite
        }
    }
    [System.Serializable] //see in inspector
    public class EmotionPortraits
    {
        public Sprite standard;
        public Sprite happy;
        public Sprite angry;
    }

    public EmotionPortraits emotionPortraits;

    public EmotionType Emotion { get; set; } //hide in inspector

    public void SetEmotionType(EmotionType newEmotion)
    {
        Emotion = newEmotion;
        switch (Emotion)
        {
            case EmotionType.Standard:
                c_portrait = emotionPortraits.standard;
                break;
            case EmotionType.Happy:
                c_portrait = emotionPortraits.happy;
                break;
            case EmotionType.Angry:
                c_portrait = emotionPortraits.angry;
                break;

        }
    }

}

public enum EmotionType
{
    Standard,
    Happy,
    Angry
}
