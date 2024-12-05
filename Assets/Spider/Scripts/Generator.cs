using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private SpiderAppearanceSettings appearanceSettings;
    private SpiderAppearance spiderAppearance;

    private void Start()
    {
        spiderAppearance = GetComponent<SpiderAppearance>();
        // Start calling GenerateRandomSpider every second
        InvokeRepeating(nameof(GenerateRandomSpider), 0f, 1f);
    }

    public void GenerateRandomSpider()
    {
        // Randomize cephalothorax, abdomen, and leg sizes
        appearanceSettings.cephalothorax.Size = Random.Range(0.8f, 1.5f);
        appearanceSettings.abdomen.Size = Random.Range(1.0f, 2.0f);
        appearanceSettings.legs.Thickness = Random.Range(100.0f, 150.0f);

        // Randomize color
        appearanceSettings.abdomen.Color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
        );

        spiderAppearance.TriggerApplyAppearance();
    }
}
