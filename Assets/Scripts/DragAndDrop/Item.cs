using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Vector3 objectLocation = new Vector3(0, 0, 0);
    [SerializeField] private Quaternion objectRotation = Quaternion.Euler(90, 0, 0);
    [SerializeField] private Vector3 objectSize = new Vector3(1, 1, 1);

    public enum TypeOptions
    {
        Object,
        Action,
        Number,
        Destination,
        Abstract
    }

    [SerializeField] public TypeOptions type;

    // Accessors (getters)
    public Vector3 ObjectLocation => objectLocation;
    public Quaternion ObjectRotation => objectRotation;
    public Vector3 ObjectSize => objectSize;
    public TypeOptions Type => type;
}
