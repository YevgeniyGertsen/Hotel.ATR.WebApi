namespace Hotel.ATR.WebApi.Model
{
    public class Team
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string PathImage { get; set; }
        public string Description { get; set; }
        public string PositionName { get; set; }        
    }

    public class TeamDTO
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string PathImage { get; set; }
        public string Description { get; set; }
        public string PositionName { get; set; }
    }
}