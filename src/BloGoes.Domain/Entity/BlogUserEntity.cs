namespace BloGoes.Domain.Entity
{
    public class BlogUserEntity : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IList<BlogEntity> Blogs { get; set; } = new List<BlogEntity>();

        public void Update(BlogUserEntity request)
        {
            this.Email = request.Email;
            this.Password = request.Password;   
            this.Name = request.Name;
        }
    }
}
