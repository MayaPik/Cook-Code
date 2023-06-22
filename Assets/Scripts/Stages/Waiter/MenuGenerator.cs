using UnityEngine;
using System.Collections.Generic;

public class MenuGenerator : MonoBehaviour
{
    public GameObject selectedDrink;
    public GameObject selectedFood;
    public GameObject selectedDessert;



    [SerializeField]
    private List<GameObject> drinks;
    [SerializeField]
    private List<GameObject> mainDishes;
    [SerializeField]
    private List<GameObject> desserts;

    [SerializeField]
    private Transform drinkParent;
    [SerializeField]
    private Transform dessertParent;
    [SerializeField]
    private Transform mainDishParent;
    


    private void Start()
    {
        GenerateMenu();
    }

    private void GenerateMenu()
    {
        // Randomly select a drink prefab
        int drinkIndex = Random.Range(0, drinks.Count);
        GameObject drinkPrefab = drinks[drinkIndex];

        // Randomly select a main dish prefab
        int mainDishIndex = Random.Range(0, mainDishes.Count);
        GameObject mainDishPrefab = mainDishes[mainDishIndex];

        // Randomly select a dessert prefab
        int dessertIndex = Random.Range(0, desserts.Count);
        GameObject dessertPrefab = desserts[dessertIndex];

        selectedDrink= drinkPrefab;
        selectedFood= mainDishPrefab;
        selectedDessert = dessertPrefab;
        Instantiate(drinkPrefab, drinkParent);
        Instantiate(mainDishPrefab, mainDishParent);
        Instantiate(dessertPrefab, dessertParent);
    }
}
