using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    public PlayerController player;  // Drag your player here
    public Scrollbar hpBar;
    public Scrollbar spBar;

    void Update()
    {
        // Scrollbars use values from 0–1 only.
        
        spBar.size = player.sp / player.maxSP;
    }
}
