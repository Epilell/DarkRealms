using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakAbleObject : MonoBehaviour
{
    private Animator animator;
    private MobDropItem dropItem;

    [SerializeField]
    private GameObject BrokenObj;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dropItem = GetComponent<MobDropItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("On");
            StartCoroutine(Break());
        }
    }

    private IEnumerator Break()
    {
        yield return new WaitForSeconds(2f);
        GameObject brokenObj = Instantiate(BrokenObj, transform.position, Quaternion.identity);
        dropItem.ItemDrop();
        //원래 오브젝트 부수기
        Destroy(this.gameObject);

    }
}
