using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class Playerhp : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] int playershp = 8;
    [SerializeField] CinemachineVirtualCamera deathcamera;
    [SerializeField] Transform weaponcamera;
    [SerializeField] Image[] shieldbars;
    [SerializeField] GameObject gameovercontainer;

    int currenthp;
    int gameovercamerapriority = 20;

    void Awake()
    {
        currenthp = Mathf.Min(playershp, shieldbars.Length);
        shieldui();
    }

    public void Damagetaken(int amount)
    {
        currenthp -= amount;
        currenthp = Mathf.Max(0, currenthp); // Prevent negative HP

        shieldui(); // Update UI

        if (currenthp <= 0)
        {
            gameoverscreen();
        }
    }

    void gameoverscreen()
    {
        weaponcamera.parent = null;
        deathcamera.Priority = gameovercamerapriority;
        gameovercontainer.SetActive(true);

        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.cursorLocked = false;

        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;                  // Show the cursor

        Destroy(this.gameObject);
    }


    void shieldui()
    {
        for (int i = 0; i < shieldbars.Length; i++)
        {
            if (i < currenthp)
            {
                shieldbars[i].gameObject.SetActive(true);
            }
            else
            {
                shieldbars[i].gameObject.SetActive(false);
            }
        }
    }
}