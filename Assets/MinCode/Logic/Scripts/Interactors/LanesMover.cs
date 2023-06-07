using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LanesMover : BehaviorBase
{
    public List<FloatReference> lanesX;
    public List<BooleanReference> isInteracting;
    [Header("The index to move to at start. 0 based index")]
    public IntegerReference startLaneIndex = IntegerReference.Create<IntegerReference>(1);
    [Header("How much time it should take to move between lanes")]
    public FloatReference timeToMove = FloatReference.Create<FloatReference>(0.3f);
    [Header("Speed from start position to end position")]
    public AnimationCurve movementCurve;
    [Header("Object that this script controls")]
    public Transform transformToMove;
    [Header("Minimum distance of touch to apply swipe")]
    public FloatReference minimumSwipeDistance = FloatReference.Create<FloatReference>(0.2f);
    public LayerMask obstaclesLayer;
    public Collider entityCollider;
    private float touchDownX;
    private bool isMovingToNewLane;
    private bool wasMouseButtonDown;
    private int currentIndex;
    private float startX;
    private float targetX;
    private float timeSinceStartedMoving;
    private int MaxIndex => lanesX.Count - 1;

    void Start()
    {
        if (lanesX.Count == 0)
        {
            Debug.LogError("No lanes defined!");
        }
        else if (startLaneIndex < 0 || startLaneIndex > lanesX.Count)
        {
            Debug.LogError($"invalid `startLaneIndex` parameter! should be in the range of 0-{lanesX.Count}");
        }

        transformToMove ??= transform;
        entityCollider ??= transformToMove.GetComponent<Collider>();
        transformToMove.position = GetPosition(lanesX[startLaneIndex]);
        currentIndex = startLaneIndex;
        wasMouseButtonDown = false;
    }

    void Update()
    {
        if (isInteracting.All(b => b))
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchDownX = TouchInput.GetScreenInputPosoition().x;
                wasMouseButtonDown = true;
            }
            else if (wasMouseButtonDown && Input.GetMouseButtonUp(0))
            {
                var touchUpX = TouchInput.GetScreenInputPosoition().x;

                if (Mathf.Abs(touchUpX - touchDownX) > minimumSwipeDistance)
                {
                    var indexDelta = touchDownX < touchUpX ? 1 : -1;
                    var newIndex = GetClampedIndex(currentIndex + indexDelta);
                    currentIndex = newIndex;
                    MoveToIndex(newIndex);
                }
            }
        }

        if (isMovingToNewLane && transform.position.x != targetX)
        {
            MoveToTargetX();
        }
        else
        {
            isMovingToNewLane = false;
        }
    }

    public void MoveToLane(int lane)
    {
        MoveToIndex(lane);
    }

    public void MoveToFreeLane()
    {
        if (IsLaneFree(currentIndex))
        {
            PrintDebugInfo($"MoveToFreeLane: lane already free. not moving");
            return;
        }

        var delta = 1;
        int potentialIndex = currentIndex;
        PrintDebugInfo($"lanes availability:");

        for (int i = 0; i < lanesX.Count; i++)
        {
            PrintDebugInfo($"lanesX[{i}] availability: {IsLaneFree(i)}");

        }

        while (true)
        {
            potentialIndex = GetClampedIndex(potentialIndex + delta);

            if (IsLaneFree(potentialIndex))
            {
                PrintDebugInfo($"found a free lane at index {potentialIndex}, moving there");
                MoveToIndex(potentialIndex);
                return;
            }

            if (potentialIndex == MaxIndex)
            {
                delta = -1;
                potentialIndex = currentIndex;
            }
            else if (potentialIndex == 0)
            {
                // no free lane found
                PrintDebugInfo($"couldn't find a free lane. not moving");
                return;
            }
        }
    }

    private bool IsLaneFree(int index)
    {
        var laneX = lanesX[GetClampedIndex(index)];
        var position = GetPosition(laneX);
        var radius = entityCollider.bounds.size.x;

        var collisions = Physics.OverlapSphere(position, radius, layerMask: obstaclesLayer, queryTriggerInteraction: QueryTriggerInteraction.Collide);
        var collisionsWithEverything = Physics.OverlapSphere(position, radius, layerMask: ~1, queryTriggerInteraction: QueryTriggerInteraction.Collide);
        PrintDebugInfo($"checking if lane is free at {position.GetString()}. found collisions with obstacle layer: {string.Join(", ", collisions.Select(c => c.gameObject.name))}. collision without the layer mask: {string.Join(", ", collisionsWithEverything.Select(c => c.gameObject.name))}");

        return collisions.Length == 0;
    }

    private int GetClampedIndex(int index)
    {
        return Mathf.Clamp(index, 0, MaxIndex);
    }

    private void MoveToIndex(int newIndex)
    {
        var clampedNewIndex = GetClampedIndex(newIndex);

        isMovingToNewLane = true;
        currentIndex = clampedNewIndex;
        targetX = lanesX[currentIndex].Value;
        startX = transform.position.x;
        timeSinceStartedMoving = 0f;
        PrintDebugInfo($"Moving to index: {clampedNewIndex}. New lane X: {targetX.GetString()}");
    }

    private void MoveToTargetX()
    {
        var targetPosition = GetPosition(targetX);
        var startPosition = GetPosition(startX);

        if (targetPosition != transformToMove.position)
        {
            var curvePosition = timeSinceStartedMoving / timeToMove;
            var curveValue = movementCurve.Evaluate(curvePosition);
            transformToMove.position = Vector3.Lerp(startPosition, targetPosition, curveValue);
            timeSinceStartedMoving += Time.deltaTime;

            if (Mathf.Abs(targetPosition.x - transformToMove.position.x) <= 0.01f)
            {
                PrintDebugInfo($"got to target position! {targetPosition.GetString()}");
                transformToMove.position = targetPosition;
            }
        }
    }

    private Vector3 GetPosition(float newX, float? newY = null, float? newZ = null)
    {
        return new Vector3(newX, newY ?? transform.position.y, newZ ?? transform.position.z);
    }
}
