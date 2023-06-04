using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public Vector3 objectLocation = new Vector3(0,0,0);
    [SerializeField] public Quaternion objectRotation = Quaternion.Euler(90, 0, 0);
    [SerializeField] public Vector3 objectSize= new Vector3(1,1,1);
    public enum TypeOptions
        {
            Object,
            Action,
            Number,
            Destination,
            Abstract
        }

        [SerializeField]
        public TypeOptions type;

}
