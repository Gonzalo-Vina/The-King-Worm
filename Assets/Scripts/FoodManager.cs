using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] GameObject foodPrefab;

    public void CreateFood()
    {
        Vector2 tempFoodPosition = new Vector2(Random.Range(-5, 5), Random.Range(-9, 9));
        tempFoodPosition.x = tempFoodPosition.x - 0.5f;
        tempFoodPosition.y = tempFoodPosition.y - 0.5f;
        if (tempFoodPosition.x < -4.5f || tempFoodPosition.y < -8.5f)
        {
            tempFoodPosition.x = tempFoodPosition.x + 1;
            tempFoodPosition.y = tempFoodPosition.y + 1;
        }

        if (Physics2D.OverlapCircle(tempFoodPosition, 0.15f)) //Esta funcion comprueba si la casilla está ocupada.
        {
            CreateFood();
        }
        else
        {
            Instantiate(foodPrefab, tempFoodPosition, Quaternion.identity);
        }
    }
}
