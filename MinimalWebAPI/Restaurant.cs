namespace MinimalWebAPI
{
    public class Restaurant
    {

        public int Id { get; set; }
        public int Rating { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string Selection { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;


    }
}
