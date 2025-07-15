using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MinhaPrimeiraApp.Components.Cascading
{
    public class StyleContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        string color;

        public string BackgroundColor
        {
            get => color;
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = default)
       => PropertyChanged?.Invoke(this, new(propertyName));
    }
}
