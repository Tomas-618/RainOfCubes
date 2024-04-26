using UnityEngine;

public class RandomUtils
{
    public static Color GetRandomColor()
    {
        float minColorValue = 0;
        float maxColorValue = 1;

        float redValue = Random.Range(minColorValue, maxColorValue);
        float greenValue = Random.Range(minColorValue, maxColorValue);
        float blueValue = Random.Range(minColorValue, maxColorValue);

        return new Color(redValue, greenValue, blueValue);
    }
}
