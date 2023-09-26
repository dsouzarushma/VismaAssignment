namespace FakeBloggingSystem.DBConfiguration
{
    public class AuthorData
    {
        private readonly List<AuthorDataModel> _authors;
        public AuthorData()
        {
            _authors = new List<AuthorDataModel>{
                new AuthorDataModel { Id = 1, Name = "Rushma", SurName = "Lopes" },
                new AuthorDataModel { Id = 2, Name = "Ruvan", SurName = "Lopes" },
                new AuthorDataModel { Id = 3, Name = "Alaia", SurName = "Lopes" },
            };
        }
        public List<AuthorDataModel> Authors => _authors; 
    }
}
