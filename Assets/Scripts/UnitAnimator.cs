using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;
	[SerializeField] Transform bulletProjectilePrefab;
	[SerializeField] Transform shootPointTransform;

	private void Awake()
	{
		if (TryGetComponent(out MoveAction moveAction))
		{
			moveAction.OnStartMoving += MoveAction_OnStartMoving;
			moveAction.OnStopMoving += MoveAction_OnStopMoving;
		}

		if (TryGetComponent(out ShootAction shootAction))
		{
			shootAction.OnShoot += shootAction_OnShoot;
		}
	}

	private void MoveAction_OnStartMoving(object sender, EventArgs e)
	{
		animator.SetBool("IsWalking", true);
	}

	private void MoveAction_OnStopMoving(object sender, EventArgs e)
	{
		animator.SetBool("IsWalking", false);
	}

	private void shootAction_OnShoot(object sender, ShootAction.OnShootEventArgs e)
	{
		animator.SetTrigger("Shoot");
		Transform bulletProjectileTransform = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);

		BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

		Vector3 targetUnitShootAtPosition = e.targetUnit.GetWorldPosition();
		targetUnitShootAtPosition.y = shootPointTransform.position.y;

		bulletProjectile.Setup(targetUnitShootAtPosition);
	}
}
