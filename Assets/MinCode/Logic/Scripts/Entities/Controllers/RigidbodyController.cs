using UnityEngine;
using System.Collections;

/// <summary>
/// Adds force or torque to a rigidbody
/// </summary>
public class RigidbodyController : ActionsExecutor
{
    public Vector3Reference forceFactor = Vector3Reference.Create<Vector3Reference>(Vector3.one);
    public Vector3Reference torqueFactor = Vector3Reference.Create<Vector3Reference>(Vector3.one);
    private Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void AddForce()
    {
        PrintDebugInfo($"AddForce() myRigidbody: {myRigidbody}, executeActions: {executeActions}");
        if (myRigidbody != null && executeActions)
        {
            ExecuteActions("executing actions");
            var force = GetDirectionForce(-transform.forward, forceFactor);
            PrintDebugInfo($"adding force of: {force.GetString()}");
            myRigidbody.AddForce(force);
        }
    }

    public void AddTorque()
    {
        PrintDebugInfo($"AddTorque() myRigidbody: {myRigidbody}, executeActions: {executeActions}");
        if (myRigidbody != null && executeActions)
        {
            ExecuteActions("executing actions");
            var force = GetDirectionForce(-transform.forward, torqueFactor);
            PrintDebugInfo($"adding torque of: {force.GetString()}");
            myRigidbody.AddTorque(force);
        }
    }

    private Vector3 GetDirectionForce(Vector3 direction, Vector3 force)
    {
        direction = direction.normalized;

        return new Vector3(direction.x * force.x, direction.y * force.y, direction.z * force.z);
    }
}
