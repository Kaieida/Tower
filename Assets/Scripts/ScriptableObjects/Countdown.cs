using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] Text countdown;
    [SerializeField] int time;
    [SerializeField] float fTime;
    [SerializeField] EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        countdown.text = time.ToString();
        fTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    public void Timer()
    {
        if (time != 0)
        {
            fTime -= Time.deltaTime;
            time = (int)fTime;
            countdown.text = time.ToString();
        }
        else
        {
            //enemyManager.RestartLevel(GameObject.FindWithTag("Enemy"));
            //ResetCountdown();
        }
    }

    public void ResetCountdown()
    {
        time = 31;
        fTime = 31;
    }
}
