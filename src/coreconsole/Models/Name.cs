
namespace ConsoleSolution.Models
{
    public class Name
    {
        public string Last { get; set; }
        public string First { get; set; }

        public string FullName => $"{First} {Last}";
    }
}