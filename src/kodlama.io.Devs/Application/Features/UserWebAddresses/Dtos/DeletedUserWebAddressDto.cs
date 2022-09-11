namespace Application.Features.UserWebAddresses.Dtos
{
    public class DeletedUserWebAddressDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GithubAddress { get; set; }
    }
}
