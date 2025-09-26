using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    [SerializeField] InputActionAsset action;
    private InputAction rotateAction;

    private Vector2 rotatePos;

    private Rigidbody2D rb2D;

    [SerializeField] public float rotateSpd;

    private void OnEnable()
    {
        action.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        action.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        rotateAction = InputSystem.actions.FindAction("Rotate");

        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
        rotatePos = rotateAction.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
       
        RotateMove();
    }
    void RotateMove()
    {
        Debug.Log(rotatePos);
        if (rotatePos.x != 0)
        {
            float rotationPos = -rotatePos.x * rotateSpd * Time.deltaTime;
            //Quaternion deltaRotation = Quaternion.Euler(0, 0, rotationPos);
            transform.Rotate(0, 0, rotationPos);
        }
    }
}
