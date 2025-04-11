namespace f_client.Data.Response;

public class UserAuthResponse
{
    public string? Token { get; set; }
    public bool RequiresTwoFactor { get; set; }
    public string? UserId { get; set; }
    public string? Message { get; set; }
}