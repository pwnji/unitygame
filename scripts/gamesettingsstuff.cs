using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gamesettingsstuff : MonoBehaviour
{

    [SerializeField] TMP_Text enemysleft;
    [SerializeField] GameObject youtwintext;

    int enemycount = 0;
    const string enemyTag = "enemys left: ";


    public void quitbutton()
    {
        Application.Quit();
        Debug.Log("Quit Game");

    }


    public void Enemycount(int amount)
    {
        enemycount += amount;
        enemysleft.text = enemyTag + enemycount.ToString();
        if (enemycount <= 0)
        {
            youtwintext.SetActive(true);
            
        }

    }



    public void restartlevel()
    {
      int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

    }
}
