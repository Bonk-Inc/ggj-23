using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private static Player instance;
    public static Player Instance => instance;

    [field: SerializeField]
    public PlayerStat Health { get; private set; }

    [field: SerializeField]
    public PlayerStat Stamina { get; private set; }

    [field: SerializeField]
    public PlayerLevelManager PlayerLevel { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            throw new System.Exception("Multiple players not allowed");
        }
    }

    public void ResetStats()
    {
        Health.Reset();
        Stamina.Reset();
        PlayerLevel.ResetLevel();
    }

}

[System.Serializable]
public class PlayerStat
{

    [field: SerializeField]
    public int MinValue { get; private set; } = 0;

    [field: SerializeField]
    public int MaxValue { get; private set; } = 100;

    public int CurrentValue { get; private set; }

    public event Action<StatChangeArgs> OnChange;

    public PlayerStat()
    {
        Reset();
    }

    public void Increase(int amount)
    {
        SetValue(CurrentValue + amount);
    }

    public void Decrease(int amount)
    {
        SetValue(CurrentValue - amount);
    }

    public void Reset()
    {
        CurrentValue = MaxValue;
    }

    public void SetValue(int value)
    {
        if (CurrentValue == value)
            return;

        var actualNewValue = Math.Clamp(value, MinValue, MaxValue);
        var previousValue = CurrentValue;
        CurrentValue = value;
        CallChangeEvent(previousValue, value);
    }

    public void SetMax()
    {
        SetValue(MaxValue);
    }

    private void CallChangeEvent(int previousValue, int originalNewValue)
    {
        var args = new StatChangeArgs()
        {
            Min = MinValue,
            Max = MaxValue,
            TotalDiff = originalNewValue - previousValue,
            NormalizedDiff = CurrentValue - previousValue,
            CurrentValue = CurrentValue
        };

        OnChange?.Invoke(args);
    }

    public class StatChangeArgs
    {
        public int Max { get; set; }
        public int Min { get; set; }

        public bool MinReached => CurrentValue == Min;

        public int TotalDiff { get; set; }

        public int NormalizedDiff { get; set; }

        public int PreviousValue { get; set; }

        public int CurrentValue { get; set; }
    }

}