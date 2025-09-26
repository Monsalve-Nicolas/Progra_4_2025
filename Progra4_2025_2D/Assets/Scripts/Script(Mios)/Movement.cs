using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    [SerializeField] InputActionAsset action;
    private PlayerStats stats;
    private InputAction moveAction;
    private InputAction rotateAction;

    private Vector2 movePos;
    private Vector2 rotatePos;

    [SerializeField] Rigidbody2D rb2D;

    [SerializeField] public float moveSpd, rotateSpd;

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
        //le entregamos su componente
        stats = GetComponent<PlayerStats>();
        moveAction = InputSystem.actions.FindAction("Move");
        //rotateAction = InputSystem.actions.FindAction("Look");
    }
    private void Update()
    {
        movePos = moveAction.ReadValue<Vector2>();
        //rotatePos = rotateAction.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        MoveRB2D();
        RotateMove();
    }
    public void MoveRB2D()
    {
        rb2D.MovePosition(rb2D.position + (Vector2)transform.up * movePos.y * moveSpd * Time.deltaTime);

    }
    void RotateMove()
    {
        Debug.Log(movePos);
        if(movePos.x != 0)
        {
            float rotationPos = -movePos.x * rotateSpd * Time.deltaTime;
            //Quaternion deltaRotation = Quaternion.Euler(0, 0, rotationPos);
            transform.Rotate(0, 0, rotationPos);
        }
    }
    void UpdateStatsBase()
    {
        Debug.Log("Stats Actualizado: velocidad = " + stats.currentSpd);
    }
}
