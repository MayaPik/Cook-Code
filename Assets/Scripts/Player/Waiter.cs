using System.Collections;
using UnityEngine;

public class Waiter : Player
{
    private MenuGenerator menuGenerator;
    [SerializeField] GameObject tray;

    private void Awake()
    {
        menuGenerator = FindObjectOfType<MenuGenerator>();
    }

    public override IEnumerator GetItem(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
        yield return StartCoroutine(GetItemCoroutine(slot, itemGameObject, animator, hand));
    }

    private IEnumerator GetItemCoroutine(Slot slot, GameObject itemGameObject, Animator animator, GameObject hand)
    {
        bool isDrink = slot.tag == "Drink";
        bool isFood = slot.tag == "Food";
        bool isDessert = slot.tag == "Dessert";

        if ((isDrink && itemGameObject.name == menuGenerator.selectedDrink.name) ||
            (isFood && itemGameObject.name == menuGenerator.selectedFood.name) ||
            (isDessert && itemGameObject.name == menuGenerator.selectedDessert.name))
        {
        animator.SetTrigger("Idle");
        GameObject item = Instantiate(itemGameObject);
        Item itemComponent = item.GetComponent<Item>();
        SetItemProperties(item, itemComponent);
        ObjectAction objectAction = item.GetComponent<ObjectAction>();
         if (actionParticles != null)
        {
                actionParticles.Play(); // Start the ParticleSystem
        }
        playSound.TriggerSoundEvent(); 
        if (objectAction != null)
        {
            objectAction.PerformAction();
        }
        yield return new WaitForSeconds(2.0f);
        }
        else
        {
        yield return StartCoroutine(BreakParentLoop());
        }
    }
}
