using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EditScene : MonoBehaviour
{
    [SerializeField] EditBoxLayouter Layouter = null;

    public void StartGame()
    {
        GameManager.SavePlayerDeck(Layouter.GetDeck());
        GameManager.CreateRandomEnemyDeck();

        SceneManager.LoadScene(1);
    }
}
