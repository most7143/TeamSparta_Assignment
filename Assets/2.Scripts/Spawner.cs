using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int Index;

    public void Spawn()
    {
        Monster monster = ObjectPool.Instance.SpawnMonster().GetComponent<Monster>();

        if (monster != null)
        {
            monster.transform.position = transform.position;
            monster.SpawnIndex = Index;
            monster.FixY = transform.position.y;
            monster.gameObject.SetActive(true);
        }
    }
}