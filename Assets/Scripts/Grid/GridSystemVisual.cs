using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] Transform gridVisualSinglePrefab;

    private GridVisualSingle[,] gridVisualSingleArray;

    public static GridSystemVisual Instance { get; private set; }

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
    }

    private void Update()
    {
        UpdateGridVisual();
    }

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

    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            gridVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }    
    }    

    private void UpdateGridVisual()
    {
        HideAllGridPosition();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        ShowGridPositionList(
                        selectedUnit.GetMoveAction().GetValidActionGridPositionList());
    }    
}
