
using UnityEngine;
using Cinemachine;
using System.Collections;




public class PlayerController : MonoBehaviour
{
   
    [SerializeField] private float _speed = 6f, _jump = 1f, _gravity = -9.8f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundLayerMask;

    public bool canMove = false;
    public bool isDied = false;
    
    Vector3 _velocity;
    bool isGrounded;

    Animator animator;
   
    [SerializeField] float sensitivity = 10f;
    Vector3 orijinalCamPos;

    [SerializeField] float minAimLimit = -0.7f;
    [SerializeField] float maxAimLimit = 0.9f;
    [SerializeField] float aimAnimationSensitivity = 0.1f;
         
    #region Component Fields
    FireController fireController;
    HealthBarController healthBarController;
    private CharacterController characterController;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private CinemachinePOV povComponent;
    #endregion

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        healthBarController = FindObjectOfType<HealthBarController>();
        fireController = GetComponent<FireController>();
        animator = GetComponent<Animator>();


        povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        orijinalCamPos= virtualCamera.transform.position;

        
    }

    private void Update()
    {
       
        if (canMove ==true)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

            if (isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2;
            }

            float aimVertical = animator.GetFloat("AimVertical") + Input.GetAxis("Mouse Y") * aimAnimationSensitivity * Time.deltaTime;
            aimVertical = Mathf.Clamp(aimVertical, minAimLimit, maxAimLimit); // Aim sýnýrlarýný belirleyin
            animator.SetFloat("AimVertical", aimVertical);

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

            if (Input.GetButtonDown("Jump") && canMove)
            {
                Jump();
            }
            else
            {
                animator.SetBool("CanJump", false);
                _velocity.y += _gravity * Time.deltaTime;
                characterController.Move(_velocity * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > fireController.gunTimer)
            {
                if (fireController.bulletCount <= 0)
                {
                    fireController.canFire = false;
                    SoundController.instance.PlaySoundEffect(1);
                }
                else if (fireController.canFire)
                {
                   // animator.SetBool("IsShooting", true);
                    //canMove = false;
                    fireController.Fire();
                    fireController.gunTimer = Time.time + fireController.autoFireRate;
                    animator.SetTrigger("ShootingTrigger");
                    animator.SetLayerWeight(1, 1); 
                }
            }
            else
            {
               
               // animator.SetLayerWeight(1, 0);
                canMove = true;
            }


            PlayerRotateTheDirection();
            ReloadingRifle();
        }
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
            fireController.bulletText.text = fireController.bulletCount.ToString();
            fireController.totalBulletText.text = fireController.totalBullet.ToString();
            yield return new WaitForSeconds(0.05f);
        }
      
        fireController.canFire = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            fireController.PickUpAmmoTrigger(other.gameObject.GetComponent<BulletBaseClass>().bulletAmount);
            SoundController.instance.PlayAroundSounds(0);
            other.gameObject.SetActive(false);
            StartCoroutine(BulletActivate(other.gameObject));
        }
        if (other.gameObject.CompareTag("HealItem") && healthBarController.health < 100f)
        {
            healthBarController.Healing(10f);
            SoundController.instance.PlayAroundSounds(1);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.transform.parent.GetComponent<CoinController>().CoinCollector();
            SoundController.instance.PlayAroundSounds(2);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.transform.parent.GetComponent<KeyController>().KeyPickUpKey();

        }
    }

   
    IEnumerator BulletActivate(GameObject obj)
    {
        yield return new WaitForSeconds(5f);
        obj.SetActive(true);
    }


/*
    void XXX()
    {
           

            // Aim kontrolü için fare hareketi
            // float aimVertical = Input.GetAxis("Mouse Y");
            // Aim kontrolünde sabit kalma için deðer birikimi
            float aimVertical = animator.GetFloat("AimVertical") + Input.GetAxis("Mouse Y") * aimSensitivity * Time.deltaTime;
            aimVertical = Mathf.Clamp(aimVertical, minAimLimit, maxAimLimit); // Aim sýnýrlarýný belirleyin
            animator.SetFloat("AimVertical", aimVertical);


            if (animator != null)
            {
                // Yürüme durumunu belirleme
                bool isWalking = x != 0 || y != 0;
                animator.SetBool("Walk", isWalking);

                // AimVertical parametresini blend tree ile kullanmak üzere ayarlama
                animator.SetFloat("AimVertical", aimVertical);

                // Ateþ etme kontrolü (Mouse0 basýlýysa)
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    animator.SetLayerWeight(1, 1); // Ýkinci layer aktif
                    animator.SetBool("IsDamage", true);
                }
                else
                {
                    // animator.SetLayerWeight(1, 0); // Ýkinci layer pasif
                    animator.SetBool("IsDamage", false);
                }

                // Üçüncü layer için aim animasyonlarý kontrolü
                animator.SetLayerWeight(2, 1); // Üçüncü layer aktif tutuluyor
            }
    }*/
    
}