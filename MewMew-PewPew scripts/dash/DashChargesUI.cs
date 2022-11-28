using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashChargesUI : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] int maxDashCharges;
    [SerializeField] int usableDashCharges;
    [SerializeField] Image dashIcon;

    [SerializeField] List<Image> UIDashes = new List<Image>();

    private void OnEnable()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.dashUpdated += UpdateDashIcons;
    }

    private void OnDisable()
    {
        playerMovement.dashUpdated -= UpdateDashIcons;
    }

    void UpdateDashIcons() 
    {
        for (int i = UIDashes.Count - 1; i >= 0; i--)
        {
            Debug.Log(i);
            Image image = UIDashes[i];
            UIDashes.RemoveAt(i);
            Destroy(image.gameObject);
        }
        InstantiateDashIcons();
    }

    void Start()
    {
        maxDashCharges = playerMovement.maxDashCharges;
        usableDashCharges = playerMovement.dashCharges;
        InstantiateDashIcons();
    }
    void InstantiateDashIcons()
    {
        for (int i = 0; i < playerMovement.maxDashCharges; i++)
        {
            Image dashImage;
            dashImage = Instantiate(dashIcon);
            //ChangeColorToActive(dashImage);
            AddDashImageToPool(dashImage);            
        }
    }
    void Update()
    {
        maxDashCharges = playerMovement.maxDashCharges;
        usableDashCharges = playerMovement.dashCharges;
        
        for (int i = 0; i < (maxDashCharges - usableDashCharges); i++)
        {
            ChangeColorToInactive(UIDashes[i]);
        }

        if (usableDashCharges > 0)
        {
            for (int i = maxDashCharges; i >= 0; i--)
            {
                if (i == usableDashCharges)
                {
                    for (int j = maxDashCharges - 1; j > (maxDashCharges - 1) - usableDashCharges; j--)
                    {
                        ChangeColorToActive(UIDashes[j]);
                    }
                }
            }
        }
    }

    void ChangeColorToInactive(Image dashImage)
    {
        dashImage.color = Color.gray;
    }

    void ChangeColorToActive(Image dashImage)
    {
        dashImage.color = Color.white;
    }

    void AddDashImageToPool(Image dashIcon)
    {
        UIDashes.Add(dashIcon);
        dashIcon.transform.parent = this.transform;
    }
}
