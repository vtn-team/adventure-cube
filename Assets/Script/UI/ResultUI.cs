using UnityEngine;
using System.Collections;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Winner = null;

    bool isAlreadySetup = false;

    public void SetLoser(int playerId)
    {
        if (isAlreadySetup) return;

        Winner.text = playerId == 2 ? "Player Win !" : "Enemy Win !";
        isAlreadySetup = true;
    }
}
