using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Text valueText;

    //maybe can use for the indication of recharge
    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color lowColor;

    [SerializeField]
    private bool lerpColor;

    public float MaxValue { get; set; }
    public float Value
    {
        set
        {
            if (valueText != null)
            {
                string[] tmp = valueText.text.Split(':');
                valueText.text = tmp[0] + ": " + value;
            }
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    } 

    // Use this for initialization
    void Start () {
	    if(lerpColor)
        {
            content.color = fullColor;
        }
	}
	
	// Update is called once per frame
	void Update () {
        HandleBar();
	}

    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount,fillAmount, Time.deltaTime * lerpSpeed);
        }

        if(lerpColor)
        {
            content.color = Color.Lerp(lowColor, fullColor, fillAmount);
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
