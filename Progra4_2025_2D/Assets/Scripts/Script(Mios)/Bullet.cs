using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    
    }
}
