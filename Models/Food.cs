namespace FoodApp.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string? FoodName { get; set; }
        public string? FoodImg { get; set; }
        public string? FoodDesc { get; set; }
        public decimal? FoodCost { get; set; }
        public decimal? FoodCal { get; set; }
    }
}
