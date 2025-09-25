using UnityEngine;
using UnityEngine.InputSystem;


public class GatherInput : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputActionMap playerMap;
    private InputActionMap uiMap;

    public InputActionReference jumpActionRef;
    public InputActionReference moveActionRef;

    [HideInInspector]
    public float horizontalInput;

    private void OnEnable()
    {
        jumpActionRef.action.performed += TryToJump;
        jumpActionRef.action.canceled += StopJump;
    }
    private void OnDisable()
    {
        jumpActionRef.action.performed -= TryToJump;
        jumpActionRef.action.canceled -= StopJump;

        playerMap.Disable();
    }
    private void TryToJump(InputAction.CallbackContext Value)
    {
        Debug.Log("JUMP");
    }
    private void StopJump(InputAction.CallbackContext Value)
    {
        Debug.Log("STOPJUMP");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMap = playerInput.actions.FindActionMap("Player");
        uiMap = playerInput.actions.FindActionMap("UI");
        playerMap.Enable();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = moveActionRef.action.ReadValue<float>();
        Debug.Log("Horizontal Input: " + horizontalInput);
    }
}
