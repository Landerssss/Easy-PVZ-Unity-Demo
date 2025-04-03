using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Start_Game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
