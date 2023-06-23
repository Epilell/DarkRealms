using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// dstPos를 이동시켜서 텔레포트 위치를 지정하여 사용
//


public class PortalSystem : MonoBehaviour
{
    //Private Field
    #region

    //이동시킬 플레이어 오브젝트
    private GameObject targetPlayer;

    //이동시킬 위치좌표
    [SerializeField]
    private GameObject dstPos;

    //포탈 상호작용키 UI
    [SerializeField]
    private GameObject popUpUI;

    #endregion

    //Private Method
    #region

    /// <summary>
    /// 3초후 텔레포트 실행
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

    //상호작용 팝업창 활성화 여부
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
