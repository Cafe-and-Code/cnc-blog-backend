namespace Blog.API.Commons
{
    public enum UserRole
    {
        Admin = 0,
        User = 1,
    }

    public enum UserStatus
    {
        Inactive = 0,
        Active = 1,
    }

    public enum PostStatus
    {
        Deleted = 0,
        Public = 1,
        Draft = 2,
    }
}
