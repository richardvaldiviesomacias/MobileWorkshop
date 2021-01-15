namespace Onboarding.Models.Builders
{
    public static class OnboardingProfileBuilder
    {
        //
        // NOTE: we could make this data driven from JSON to provide dynamic, easy-to-update content
        //

        // TODO: align ids with E$ production
        public static TitledIconList Goals => new TitledIconList() {
            new TitledIcon("pay_off_debt", "Pay Off Debt", "icon_link_broken.png"),
            new TitledIcon("retirement", "Save For Retirement", "icon_money_coins.png"),
            new TitledIcon("college", "Pay For Kids' College", "icon_graduation_cap.png"),
            new TitledIcon("home", "Save for a Home", "icon_home.png"),
            new TitledIcon("pay_to_pay", "Stop Living Pay to Pay", "icon_time_loop.png"),
            new TitledIcon("travel", "Travel", "icon_map_compass.png"),
        };

        // TODO: align ids with E$ production
        public static TitledIconList Status => new TitledIconList {
            new TitledIcon("own_home", "I Own a Home", "icon_home.png"),
            new TitledIcon("rent", "I Rent", "icon_home_rent.png"),
            new TitledIcon("married", "I Am Married", "icon_ring.png"),
            new TitledIcon("kids", "I Have Kids", "icon_pacifier.png"),
            new TitledIcon("own_car", "I Own a Car", "icon_sport_car.png"),
            new TitledIcon("own_pets", "I Have Pets", "icon_paw.png"),
        };

        

        public static OnboardingProfile Build(Budget budget)
            => new OnboardingProfile(budget, Goals, Status);

        public static OnboardingProfile Build() => Build(BudgetBuilder.Build());
    }
}