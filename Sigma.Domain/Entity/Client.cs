﻿
using System.Security.AccessControl;

namespace Sigma.Domain.Entity;

public class Client
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? StartInterval { get; set; }
    public DateTime? EndInterval { get; set; }
    public string? LinkednURL { get; set; }
    public string? GithubURL { get; set; }
    public string Comment { get; set; }
}