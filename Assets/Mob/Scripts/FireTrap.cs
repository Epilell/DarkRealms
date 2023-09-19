using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField]
    private float Damage=1;
    [SerializeField]
    private float Duration=5f;

    private Light light;

    private bool canDamage = false;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        light = GetComponent<Light>();
        StartCoroutine(TrapRoutine());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canDamage)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().P_TakeDamage(Damage);
            }
        }
    }
    private IEnumerator TrapRoutine()
    {
        while (true)
        {
            canDamage = true;
            animator.SetBool("On",true);
            FindObjectOfType<SoundManager>().isFire = true;
            light.intensity = 5;
            yield return new WaitForSeconds(Duration);
            canDamage = false;
            animator.SetBool("On",false);
            FindObjectOfType<SoundManager>().isFire = false;
            light.intensity = 0;
            yield return new WaitForSeconds(Duration);
        }
    }
}
