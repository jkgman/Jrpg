﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveUI : MonoBehaviour {

    [SerializeField, Tooltip("Description")]
    private Text _description;
    [SerializeField, Tooltip("Selection Texts")]
    private Text[] _selections;
    [SerializeField, Tooltip("Prefab for target")]
    private TargetUI _targetPrefab;
    private BattleHandler battleHandler;
    private int textoption = 0;
    private int position = 0;
    private bool isInTarget = false;
    private UIObject ui;
    private UIHandler uiHandler;
    private TargetUI target;
    //private string[,] currentUI;
    private int goingUp = -1, goingDown = 1, goingFirst = -10, goingLast = 10;
    private void Awake()
    {
        battleHandler = (BattleHandler)GameObject.FindWithTag("Battle Handler").GetComponent(typeof(BattleHandler));
    }
    public bool HasObject() {
        if(ui != null)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void Populate(PlayerEntity player, UIHandler UI)
    {
        uiHandler = UI;
        if(!HasObject())
        {
            ui = new UIObject(player);
        }
        SetScreen(0);
    }

    //0 is main, 1 is skill, 2 is inventory
    private void SetScreen(int screen)
    {
        ui.SelectMenu(screen);
        UpdateOptions();
        UpdateDescription();

        Highlight(ui.CurrentSelection);
    }

    private void UpdateDescription()
    {
        //Displays discription of menu item given
        _description.text = ui.Description();
    }
    private void UpdateOptions()
    {
        //Displays menu items
        _selections[0].text = ui.Option(0);
        _selections[1].text = ui.Option(1);
        _selections[2].text = ui.Option(2);
    }


    public void HandleInput(string input)
    {
        if(target == null)
        {
            Restore(ui.CurrentTextItem);//restore every loop to prepare for change
            if(input == "Select")
            { 
                if(ui.Action == "Skills")
                {
                    SetScreen(1);
                } else if(ui.Action == "Inventory")
                {
                    Debug.LogWarning("No Inventory yet");
                    //SetScreen(2);
                } else
                {
                    target = Instantiate(_targetPrefab, new Vector2(0, 0), Quaternion.identity);
                    target.Action = ui.Action;
                    isInTarget = true;
                }
            }
            if(input == "Back")
            {
                if(ui.CurrentScreen != 0)
                {
                    SetScreen(0);
                } else
                {
                    uiHandler.Destroy();
                    battleHandler.PreviousMember();
                    Destroy(gameObject);
                }          
            }
            if(input == "Up")
            {
                ui.UpSelect();
            }
            if(input == "Down")
            {
                ui.DownSelect();
            }
            if(input == "Left")
            {
                ui.FirstSelect();
            }
            if(input == "Right")
            {
                ui.LastSelect();
            }
            UpdateOptions();
            UpdateDescription();
            Highlight(ui.CurrentTextItem);//highlight at the end of every update to assure a highlighted option
        } else
        {
            target.HandleInput(input);
        }
    }
   
    private void Highlight(int pos)
    {
        //highlights an item at position
        _selections[pos].fontSize = 16;
        _selections[pos].fontStyle = FontStyle.Bold;
    }
    private void Restore(int pos)
    {
        //unhighlights an item at position
        _selections[pos].fontSize = 14;
        _selections[pos].fontStyle = FontStyle.Normal;
    }

    public void Select(int target) {
        
        battleHandler.Select(target, ui.Action);
        uiHandler.Destroy();
        Destroy(gameObject);
    }
}
