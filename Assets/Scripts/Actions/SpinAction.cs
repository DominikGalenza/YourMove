using System.Collections;
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
			isActive = false;
		}
	}

	public void Spin()
	{
		isActive = true;
		totalSpinAmount = 0f;
	}
}
