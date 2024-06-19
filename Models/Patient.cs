namespace Project_Kaveri.Models
{
    public class Patient : BaseModel
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public long PhoneNo { get; set; }
    }
}
