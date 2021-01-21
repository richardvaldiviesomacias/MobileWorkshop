using System;

namespace Onboarding.Models.Sync
{
    public class GoalsStatusSync
    {
        public virtual void LogSelectedStatus(TitledIconList status)
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }
            
            // TODO: log these to Firebase or whatever E$ does with these
            string commaSeperatedList = status.GetSelectedIconIdsAsCommaSeparatedList();
            Console.WriteLine(commaSeperatedList);
        }
        
        public virtual void LogSelectedGoals(TitledIconList goals)
        {
            if (goals == null)
            {
                throw new ArgumentNullException(nameof(goals));
            }

            // TODO: log these to Firebase or whatever E$ does with these
            string commaSeperatedList = goals.GetSelectedIconIdsAsCommaSeparatedList();
            Console.WriteLine(commaSeperatedList);
        }
    }
}