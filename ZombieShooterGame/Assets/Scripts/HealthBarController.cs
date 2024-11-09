using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeBarSlider;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float health;
    float animationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }
        if(healthSlider.value != easeBarSlider.value)
        {
            easeBarSlider.value = Mathf.Lerp(easeBarSlider.value, health, animationSpeed*Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.T)) 
        {
            StartCoroutine(HealAnimation(10));
        }
    }


    public IEnumerator HealAnimation(float healAmount)
    {
        float newHealth = health + healAmount; 
        while (health < newHealth)
        {
            health += Time.deltaTime * 2f;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            yield return null;//sonraki frame bekle
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }
}
