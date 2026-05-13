//using UnityEngine;

//// Just a simple health manager to show how you can controll the HealthBar UI
//public class HealthManager : MonoBehaviour
//{
//    public int maxhealth = 100;
//    public int currenthealth;

//    public HealthBar healthbar;

//    void Start()
//    {
//        currenthealth = maxhealth;
//        healthBar.SetMaxHealth(maxHealth);
//    }

//    void Update()
//    {
//        // Damage player when we press the G key
//        if (Input.GetKeyDown(KeyCode.G))
//        {
//            TakeDamage(20);
//        }
//    }

//    void TakeDamage(int damage)
//    {
//        currentHealth -= damage;

//        healthBar.SetCurrentHealth(currentHealth);
//    }
//}