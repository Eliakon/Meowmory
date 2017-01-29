using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LeaderboardScrollList : MonoBehaviour
{
    [SerializeField]
    private Text listEmptyMessage;

    [SerializeField]
    private CustomEvents.UnityObjectArrayEvent setEntries;

    [SerializeField]
    private UnityEvent resumeSwitchEntries;

    private bool currentEntries4x4;

    private void OnEnable()
    {
        currentEntries4x4 = true;
        SetEntries();
    }

    private void SetEntries()
    {
        var key = currentEntries4x4 ? LeaderboardDataManager.Entries4x4Key : LeaderboardDataManager.Entries6x6Key;
        var entries = LeaderboardDataManager.GetEntries(key);
        var objectEntries = System.Array.ConvertAll(entries, (entry) => (object)entry);

        listEmptyMessage.gameObject.SetActive(entries.Length == 0);
        setEntries.Invoke(objectEntries);
    }

    public void SwitchBoard()
    {
        currentEntries4x4 = !currentEntries4x4;
        SetEntries();
        resumeSwitchEntries.Invoke();
    }
}
