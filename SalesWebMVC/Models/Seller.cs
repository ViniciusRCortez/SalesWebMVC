namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public double Salary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthday, double salary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            Birthday = birthday;
            Salary = salary;
            Department = department;
        }

        public void addSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void removeSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public double totalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
