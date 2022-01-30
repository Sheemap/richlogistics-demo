namespace HexDemoSite.Models;

public class OpenPosition
{
    public int Id { get; set; }
    public DateTimeOffset? HRDateApproved { get; set; }
    public DateTimeOffset? LeadershipDateApproved { get; set; }
    public DateTimeOffset? DateFilled { get; set; }
    
    public virtual DepartmentPosition DepartmentPosition { get; set; }
}