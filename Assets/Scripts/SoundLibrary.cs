﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour {

    public SoundGroup[] soundGroups;

    Dictionary<string, AudioClip> groupDictionary = new Dictionary<string, AudioClip>();

    void Awake()
    {
        foreach(SoundGroup soundGroup in soundGroups)
        {
            groupDictionary.Add(soundGroup.groupID, soundGroup.group);
        }
    }

    public AudioClip GetClipFromName(string name)
    {
        if (groupDictionary.ContainsKey(name))
        {
            return groupDictionary[name];
        }
        return null;
    }

    [System.Serializable]
    public class SoundGroup
    {
        public string groupID;
        public AudioClip group;
    }

    

}
