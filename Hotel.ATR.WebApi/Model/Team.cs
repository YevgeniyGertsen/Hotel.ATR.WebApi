namespace Hotel.ATR.WebApi.Model
{
    public class Team
    {
        public Team()
        {
            
        }
        public Team(string FullName, string PathImage, string Description, string PositionName)
        {
            this.FullName = FullName;
            this.PathImage = PathImage;
            this.Description = Description;
            this.PositionName = PositionName;
        }
        public string FullName { get; set; }
        public string PathImage { get; set; }
        public string Description { get; set; }
        public string PositionName { get; set; }
    }
}