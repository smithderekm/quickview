namespace QuickView.Data.LocalStorage.Entities
{
    public class Subject
    {
        public Subject(string owner, string name)
        {
            this.Owner = owner;
            this.Name = name;
        }

        public string Owner { get; set; }
        public string Name { get; set; }


    }
}
