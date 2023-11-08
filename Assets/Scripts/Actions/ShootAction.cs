using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
	[SerializeField] private int maxShootDistance = 7;
	[SerializeField] private float aimingStateTime = 1f;
	[SerializeField] private float shootingStateTime = 0.1f;
	[SerializeField] private float coolOffStateTime = 0.5f;
	[SerializeField] private float rotateSpeed = 10f;

	private enum State
	{
		Aiming,
		Shooting,
		Cooloff,
	}

	private State state;
	private float stateTimer;	
	private Unit targetUnit;
	private bool canShoot;

	private void Update()
	{
		if (!isActive) return;

		stateTimer -= Time.deltaTime;

		switch (state)
		{
			case State.Aiming:
				Vector3 aimDirection = targetUnit.transform.position - unit.transform.position;
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), Time.deltaTime * rotateSpeed);
				break;
			case State.Shooting:
				if (canShoot)
				{
					Shoot();
					canShoot = false;	
				}
				break;
			case State.Cooloff:
				break;
		}

		if (stateTimer <= 0f)
		{
			NextState();
		}
	}

	private void Shoot()
	{
		targetUnit.Damage();
	}

	private void NextState()
	{
		switch (state)
		{
			case State.Aiming:
				state = State.Shooting;
				stateTimer = shootingStateTime;
				break;
			case State.Shooting:
				state = State.Cooloff;
				stateTimer = coolOffStateTime;
				break;
			case State.Cooloff:
				ActionComplete();
				break;
		}
	}

	public override string GetActionName()
	{
		return "Shoot";
	}

	public override List<GridPosition> GetValidActionGridPositionList()
	{
		List<GridPosition> validGridPositionList = new List<GridPosition>();
		GridPosition unitGridPosition = unit.GetGridPosition();
		for (int x = -maxShootDistance; x <= maxShootDistance; x++)
		{
			for (int z = -maxShootDistance; z < maxShootDistance; z++)
			{
				GridPosition offsetGridPosition = new GridPosition(x, z);
				GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

				if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;

				int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
				if (testDistance > maxShootDistance) continue;

				if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;

				Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);
				if (targetUnit.IsEnemy() == unit.IsEnemy()) continue;

				validGridPositionList.Add(testGridPosition);
			}
		}

		return validGridPositionList;
	}

	public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
	{
		ActionStart(onActionComplete);

		targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

		state = State.Aiming;
		stateTimer = aimingStateTime;
		canShoot = true;
	}
}
