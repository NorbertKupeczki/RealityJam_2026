using System;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoSingleton<ChallengeManager>
{
    public static event Action<uint> OnBreakTerminal;
    private static Queue<uint> m_ChallengeQueue;

    protected override void Awake()
    {
        base.Awake();
        m_ChallengeQueue = new();
        m_ChallengeQueue.Enqueue(0);
        m_ChallengeQueue.Enqueue(1);
        m_ChallengeQueue.Enqueue(2);
    }

    public static void TerminalIsReset(uint terminalId)
    {
        if (m_ChallengeQueue.Count < 1) { return; }

        Debug.Log($"Terminal {terminalId} is reset");
        if (TryTerminalId(terminalId))
        {
            Debug.Log("Success");
            if (m_ChallengeQueue.Count > 0) { return; }
            Debug.Log("Challenge cleared!");
            return;
        }

        Debug.Log("Challenge failed");
        m_ChallengeQueue.Clear();
    }

    private static bool TryTerminalId(uint terminalId)
    {
        var nextIdInQueue = m_ChallengeQueue.Dequeue();
        return nextIdInQueue == terminalId;
    }
}
