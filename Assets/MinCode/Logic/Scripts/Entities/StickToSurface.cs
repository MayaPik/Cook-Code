using UnityEngine;
using System.Collections;

public class StickToSurface : BehaviorBase
{
    public LayerMask surfaceLayer = ~0;
    public BooleanReference isSticking = BooleanReference.Create(true);
    private Collider myCollider;

    private void Start()
    {
        myCollider = GetComponentInChildren<Collider>();
    }

    void LateUpdate()
    {
        if (isSticking)
        {
            RaycastHit hit;
            var height = myCollider.bounds.size.y;
            var halfHeight = height / 2f;
            var halfDepth = myCollider.bounds.size.z / 2;
            var measurePoint = new Vector3(
                transform.position.x,
                transform.position.y + halfHeight,
                transform.position.z + halfDepth);

            Debug.DrawRay(measurePoint, Vector3.down, Color.red);

            if (Physics.Raycast(measurePoint, Vector3.down, out hit, Mathf.Infinity, surfaceLayer))
            {
                var oldY = transform.position.y;
                var newY = hit.point.y + halfHeight;

                if (oldY != newY)
                {
                    transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                    PrintDebugInfo($"Hit {hit.collider.name} at {hit.point.GetString()}. Setting y from {oldY.GetString()} to {newY.GetString()}. Distance to surface is: {hit.distance.GetString()}, delta is: {(newY - oldY).GetString()}. Height is {height.GetString()}. Raycasted from {measurePoint.GetString()}");
                }
            }
        }
    }
}
