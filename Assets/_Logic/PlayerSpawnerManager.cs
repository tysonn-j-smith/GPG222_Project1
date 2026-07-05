using System.Collections;
using UnityEngine;

public class PlayerSpawnerManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float spawnDelay = 2f;

    private GameObject playerObj;

    private bool isSpawning = false;

    private void Start()
    {
        if (spawnPos == null)
        {
            Debug.LogError($"Player spawn position not set!");
            return;
        }

        if(isSpawning == true)
        {
            return;
        }

        CallSpawn();
    }

    public void CallSpawn()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnPlayerRoutine());
    }

    private IEnumerator SpawnPlayerRoutine()
    {
        isSpawning = true;

        yield return new WaitForSeconds(spawnDelay);

        playerObj = ObjectPooler.Instance.GetFromPool("player", spawnPos.position, Quaternion.identity);

        if (playerObj == null)
        {
            Debug.LogError($"Player does not exist in pool!");
            isSpawning = false;

            yield break;
        }

        isSpawning = false;
    }
}
