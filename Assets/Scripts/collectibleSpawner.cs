using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // UI�� ����ϱ� ���� �߰�

public class collectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] collectiblePrefabs; // �ݷ��ͺ� ������ �迭
    public float collectibleSpawnTime = 2f;  // �ݷ��ͺ� ���� �ð�
    public float collectibleSpeed = 1f; // �ݷ��ͺ� �ӵ�


    private float timeUntilCollectibleSpawn;

    // �ݷ��ͺ� ȹ�� ���� �����ϴ� ����
    private Dictionary<string, int> collectibleCount = new Dictionary<string, int>();

    [Header("UI")]
    public Text collectibleCountText;  // UI���� �ݷ��ͺ� ȹ�� ���� ǥ���� �ؽ�Ʈ

    private void Update()
    {
        SpawnLoop();
        UpdateUI();
    }

    private void SpawnLoop()
    {
        timeUntilCollectibleSpawn += Time.deltaTime;

        // �ݷ��ͺ� ����
        if (timeUntilCollectibleSpawn >= collectibleSpawnTime)
        {
            Spawn();
            timeUntilCollectibleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        // �ݷ��ͺ� ���� ����
        GameObject collectibleToSpawn = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];
        GameObject spawnedCollectible = Instantiate(collectibleToSpawn, transform.position, Quaternion.identity);

        // �ݷ��ͺ� Rigidbody2D�� �߰��Ͽ� �ӵ� ����
        Rigidbody2D rb = spawnedCollectible.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.left * collectibleSpeed;  // �������� �̵�
        }

        // �ݷ��ͺ��� ��� ī����
        string prefabName = collectibleToSpawn.name;
        if (!collectibleCount.ContainsKey(prefabName))
        {
            collectibleCount[prefabName] = 0;  // ���� ���� �� ī��Ʈ 0
        }
    }

    // UI ������Ʈ (�� �ݷ��ͺ� ���� ǥ��)
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

    // �ݷ��ͺ��� ����� �� ȣ��Ǵ� �޼��� (��: �浹 ��)
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

        Destroy(collectible);  // �ݷ��ͺ� ����
    }
}