namespace StudioTemplate.ViewModels
{
    public class UpdateTeamVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Position { get; set; }
        public IFormFile Photo { get; set; }
    }
}
