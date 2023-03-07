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

    private void Start()
    {
        endTurnbutton.onClick.AddListener(() =>
        {
            TurnSystem.Instance.NextTurn();
        });
        UpdateTrunText();

        TurnSystem.Instance.OnTurnChange += TurnStstem_OnTurnChange;
    }

    private void TurnStstem_OnTurnChange(object sender, EventArgs eventArgs)
    {
        UpdateTrunText();
    }

    private void UpdateTrunText()
    {
        turnNumberText.text = "Turn :" + TurnSystem.Instance.GetTurnNumber();
    }
}
