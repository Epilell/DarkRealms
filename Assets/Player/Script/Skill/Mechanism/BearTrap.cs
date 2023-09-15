using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    //Field
    #region .
    private readonly BearTrapData data;
    private Animator animator;
    #endregion

    //Private Method
    #region

    //애니메이션을 플레이하고 1초후 오브젝트 삭제
    private IEnumerator PlayAndDestroy()
    {
        animator.Play("Bear Trap");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    #endregion

    //Unity Event
    #region

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob") && collision != null)
        {
            MobHP target = collision.gameObject.GetComponent<MobHP>();

            //target에게 대미지와 CC를 적용
            target.TakeDamage(data.Damage); target.TakeCC(data.CCType, data.CCDuration);
            StartCoroutine(PlayAndDestroy());
        }
    }
    #endregion
}
