namespace QuickView.UI.Windows.Feeds
{
    using System;

    public class EditableFeed : ValidatableBindableBase
    {
        private Guid id;

        public Guid Id
        {
            get { return this.id; }
            set { SetProperty(ref this.id, value); }
        }

        private string name;

        public string Name
        {
            get { return this.name; }
            set { SetProperty(ref this.name, value); }
        }
    }
}
