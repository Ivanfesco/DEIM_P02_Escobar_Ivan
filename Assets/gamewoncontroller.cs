using UnityEngine;
using UnityEngine.SceneManagement;

public class gamewoncontroller : MonoBehaviour
{

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
