using UnityEngine;

public class enemyhp : MonoBehaviour
{
    [SerializeField] GameObject deathfx; // Assign the hit effect prefab in the Inspector
    [SerializeField] int basehp = 3;

    int currenthp;

    gamesettingsstuff gameSettings;


     void Start()
    {
      gameSettings = FindAnyObjectByType<gamesettingsstuff>();
      gameSettings.Enemycount(1); // Notify the game settings about this enemy
    }

    void Awake()
    {
        currenthp = basehp;
    }
    public void Damagetaken(int amount)
    {
        currenthp -= amount;
        if (currenthp <= 0)
        {
            gameSettings.Enemycount(-1); // Notify the game settings about this enemy being destroyed
            kaboom();
        }
    }

    public void kaboom()
    {
        Instantiate(deathfx, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }
}
