namespace HexDemoSite.Models;

public class DepartmentPosition
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int? EmployeeId { get; set; }
    public int? OpenPositionId { get; set; }
    
    public string JobTitle { get; set; }
    public string Department { get; set; }
    public string Location { get; set; }
    public string JobDescription { get; set; }
    
    public virtual Role Role { get; set; }
    public virtual Employee? Employee { get; set; }
    public virtual OpenPosition? OpenPosition { get; set; }
}