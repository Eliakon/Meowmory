using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class LeaderboardDataManager
{
    public static readonly string Entries4x4Key = "Leaderboard4x4";
    public static readonly string Entries6x6Key = "Leaderboard6x6";
    private static readonly int LeaderboardSize = 15;

    private static Dictionary<string, LeaderboardEntry[]> leaderboards = new Dictionary<string, LeaderboardEntry[]>();

    public static LeaderboardEntry[] GetEntries(string boardKey)
    {
        if (!leaderboards.ContainsKey(boardKey))
        {
            leaderboards.Add(boardKey, ParsePlayerPrefs(boardKey));
        }
        return leaderboards[boardKey];
    }

    private static LeaderboardEntry[] ParsePlayerPrefs(string boardKey)
    {
        if (PlayerPrefs.HasKey(boardKey))
        {
            var data = PlayerPrefs.GetString(boardKey);
            var items = data.Split(';');
            var leaderboard = new LeaderboardEntry[items.Length];
            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i].Split(',');
                leaderboard[i] = new LeaderboardEntry {
                    Rank = i + 1,
                    Name = item[0],
                    Moves = System.Convert.ToInt32(item[1])
                };
            }
            return leaderboard;
        }
        return new LeaderboardEntry[0];
    }

    public static bool IsHighScore(string boardKey, int score)
    {
        var leaderboard = GetEntries(boardKey);

        if (leaderboard.Length < LeaderboardSize)
        {
            return true;
        }

        var lowestScore = leaderboard[leaderboard.Length - 1].Moves;
        return score < lowestScore;
    }

    public static void SaveScore(string boardKey, int score, string name)
    {
        var leaderboard = GetEntries(boardKey);
        var newData = new StringBuilder();
        var newLeaderboardLength = Mathf.Min(LeaderboardSize, leaderboard.Length + 1);
        var scoreAdded = false;
        var currentLength = 0;
            
        System.Array.ForEach(leaderboard, (entry) =>
        {
            if (currentLength >= newLeaderboardLength)
            {
                return;
            }

            if (!scoreAdded && entry.Moves > score)
            {
                AddLine(newData, name, score);
                scoreAdded = true;
                currentLength++;
            }

            if (currentLength >= newLeaderboardLength)
            {
                return;
            }

            AddLine(newData, entry.Name, entry.Moves);
            currentLength++;
        });

        if (!scoreAdded)
        {
            AddLine(newData, name, score);
        }

        leaderboards.Remove(boardKey);
        PlayerPrefs.SetString(boardKey, newData.ToString());
        PlayerPrefs.Save();
    }

    private static void AddLine(StringBuilder data, string name, int moves)
    {
        if (0 < data.Length)
        {
            data.Append(";");
        }
        data.AppendFormat("{0},{1}", name, moves);
    }
}
