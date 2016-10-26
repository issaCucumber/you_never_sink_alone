using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TeamUtility.IO;
using UnityEngine.SceneManagement;

public class CutSceneManager2 : MonoBehaviour {
    public Text nameText;
    public Text dialogueText;
    public float textSpeed;
    public string nextScene;
    public int levelClear;

    public GameObject leftImage;
    public GameObject middleImage;
    public GameObject rightImage;
    public GameObject nextImage;
    public GameObject backGround;

    public GameObject[] waves;
    public GameObject[] lightnings;
    public GameObject rain;

    public DialogueEntity2[] dialogueEntity;

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
    void Start()
    {

        noOfDialogueInstance = dialogueEntity.Length;
        defaultTextSpeed = textSpeed;
        fastTextSpeed = textSpeed / 100;

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        leftImage.SetActive(false);
        middleImage.SetActive(false);
        rightImage.SetActive(false);
        nextImage.SetActive(false);

        if (currDialogueEntityPointer < noOfDialogueInstance)
        {
            nameText.text = dialogueEntity[currDialogueEntityPointer].CharacterName;

            HandleSound(dialogueEntity[currDialogueEntityPointer]);
            HandleImage(dialogueEntity[currDialogueEntityPointer]);
            HandleWave(dialogueEntity[currDialogueEntityPointer]); //if there is wave
            HandleLightning(dialogueEntity[currDialogueEntityPointer]);
            HandleRain(dialogueEntity[currDialogueEntityPointer]);
            

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

                //if (InputManager.GetButtonDown("Submit"))
                if (InputManager.GetKeyDown(KeyCode.Space) || InputManager.GetKeyDown(KeyCode.Joystick1Button2))
                {
                    defaultTextSpeed = fastTextSpeed;
                }
            }
            else
            {
                nextImage.SetActive(true);
                //if (InputManager.GetButtonDown("Submit"))
                if (InputManager.GetKeyDown(KeyCode.Space) || InputManager.GetKeyDown(KeyCode.Joystick1Button2))
                {
                    defaultTextSpeed = textSpeed;
                    dialogue = "";
                    currDialogueEntityPointer++;
                    currChar = 0;
                    once = true;
                    if (currDialogueEntityPointer == noOfDialogueInstance)
                    {
                        PlayerPrefs.SetInt(Constants.LEVELCLEARED, levelClear);
                        SceneManager.LoadScene(nextScene);
                    }
                }
            }
        }
        //else
        //{
        //    if (InputManager.GetButtonDown("Submit"))
        //    {
        //        PlayerPrefs.SetInt(Constants.LEVELCLEARED, levelClear);
        //        SceneManager.LoadScene(nextScene);
        //    }
        //}

    }

    private void HandleImage(DialogueEntity2 currDialogueEntity)
    {
        if (currDialogueEntity.SpritePosition == 0)
        {
            leftImage.SetActive(true);
            leftImage.GetComponent<Image>().sprite = currDialogueEntity.SpriteImage;
        }
        else if (currDialogueEntity.SpritePosition == 1)
        {
            middleImage.SetActive(true);
            middleImage.GetComponent<Image>().sprite = currDialogueEntity.SpriteImage;
        }
        else if (currDialogueEntity.SpritePosition == 2)
        {
            rightImage.SetActive(true);
            rightImage.GetComponent<Image>().sprite = currDialogueEntity.SpriteImage;
        }

        if (currDialogueEntity.Background != null)
        {
            backGround.GetComponent<Image>().enabled = true;
            backGround.GetComponent<Image>().sprite = currDialogueEntity.Background;
        }
        else
        {
            backGround.GetComponent<Image>().enabled = false;
        }
    }

    private void HandleWave(DialogueEntity2 currDialogueEntity)
    {
        if (currDialogueEntity.Wave) //if there is wave
        {
            foreach (GameObject wave in waves)
            {
                wave.SetActive(true);
                GenerateWaveScript generateWave = wave.GetComponent<GenerateWaveScript>();
                generateWave.heightScale = currDialogueEntity.HeightScale;
                generateWave.detailScale = currDialogueEntity.DetailScale;
                generateWave.wavesSpeed = currDialogueEntity.WaveSpeed;
            }
        }
        else
        {
            foreach (GameObject wave in waves)
            {
                wave.SetActive(false);
            }
        }
    }

    private void HandleLightning(DialogueEntity2 currDialogueEntity)
    {
        if(currDialogueEntity.Lightning)
        {
            foreach(GameObject lightning in lightnings)
            {
                if (currDialogueEntity.LightningDuration > 0)
                {
                    if (Time.time - startTime < currDialogueEntity.LightningDuration)
                    {
                        lightning.SetActive(true);
                    }
                    else
                    {
                        lightning.SetActive(false);
                    }
                }
                else
                {
                    lightning.SetActive(true);
                }
                
            }
        }
        else
        {
            foreach(GameObject lightning in lightnings)
            {
                lightning.SetActive(false);
            }
        }
    }

    private void HandleRain(DialogueEntity2 currDialogueEntity)
    {
        if (currDialogueEntity.Rain)
        {
            rain.SetActive(true);
            WindZone windZone = rain.GetComponent<WindZone>();
            windZone.windMain = currDialogueEntity.WindSpeed;
            windZone.windPulseMagnitude = currDialogueEntity.WindSpeed / 2;
        }
        else
        {
            rain.SetActive(false);
        }
    }
    bool once = true;
    private void HandleSound(DialogueEntity2 currDialogueEntity)
    {
        if (currDialogueEntity.StopMusic)
        {
            AudioManager.instance.StopMusic();
        }

        if (currDialogueEntity.StopSound)
        {
            AudioManager.instance.StopSoundFx();
        }

        if (currDialogueEntity.AudioSfx != null && once)
        {
            AudioManager.instance.PlaySound2D(currDialogueEntity.AudioSfx);
            once = false;

        }
        
        if(currDialogueEntity.Music != null)
        {
            AudioManager.instance.PlayMusic(currDialogueEntity.Music);
        }

        
    }
}

[Serializable]
public class DialogueEntity2
{
    [SerializeField]
    private string characterName;
    [SerializeField]
    private string dialogue;
    [SerializeField]
    private int spritePosition;
    [SerializeField]
    private Sprite spriteImage;
    [SerializeField]
    private Sprite background;
    [SerializeField]
    private bool wave;
    [SerializeField]
    private float waveSpeed;
    [SerializeField]
    private float heightScale;
    [SerializeField]
    private float detailScale;
    [SerializeField]
    private bool lightning;
    [SerializeField]
    private int noOfLightning;
    [SerializeField]
    private float lightningDuration;
    [SerializeField]
    private bool rain;
    [SerializeField]
    private float windSpeed;
    [SerializeField]
    private AudioClip audioSfx;
    [SerializeField]
    private Boolean stopSound;
    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private Boolean stopMusic;

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

    public Sprite Background
    {
        get
        {
            return background;
        }
        set
        {
            background = value;
        }
    }

    public bool Wave
    {
        get
        {
            return wave;
        }

        set
        {
            wave = value;
        }
    }

    public float WaveSpeed
    {
        get
        {
            return waveSpeed;
        }

        set
        {
            waveSpeed = value;
        }
    }

    public float HeightScale
    {
        get
        {
            return heightScale;
        }

        set
        {
            heightScale = value;
        }
    }

    public float DetailScale
    {
        get
        {
            return detailScale;
        }

        set
        {
            detailScale = value;
        }
    }

    public bool Lightning
    {
        get
        {
            return lightning;
        }

        set
        {
            lightning = value;
        }
    }

    public bool Rain
    {
        get
        {
            return rain;
        }

        set
        {
            rain = value;
        }
    }

    public float WindSpeed
    {
        get
        {
            return windSpeed;
        }

        set
        {
            windSpeed = value;
        }
    }

    public int NoOfLightning
    {
        get
        {
            return noOfLightning;
        }

        set
        {
            noOfLightning = value;
        }
    }

    public float LightningDuration
    {
        get
        {
            return lightningDuration;
        }

        set
        {
            lightningDuration = value;
        }
    }

    public AudioClip Music
    {
        get
        {
            return music;
        }

        set
        {
            music = value;
        }
    }

    public bool StopMusic
    {
        get
        {
            return stopMusic;
        }

        set
        {
            stopMusic = value;
        }
    }

    public bool StopSound
    {
        get
        {
            return stopSound;
        }

        set
        {
            stopSound = value;
        }
    }

    public AudioClip AudioSfx
    {
        get
        {
            return audioSfx;
        }

        set
        {
            audioSfx = value;
        }
    }
}

