using UnityEngine;
using UnityEngine.UI;

public class WinMessage : MonoBehaviour
{
    [SerializeField]
    private Text movesValue;

    public void Show(int moves)
    {
        movesValue.text = moves.ToString();
        gameObject.SetActive(true);
    }
}
