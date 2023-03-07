using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    public event EventHandler OnTurnChange;

    private int trunNumber = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void NextTurn()
    {
        trunNumber++;
        OnTurnChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return trunNumber;
    }
}
