namespace SpecializedClinicAuth.Models;

public class UpdateUser
{
    public string UserId { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
}

public class GetUserBasicData
{
    public string UserId { get; set; }
}

public class GetUserBasicDataResponse
{
    public string Role { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
}

public class UserModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}