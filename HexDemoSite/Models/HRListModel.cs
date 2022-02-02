namespace HexDemoSite.Models;

public class HRListModel
{
    public IEnumerable<OpenPosition> ApprovalQueue { get; set; }
    public IEnumerable<Role> Roles { get; set; }
}