using UnityEngine;
using UnityEngine.UI;

public class XPBarController : MonoBehaviour
{
    public Slider xpSlider;
    public Text levelText;

    private int currentXP;
    private int maxXp = 100;
    private int currentLevel = 1;

    void Start()
    {
        UpdateUI();
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        UpdateUI();

        if (currentXP >= maxXp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentXP = 0;
        maxXp = CalculateMaxXPForNextLevel();
        UpdateUI();
    }

    private int CalculateMaxXPForNextLevel()
    {
        return currentLevel * 100;
    }

    private void UpdateUI()
    {
        xpSlider.value = (float)currentXP / maxXp;
        levelText.text = "Level: " + currentLevel.ToString();
    }
}
