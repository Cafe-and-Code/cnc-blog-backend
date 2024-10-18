using System;

namespace Blog.API.Models.DTO;

public class ResultLoginDTO
{
    public Guid UserId { get; set; }
    public string? UserRole { get; set; }
    public string? Token { get; set; }
    // Create ResultLoginDTO constructor
    public ResultLoginDTO(Guid userId, string userRole, string token)
    {
        UserId = userId;
        UserRole = userRole;
        Token = token;
    }

}
