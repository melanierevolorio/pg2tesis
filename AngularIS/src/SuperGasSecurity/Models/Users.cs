namespace SuperGasSecurity.Models;

public class UpdateUser
{
    public string Role { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
}

public class AddUser
{
    public string Role { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class GetUserBasicData
{
    public string Id { get; set; }
}

public class GetUserBasicDataResponse
{
    public string Role { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
}

public class UserModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}