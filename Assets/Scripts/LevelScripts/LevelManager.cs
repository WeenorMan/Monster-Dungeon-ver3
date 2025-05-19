using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int enemyCount = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (enemyCount == 0)
        {
            print("YOU WIN !!");
        }
    }
}
