
using UnityEngine;
using Cinemachine;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float _speed = 6f, _jump = 1f, _gravity = -9.8f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundLayerMask;

   

    // [SerializeField] Button jumpButton;
    Vector3 _velocity;
    bool isGrounded;

    Animator animator;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float sensitivity = 100f;
    private CinemachinePOV povComponent;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //jumpButton.onClick.AddListener(Jump);// Ziplama butonunu dinle

        Cursor.visible = false; // Mouse görünmez yap
        Cursor.lockState = CursorLockMode.Locked;

        povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();

    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        animator.SetFloat("XDirection", x);
        animator.SetFloat("ZDirection", z);
        Vector3 move = transform.right * x + transform.forward * z;
        animator.SetFloat("Speed", move.magnitude);
        if (z != 0f || x != 0f)
        {
            characterController.Move(move * _speed * Time.deltaTime);
        }

        _velocity.y += _gravity * Time.deltaTime;
        characterController.Move(_velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("IsShooting", true);
            //animator.Play("ShootAuto");
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }


        PlayerRotateTheDirection();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jump * -2f * _gravity);
        }
    }


    void PlayerRotateTheDirection()
    {
        // Mouse X hareketini oku
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        // Karakteri saða-sola döndür
        transform.Rotate(Vector3.up, mouseX);

        // Kameranýn yatay eksenini karakterin y eksenine göre ayarla
        povComponent.m_HorizontalAxis.Value = transform.eulerAngles.y;

        /*
        float mouseX = Input.GetAxis("Mouse X");

        // Nesneyi fare hareketine göre döndür
        transform.Rotate(Vector3.up, mouseX * sensivity * Time.deltaTime);

        povComponent.m_HorizontalAxis.Value += mouseX;
      */
    }
}