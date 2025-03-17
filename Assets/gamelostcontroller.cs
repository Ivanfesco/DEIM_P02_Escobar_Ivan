using UnityEngine;
using UnityEngine.SceneManagement;
public class gamelostcontroller : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        gameObject.SetActive(false);
    }


    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
