using TMPro;
using UnityEngine;

public class TerminalUI : MonoBehaviour
{
    [SerializeField] private TMP_Text m_IdText;

    public void SetID(uint id)
    {
        m_IdText.text = id.ToString();
    }
}
