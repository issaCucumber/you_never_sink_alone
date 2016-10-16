using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Prestige {

    [SerializeField]
    private PrestigeScript prestige;

    [SerializeField]
    private int prestigeVal;

    public int PrestigeVal
    {
        get
        {
            return prestigeVal;
        }

        set
        {
            prestigeVal = value;
            if(prestigeVal < 0)
            {
                prestigeVal = 0;
            }
            prestige.Value = prestigeVal;
        }
    }

    public void Initialize()
    {
        this.PrestigeVal = prestigeVal;
    }
}
