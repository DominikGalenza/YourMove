using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
	[SerializeField] private Animator unitAnimator;
	[SerializeField] private int maxMoveDistance = 4;

	private Vector3 targetPosition;
	private float stoppingDistance = 0.1f;
	private float moveSpeed = 4f;
	private float rotationSpeed = 10f;
	private Unit unit;

	private void Awake()
	{
		unit = GetComponent<Unit>();
		targetPosition = transform.position;
	}

	private void Update()
	{
		if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
		{
			Vector3 moveDirection = (targetPosition - transform.position).normalized;
			transform.position += moveDirection * moveSpeed * Time.deltaTime;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), Time.deltaTime * rotationSpeed);
			unitAnimator.SetBool("IsWalking", true);
		}
		else
		{
			unitAnimator.SetBool("IsWalking", false);
		}
	}

	public void Move(GridPosition gridPosition)
	{
		this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
	}

	public bool IsValidActionGridPosition(GridPosition gridPosition)
	{
		List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
		return validGridPositionList.Contains(gridPosition);
	}

	public List<GridPosition> GetValidActionGridPositionList()
	{
		List<GridPosition> validGridPositionList = new List<GridPosition>();
		GridPosition unitGridPosition = unit.GetGridPosition();
		for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
		{
			for (int z = -maxMoveDistance; z < maxMoveDistance; z++)
			{
				GridPosition offsetGridPosition = new GridPosition(x, z);
				GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

				if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
				{
					continue;
				}

				if (unitGridPosition == testGridPosition)
				{
					continue;
				}

				if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
				{
					continue;
				}

				validGridPositionList.Add(testGridPosition);
			}
		}

		return validGridPositionList;
	}
}
