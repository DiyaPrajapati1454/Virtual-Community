namespace AngularAuthAPI.Dto
{
    public class AddMissionReq
    {
        public string MissionTitle { get; set; }

        public string MissionDescription { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
       // public DateTime? RegistrationDeadLine { get; set; }
        public int MissionThemeId { get; set; }

        public int MissionSkillId { get; set; }
        public string MissionImages { get; set; }
        public int CountryId { get; set; }

        public int CityId { get; set; }
        public int? TotalSheets { get; set; }
    }
}
