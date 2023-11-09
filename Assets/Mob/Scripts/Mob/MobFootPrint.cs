using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobFootPrint : MonoBehaviour
{
    [SerializeField]
    private GameObject footprint1;
    [SerializeField]
    private GameObject footprint2;
    [SerializeField]
    private GameObject footprint3;

    private MobHP mobHP;
    private MobAI mobAI;
    [SerializeField]
    private float footPrintSize=1;
    [SerializeField]
    private GameObject footPrintPos;
    [SerializeField]
    private float footPrintDelay = 1f;
    private Vector3 footvec;
    [SerializeField]
    private Vector3 footvecCV= new Vector3(0,-0.3f,-4);
    // Start is called before the first frame update
    private void Start()
    {
        mobHP = GetComponent<MobHP>();
        mobAI = GetComponent<MobAI>();
        footvec = footPrintPos.transform.localPosition;
        StartCoroutine(printing());
    }
    private IEnumerator printing()
    {
        while (!mobHP.IsDie)
        {
            if (mobAI.moveSpeed != 0f|| !mobAI.IsAttack)
            {
                GameObject print1 = Instantiate(footprint1, this.transform.position + footvec + footvecCV, Quaternion.identity);
                print1.GetComponent<DecreaseAlpha>().SetUp(1f);
                print1.transform.localScale = new Vector3(footPrintSize, footPrintSize,footPrintSize);
                Destroy(print1, 0.5f);
                yield return new WaitForSeconds(footPrintDelay);

                GameObject print2 = Instantiate(footprint2, this.transform.position + footvec + footvecCV, Quaternion.identity);
                print2.GetComponent<DecreaseAlpha>().SetUp(1f);
                print2.transform.localScale = new Vector3(footPrintSize, footPrintSize, footPrintSize);
                Destroy(print2, 0.5f);
                yield return new WaitForSeconds(footPrintDelay);

                GameObject print3 = Instantiate(footprint3, this.transform.position + footvec + footvecCV, Quaternion.identity);
                print3.GetComponent<DecreaseAlpha>().SetUp(1f);
                print3.transform.localScale = new Vector3(footPrintSize, footPrintSize, footPrintSize);
                Destroy(print1, 0.5f);
                yield return new WaitForSeconds(footPrintDelay);
            }
        }
    }
}
