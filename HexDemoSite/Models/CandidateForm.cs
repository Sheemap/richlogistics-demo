namespace HexDemoSite.Models;

public class CandidateForm
{
    public int Id { get; set; }
    public int OpenPositionId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public virtual OpenPosition OpenPosition { get; set; }
}