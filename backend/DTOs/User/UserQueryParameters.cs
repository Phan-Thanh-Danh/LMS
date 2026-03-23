namespace backend.DTOs.User
{
    public class UserQueryParameters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SearchTerm { get; set; }
        public int? RoleId { get; set; }
    }
}
