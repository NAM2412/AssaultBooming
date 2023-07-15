using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssaultBooming.Grid;

public class Testing : MonoBehaviour
{
    private GridSystem gridSystem;
    private void Start()
    {
        gridSystem = new GridSystem(10, 10, 3f);
    }
    private void Update()
    {
        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}
