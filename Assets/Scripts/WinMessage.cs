using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinMessage : MonoBehaviour
{
    [SerializeField]
    private Text movesValue;

    [SerializeField]
    private GameObject[] highScore;

    [SerializeField]
    private InputField nameInput;

    [SerializeField]
    private CanvasGroup highScoreOkButton;

    [SerializeField]
    private UnityEvent hideWinMessage;

    private bool isHighScore;
    private int moves;
    private string boardKey;

    public void Show(string board, int movesCount)
    {
        moves = movesCount;
        boardKey = board;

        movesValue.text = moves.ToString();

        isHighScore = LeaderboardDataManager.IsHighScore(boardKey, moves);
        for (var i = 0; i < highScore.Length; i++)
        {
            highScore[i].SetActive(isHighScore);
        }
        nameInput.text = string.Empty;
        
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (isHighScore)
        {
            nameInput.text = nameInput.text
                .Replace(";", string.Empty)
                .Replace(",", string.Empty);

            if (string.Empty == nameInput.text)
            {
                return;
            }
            LeaderboardDataManager.SaveScore(boardKey, moves, nameInput.text);
        }
        hideWinMessage.Invoke();
    }

    public void NameChanged(string value)
    {
        highScoreOkButton.alpha = string.Empty == value ? 0.5f : 1f;
    }
}
