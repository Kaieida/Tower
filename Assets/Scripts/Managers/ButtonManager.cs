using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button[] levelButtons;
    [SerializeField] int levelRow;
    [SerializeField] FloorManager floorManager;
    [SerializeField] EnemyManager enemyManager;

    private void Start()
    {
        SetLevelButtons();
    }

    private void Update()
    {
        Debug.Log(enemyManager.maxCompletedLevel / 5);
    }

    public void ChangeLevelButtonsRight()
    {
        if (enemyManager.maxCompletedLevel / 5 >= levelRow+1)
        {
            levelRow++;
            SetLevelButtons();
        }
    }

    public void ChangeLevelButtonsLeft()
    {
        if (levelRow != 0)
        {
            levelRow--;
            SetLevelButtons();
        }
    }

    private void SetLevelButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = (i + 1) + 5 * levelRow;
            levelButtons[i].onClick.RemoveAllListeners();
            levelButtons[i].onClick.AddListener(delegate { floorManager.ChangeFloor(level-1); });
            levelButtons[i].GetComponentInChildren<Text>().text = level.ToString();
        }
        UpdateButtons();
    }

    public void UpdateButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = (i + 1) + 5 * levelRow;
            if(level <= enemyManager.maxCompletedLevel)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
