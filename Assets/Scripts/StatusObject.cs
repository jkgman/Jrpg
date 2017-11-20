﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Debug.Log("At " + (int) Time.time + " seconds: " + "");
public class StatusObject : MonoBehaviour {

    static string[] EffectsList = new string[] {"Damage", "Heal" };

    [SerializeField, Tooltip("Which effect will be used")]
    private StatusOptions _effect;
    [SerializeField, Tooltip("Number of turns effect will be active")]
    private int _duration = 1;
    [SerializeField, Tooltip("Margin around duration")]
    private int _durationMargin = 1;
    [SerializeField, Tooltip("Value specific to effect")]
    private float _value = 1;
    [SerializeField, Tooltip("Determines if it is percent or actual value margin")]
    private bool _isPercent = false;
    [SerializeField, Tooltip("Amount above and below value that it could possibly be")]
    private float _valueMargin = 1;
    [SerializeField, Tooltip("Amount above and below value that it could possibly be")]
    private StatsObject _masterStats;
    [SerializeField, Tooltip("Amount above and below value that it could possibly be")]
    private bool _instantCalculate = false;
    public bool log = false;
    

    private int attack;
    private int intelligence;

    private int ThisDuration;

    public void Setup()
    {
        Attack = _masterStats.CurrentAttack;
        if(log)
        {
            Debug.Log("At " + (int)Time.time + " seconds: " + "Attack set to: " + _masterStats.CurrentAttack);
        }
       
        
        Intelligence = _masterStats.CurrentIntelligence;
    }

    public void RollDuration() {
        ThisDuration = _duration + Random.Range(-_durationMargin, _durationMargin);
    }
    public StatusOptions Effect
    {
        get {
            return _effect;
        }
    }

    public int Length
    {
        get {
            return ThisDuration;
        }
        set {
            ThisDuration = value;
        }
    }

    public float Value
    {
        get {
            if(_isPercent)
            {
                return _value + _value * Random.Range(-_valueMargin, _valueMargin);
            } else
            {
                return _value + Random.Range(-_valueMargin, _valueMargin);
            }
            
        }
    }

    public bool InstantCalculate
    {
        get {
            return _instantCalculate;
        }
    }

    public int Attack
    {
        get {
            return attack;
        }

        private set {
            attack = value;
        }
    }

    public int Intelligence
    {
        get {
            return intelligence;
        }

        private set {
            intelligence = value;
        }
    }

    public float ActualValue
    {
        get {
            return _value;
        }

    }
}
