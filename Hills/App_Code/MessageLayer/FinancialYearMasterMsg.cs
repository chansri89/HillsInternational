using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for StateMasterMsg
/// </summary>
public class FinancialYearMasterMsg
{
    public FinancialYearMasterMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int FinancialYearId { get; set; }
    public int CompanyId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int FiscalYear { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string FinancialYearResult { get; set; }
    public bool IsActive { get; set; }
    public string Result { get; set; }
    public int StartYear { get; set; } //added by madhu 201009
    public string FinancialYear { get; set; } //added by madhu 201009
}