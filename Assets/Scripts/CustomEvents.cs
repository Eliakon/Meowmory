using System;
using UnityEngine.Events;

public class CustomEvents
{
    [Serializable]
    public class UnityIntEvent : UnityEvent<int> { }

    [Serializable]
    public class UnityStringIntEvent : UnityEvent<string, int> { }

    [Serializable]
    public class UnityCardEvent : UnityEvent<Card> { }

    [Serializable]
    public class UnityObjectArrayEvent : UnityEvent<object[]> { }
}
