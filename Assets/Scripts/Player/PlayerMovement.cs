using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float m_MoveSpeed = 500f;
    [SerializeField] private float m_RotateSpeed = 10f;
    [SerializeField] private float m_JumpForce = 5f;
    [SerializeField] private CinemachineCamera m_Camera;
    
    private Rigidbody m_Rigidbody;
    private float m_CurrentXRotation = 0f;
    private bool m_IsGrounded = false;

    private const float MAX_CAMERA_TILT = 60;

    private void Awake()
    {
        if (!TryGetComponent<Rigidbody>(out m_Rigidbody))
        {
            Debug.LogError($"<color=red><b>PlayerMovement</color></b> >> No rigidbody found on {name}");
        }
        
        m_Rigidbody.freezeRotation = true;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_CurrentXRotation = m_Camera.transform.rotation.eulerAngles.x;
        InputManager.Instance.OnJumpPressed += HandleJump;
        
        InputManager.Instance.SwitchToInputMap(InputManager.InputMaps.Game);
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnJumpPressed -= HandleJump;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleLook();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (!(Vector3.Angle(contact.normal, Vector3.up) < 45f)) continue;

            if (!m_IsGrounded)
            {
                //Debug.Log("Land");
                //AudioManager.Instance.PlayOneShotAudio(
                //    AudioLibrary.Instance.PlayerLands,
                //    m_Camera.transform.position);
            }
            m_IsGrounded = true;
            return;
        }
    }

    // private void OnCollisionExit(Collision collision)
    // {
    //     m_IsGrounded = false;
    // }

    private void HandleMovement()
    {
       var targetVelocity = (
            transform.forward * InputManager.Instance.GetMovementVectorNormalized().z +
            transform.right * InputManager.Instance.GetMovementVectorNormalized().x) * m_MoveSpeed;
        
        // Preserve the current Y velocity (gravity / jumping)
        targetVelocity.y = m_Rigidbody.linearVelocity.y;

        if (!m_IsGrounded) { return; }
        m_Rigidbody.linearVelocity = targetVelocity;
    }

    private void HandleJump()
    {
        if (!m_IsGrounded) return;
        //Debug.Log("Jump");
        //AudioManager.Instance.PlayOneShotAudio(
        //    AudioLibrary.Instance.PlayerJump,
        //    m_Camera.transform.position);
        
        m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        m_Rigidbody.position += Vector3.up * 0.1f;
        m_IsGrounded = false;
    }

    private void HandleLook()
    {
        var mouseInput = InputManager.Instance.GetLookNormalized();
        
        // Rotate player rigidbody
        var rotationAmount = mouseInput.x * m_RotateSpeed * Time.deltaTime * 1.5f;
        var deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        
        // Tilt the camera
        m_CurrentXRotation -= mouseInput.y * m_RotateSpeed * Time.deltaTime;
        m_CurrentXRotation = Mathf.Clamp(m_CurrentXRotation, -MAX_CAMERA_TILT, MAX_CAMERA_TILT);
        m_Camera.transform.localRotation = Quaternion.Euler(m_CurrentXRotation, 0f, 0f);
    }
}
