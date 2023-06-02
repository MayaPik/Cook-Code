using UnityEngine;

public class PlaceOnTableAction : ObjectAction
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
        }
        
        // Find the highest object on the table
        float highestY = float.MinValue;
        GameObject highestObject = null;
        foreach (Collider collider in tableColliders)
        {
            if (collider.gameObject != gameObject && collider.bounds.max.y > highestY)
            {
                highestY = collider.bounds.max.y;
                highestObject = collider.gameObject;
            }
        }

        if (highestObject != null && highestObject.transform.position.x != 0)
        {
            // Place the object on top of the highest object
            Vector3 newPosition = new Vector3(highestObject.transform.position.x, highestY - transform.localScale.y * 0.5f, highestObject.transform.position.z);
            transform.position = newPosition;
        }
        else
        {
            // No objects on the table, place it in the center
            if (tableCollider != null )
            {
                transform.position = tableCollider.bounds.center;
            }
        }
    }
}
