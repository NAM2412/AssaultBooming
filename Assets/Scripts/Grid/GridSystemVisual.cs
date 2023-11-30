using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] Transform gridVisualSinglePrefab;
    [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterials;

    private GridVisualSingle[,] gridVisualSingleArray;

    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public GridVisualType gridVisualType;
        public Material material;
    }

    public enum GridVisualType
    {
        White,
        Blue,
        Red,
        Yellow,
        Redsoft
    }

    

    public static GridSystemVisual Instance { get; private set; }

    #region Monobehaviour

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one GridSystemVisual! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        gridVisualSingleArray = new GridVisualSingle[
                                     LevelGrid.Instance.GetWidth(),
                                     LevelGrid.Instance.GetWidth()
        ];
        for (int x = 0; x < LevelGrid.Instance.GetWidth();x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight();z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);
                Transform gridVisualSingleTransform = Instantiate(gridVisualSinglePrefab, 
                                                                  LevelGrid.Instance.GetWorldPosition(gridPosition), 
                                                                  Quaternion.identity);
                gridVisualSingleArray[x, z] = gridVisualSingleTransform.GetComponent<GridVisualSingle>();
            }
        }

        UnitActionSystem.Instance.OnSelectedActionChange += UnitActionSystem_OnSelectedActionChange;

        LevelGrid.Instance.OnAnyUnitMoveGridPosition += LevelGrid_OnAnyUnitMoveGridPosition;

        UpdateGridVisual();
    }

    

    #endregion

    #region Grid position visual
    public void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                gridVisualSingleArray[x, z].Hide();
            }
        }
    }    

    private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisualType gridVisualType)
    {
        List<GridPosition> gridPositions = new List<GridPosition>();
        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x, z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Math.Abs(x) + Math.Abs(z);
                if (testDistance > range)
                {
                    continue;
                }

                gridPositions.Add(testGridPosition);
            }
        }

        ShowGridPositionList(gridPositions, gridVisualType);
    }

    private void ShowGridPositionRangeSquare(GridPosition gridPosition, int range, GridVisualType gridVisualType)
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x, z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                gridPositionList.Add(testGridPosition);
            }
        }

        ShowGridPositionList(gridPositionList, gridVisualType);
    }


    public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            gridVisualSingleArray[gridPosition.x, gridPosition.z]
                .Show(GetGridVisualTypeMaterial(gridVisualType));
        }    
    }

    private void UpdateGridVisual()
    {
        HideAllGridPosition();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();

        GridVisualType gridVisualType;

        switch (selectedAction)
        {
            default:
            case MoveAction moveAction:
                gridVisualType = GridVisualType.White;
                break;

            case SpinAction spinAction:
                gridVisualType = GridVisualType.Blue;
                break;

            case ShootAction shootAction:
                gridVisualType = GridVisualType.Red;
                ShowGridPositionRange(selectedUnit.GetGridPosition(), shootAction.GetMaxShootDistance(), GridVisualType.Redsoft);
                break;

            case GrenadeAcion grenadeAction:
                gridVisualType = GridVisualType.Yellow;
                break;

            case SwordAction swordAction:
                gridVisualType = GridVisualType.Red;
                ShowGridPositionRangeSquare(selectedUnit.GetGridPosition(), swordAction.GetMaxSwordDistance(), GridVisualType.Redsoft);
                break;

            case InteractAction interactAction:
                gridVisualType = GridVisualType.Blue;
                break;

        }

        ShowGridPositionList(
                        selectedAction.GetValidActionGridPositionList(), gridVisualType);
    }
    #endregion

    #region Event
    private void UnitActionSystem_OnSelectedActionChange(object sender, System.EventArgs e)
    {
        UpdateGridVisual();
    }
    private void LevelGrid_OnAnyUnitMoveGridPosition(object sender, System.EventArgs e)
    {
        UpdateGridVisual();
    }
    #endregion 

    private Material GetGridVisualTypeMaterial (GridVisualType gridVisualType)
    {
        foreach (GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualTypeMaterials)
        {
            if (gridVisualTypeMaterial.gridVisualType == gridVisualType)
            {
                return gridVisualTypeMaterial.material;
            }
        }

        Debug.LogError("Could not find GridVisiualTypeMaterial for GridVisualType " + gridVisualType);
        return null;
    }
}
