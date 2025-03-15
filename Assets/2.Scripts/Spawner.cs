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
            monster.Init();
            monster.gameObject.SetActive(true);
            SetLayer(monster);
        }
    }

    private void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, layer);
        }
    }

    private void SetLayer(Monster monster)
    {
        int layer = 0;

        switch (Index)
        {
            case 1:
                {
                    layer = LayerMask.NameToLayer("Monster1");
                    monster.Collider.excludeLayers = LayerMask.GetMask("Monster2", "Monster3", "Floor2", "Floor3");
                }
                break;

            case 2:
                {
                    layer = LayerMask.NameToLayer("Monster2");
                    monster.Collider.excludeLayers = LayerMask.GetMask("Monster1", "Monster3", "Floor1", "Floor3");
                }
                break;

            case 3:
                {
                    layer = LayerMask.NameToLayer("Monster3");
                    monster.Collider.excludeLayers = LayerMask.GetMask("Monster1", "Monster2", "Floor1", "Floor2");
                }
                break;
        }

        ChangeLayerRecursively(monster.gameObject, layer);
    }
}