using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 9;
    private GridPosition gridPosition;
    private HealthSystem healthSystem; 
    private int actionPoints = ACTION_POINTS_MAX;
    private BaseAction[] baseActionArray;

    [SerializeField] private bool isEnemy;

    public static event EventHandler OnAnyActionChanged;
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;

    #region Monobehaviour
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        baseActionArray = GetComponents<BaseAction>();
    }
    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        healthSystem.OnDead += HealthSystem_OnDead;

        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;

            LevelGrid.Instance.UnitMovedGridPosition(this, oldGridPosition, newGridPosition);
        }
    }
    #endregion

    #region Unity event
    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        if ((IsEnemy() && !TurnSystem.Instance.IsOnPlayerTurn())
                                    || (!IsEnemy() && TurnSystem.Instance.IsOnPlayerTurn()))
        {
            actionPoints = ACTION_POINTS_MAX;

            OnAnyActionChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        Destroy(gameObject);

        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    #region Get methods
    public T GetAction<T>() where T : BaseAction
    {
        foreach (BaseAction baseAction in baseActionArray)
        {
            if (baseAction is T)
            {
                return (T)baseAction;
            }
        }
        return null;
    }

    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

    public float GetHealthNormalized()
    {
        return healthSystem.GetHeathNomalized();
    }
    #endregion

    #region Grid and World
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    #endregion

    #region ActionPoint Consume
    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;

        OnAnyActionChanged?.Invoke(this, EventArgs.Empty);
    }
    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (CanSpendActionPointToTakeBaseAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        else return false;
    }
    public bool CanSpendActionPointToTakeBaseAction(BaseAction baseAction)
    {
        return actionPoints >= baseAction.GetActionPointsCost();
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }
    #endregion


    public bool IsEnemy()
    {
        return isEnemy;
    }

    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }




}

