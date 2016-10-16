using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TeamUtility.IO;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public float textSpeed;
    public string nextScene;

    public DialogueEntity[] dialogueEntity;

    private char[] charArray;
    private int charArrayLength;
    private int currChar = 0;
    private int currDialogueEntityPointer = 0;
    private int noOfDialogueInstance;
    private float startTime;
    private string dialogue;
    private float defaultTextSpeed;
    private float fastTextSpeed;

	// Use this for initialization
	void Start () {

        noOfDialogueInstance = dialogueEntity.Length;
        defaultTextSpeed = textSpeed;
        fastTextSpeed = textSpeed / 100;

        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (currDialogueEntityPointer < noOfDialogueInstance)
        {
            nameText.text = dialogueEntity[currDialogueEntityPointer].CharacterName;
            if (currChar == 0)
            {
                charArray = dialogueEntity[currDialogueEntityPointer].Dialogue.ToCharArray();
                charArrayLength = charArray.Length;
            }

            if (currChar < charArrayLength)
            {
                if (Time.time - startTime > defaultTextSpeed)
                {
                    dialogue += charArray[currChar];
                    dialogueText.text = dialogue;
                    currChar++;
                    startTime = Time.time;
                }

                if (InputManager.GetButtonDown("Submit"))
                {
                    defaultTextSpeed = fastTextSpeed;
                }
            }
            else
            {
                if (InputManager.GetButtonDown("Submit"))
                {
                    defaultTextSpeed = textSpeed;
                    dialogue = "";
                    currDialogueEntityPointer++;
                    currChar = 0;
                }
            }
        }
        else
        {
            if (InputManager.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene(nextScene);
            }
        }

	}
}

[Serializable]
public class DialogueEntity
{
    [SerializeField]
    private string characterName;
    [SerializeField]
    private string dialogue;
    [SerializeField]
    private int spritePosition;
    [SerializeField]
    private int spriteImage;

    public string CharacterName
    {
        get
        {
            return characterName;
        }

        set
        {
            characterName = value;
        }
    }

    public string Dialogue
    {
        get
        {
            return dialogue;
        }

        set
        {
            dialogue = value;
        }
    }

    public int SpritePosition
    {
        get
        {
            return spritePosition;
        }

        set
        {
            spritePosition = value;
        }
    }

    public int SpriteImage
    {
        get
        {
            return spriteImage;
        }

        set
        {
            spriteImage = value;
        }
    }
}
