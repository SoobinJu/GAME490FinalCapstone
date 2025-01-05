using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // UI를 사용하기 위해 추가

public class collectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] collectiblePrefabs; // 콜렉터블 프리팹 배열
    public float collectibleSpawnTime = 2f;  // 콜렉터블 스폰 시간
    public float collectibleSpeed = 1f; // 콜렉터블 속도


    private float timeUntilCollectibleSpawn;

    // 콜렉터블 획득 수를 추적하는 변수
    private Dictionary<string, int> collectibleCount = new Dictionary<string, int>();

    [Header("UI")]
    public Text collectibleCountText;  // UI에서 콜렉터블 획득 수를 표시할 텍스트

    private void Update()
    {
        SpawnLoop();
        UpdateUI();
    }

    private void SpawnLoop()
    {
        timeUntilCollectibleSpawn += Time.deltaTime;

        // 콜렉터블 스폰
        if (timeUntilCollectibleSpawn >= collectibleSpawnTime)
        {
            Spawn();
            timeUntilCollectibleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        // 콜렉터블 랜덤 선택
        GameObject collectibleToSpawn = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];
        GameObject spawnedCollectible = Instantiate(collectibleToSpawn, transform.position, Quaternion.identity);

        // 콜렉터블에 Rigidbody2D를 추가하여 속도 설정
        Rigidbody2D rb = spawnedCollectible.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.left * collectibleSpeed;  // 왼쪽으로 이동
        }

        // 콜렉터블인 경우 카운팅
        string prefabName = collectibleToSpawn.name;
        if (!collectibleCount.ContainsKey(prefabName))
        {
            collectibleCount[prefabName] = 0;  // 최초 생성 시 카운트 0
        }
    }

    // UI 업데이트 (각 콜렉터블 개수 표시)
    public void UpdateUI()
    {
        if (collectibleCountText != null)
        {
            collectibleCountText.text = "Collectibles: \n";
            foreach (KeyValuePair<string, int> entry in collectibleCount)
            {
                collectibleCountText.text += $"{entry.Key}: {entry.Value}\n";
            }
        }
    }

    // 콜렉터블을 얻었을 때 호출되는 메서드 (예: 충돌 시)
    public void Collect(GameObject collectible)
    {
        //string collectibleName = collectible.name;
        string collectibleName = collectible.name.Replace("(Clone)", "");

        if (collectibleCount.ContainsKey(collectibleName))
        {
            collectibleCount[collectibleName]++;
        }
        else
        {
            collectibleCount[collectibleName] = 1;
        }

        Destroy(collectible);  // 콜렉터블 제거
    }
}