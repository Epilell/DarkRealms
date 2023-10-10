using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    public float magnetForce = 1f; // 자석 효과의 강도
    public float magnetRange = 4f;
    private float distanceToPlayer;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < magnetRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, magnetForce * Time.deltaTime);
        }
    }
}
