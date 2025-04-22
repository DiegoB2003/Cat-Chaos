using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}