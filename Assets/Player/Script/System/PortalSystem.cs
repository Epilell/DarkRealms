using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// dstPos�� �̵����Ѽ� �ڷ���Ʈ ��ġ�� �����Ͽ� ���
//


public class PortalSystem : MonoBehaviour
{
    //Private Field
    #region

    //�̵���ų �÷��̾� ������Ʈ
    private GameObject targetPlayer;

    //�̵���ų ��ġ��ǥ
    [SerializeField]
    private GameObject dstPos;

    //��Ż ��ȣ�ۿ�Ű UI
    [SerializeField]
    private GameObject popUpUI;

    #endregion

    //Private Method
    #region

    /// <summary>
    /// 3���� �ڷ���Ʈ ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator UsePortal()
    {
        if (targetPlayer != null)
        {
            yield return new WaitForSeconds(3f);
            targetPlayer.transform.position = dstPos.transform.position;
        }

    }

    //��ȣ�ۿ� �˾�â Ȱ��ȭ ����
    private void PopUpMessage()
    {
        if(targetPlayer != null)
        {
            popUpUI.SetActive(true);
        }
        else
        {
            popUpUI.SetActive(false);
        }
        
    }


    #endregion

    //Unity Event
    #region

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            targetPlayer = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            targetPlayer = null;
        }
    }

    private void Update()
    {
        if(popUpUI != null)
        {
            PopUpMessage();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(UsePortal());
        }
    }



    #endregion
}
