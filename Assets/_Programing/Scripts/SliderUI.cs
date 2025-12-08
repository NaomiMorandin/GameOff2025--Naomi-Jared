using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    public PlayerController player;
    public Image hpBar;
    public Image spBar;

    void Update()
    {

        spBar.fillAmount = player.sp / player.maxSP;
        hpBar.fillAmount = player.hp / player.maxHP;
    }
}
