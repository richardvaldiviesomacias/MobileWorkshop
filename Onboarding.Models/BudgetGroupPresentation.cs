namespace Onboarding.Models
{
    public class BudgetGroupPresentation
    {
        public string ColorKey { get; set; }
        public string IconSource { get; set; }
        public string Description { get; set; }
        public string Subtitle { get; set; }
        public string AddItemText { get; set; }
        public string CustomTitle { get; set; }
        public bool HasSecondaryHeader { get; set; }
        public bool HasDateOnHeader { get; set; }

        public const string DefaultSubtitle = "Don't worry, you can edit these later";
        public const string DefaultColorKey = "OnboardingBlue";
        public const string DefaultAddItemText = "+ Add Item";

        public BudgetGroupPresentation(
            string colorKey = DefaultColorKey,
            string iconSource = "",
            string description = "",
            string subtitle = DefaultSubtitle,
            string addItemText = DefaultAddItemText,
            string customTitle = "",
            bool hasSecondaryHeader = false,
            bool hasDateOnHeader = false)
        {
            ColorKey = colorKey;
            IconSource = iconSource;
            Description = description;
            Subtitle = subtitle;
            AddItemText = addItemText;
            CustomTitle = customTitle;
            HasSecondaryHeader = hasSecondaryHeader;
            HasDateOnHeader = hasDateOnHeader;
        }

    }
}