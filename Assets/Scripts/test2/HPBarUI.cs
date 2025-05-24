using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    public Slider hpSlider;
    public CharacterStats targetStats;
    
    void Start()
    {
        hpSlider.maxValue = targetStats.maxHP;
        hpSlider.value = targetStats.currentHP;
    }


    void Update()
    {
        hpSlider.value = targetStats.currentHP;
    }
}
