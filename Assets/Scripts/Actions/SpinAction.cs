using System;
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
			isActive = false;
			onActionComplete();
		}
	}

	public void Spin(Action onActionComplete)
	{
		this.onActionComplete = onActionComplete;
		isActive = true;
		totalSpinAmount = 0f;
	}
}
