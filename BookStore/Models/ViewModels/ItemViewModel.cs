// File: /Models/ViewModels/ItemViewModel.cs

namespace BookStore.Models.ViewModels
{
    public class ItemViewModel
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; } 
    }
}
