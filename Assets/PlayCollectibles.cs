using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCollectibles : MonoBehaviour
{
    public Text textComponent;
    public int gemNumber = 0;
    void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        textComponent.text = gemNumber.ToString();
    }

    public void GemCollected()
    {
        gemNumber++;
        UpdateText();
    }

}
