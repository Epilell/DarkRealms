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

    //�ִϸ��̼��� �÷����ϰ� 1���� ������Ʈ ����
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

            //target���� ������� CC�� ����
            target.TakeDamage(data.Damage); target.TakeCC(data.CCType, data.CCDuration);
            StartCoroutine(PlayAndDestroy());
        }
    }
    #endregion
}
