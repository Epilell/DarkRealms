using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField][Range(0.5f,5f)]
    private float Speed = 2f;

    //총알 삭제
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        //생성시 5초후 삭제
        Invoke("DestroyBullet", 5);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }
}
