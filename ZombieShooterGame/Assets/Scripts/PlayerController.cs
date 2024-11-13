
using UnityEngine;
using Cinemachine;
using System.Collections;
using Unity.VisualScripting;



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
    HealthBarController healthBarController;
    #endregion
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        healthBarController = FindObjectOfType<HealthBarController>();
        fireController = GetComponent<FireController>();
        animator = GetComponent<Animator>();

        Cursor.visible = false; // Mouse görünmez yap
        Cursor.lockState = CursorLockMode.Locked;

        povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        orijinalCamPos= virtualCamera.transform.position;

        
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

        if (Input.GetKeyDown(KeyCode.Mouse0) &&  Time.time > fireController.gunTimer)
        {
            if (fireController.bulletCount <= 0 )
            {
                fireController.canFire = false;
                SoundController.instance.PlaySoundEffect(1);
            }
            else if(fireController.canFire)
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
            _velocity.y = Mathf.Sqrt(_jump * -2f * _gravity);
            animator.SetBool("CanJump",true);
        }
    }


    void PlayerRotateTheDirection()
    {
        // Mouse X hareketini oku
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        // Karakteri saða-sola döndür
        transform.Rotate(Vector3.up, mouseX);
       // transform.Rotate(Vector3.left, mouseY);
        // Kameranýn yatay eksenini karakterin y eksenine göre ayarla
        povComponent.m_HorizontalAxis.Value = transform.eulerAngles.y;

    }

    
    void ReloadingRifle()
    {
        if (Input.GetKeyDown(KeyCode.R) && fireController.bulletCount < fireController.magazinAmount && fireController.totalBullet > 0)
        {
            animator.SetTrigger("ReloadTrigger");
            StartCoroutine(ReloadWaiting());
            SoundController.instance.PlaySoundEffect(2);
        }
    }


    IEnumerator ReloadWaiting()
    { 
        fireController.canFire = false;
        yield return new WaitForSeconds(4f);
        SoundController.instance.fxSource.Stop();
        for (int i = fireController.bulletCount; i < fireController.magazinAmount; i++)
        {
            fireController.bulletCount += 1;
            fireController.totalBullet -= 1;
            fireController.bulletText.text = fireController.bulletCount.ToString() +" / " + fireController.totalBullet.ToString();
            yield return new WaitForSeconds(0.05f);
        }
      
        fireController.canFire = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            healthBarController.Damage(10);
            Debug.Log(other.gameObject.name);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            fireController.PickUpAmmoTrigger(other.gameObject.GetComponent<BulletBaseClass>().bulletAmount);
            SoundController.instance.PlayAroundSounds(0);
            other.gameObject.SetActive(false);
            StartCoroutine(BulletActivate(other.gameObject));
        }
    }

   
    IEnumerator BulletActivate(GameObject obj)
    {
        yield return new WaitForSeconds(5f);
        obj.SetActive(true);
    }


}