using System;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
	private float totalSpinAmount;
	private float spinAddAmount = 360f;

	private void Update()
	{
		if (!isActive) return;

		transform.eulerAngles += new Vector3(0, spinAddAmount * Time.deltaTime, 0);
		totalSpinAmount += spinAddAmount * Time.deltaTime;

		if (totalSpinAmount >= 360f)
		{
			ActionComplete();
		}
	}

	public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
	{
		totalSpinAmount = 0f;
		ActionStart(onActionComplete);
	}

	public override string GetActionName()
	{
		return "spin";
	}

	public override List<GridPosition> GetValidActionGridPositionList()
	{
		GridPosition unitGridPosition = unit.GetGridPosition();

		return new List<GridPosition>()
		{
			unitGridPosition
		};
	}

	public override int GetActionPointsCost()
	{
		return 2;
	}

	public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
	{
		return new EnemyAIAction()
		{
			gridPosition = gridPosition,
			actionValue = 0,
		};
	}
}