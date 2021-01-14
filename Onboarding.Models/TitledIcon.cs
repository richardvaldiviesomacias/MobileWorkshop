using System.ComponentModel;

namespace Onboarding.Models
{
    public class TitledIcon: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public bool IsSelected { get; set; }

        public TitledIcon(string id, string title, string imageSource)
        {
            Id = id;
            Title = title;
            ImageSource = imageSource;
        }
    
    }
}