using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    public int startingLives;
    private int lifeCounter;
    private Text lifeText;

    void Start()
    {
        lifeText = GetComponent<Text>();
        lifeCounter = startingLives;
    }

    void Update()
    {
        lifeText.text = "x " + lifeCounter;
    }

    public void GiveLife()
    {
        lifeCounter++;
    }

    public void TakeLife()
    {
        lifeCounter--;
    }
}
