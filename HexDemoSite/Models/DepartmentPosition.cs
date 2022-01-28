namespace HexDemoSite.Models;

public class DepartmentPosition
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int? EmployeeId { get; set; }
    
    public virtual Role Role { get; set; }
    public virtual Employee? Employee { get; set; }
}