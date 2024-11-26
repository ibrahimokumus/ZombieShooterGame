using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FireController : MonoBehaviour
{
   
   
    public bool canFire =true;
    public float gunTimer;
    public float autoFireRate;
   
    public float distance;

    public int bulletCount = 0;
    public int magazinAmount = 10;
    public Text bulletText;
    public Text totalBulletText;
    public int totalBullet = 50;
    ParticleEffectController particleEffectController;
   [SerializeField] EnemyBaseClass[] enemyBaseControllers;
    RecoilController recoilController;

    private void Start()
    {
        bulletText.text = bulletCount.ToString();
        totalBulletText.text = totalBullet.ToString();
        particleEffectController = FindObjectOfType<ParticleEffectController>();
        recoilController = FindObjectOfType<RecoilController>(); 
    }

    /// <summary>
    /// ates etme methodu
    /// </summary>
    public void Fire()
    {

        SoundController.instance.PlaySoundEffect(0);
        particleEffectController.PlayParticleEffect("Muzzle");
        Invoke("BulletShellSoundPlay",0.5f);// mermi kovani sesi gecikmeli cal
        bulletCount -= 1;
        bulletText.text = bulletCount.ToString();
        Vector3 rayOrigin = Camera.main.transform.position; // Kamera konumu
        Vector3 rayDirection = Camera.main.transform.forward; // Kamera yönü
        recoilController.Recoil();// geri tepme
        RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection, distance);
        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.isTrigger)
            {
                // Eğer vurulan nesne bir düşmansa hasar metodunu çağır
                if (hit.transform.CompareTag("Enemy"))
                {
                    enemyBaseControllers[0].TakeDamage(15);
                }
                if (hit.transform.CompareTag("Police"))
                {
                    enemyBaseControllers[1].TakeDamage(10);
                }
                break;
            }
        }

    }

    /// <summary>
    /// mermi toplama methodu
    /// </summary>
    /// <param name="amount">eklenecek miktar</param>
    public void PickUpAmmoTrigger(int amount)
    {
        StartCoroutine(PickUpAmmo(amount));
    }

    IEnumerator PickUpAmmo(int amount)
    {
        // mermi toplama sesi cal
        for (int i = 0; i < amount; i++)
        {
            totalBullet += 1;
            totalBulletText.text = totalBullet.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }

    void BulletShellSoundPlay()
    {
        SoundController.instance.PlayAddictinalSounds(0);
    }
    
}
