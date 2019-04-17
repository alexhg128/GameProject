using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    public int startingLives;
    private int lifeCounter;
    private Text lifeText;
    private GameObject player;
    public GameObject gameOverScreen;

    void Start()
    {
        lifeText = GetComponent<Text>();
        lifeCounter = startingLives;
        player = GameManager.instance.Player;
    }

    void Update()
    {
        lifeText.text = "x " + lifeCounter;

        if(lifeCounter <= 0)
        {
            gameOverScreen.SetActive(true);
            player.gameObject.SetActive(false);
        }
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
