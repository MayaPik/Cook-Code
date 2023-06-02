using UnityEngine;

public class PutInside : ObjectAction
{

    [SerializeField] GameObject poolOfObjects;
    [SerializeField] GameObject table;

    public override void PerformAction()
    {
         // Detach the gameobject from the hand collider
        transform.SetParent(poolOfObjects.transform, false);
        // Reset rotation
        transform.rotation = Quaternion.identity;
        
        Collider tableCollider = table.GetComponent<Collider>();

        // Check if there is an object with a collider on the table
        Collider[] tableColliders = poolOfObjects.GetComponentsInChildren<Collider>();
        if (tableColliders.Length == 0 || tableColliders[0].gameObject == gameObject) // No objects on the table yet or the current object is the first one
        {
           if (tableCollider != null)
            {
                transform.position = tableCollider.bounds.center;
                return; // Exit early to avoid placing it above existing objects
            }
        } else {
             gameObject.SetActive(false);
        }
    }
}
