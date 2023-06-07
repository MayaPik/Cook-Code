using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Moves an object back to where it spawned from when it was awaked
/// </summary>
public class BackToStartMover : BehaviorBase
{
    public FloatReference isInPointTolernce = FloatReference.Create<FloatReference>(0.01f);
    public FloatReference moveToStartSpeed = FloatReference.Create<FloatReference>(20f);
    public FloatReference rotationSpeed = FloatReference.Create<FloatReference>(15f);
    public UnityEvent onAtStart;
    public UnityEvent onMoveToStart;
    public Vector3 StartPosition => positionOnStart;
    public bool IsMovingToStart => isMovingToStart;
    public bool IsAtStart => Vector3.Distance(StartPosition, transform.position) < isInPointTolernce;
    private Vector3 positionOnStart;
    private Vector3 lastPosition;
    private Quaternion rotationAtStart;
    private bool isMovingToStart;
    private List<UnityAction> whenAtStartOneTimeActions;

    private void Awake()
    {
        GetNameDelegate = () => transform.parent.name;
        SetStart();
    }

    public void SetStart()
    {
        rotationAtStart = transform.rotation;
        positionOnStart = transform.position;
    }

    public void StopMoveToStart(bool invokeAtStartActions)
    {
        PrintDebugInfo($"stop moving to start: {positionOnStart.GetString()}");
        isMovingToStart = false;

        if (invokeAtStartActions)
        {
            onAtStart.Invoke();
        }
    }

    public void MoveToStart()
    {
        MoveToStart(null);
    }

    public void MoveToStart(UnityAction whenAtStart)
    {
        if (whenAtStart != null)
        {
            if (whenAtStartOneTimeActions == null)
            {
                whenAtStartOneTimeActions = whenAtStart.YieldSingle().ToList();
            }
            else
            {
                whenAtStartOneTimeActions.Add(whenAtStart);
            }
        }

        if (IsAtStart)
        {
            PrintDebugInfo("not moving to start, already at start position");
            OnAtStart();
        }
        else if (!isMovingToStart)
        {
            PrintDebugInfo($"moving to start: {positionOnStart.GetString()}");

            onMoveToStart.Invoke();
            isMovingToStart = true;
        }
        else
        {
            PrintDebugInfo($"not moving to start, already moving");
        }
    }

    private void Update()
    {
        lastPosition = transform.position;

        if (isMovingToStart)
        {
            if (transform.position.y < StartPosition.y - 2)
            {
                transform.rotation = rotationAtStart;
                transform.position = new Vector3(transform.position.x, StartPosition.y, transform.position.z);
            }

            if (IsAtStart)
            {
                PrintDebugInfo("got to start position");
                OnAtStart();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, positionOnStart, moveToStartSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationAtStart, rotationSpeed * Time.deltaTime);
            }

            if (lastPosition == transform.position)
            {
                OnAtStart();
            }
        }
    }

    private void OnAtStart()
    {
        PrintDebugInfo("OnAtStart()");
        onAtStart.Invoke();

        if (whenAtStartOneTimeActions != null)
        {
            foreach (var action in whenAtStartOneTimeActions)
            {
                action.Invoke();
            }

            whenAtStartOneTimeActions = null;
        }

        isMovingToStart = false;
        transform.rotation = rotationAtStart;
        transform.position = positionOnStart;
    }
}
