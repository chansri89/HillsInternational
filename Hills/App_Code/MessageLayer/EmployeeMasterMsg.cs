using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeMasterMsg
/// </summary>
public class EmployeeMasterMsg
{
	public EmployeeMasterMsg()
	{
        //
        // TODO: Add constructor logic here
        //
        
	}
    public int ActId { get; set; }
    public int EnterpriseId { get; set; }
    public int CompanyId { get; set; }//
    public int DepartmentId { get; set; }//
    public string EnterpriseName { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public string EmailId { get; set; }
    public string Password { get; set; }
    public string ManagerCode { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public string ManagerName { get; set; }
    public string EmployeeDesignation{get;set;}
    public bool IsActive { get; set; }
    public bool IsAuditor { get; set; }
    public bool IsCompanyAdmin { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string Flag { get; set; }
    public string EmployeeResult { get; set; }
    public string LoginEmployeeCode { get; set; }
    public string Department { get; set; }
    public string Category { get; set; }
    public string Designation { get; set; }
    public string Sex { get; set; }
    public string Religion { get; set; }
    public string EmployeeFatherHusbandName { get; set; }
    public DateTime DOB { get; set; }
    public DateTime DOJ { get; set; }
    public DateTime DOC { get; set; }
    public DateTime DOT { get; set; }
    public string MaritalStatus { get; set; }
    public Double Basic { get; set; }
    public Double DA { get; set; }
    public Double PerPay { get; set; }
    public string Bank { get; set; }
    public string AccNo { get; set; }
    public string Branch { get; set; }
    public string IFSC { get; set; }
    public string UAN { get; set; }
    public string PFCode { get; set; }
}
public class LoginUserMsg
{
    public LoginUserMsg()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public int LoginUserId { get; set; }
    public int CompanyId { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentCode { get; set; }
    public string DepartmentName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string EmailId { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsSuperUser { get; set; }
    public bool IsActive { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }

    
    public string Flag { get; set; }
    public string LoginResult { get; set; }
    
}
