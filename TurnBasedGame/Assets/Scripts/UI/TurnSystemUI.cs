using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField]
    private Button endTurnbutton;
    [SerializeField]
    private TextMeshProUGUI turnNumberText;
    [SerializeField] 
    private GameObject enemyTurnObject;  
    [SerializeField] 

    private void Start()
    {
        endTurnbutton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });
        UpdateTrunText();
        UpdateEnemyTurnObjectVisual();
        UpdateEndTurnButtonVisibility();
        TurnSystem.Instance.OnTurnChange += TurnStstem_OnTurnChange;
    }

    private void TurnStstem_OnTurnChange(object sender, EventArgs eventArgs)
    {
        UpdateTrunText();
        UpdateEnemyTurnObjectVisual();
        UpdateEndTurnButtonVisibility();
    }

    private void UpdateTrunText()
    {
        turnNumberText.text = "Turn :" + TurnSystem.Instance.GetTurnNumber();
    }

    private void UpdateEnemyTurnObjectVisual()
    {
        enemyTurnObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    private void UpdateEndTurnButtonVisibility()
    {
        endTurnbutton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }

}
