using System.Collections;
using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class PlayerSpawnerManager : NetworkBehaviour
{
    [Header("Player Spawner Manager Settings")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float spawnDelay = 2f;

    private bool isSpawning = false;

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            return;
        }

        //CallSpawn();
    }

    public void CallSpawn()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnPlayerRoutine());
        }
    }

    private IEnumerator SpawnPlayerRoutine()
    {
        isSpawning = true;

        yield return new WaitForSeconds(spawnDelay);

        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points assigned.");
            isSpawning = false;

            yield break;
        }

        Transform rngPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject playerObj = ObjectPooler.Instance.GetFromPool("player", rngPoint.position, Quaternion.identity);
        Player player = playerObj.GetComponent<Player>();
        if(player != null)
        {
            player.RegisterPlayer();
        }

        if (playerObj == null)
        {
            Debug.LogError("Player does not exist in pool!");
        }

        isSpawning = false;
    }
}
