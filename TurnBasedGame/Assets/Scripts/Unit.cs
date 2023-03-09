using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private bool isEnenmy;

    private const int ACTTION_POINT_MAX = 2;
    public static event EventHandler OnAnyActionPointsChanged;

    private GridPosition gridPosition;

    private MoveAction moveAction;
    private SpinAction spinAction;
    private ShotAction shootAction;
    private HealSystem healSystem;
    private ActionBase[] actionBaseArray;

    private int actionPoints = ACTTION_POINT_MAX;

    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;

    private void Awake()
    {
        healSystem = GetComponent<HealSystem>();
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        shootAction = GetComponent<ShotAction>();
        actionBaseArray = GetComponents<ActionBase>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChange += TurnStstem_OnTurnChange;

        healSystem.OnDead += HealSystem_OnDead;
        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;
            LevelGrid.Instance.UnitMoveGridPosition(this, oldGridPosition, newGridPosition);
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

    public ShotAction GetShootAction()
    {
        return shootAction;
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
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    private void TurnStstem_OnTurnChange(object sender, EventArgs eventArgs)
    {
        if ((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
            (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            actionPoints = ACTTION_POINT_MAX;

            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }


    public bool IsEnemy()
    {
        return isEnenmy;
    }

    public void Damage(int damageAmount)
    {
        healSystem.Damage(damageAmount);
    }

    private void HealSystem_OnDead(object sender, EventArgs eventArgs)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        Destroy(gameObject);

        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return healSystem.GetHealthNormalized();
    }


}
