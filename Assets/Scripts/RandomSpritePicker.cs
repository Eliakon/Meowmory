using UnityEngine;

public class RandomSpritePicker : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    public Sprite Pick()
    {
        if (null == sprites || 0 == sprites.Length)
        {
            Debug.LogError("Try to pick random sprite, but array is null or empty");
            return null;
        }

        var index = Random.Range(0, sprites.Length);
        return sprites[index];
    }
}
