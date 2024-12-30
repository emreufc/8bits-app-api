namespace _8bits_app_api.Dtos
{
    public class UserDietPreferenceDto
    {
        public int DietPreferenceId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? UserId { get; set; }
    }
}
