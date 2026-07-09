using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LibFunctions
/// </summary>
public class LibFunctions
{
    BaseClass BaseMsg = new BaseClass();
    ProcessBus Bus = new ProcessBus();
     List<CompanyMessage> CmpList = new List<CompanyMessage>();
     EmployeeMasterMsg emp = new EmployeeMasterMsg();
     
	public LibFunctions()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<CompanyMessage> LoadCompany()
    {
       emp.EmployeeCode = BaseMsg.EmployeeCode;
       CmpList = Bus.CompanyMasterSelect(emp);
       CmpList = LoadCompanyOnUserRight(CmpList);
        return CmpList;
    }
    public List<CompanyMessage> LoadCompanyOnUserRight(List<CompanyMessage> CompanyList)
    {
        List<CompanyMessage> CmpLst = new List<CompanyMessage>();
        if (BaseMsg.IsAdmin == true)
        {
            CmpLst = CompanyList.ToList();
        }
        else
        {
            CmpLst = (from c in CompanyList where c.CompanyCode == BaseMsg.CompanyCode select c).ToList();
        }

        return CmpLst;
    }
    public List<EmployeeMasterMsg> LoadEmployeesOnUserRight(List<EmployeeMasterMsg> EmployeeList)
    {
        List<EmployeeMasterMsg> EmpLst = new List<EmployeeMasterMsg>();
        if (BaseMsg.IsAdmin == true)
        {
            EmpLst = EmployeeList.ToList();
        }
        else
        {
            EmpLst = (from c in EmployeeList where c.CompanyCode == BaseMsg.CompanyCode select c).ToList();
        }

        return EmpLst;
    }
    public List<ClientMsg> LoadClient(Int32 WCompanyId)
    {
        List<ClientMsg> ClList = new List<ClientMsg>();
        ClientMsg Cmp = new ClientMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = WCompanyId;
        ClList = Bus.MasClientInsertUpdateandDelete(Cmp);
        ClList = (from Cl in ClList where Cl.ClientType.Trim().ToUpper() == "CLIENT" select Cl).ToList();
        return ClList;
    }
  
}