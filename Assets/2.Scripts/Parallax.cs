using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float[] speed;
    public Transform[] sprites;

    public float MaxX;

    private void Update()
    {
        Scrolling();
    }

    private void Scrolling()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].transform.position += -transform.right * speed[i] * Time.deltaTime;

            if (sprites[i].transform.position.x < MaxX)
            {
                sprites[i].transform.position = Vector3.zero;
            }
        }
    }
}