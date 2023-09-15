using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private float damage = 20f;

    public Transform pos;
    public Vector2 boxSize = new Vector2(1, 1);
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (this.transform.GetChild(0) != null)
        {
            pos = this.transform.GetChild(0);
        }
    }
    protected void OnDrawGizmos()
    {
        pos = this.transform.GetChild(0);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(SpikeAtk());
        }

    }
    private IEnumerator SpikeAtk()
    {
        FindObjectOfType<SoundManager>().PlaySound("Trap");
        animator.SetTrigger("On");
        yield return new WaitForSeconds(0.5f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            Debug.Log(collider.tag);
            if (collider.tag == "Player")
            {
                collider.GetComponent<Player>().P_TakeDamage(damage);
            }
        }

    }
}
