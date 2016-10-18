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
    public int levelClear;

    public GameObject leftImage;
    public GameObject middleImage;
    public GameObject rightImage;

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

        leftImage.SetActive(false);
        middleImage.SetActive(false);
        rightImage.SetActive(false);

        if (currDialogueEntityPointer < noOfDialogueInstance)
        {
            nameText.text = dialogueEntity[currDialogueEntityPointer].CharacterName;

            if(dialogueEntity[currDialogueEntityPointer].SpritePosition == 0)
            {
                leftImage.SetActive(true);
                leftImage.GetComponent<Image>().sprite = dialogueEntity[currDialogueEntityPointer].SpriteImage;
            }
            else if(dialogueEntity[currDialogueEntityPointer].SpritePosition == 1)
            {
                middleImage.SetActive(true);
                middleImage.GetComponent<Image>().sprite = dialogueEntity[currDialogueEntityPointer].SpriteImage;
            }
            else if (dialogueEntity[currDialogueEntityPointer].SpritePosition == 2)
            {
                rightImage.SetActive(true);
                rightImage.GetComponent<Image>().sprite = dialogueEntity[currDialogueEntityPointer].SpriteImage;
            }

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
                PlayerPrefs.SetInt(Constants.LEVELCLEARED, levelClear);
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
    private Sprite spriteImage;

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

    public Sprite SpriteImage
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
