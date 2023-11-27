using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private int grenadeDamage = 30;
    private Action onGrenadeBehaviourComplete;
    private Vector3 targetPosition;

    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float reachedTargetDistance = 0.2f;
        if (Vector3.Distance(transform.position, targetPosition) < reachedTargetDistance)
        {
            float damageRadius = 4f;
            Collider[] colliders = Physics.OverlapSphere(targetPosition, damageRadius);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<Unit>(out Unit targetUnit))
                {
                    targetUnit.Damage(grenadeDamage);
                }
            }

            Destroy(gameObject);
            onGrenadeBehaviourComplete();
        }
    }
    public void Setup(GridPosition targetGridPosition, Action onGrenadeBehaviourComplete)
    {
        this.onGrenadeBehaviourComplete = onGrenadeBehaviourComplete;
        targetPosition = LevelGrid.Instance.GetWorldPosition(targetGridPosition);
    }
}
