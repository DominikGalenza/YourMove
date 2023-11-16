using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject actionCameraGameObject;
	[SerializeField] private float shoulderOffsetAmount = 0.5f;

	private void Start()
	{
		BaseAction.OnAnyActionStarted += BaseAction_OnAnyActionStarted;
		BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;

		HideActionCamera();
	}

	private void ShowActionCamera()
    {
        actionCameraGameObject.SetActive(true);
    }

    private void HideActionCamera()
    {
		actionCameraGameObject.SetActive(false);
	}

	private void BaseAction_OnAnyActionStarted(object sender, EventArgs e)
	{
		switch (sender)
		{
			case ShootAction shootAction:
				AimAtEnemy(shootAction);
				ShowActionCamera();
				break;
		}
	}

	private void BaseAction_OnAnyActionCompleted(object sender, EventArgs e)
	{
		switch (sender)
		{
			case ShootAction shootAction:
				HideActionCamera();
				break;
		}
	}

	private void AimAtEnemy(ShootAction shootAction)
	{
		Unit shooterUnit = shootAction.GetUnit();
		Unit targetUnit = shootAction.GetTargetUnit();

		Vector3 cameraCharacterHeight = Vector3.up * 1.7f;
		Vector3 shootDirection = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;
		Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootDirection * shoulderOffsetAmount;
		Vector3 actionCameraPostion = shooterUnit.GetWorldPosition() + cameraCharacterHeight + shoulderOffset - shootDirection;

		actionCameraGameObject.transform.position = actionCameraPostion;
		actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);
	}
}
