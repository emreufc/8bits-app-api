using System;
using System.Collections.Generic;

namespace _8bits_app_api.Models;

public partial class UserRefreshToken
{
    public int UserRefreshTokenId { get; set; }

    public int UserId { get; set; }

    public string RefreshToken { get; set; } = null!;

    public DateTime ExpirationDate { get; set; }

    public virtual User User { get; set; } = null!;
}
