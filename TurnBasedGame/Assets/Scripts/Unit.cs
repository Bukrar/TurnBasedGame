using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const int ACTTION_POINT_MAX = 2;
    public static event EventHandler OnAnyActionPointsChanged;

    private GridPosition gridPosition;

    private MoveAction moveAction;
    private SpinAction spinAction;
    private ActionBase[] actionBaseArray;

    private int actionPoints = ACTTION_POINT_MAX;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        actionBaseArray = GetComponents<ActionBase>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChange += TurnStstem_OnTurnChange;
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMoveGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public ActionBase[] GetActionBaseArray()
    {
        return actionBaseArray;
    }

    public bool TrySpendActionPointsToTakeAction(ActionBase actionBase)
    {
        if (CanSpendActionPointsToTakeAction(actionBase))
        {
            SpendActionPoints(actionBase.GetActionPointsCost());
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanSpendActionPointsToTakeAction(ActionBase actionBase)
    {
        if (actionPoints >= actionBase.GetActionPointsCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;
        OnAnyActionPointsChanged?.Invoke(this,EventArgs.Empty);
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    private void TurnStstem_OnTurnChange(object sender, EventArgs eventArgs)
    {
        actionPoints = ACTTION_POINT_MAX;

        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }
}
