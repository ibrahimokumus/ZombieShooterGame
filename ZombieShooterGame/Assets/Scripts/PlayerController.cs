
using UnityEngine;
using Cinemachine;
using System.Collections;



public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float _speed = 6f, _jump = 1f, _gravity = -9.8f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundLayerMask;

    bool canMove = true;

    // [SerializeField] Button jumpButton;
    Vector3 _velocity;
    bool isGrounded;

    Animator animator;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float sensitivity = 100f;
    private CinemachinePOV povComponent;
    Vector3 orijinalCamPos;


    #region Deneme
    FireController fireController;
    #endregion
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //jumpButton.onClick.AddListener(Jump);// Ziplama butonunu dinle

        Cursor.visible = false; // Mouse görünmez yap
        Cursor.lockState = CursorLockMode.Locked;

        povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        orijinalCamPos= virtualCamera.transform.position;
        InvokeRepeating("Damage", 3f,1f);

        fireController = GetComponent<FireController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2;
            virtualCamera.transform.position += new Vector3(0,-5f,0);
        }
       

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        animator.SetFloat("XDirection", x);
        animator.SetFloat("ZDirection", z);
        Vector3 move = transform.right * x + transform.forward * z;
        animator.SetFloat("Speed", move.magnitude);
        if ((z != 0f || x != 0f) && canMove)
        {
            characterController.Move(move * _speed * Time.deltaTime);
        }

        _velocity.y += _gravity * Time.deltaTime;
        characterController.Move(_velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") &&canMove)
        {
            Jump();
        }
        else
        {
            animator.SetBool("CanJump", false);
            _velocity.y += _gravity * Time.deltaTime;
            characterController.Move(_velocity * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Mouse0) && fireController.canFire && Time.time > fireController.gunTimer)
        {
            if (fireController.bulletAmount <= 0)
            {
                Debug.Log("Mermi yok");
                //sarjor bitti sesi cal
            }
            else
            {
                animator.SetBool("IsShooting", true);
                canMove = false;
                fireController.Fire();
                fireController.gunTimer = Time.time + fireController.autoFireRate; ;
            }
        }
        else
        {
            animator.SetBool("IsShooting", false);
            canMove = true;
        }
      

        PlayerRotateTheDirection();
        ReloadingRifle();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            virtualCamera.transform.position += new Vector3(0,5f,0);
            _velocity.y = Mathf.Sqrt(_jump * -2f * _gravity);
            animator.SetBool("CanJump",true);
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

    }

    void ReloadingRifle()
    {
        if (Input.GetKeyDown(KeyCode.R) && fireController.bulletAmount < 10)
        {
            animator.SetTrigger("ReloadTrigger");
            StartCoroutine(ReloadWaiting());
            //sarjor sesi cal
        }
    }


    IEnumerator ReloadWaiting()
    {
        yield return new WaitForSeconds(2f);
        fireController.bulletAmount = 10;
        fireController.bulletText.text = fireController.bulletAmount.ToString();
    }
/*
    void TakeDamage()
    {

        animator.SetTrigger("GetHitTrigger");
        canMove= false;
    }*/
}