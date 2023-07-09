using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;


public class GameBehavior : MonoBehaviour, IManager
{
    public Stack<string> lootStack = new Stack<string>();

    public string labelText = "Collected all 4 items and win your freedom!";
    public int maxItemsCount = 4;
    public bool showWinScreen = false;
    public bool showLossScreen = false;

    private string state;
    public string State
    {
        get { return state; }
        set { state = value; }
    }

    int itemsColleted = 0;
    public int Items
    {
        get { return itemsColleted; }
        set { itemsColleted = value;
            if (itemsColleted >= maxItemsCount)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else labelText = $"Item found, only {maxItemsCount - itemsColleted} more to go!";
        }
    }

    int playerHP = 10;
    public int HP 
    {
        get { return playerHP; } 
        set {  playerHP = value;
            if(playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Ouch... that's got hurt.";
            }
        } 
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        state = "Manager initialized...";
        state.FancyDebug();
        Debug.Log(state);

        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");

    }

    public void PrinLootReport()
    {
        var currentItem = lootStack.Pop();
        var nextItem = lootStack.Peek();
        Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem, nextItem);
        Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), $"Player Health: {playerHP}");
        GUI.Box(new Rect(20, 50, 150, 25), $"Item collected: {itemsColleted}");
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen)
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You Win!"))
            {
                Utilities.RestartLevel(0);
            }

        if (showLossScreen)
        {
            if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-50,200,100),"You lose..."))
            {
                Utilities.RestartLevel();
            }
        }
    }
}
