using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAction : BaseAction
{
    private int maxInteractDistance = 3;

    #region Monobehaviour
    private void Update()
    {
        if (!isActive)
        {
            return;
        }
    }
    #endregion

    #region Override method
    public override string GetActionName()
    {
        return "Interact";
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxInteractDistance; x <= maxInteractDistance; x++)
        {
            for (int z = -maxInteractDistance; z <= maxInteractDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z, 0);
                GridPosition testGridPostion = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPostion))
                {
                    
                    continue;
                }

                IInteractable interactable = LevelGrid.Instance.GetInteractableAtGridPostion(testGridPostion);
                if (interactable == null)
                {   
                    // No interactable on this grid position
                    continue;
                }

                validGridPositionList.Add(testGridPostion);
            }
        }
        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        IInteractable interactable = LevelGrid.Instance.GetInteractableAtGridPostion(gridPosition);
        interactable.Interact(OnInteractComplete);
        ActionStart(onActionComplete);
    }
    #endregion

    private void OnInteractComplete()
    {
        ActionComplete();
    }
}
