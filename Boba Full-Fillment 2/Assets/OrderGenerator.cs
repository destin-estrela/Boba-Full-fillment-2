using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    public static int MaxOrderItems = 6;
    
    float currTime;
    public float timeBetweenCustomerOrders;
    public string[] toppings;
    public string[] drinkFlavors;
    public string[] foodItems;
    int currOrderId = 0;

    public List<Order> activeOrders;
    // Start is called before the first frame update
    void Start()
    {
        activeOrders = new List<Order>(10);
        currTime = timeBetweenCustomerOrders;
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
        if(Random.value > .33f)
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
        string tea = drinkFlavors[Random.Range(0, drinkFlavors.Length)];
        string topping = toppings[Random.Range(0, toppings.Length)];
        bool withMilk = Random.value > .5f;
        activeOrders.Add(new DrinkOrder(currOrderId++, tea, topping, withMilk));
    }

    void CreateFoodOrder()
    {
        string foodType = foodItems[Random.Range(0, foodItems.Length)];
        activeOrders.Add(new FoodOrder(currOrderId++, foodType));
    }


}

public class DrinkOrder : Order
{
    public string topping;
    public string tea;
    int sweetness; // 0 through 4
    int ice; // 0 through 4
    bool withMilk;
    public DrinkOrder(int orderId, string tea, string topping, bool withMilk) : base(orderId)
    {
        this.withMilk = withMilk;
        this.tea = tea;
        this.topping = topping;
        ice = Random.Range(0, 5);
        sweetness = Random.Range(0, 5);
        this.displayName = $"{tea}{(withMilk ? " Milk" : "")} Tea with {topping}, {ice * 25}% Ice, {sweetness * 25}% Sweetness";
    }
}

public abstract class Order
{
    public int orderId;
    public float timeLimit;
    public float timeRemaining;
    public string displayName;

    public Order(int orderId)
    {
        timeLimit = Random.Range(15, 45);
        timeRemaining = timeLimit;
        this.orderId = orderId;
    }
}

public class FoodOrder : Order
{
    public string foodType;
    public FoodOrder(int orderId, string foodType) : base(orderId)
    {
        this.foodType = foodType;
        this.displayName = foodType;
    }
}

