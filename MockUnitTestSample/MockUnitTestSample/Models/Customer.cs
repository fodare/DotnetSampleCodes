using System.ComponentModel.DataAnnotations;

namespace MockUnitTestSample.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool ActiveSubscriber { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
