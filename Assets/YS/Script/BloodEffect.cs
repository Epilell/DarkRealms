using System.Collections;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public Player player;
    public bool isBlood;

    private void Start() { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }

    public void Blood(float damage) { if (isBlood) { player.P_Heal(damage / 10); } }

    public void SetBlood(float duration) { StartCoroutine(ResetBlood(duration)); }

    private IEnumerator ResetBlood(float duration)
    {
        isBlood = true;

        yield return new WaitForSeconds(duration);

        isBlood = false;
    }
}