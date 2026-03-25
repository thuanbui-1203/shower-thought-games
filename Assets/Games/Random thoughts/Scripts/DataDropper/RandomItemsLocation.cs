using UnityEngine;

public class RandomItemsLocation : MonoBehaviour
{
    public static Vector2 GenerateRandomLocation()
    {
        float x = Random.Range(-12f, 12f);
        float y = Random.Range(-3f, 8f);
        return new Vector2(x, y);
    }
}
