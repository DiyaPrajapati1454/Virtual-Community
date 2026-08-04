namespace AngularAuthAPI.Dto
{
    public class AddMissionApplicationReq
    {
        public DateTime AppliedDate { get; set; }
        public int MissionId { get; set; }
        public int Sheet { get; set; }

        public bool Status { get; set; }

        public int UserId { get; set; }
    }
}
