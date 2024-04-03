using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerShip _player;
    [SerializeField] private PlayerInventory inventory;

    [Header("GUI")]
    [SerializeField] private GameObject playerGUI;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI asteroidCount;
    [SerializeField] private TextMeshProUGUI mayoCount;
    [SerializeField] private TextMeshProUGUI boostCount;

    [Header("Lose Message")]
    [SerializeField] GameObject winLoseGUI;
    [SerializeField] private TextMeshProUGUI winLoseText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Instructions")]
    [SerializeField] GameObject tutGUI;

    private float timeOut = 90;
    private TimeSpan timeSpan;
    private bool gameStart = false;

    private void DrawGUI(){
        timeOut = Mathf.Max(0, timeOut - Time.deltaTime);
        timeSpan = TimeSpan.FromSeconds(timeOut);
        timer.text = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + ":" + timeSpan.Milliseconds.ToString("00");

        asteroidCount.text = inventory.asteroidCount.ToString();// + "/" + inventory.possibleAsteroids.ToString();
        mayoCount.text = inventory.mayoCollected.ToString();
        boostCount.text = inventory.boostsCollected.ToString(); ;
       

        if (timeSpan.Seconds > 60) {
            timer.color = Color.red;
        }
    }

    private void DrawDeathDisplay(){
        winLoseText.text = "You Blew Up! \n Hit BackSpace to Play Again!";
        winLoseText.color = Color.red;

        if(inventory.asteroidCount > 0){
            scoreText.text = "You Destroyed: " + inventory.asteroidCount.ToString() + " Asteroids!";
        }
        else{ 
            scoreText.text = "You Destroyed: " + inventory.asteroidCount.ToString() + " Asteroids! \n No, the one that killed you doesn't count";
        }
    }
    private void DrawTimeOutDisplay() {
        winLoseText.text = "You Didn't Make It in Time! \n Hit BackSpace to Play Again!";
        winLoseText.color = Color.red;
        scoreText.text = "You Destroyed: " + inventory.asteroidCount.ToString() + " Asteroids!";


    }
    private void DrawWinDisplay(){
        winLoseText.text = "FANTASTIC! \n You Made it home with " + timeSpan.Seconds.ToString("00") + " seconds Left!";
        winLoseText.color = Color.blue;

        scoreText.text = "You Destroyed: " + inventory.asteroidCount.ToString() + " Asteroids!";
    }


    void Update()
    {
        if(!gameStart){ 
            tutGUI.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space)) {
                tutGUI.SetActive(false);
                gameStart = true;
                _player.playGame = true;
            }
        }

        else{
            if(!_player.isDead && timeOut > 0 && !_player.winGame){
                playerGUI.SetActive(true);
                DrawGUI();
            }

            if(timeOut <= 0){ 
                playerGUI.SetActive(false);
                _player.playGame = false;
                winLoseGUI.SetActive(true);
                DrawTimeOutDisplay();
            }

            if(_player.isDead){
                playerGUI.SetActive(false);
                winLoseGUI.SetActive(true);
                DrawDeathDisplay();
            }

            if (_player.winGame) {
                GameObject winCam = GameObject.FindGameObjectWithTag("WinCam");

                playerGUI.SetActive(false);
                winLoseGUI.SetActive(true);
                DrawWinDisplay();
            }
        }


    }
}
