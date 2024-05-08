using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IXPBarUI
{
    void SetXP(float xpNormalized);
    void SetLevel(int level);
}

public class XPBarController : MonoBehaviour, IXPBarUI
{
    public Slider xpSlider;
    public TMP_Text levelText;
    [SerializeField] EnemySpawner spawner;
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
        spawner.UpdateLevel(currentLevel);
    }

    private int CalculateMaxXPForNextLevel()
    {
        return currentLevel * 100;
    }

    public void SetXP(float xpNormalized)
    {
        xpSlider.value = xpNormalized;
    }

    public void SetLevel(int level)
    {
        levelText.text = "Level: " + level.ToString();
    }

    private void UpdateUI()
    {
        SetXP((float)currentXP / maxXp);
        SetLevel(currentLevel);
    }
}