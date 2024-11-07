using UnityEngine;

public class SpiderAppearance : MonoBehaviour
{
    [SerializeField] private SpiderAppearanceSettings appearanceSettings;

    private void Start()
    {
        ApplyAppearance();
    }
    

    //private void Update()
    //{
    //    if (appearanceSettings != null)
    //    {
    //        ApplyAppearance();
    //    }
    //}
    public void TriggerApplyAppearance()
    {
        ApplyAppearance();  // Calls the private ApplyAppearance method
    }

    private void ApplyAppearance()
    {
        // Cephalothorax Setup
        GameObject cephalothorax = transform.Find("Cephalothorax").gameObject;
        if (cephalothorax != null)
        {
            ApplyBodyPartSettings(cephalothorax, appearanceSettings.cephalothorax);
        }

        // Abdomen Setup
        GameObject abdomen = transform.Find("Abdomen").gameObject;
        if (abdomen != null)
        {
            ApplyBodyPartSettings(abdomen, appearanceSettings.abdomen);
        }

        // Legs Setup
        Transform legParent = transform.Find("Legs");
        if (legParent != null)
        {
            foreach (Transform leg in legParent)
            {
                ApplyLegSettings(leg.gameObject, appearanceSettings.legs);
            }
        }

        // Additional parts: spinnerets, pedipalps, etc.
        // Repeat this process as needed for other body parts
    }

    private void ApplyBodyPartSettings(GameObject bodyPart, SpiderAppearanceSettings.BodyPartSettings settings)
    {
        MeshRenderer renderer = bodyPart.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material.color = settings.Color;
            renderer.material.mainTexture = settings.Texture;
        }

        bodyPart.transform.localScale = Vector3.one * settings.Size;
    }

    private void ApplyLegSettings(GameObject leg, SpiderAppearanceSettings.LegSettings settings)
    {
        leg.transform.localScale = new Vector3(settings.Thickness, settings.LegLength, settings.Thickness);

        MeshRenderer renderer = leg.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material.color = settings.Color;
            renderer.material.mainTexture = settings.Texture;
        }
    }
}
