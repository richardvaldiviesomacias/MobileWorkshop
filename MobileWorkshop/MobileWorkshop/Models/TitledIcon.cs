using System.ComponentModel;
using PropertyChanged;

namespace MobileWorkshop.Models
{
    public class TitledIcon: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public bool IsSelected { get; set; } = false;
        
        [DependsOn(nameof(IsSelected))]
        public bool IsNotSelected => !IsSelected;
    }
}