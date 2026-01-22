namespace DWA_AU24_Lab2_Group_11.Models
{
    /// <summary>
    /// Defines the types of crops supported by the FarmTrack system.
    /// The numeric value represents the default growing duration in days for each type.
    /// </summary>
    public enum CropType
    {
        /// <summary>Grain crops (e.g., wheat, rice). Default: 120 days.</summary>
        Grain = 120,

        /// <summary>Vegetable crops (e.g., carrots, lettuce). Default: 90 days.</summary>
        Vegetable = 90,

        /// <summary>Fruit crops (e.g., tomatoes, strawberries). Default: 100 days.</summary>
        Fruit = 100,

        /// <summary>Herb crops (e.g., basil, mint). Default: 80 days.</summary>
        Herb = 80,

        /// <summary>Legume crops (e.g., beans, peas). Default: 50 days.</summary>
        Legume = 50,

        /// <summary>Root crops (e.g., carrots, radishes). Default: 40 days.</summary>
        Root = 40,

        /// <summary>Tuber crops (e.g., potatoes). Default: 23 days.</summary>
        Tuber = 23,

        /// <summary>Nut crops (e.g., almonds, walnuts). Default: 121 days.</summary>
        Nut = 121,

        /// <summary>Cereal crops (e.g., oats, barley). Default: 89 days.</summary>
        Cereal = 89
    }
}
