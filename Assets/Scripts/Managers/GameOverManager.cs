using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject deadLine;
    [SerializeField] private Transform fruitsParent;

    [Header(" Timer ")]
    [SerializeField] private float durationThreshold;
    private float timer;
    private bool timerOn;
    private bool isGameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            ManageGameOver();
        }
    }

    private void ManageGameOver()
    {
        if (timerOn)
        {
            ManageTimerOn();
            
        }
        else
        {
            if (IsFruitAboveLine())
            {
                StartTimer();
            }
        }
    }

    private void ManageTimerOn()
    {
        timer += Time.deltaTime;

        Debug.Log("Timer: " + timer);

        if (!IsFruitAboveLine())
        {
            StopTimer();
        }

        if (timer >= durationThreshold)
        {
            GameOver();
        }
    }

    private bool IsFruitAboveLine()
    {
        for (int i = 0; i < fruitsParent.childCount; ++i)
        {
            Fruit fruit = fruitsParent.GetChild(i).GetComponent<Fruit>();

            if (!fruit.HasCollided())
            {
                continue;
            }

            if(IsFruitAboveLine(fruitsParent.GetChild(i)))
            {
                return true;
            }
        }

        return false;
    }

    private void CheckForGameOver()
    {
        for (int i = 0; i < fruitsParent.childCount; ++i)
        {
            Fruit fruit = fruitsParent.GetChild(i).GetComponent<Fruit>();

            if (!fruit.HasCollided())
            {
                continue;
            }

            IsFruitAboveLine(fruitsParent.GetChild(i));
        }
    }

    private bool IsFruitAboveLine(Transform fruit)
    {
        if (fruit.position.y > deadLine.transform.position.y)
        {
            return true;
        }

        return false;
    }

    private void StartTimer()
    {
        timer = 0;
        timerOn = true;
    }

    private void StopTimer()
    {
        timerOn = false;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        isGameOver = true;

        GameManager.instance.SetGameOverState();
    }
}
