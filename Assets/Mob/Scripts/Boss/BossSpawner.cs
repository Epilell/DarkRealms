using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject bossSpawningEffect;
    [SerializeField]
    private GameObject bossSpawnPoint;
    [SerializeField]
    private GameObject bossHPSliderPrefab;
    private Transform canvasTransform;
    private bool _isSpawn = false;

    private GameObject mainCamera;
    private Camera camera;
    private Transform cameraTransform;

    public FollowObject followObject;

    private void Awake()
    {
        //StartCoroutine("BossSpawn");
        canvasTransform = GameObject.Find("Canvas").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (_isSpawn == false)
            {
                StartCoroutine(CameraGrow());
                _isSpawn = true;
            }

        }

    }
    private IEnumerator CameraGrow()
    {
        mainCamera = GameObject.Find("Main Camera");
        camera = mainCamera.GetComponent<Camera>();
        cameraTransform = mainCamera.transform;

        StartCoroutine(FindObjectOfType<FadeOut>().BossFadeInOut()); // 여기서 코루틴 만들면 할당할 오브젝트 많아서 FadeOut에서 생성 후 호출함
        yield return new WaitForSeconds(1f); // 1초 대기 후 카메라 시점 변환

        for (int i = 50; i <= 100; i++)
        {
            camera.orthographicSize = i/10;

            cameraTransform.localPosition = new Vector3(0, (i/10)-5, -30f);
            yield return new WaitForSeconds(0.02f);
        }

        followObject.cameraChangeUp = true; // 카메라 시점 변환 완료

        StartCoroutine(BossSpawn());
    }

    private IEnumerator BossSpawn()
    {
        yield return new WaitForSeconds(1f);
        GameObject bossSpawn = Instantiate(bossSpawningEffect, bossSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        FindObjectOfType<SoundManager>().BossPlaySound("Roar");
        Destroy(bossSpawn, 4f);
        yield return new WaitForSeconds(4f);
        GameObject clone = Instantiate(boss, bossSpawnPoint.transform.position, Quaternion.identity) as GameObject;
        clone.GetComponent<BossPattern>().SetCanvas(canvasTransform);
        SpawnBossHPSlider(clone);
        yield break;
    }
    private void SpawnBossHPSlider(GameObject Boss)
    {
        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(bossHPSliderPrefab);

        //Slider UI 프로젝트를 parent("Canvas" 오브젝트)의 자식으로 설정 단, UI는 캔버스의 자식으로 설정되어 있어야 화면에 보임
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(480, -110);
        sliderClone.GetComponent<BossHPViewer>().Setup(Boss.GetComponent<BossHP>());
    }
}
