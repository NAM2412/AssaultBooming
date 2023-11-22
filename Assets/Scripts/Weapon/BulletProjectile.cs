using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform bulletHitVFX;
    private Vector3 targetPosition;
    private float bulletSpeed = 200f;

    private void Update()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);

        transform.position += moveDirection * bulletSpeed * Time.deltaTime;

        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        CheckDistance(distanceBeforeMoving, distanceAfterMoving); // If bullet reach the unit, destroy bullet
    }

    private void CheckDistance(float previousDistance, float afterDistance)
    {
        if (previousDistance < afterDistance)
        {
            transform.position = targetPosition;

            trailRenderer.transform.parent = null;

            Destroy(gameObject);

            Instantiate(bulletHitVFX, targetPosition, Quaternion.identity);
        }
    }

    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;

        
    }
}
