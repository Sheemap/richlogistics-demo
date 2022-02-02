namespace HexDemoSite.Models;

public class DepartmentListModel
{
    public IEnumerable<DepartmentPosition> Positions { get; set; }
    public IEnumerable<Role> AvailableRoles { get; set; }
}