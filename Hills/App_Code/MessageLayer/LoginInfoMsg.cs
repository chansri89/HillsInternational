using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginInfoMsg
/// </summary>
public class LoginInfoMsg
{
	public LoginInfoMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string UserName { get; set; }
    public string Password { get; set; }
    public string CompanyId { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; } // Scs 210224 
    public int DepartmentId { get; set; } //SCS 210224
    public bool IsAdmin { get; set; } //SCS 210224
    public bool IsSuperUser { get; set; } //SCS 210806
    public string Result { get; set; }
    public int UserSessionId { get; set; }
    public string MachineIP { get; set; }
}