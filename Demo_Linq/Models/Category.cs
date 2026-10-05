namespace Demo_Linq.Models
{
    public class Category 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        //ctor
        public Category(int id, string name, string? desc = null)
        {
            Id = id;
            Name = name;
            Description = desc;
        }

        //toString et l'opérateur "=="
        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
        public static bool operator ==(Category? left, Category? right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)            
                return false;            

            return left.Name == right.Name;
        }
        public static bool operator !=(Category? left, Category? right)
        {
            return !(left == right);
        }
    }
}
