using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : InfiniteScrollListItem
{
    [SerializeField]
    private Text rank;

    [SerializeField]
    private Text playerName;

    [SerializeField]
    private Text moves;

    [SerializeField]
    private Image background;

    [SerializeField]
    private Color evenColor;

    [SerializeField]
    private Color oddColor;

    public override void Set(object value, int index)
    {
        base.Set(value, index);

        var entry = value as LeaderboardEntry?;
        gameObject.SetActive(entry.HasValue);

        if (!entry.HasValue)
        {
            return;
        }

        rank.text = string.Format("{0}.", entry.Value.Rank);
        playerName.text = entry.Value.Name;
        moves.text = entry.Value.Moves.ToString();
        background.color = index % 2 == 0 ? evenColor : oddColor;
    }
}
