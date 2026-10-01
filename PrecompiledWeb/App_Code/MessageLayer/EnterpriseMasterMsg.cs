using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EnterpriseMasterMsg
/// </summary>
public class EnterpriseMasterMsg
{
	public EnterpriseMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int EnterpriseId { get; set; }
    public string EnterpriseName { get; set; }
    public string Addr1 { get; set; }
    public string Addr2 { get; set; }
    public string Addr3 { get; set; }
    public string Addr4 { get; set; }
    public Int64 Phone1 { get; set; }
    public Int64 Phone2 { get; set; }
    public int Fax { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    //public int AssemblyID { get; set; }
    //public int CertificationId { get; set; }
    public string Flag { get; set; }
    public string EnterpriseResult { get; set; }
}