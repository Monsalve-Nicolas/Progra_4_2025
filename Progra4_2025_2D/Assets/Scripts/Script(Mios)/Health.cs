using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [Header("Vida")]
    public int maxHealth = 100;
    int currentHealth;

    [Header("Puntos por morir")]
    public int minPoints;
    public int maxPoints;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        if (CompareTag("Enemy"))
        {
            int puntos = Random.Range(minPoints, maxPoints +1);
            LevelManager.Instance.AddPoints(puntos);
        }
        Destroy(gameObject);
    }
}
