using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private GameObject actionCameraGO;
    private float shoulderUnitHeight = 1.7f;
    private void Start()
    {
        BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;

        HideActionCamera();
    }

    private void BaseAction_OnAnyActionCompleted(object sender, System.EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                HideActionCamera();
                break;
        }
    }

    private void BaseAction_OnAnyActionStarted(object sender, System.EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction:
                Unit shooterUnit = shootAction.GetUnit();
                Unit targetUnit = shootAction.GetTargetUnit();

                Vector3 shootingDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;

                Vector3 cameraCharacterHeight = Vector3.up * shoulderUnitHeight;

                float shoulderOffsetAmount = 0.5f;
                Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootingDir * shoulderOffsetAmount;

                Vector3 actionCameraPosition = shooterUnit.GetWorldPosition() + cameraCharacterHeight 
                                                                              + shoulderOffset 
                                                                              + (shootingDir*-1);

                actionCameraGO.transform.position = actionCameraPosition;
                actionCameraGO.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);

                ShowActionCamera();
                break;
        }
    }

    private void ShowActionCamera()
    {
        actionCameraGO.SetActive(true);
    }

    private void HideActionCamera()
    {
        actionCameraGO?.SetActive(false);
    }
}
