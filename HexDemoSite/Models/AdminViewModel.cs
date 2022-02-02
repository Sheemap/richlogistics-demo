namespace HexDemoSite.Models;

public class AdminViewModel
{
    public DepartmentListModel DepartmentModel { get; set; }
    public HRListModel HRModel { get; set; }
    public IEnumerable<OpenPosition> OpenPositionsModel { get; set; }

}