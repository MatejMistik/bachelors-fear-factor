using UnityEngine;

[CreateAssetMenu(fileName = "SpiderAppearanceSettings", menuName = "Spider/AppearanceSettings")]
public class SpiderAppearanceSettings : ScriptableObject
{
    [Header("Cephalothorax")]
    public BodyPartSettings cephalothorax = new BodyPartSettings
    {
        Size = 1.0f,
        Color = Color.black,
        Texture = null
    };

    [Header("Abdomen")]
    public AbdomenSettings abdomen = new AbdomenSettings
    {
        Size = 1.0f,
        Color = Color.black,
        Texture = null,
        PatternColor = Color.red
    };

    [Header("Legs")]
    public LegSettings legs = new LegSettings
    {
        LegCount = 8,
        LegLength = 1.0f,
        Thickness = 0.1f,
        Color = Color.black
    };

    [Header("Pedipalps")]
    public PedipalpSettings pedipalps = new PedipalpSettings
    {
        Size = 0.5f,
        Color = Color.black,
        HairDensity = 0.5f
    };

    [Header("Spinnerets")]
    public SpinneretSettings spinnerets = new SpinneretSettings
    {
        Count = 2,
        Size = 0.3f,
        Color = Color.black
    };

    [Header("Eyes")]
    public EyeSettings eyes = new EyeSettings
    {
        EyeCount = 8,
        EyeSize = 0.1f,
        EyeColor = Color.black
    };

    [Header("Chelicerae & Fangs")]
    public CheliceraeSettings chelicerae = new CheliceraeSettings
    {
        Size = 0.4f,
        Color = Color.black,
        FangLength = 0.2f,
        VenomPotency = 0.5f
    };

    #region Nested Classes for Settings

    [System.Serializable]
    public class BodyPartSettings
    {
        public float Size = 1.0f;
        public Color Color = Color.black;
        public Texture2D Texture;
    }

    [System.Serializable]
    public class AbdomenSettings : BodyPartSettings
    {
        public bool HasPattern = false;
        public Texture2D PatternTexture;
        public Color PatternColor = Color.red;
    }

    [System.Serializable]
    public class LegSettings
    {
        [Range(4, 8)] public int LegCount = 8;
        [Range(100, 150)] public float LegLength = 1.0f;
        [Range(100,150)] public float Thickness = 0.1f;
        public Color Color = Color.black;
        public Texture2D Texture;
    }

    [System.Serializable]
    public class PedipalpSettings
    {
        public float Size = 0.5f;
        public Color Color = Color.black;
        public bool HasHair = true;
        [Range(0, 1)] public float HairDensity = 0.5f;
        public Color HairColor = Color.gray;
    }

    [System.Serializable]
    public class SpinneretSettings
    {
        [Range(1, 4)] public int Count = 2;
        public float Size = 0.3f;
        public Color Color = Color.black;
        public Texture2D Texture;
    }

    [System.Serializable]
    public class EyeSettings
    {
        [Range(2, 8)] public int EyeCount = 8;
        public float EyeSize = 0.1f;
        public Color EyeColor = Color.black;
        public Vector3[] EyePositions;  // Define positions relative to the head

        // Automatically reset eye positions if the count changes
        public void InitializeEyePositions()
        {
            if (EyePositions == null || EyePositions.Length != EyeCount)
            {
                EyePositions = new Vector3[EyeCount];
                // Default eye position setup here, if needed.
            }
        }
    }

    [System.Serializable]
    public class CheliceraeSettings
    {
        public float Size = 0.4f;
        public Color Color = Color.black;
        public Texture2D Texture;
        public float FangLength = 0.2f;
        public bool HasVenom = true;
        [Range(0, 1)] public float VenomPotency = 0.5f;
    }

    #endregion
}
