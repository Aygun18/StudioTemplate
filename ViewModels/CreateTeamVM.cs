namespace StudioTemplate.ViewModels
{
    public class CreateTeamVM
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string TwitterLink { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedinLink { get; set; }
        public IFormFile Photo { get; set; }
    }
}
