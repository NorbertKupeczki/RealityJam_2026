using System;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoSingleton<ChallengeManager>
{
    public static event Action<float> OnSystemIntegrityChanged;
    public static event Action<bool> OnTermialReset;
    public static event Action OnChallengeSuccessful;
    public static event Action OnMainframeBroken;

    [Header("System Integrity")]
    [SerializeField] private float m_SystemIntegrity;
    [SerializeField] private float m_DegradationRate;
    [SerializeField] private float m_ChallengeSpawnTimer;

    private Queue<uint> m_ChallengeQueue;
    private bool m_IsChallengeActive;

    private const float STARTING_DEGRADATION_RATE = 0.5f;
    private const float DEGRADATION_INCREASE = 0.1f;
    private const int INTEGRITY_BONUS = 5;
    private const float MAX_INTEGRITY = 100.0f;
    private const float CHALLENGE_SUCCESS_DELAY = 1.0f;
    private const float CHALLENGE_FAILURE_DELAY = 5.0f;

    [Header("DEBUG")]
    [SerializeField] private bool m_SystemDegradationOnPause;

    protected override void Awake()
    {
        base.Awake();
        Instance.m_DegradationRate = STARTING_DEGRADATION_RATE;
        Instance.m_SystemIntegrity = MAX_INTEGRITY;
        Instance.m_IsChallengeActive = false;
        Instance.m_ChallengeSpawnTimer = CHALLENGE_SUCCESS_DELAY;
    }

    private void Update()
    {
        ApplyDegradation();
        TrySpawnNewChallenge();
    }

    public static void TerminalIsReset(uint terminalId)
    {
        if (Instance.m_ChallengeQueue.Count < 1) { return; }

        //Debug.Log($"Terminal {terminalId} is reset");
        if (TryTerminalId(terminalId))
        {
            OnTermialReset?.Invoke(true);
            if (Instance.m_ChallengeQueue.Count > 0) { return; }
            ChallengeCleared();
            return;
        }
        
        ChallengeFailed();
        Instance.m_ChallengeQueue.Clear();
    }

    private static bool TryTerminalId(uint terminalId)
    {
        var nextIdInQueue = Instance.m_ChallengeQueue.Dequeue();
        return nextIdInQueue == terminalId;
    }

    private static void GenerateChallenge(int difficulty)
    {
        Debug.Log("New Challenge");
        Instance.m_ChallengeQueue = new();

        for (int i = 0; i < difficulty; ++i)
        {
            int newId;
            do
            {
                newId = UnityEngine.Random.Range(0, 6);
            } while (Instance.m_ChallengeQueue.Contains((uint)newId));
            Instance.m_ChallengeQueue.Enqueue((uint)newId);
        }
        Instance.m_IsChallengeActive = true;

        foreach(var id in Instance.m_ChallengeQueue)
        {
            Debug.Log(id + 1);
        }
    }

    private static void ChallengeCleared()
    {
        OnChallengeSuccessful?.Invoke();
        Instance.m_SystemIntegrity = Mathf.Clamp(Instance.m_SystemIntegrity + INTEGRITY_BONUS, 0.0f, MAX_INTEGRITY);
        Instance.m_DegradationRate += DEGRADATION_INCREASE;

        SetChallengeSpawnTimer(CHALLENGE_SUCCESS_DELAY);
    }

    private static void ChallengeFailed()
    {
        OnTermialReset?.Invoke(false);
        Instance.m_DegradationRate += DEGRADATION_INCREASE;

        SetChallengeSpawnTimer(CHALLENGE_FAILURE_DELAY);
    }

    private static void SetChallengeSpawnTimer(float challengeDelay)
    {
        Instance.m_IsChallengeActive = false;
        Instance.m_ChallengeSpawnTimer = challengeDelay;
    }

    private static void ApplyDegradation()
    {
        if (Instance.m_SystemDegradationOnPause) { return; }

        Instance.m_SystemIntegrity -= Instance.m_DegradationRate * Time.deltaTime;        

        if (Instance.m_SystemIntegrity > 0.0f)
        {
            OnSystemIntegrityChanged?.Invoke(Instance.m_SystemIntegrity);
            return;
        }

        Instance.m_SystemIntegrity = 0.0f;
        OnSystemIntegrityChanged?.Invoke(Instance.m_SystemIntegrity);

        OnMainframeBroken?.Invoke();
    }

    private static void TrySpawnNewChallenge()
    {
        if (Instance.m_IsChallengeActive) { return; }

        Instance.m_ChallengeSpawnTimer -= Time.deltaTime;

        if (Instance.m_ChallengeSpawnTimer > 0.0f) { return; }

        GenerateChallenge(5 - Mathf.CeilToInt(Instance.m_SystemIntegrity / 25.0f));
    }
}
