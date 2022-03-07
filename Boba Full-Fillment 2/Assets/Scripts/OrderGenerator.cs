using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodMenuItem
{
    public string food;
    public int price;
}
public class OrderGenerator : MonoBehaviour
{
    public static int MaxOrderItems = 6;
    
    float currTime;
    public float timeBetweenCustomerOrders;
    public string[] toppings;
    public string[] drinkFlavors;
    public FoodMenuItem[] foodItems;
    int currOrderId = 0;

    public List<Order> activeOrders;
    // Start is called before the first frame update
    void Start()
    {
        activeOrders = new List<Order>(10);
    }

    // Update is called once per frame
    void Update()
    {
        currTime -= Time.deltaTime;

        if(currTime <= 0 && activeOrders.Count < MaxOrderItems)
        {
            currTime = timeBetweenCustomerOrders;
            CreateOrder();
        }

        foreach (Order order in activeOrders)
        {
            order.timeRemaining -= Time.deltaTime;
        }
    }

    void CreateOrder()
    {
        if(Random.value > .33f || foodItems.Length == 0)
        {
            CreateDrinkOrder();
        }
        else
        {
            CreateFoodOrder();
        }
    }

    void CreateDrinkOrder()
    {
        // randomly choose a drink flavor and a topping
        // also calculate the price 
        int value = 3;
        string tea = drinkFlavors[Random.Range(0, drinkFlavors.Length)];
        string topping = toppings[Random.Range(0, toppings.Length)];
        bool withMilk = Random.value > .5f;
        value += withMilk ? 1 : 0; // milk adds 1$
        value += topping != "" ? 1 : 0; // a topping adds 1$;
        activeOrders.Add(new DrinkOrder(currOrderId++, tea, topping, withMilk, value));
    }

    void CreateFoodOrder()
    {
        
        if(foodItems.Length < 1)
        {
            Debug.Log("No food items");
            return;
        }
        var foodMenuItem = foodItems[Random.Range(0, foodItems.Length)];
        activeOrders.Add(new FoodOrder(currOrderId++, foodMenuItem.food, foodMenuItem.price));
    }


}

public class DrinkOrder : Order
{
    public string topping;
    public string tea;
    public int sweetness; // 0 through 4
    public int ice; // 0 through 4
    public bool withMilk;
    public DrinkOrder(int orderId, string tea, string topping, bool withMilk, int value) : base(orderId, value)
    {
        this.withMilk = withMilk;
        this.tea = tea;
        this.topping = topping;

        // randomly determine ice and sweetness
        ice = Random.Range(0, 5);
        sweetness = Random.Range(0, 1);
        this.displayName = $"{tea}{(withMilk ? " Milk" : "")} Tea{(topping != "" ? " with " : "")}{topping}, {ice * 25}% Ice, {sweetness * 25}% Sweetness";
    }
}

public abstract class Order
{
    public int orderId;
    public float timeLimit;
    public float timeRemaining;
    public string displayName;
    public int value;
    public static int minOrderTimer = 60;
    public static int maxOrderTimer = 120;
    public Order(int orderId, int value)
    {
        
        // determine random time limit 
        timeLimit = Random.Range(minOrderTimer, maxOrderTimer);
        timeRemaining = timeLimit;
        this.orderId = orderId;
        this.value = value; 
    }
}

public class FoodOrder : Order
{
    public string foodType;
    public FoodOrder(int orderId, string foodType, int value) : base(orderId, value)
    {
        this.foodType = foodType;
        this.displayName = foodType;
    }
}

