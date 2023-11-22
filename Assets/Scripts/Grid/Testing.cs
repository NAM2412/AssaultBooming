using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Testing : MonoBehaviour
{
    [SerializeField] Unit unit;

    private void Update()
    {
        /*if(Input.GetKey(KeyCode.T))
        {
            GridSystemVisual.Instance.HideAllGridPosition();
            GridSystemVisual.Instance.ShowGridPositionList(
                            unit.GetMoveAction().GetValidActionGridPositionList());
        }*/
    }
}
