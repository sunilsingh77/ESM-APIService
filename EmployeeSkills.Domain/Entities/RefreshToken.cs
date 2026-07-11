using EmployeeSkillsSummary.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeSkills.Domain.Entities;

public class RefreshToken
{
    public int Id { get; private set; }
    public string Token { get; private set; }
    public DateTime Expires { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime? Revoked { get; private set; }
    public string UserId { get; private set; }
    public ApplicationUser User { get; private set; }
    [NotMapped]
    public bool IsExpired => DateTime.UtcNow >= Expires;
    [NotMapped]
    public bool IsActive => Revoked == null && !IsExpired;
    private RefreshToken() { }
    public RefreshToken(
        string token,
        DateTime expires,
        string userId)
    {
        Token = token;
        Expires = expires;
        UserId = userId;
        Created = DateTime.UtcNow;
    }

    public void Revoke()
    {
        Revoked = DateTime.UtcNow;
    }
}