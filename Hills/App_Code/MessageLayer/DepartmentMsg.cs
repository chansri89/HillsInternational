using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DepartmentMsg
/// </summary>
public class DepartmentMsg
{
	public DepartmentMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int DepartmentId { get; set; }
    public int CompanyId { get; set; }
    public int TeamId { get; set; }
    public int CostCenterId { get; set; } //scs 230122 as per disc with Vj tbldepartments carry costcenterid
    public int Id { get; set; }
    public string DepartmentName { get; set; }
    public string DepartmentCode { get; set; }
    public string DepartmentShortName { get; set; }
    public string DepartmentTeam { get; set; }
    public string Team { get; set; }
    public int WeighmentNoLmt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string DeptResult { get; set; }
    public string Flag { get; set; }
    public bool IsActive { get; set; }
    public bool IsFactory { get; set; }
    public int EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeId { get; set; }
}
public class CostCodeMsg
{
    public CostCodeMsg()
    {
    }
    public int CostCodeId { get; set; }
    public string CostDescription { get; set; }
    public string CostCode { get; set; }
    public string CostGroupDescription1 { get; set; }
    public string CostGroupDescription2 { get; set; }
    public string AGGR1 { get; set; }
    public string AGGR2 { get; set; }
}

public class CostCenterMsg
{
    public CostCenterMsg()
    {
    }
    public int CostCenterId { get; set; }
    public string CostCenterName { get; set; }
    public string CostCenterCode { get; set; }
    public bool IsActive { get; set; }
   
}


public class EmployeeMsg
{
    public EmployeeMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //public int DepartmentId { get; set; }
    //public int TeamId { get; set; }
    //public int Id { get; set; }
    //public string DepartmentName { get; set; }
    //public string DepartmentCode { get; set; }
    //public string DepartmentShortName { get; set; }
    //public string DepartmentTeam { get; set; }
    //public string Team { get; set; }
    //public int WeighmentNoLmt { get; set; }
    //public string CreatedBy { get; set; }
    //public DateTime CreatedDate { get; set; }
    //public string ModifiedBy { get; set; }
    //public DateTime ModifiedDate { get; set; }
    //public string DeptResult { get; set; }
    //public string Flag { get; set; }
    //public bool IsActive { get; set; }
    //public bool IsFactory { get; set; }
    public int EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public int EmployeeId { get; set; }
}
public class StoresMsg
{
    public StoresMsg()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public int StoreId { get; set; }
    public int DivisionId { get; set; }
    public string StoreCode { get; set; }
    public string StoreName { get; set; }
    public string StoreAC{ get; set; }
    public string CrAC { get; set; }
    public int CostCenterId { get; set; }
    public int CompanyId { get; set; }
    public bool IsActive { get; set; }
    public bool IsGroupStore { get; set; }

}
