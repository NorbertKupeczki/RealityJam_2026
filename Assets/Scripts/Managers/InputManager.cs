using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoSingleton<InputManager>
{
    public enum InputMaps
    {
        UI = 0,
        Game
    }
    
    private GameInput m_GameInput;

    // UI Input Events
    public event Action<Vector2> OnUiNavigatePressed;
    public event Action OnSubmitPressed;
    public event Action OnCancelPressed;
    
    // Game Input Events
    public event Action OnActionPressed;
    public event Action OnJumpPressed;
    public event Action OnMenuPressed;

    [Header("DEBUG")]
    [SerializeField] private Vector2 m_MoveVector;
    [SerializeField] private Vector2 m_Look;
    
    protected override void Awake()
    {
        base.Awake();
        
        InitialiseInputManager();
        DontDestroyOnLoad(gameObject);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_MoveVector = Vector2.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        m_MoveVector = m_GameInput.Game.Move.ReadValue<Vector2>();
        m_Look = m_GameInput.Game.Look.ReadValue<Vector2>();
    }
    
    private void InitialiseInputManager()
    {
        m_GameInput = new GameInput();
        
        DisableAllInputMaps();
        
        // Connecting UI Input action mapping to handlers
        m_GameInput.UI.Navigate.performed += HandleNavigatePerformed;
        m_GameInput.UI.Submit.performed += HandleSubmitPerformed;
        m_GameInput.UI.Cancel.performed += HandleCancelPerformed;
        
        // Connecting Game Input action mapping to handlers
        m_GameInput.Game.Action.performed += HandleActionPerformed;
        m_GameInput.Game.Jump.performed += HandleJumpPerformed;
        m_GameInput.Game.Menu.performed += HandleMenuPerformed;
    }

    private void OnDestroy()
    {
        // Disconnecting UI Input action mapping to handlers
        m_GameInput.UI.Navigate.performed -= HandleNavigatePerformed;
        m_GameInput.UI.Submit.performed -= HandleSubmitPerformed;
        m_GameInput.UI.Cancel.performed -= HandleCancelPerformed;
        
        // Disconnecting Game Input action mapping to handlers
        m_GameInput.Game.Action.performed -= HandleActionPerformed;
        m_GameInput.Game.Jump.performed -= HandleJumpPerformed;
        m_GameInput.Game.Menu.performed -= HandleMenuPerformed;
    }

#region >>>>> UI INPUT MAP HANDLERS <<<<<

    private void HandleNavigatePerformed(InputAction.CallbackContext obj)
    {
        OnUiNavigatePressed?.Invoke(m_GameInput.UI.Navigate.ReadValue<Vector2>());
    }

    private void HandleSubmitPerformed(InputAction.CallbackContext obj)
    {
        OnSubmitPressed?.Invoke();
    }

    private void HandleCancelPerformed(InputAction.CallbackContext obj)
    {
        OnCancelPressed?.Invoke();
    }
#endregion
    
#region >>>>> GAME INPUT MAP HANDLERS <<<<<
    private void HandleActionPerformed(InputAction.CallbackContext obj)
    {
        OnActionPressed?.Invoke();
    }

    private void HandleJumpPerformed(InputAction.CallbackContext obj)
    {
        OnJumpPressed?.Invoke();
    }

    private void HandleMenuPerformed(InputAction.CallbackContext obj)
    {
        OnMenuPressed?.Invoke();
    }
#endregion
    
    public Vector3 GetMovementVectorNormalized()
    {
        var inputVector = m_GameInput.Game.Move.ReadValue<Vector2>();
        return new Vector3(inputVector.x, 0, inputVector.y);
    }

    public Vector2 GetLookNormalized()
    {
        return m_GameInput.Game.Look.ReadValue<Vector2>();
    }

    public void TogglePlayerControls(bool toggle)
    {
        if (m_GameInput.Game.enabled == toggle) { return; }
        
        if (toggle)
        {
            m_GameInput.Game.Enable();
        }
        else
        {
            m_GameInput.Game.Disable();
        }
    }

    public void DisableAllInputMaps()
    {
        m_GameInput.UI.Disable();
        m_GameInput.Game.Disable();
    }
    
    public void SwitchToInputMap(InputMaps inputMap)
    {
        if (inputMap == InputMaps.Game)
        {
            m_GameInput.Game.Enable();
            m_GameInput.UI.Disable();
        }
        else
        {
            m_GameInput.Game.Disable();
            m_GameInput.UI.Enable();
        }
    }

    //public void ToggleLockCursor(bool toggle)
    //{
    //    Cursor.lockState = toggle ? CursorLockMode.Locked : CursorLockMode.None;
    //    Cursor.visible = !toggle;
    //}
}
