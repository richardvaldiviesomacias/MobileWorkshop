using System.Collections.Generic;
using System.Linq;

namespace Onboarding.Models
{
    public class TitledIconList: List<TitledIcon>
    {
        public List<string> GetSelectedIconIds()
            => this.Where(icon => icon.IsSelected).Select(icon => icon.Id).ToList();

        public string GetSelectedIconIdsAsCommaSeparatedList()
            => string.Join(",", GetSelectedIconIds());
    }
}