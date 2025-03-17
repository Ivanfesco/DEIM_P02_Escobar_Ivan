using UnityEngine;
using UnityEngine.SceneManagement;
public class mainmenumanager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        gameObject.SetActive(false);
    }

    public void Quitgame()
    {
        Application.Quit();
    }


}
