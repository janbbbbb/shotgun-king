using UnityEngine;
using UnityEngine.UI;

public class AmmoPanelUI : MonoBehaviour
{
    public Image[] ammoSlots;        // Assign your UI Image slots
    public Sprite fullShellSprite;
    public Sprite emptyShellSprite;

    private PlayerShooting playerShooting;

    void Update()
    {
        // Check if player exists, if not try to find it
        if (playerShooting == null)
        {
            playerShooting = FindObjectOfType<PlayerShooting>();
            if (playerShooting == null)
                return; // Player not yet spawned
        }

        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        int currentAmmo = playerShooting.currentAmmo;
        int maxAmmo = playerShooting.maxAmmo;

        for (int i = 0; i < ammoSlots.Length; i++)
        {
            if (i < currentAmmo)
                ammoSlots[i].sprite = fullShellSprite;
            else
                ammoSlots[i].sprite = emptyShellSprite;
        }
    }
}
