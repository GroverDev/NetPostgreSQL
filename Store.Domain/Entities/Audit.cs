namespace Store.Domain;

public class Audit
{
    public bool State { get; set; }
    public int CreatedBy { get; set; }
    public DateTime Created { get; set; }
    public int ModifiedBy { get; set; }
    public DateTime Modified { get; set; }
}
