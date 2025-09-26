using UnityEngine;
using UnityEngine.InputSystem;

public class AttackTurret : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] InputActionAsset actionAsset;
    private InputAction attackAction;

    [Header("Variables")]
    public float attackForce;

    [Header("Objetos")]
    public GameObject bulletPrefba;
    public Transform spawnBullet;


    private void OnEnable()
    {
        actionAsset.FindActionMap("Player");
    }
    private void OnDisable()
    {
        actionAsset.FindActionMap("Player");
    }
    private void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
    }
    private void Update()
    {
        if (attackAction.WasPerformedThisFrame())
        {
            GunShoot();
        }
    }
    public void GunShoot()
    {
        GameObject bullet = Instantiate(bulletPrefba, spawnBullet.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if(rb != null )
        {
            rb.linearVelocity = spawnBullet.up * attackForce;
            rb.transform.up = spawnBullet.up;
        }
    }
}
