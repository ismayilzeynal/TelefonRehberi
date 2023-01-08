using System;
using System.Collections.Generic;

namespace DataApp.Models;

public partial class User
{
    public int EmployeeId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Title { get; set; }

    public string? TitleOfCourtesy { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? HireDate { get; set; }

    public string? EmailAddress { get; set; }

    public string? City { get; set; }

    public string? Phone { get; set; }
}
