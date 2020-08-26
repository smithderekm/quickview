namespace QuickView.UI.Windows
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using QuickView.UI.Windows.Annotations;

    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void SetProperty<T>(
            ref T member,
            T val,
            [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val))
            {
                return;
            }

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
