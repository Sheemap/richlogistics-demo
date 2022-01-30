namespace HexDemoSite.Models;

public class OpenPosition
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public DateTimeOffset? DateApproved { get; set; }
    
    public virtual Role Role { get; set; }
}