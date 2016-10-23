using UnityEngine;
using System.Collections;

public class SoundManagement : MonoBehaviour {

    public void PlayOnEnter()
    {
        AudioManager.instance.PlaySound2D("shift");
    }

    public void PlayOnClick()
    {
        AudioManager.instance.PlaySound2D("select");
    }

    public void PlayOnUpgrade()
    {
        AudioManager.instance.PlaySound2D("upgrade");
    }
}
