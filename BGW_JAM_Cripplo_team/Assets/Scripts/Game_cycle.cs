﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_cycle : MonoBehaviour {

    public float level_time = 120.0f;
    public string win_scene;
    public string lose_scene;
    float level_timer = 0.0f;


   

    public Text text;
    bool finished = false;
    bool win = false;
    // Use this for initialization
    void Start()
    {
        level_timer = level_time;
    }

    // Update is called once per frame
    void Update()
    {
        level_timer -= Time.deltaTime;

        if (level_timer <= 0.0f)
        {
            finished = true;
        }

        text.text = " " + (int)level_timer;

    }

    public void MenuToNewGame(string scene)
    {
        Application.LoadLevel(scene);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void FinishGame()
    {
        Player_behaviour pl = GameObject.Find("Player").GetComponent<Player_behaviour>();
        if (!pl.alive)
        {
            win = false;
        }
        else
        {
            win = true;
        }

        finished = true;

        if(win)
        {
            Application.LoadLevel(win_scene);
        }
        else
        {
            Application.LoadLevel(lose_scene);
        }

    }
}
