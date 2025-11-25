using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    public PlayerController player;  // Drag your player here
    public Image hpBar;
    public Image spBar;

    void Update()
    {
        // Scrollbars use values from 0–1 only.
        
         hpBar.fillAmount = player.hp / player.maxHP;
         spBar.fillAmount = player.sp / player.maxSP;
    }
}
