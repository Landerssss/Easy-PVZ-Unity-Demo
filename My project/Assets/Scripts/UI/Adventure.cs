using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Adventure : MonoBehaviour
{
    public void OnAdventureClick()
    {
        SceneManager.LoadScene(2);
    }

}
