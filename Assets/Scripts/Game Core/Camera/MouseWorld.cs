using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;
    [SerializeField] LayerMask mousePLaneLayerMask;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        this.transform.position = MouseWorld.GetPosition();
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePLaneLayerMask);
        return raycastHit.point;
    }
}
