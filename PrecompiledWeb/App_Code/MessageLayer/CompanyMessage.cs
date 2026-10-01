using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for CompanyMessage
/// </summary>
public class CompanyMessage
{
	public CompanyMessage()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    public int CompanyId { get; set; } //scs 200220
    public string CompanyCode { get; set; }
    public string EmployeeCode { get; set; }
    public string CompanyName { get; set; }
    public string CompanyShortName { get; set; }
    public string CompanyFlag { get; set; }
    public string ParentCompanyCode { get; set; }
    public string ParentCompanyName{ get; set; }
    public string CompanyResult { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; } 
    public string Flag { get; set; }
    public bool IsActive { get; set; }
    public string StateShortName { get; set; }
    public int StateID { get; set; }
    public int EnterpriseId { get; set; }
    public string EnterpriseName { get; set; }
    public string StateName { get; set; }

    public int LocationTypeID { get; set; }
    public int LocationID { get; set; }
    public string LocationName { get; set; }

   
}
public class LocationMsg
{
    public LocationMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GstinUserName { get; set; }
    public string GstinUserPwd { get; set; }
    //public string GSPName { get; set; }
    //public string AspId { get; set; }
    //public string AspPwd { get; set; }
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public string LocationShortName { get; set; }
    public string LocationResult { get; set; }

    public int PKTLocationId { get; set; }
    public string LocationCode { get; set; }
   public string LocationType { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Area { get; set; }
    public string District { get; set; }
    public string State { get; set; }
    public string PinCode { get; set; }
    public string LocationGSTIN { get; set; }
    public string LocationCIN { get; set; }
    public string LocationGSTCode { get; set; }
    public string CompanyCode { get; set; }
    public string BankName { get; set; }
    public string Account { get; set; }
    public string IFSCCode { get; set; }
}
public class InvoiceTypeMsg
{
    public InvoiceTypeMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int InvoiceTypeId { get; set; }
    public string InvoiceTypeName { get; set; }
    public string InvoiceType { get; set; }
}
public class InvoiceOtherChargesMsg
{
    public InvoiceOtherChargesMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int InvoiceOtherChargesID { get; set; }
    public string ChargeName { get; set; }
    public double AmountCharged { get; set; }
    public double CGST { get; set; }
    public double SGST { get; set; }
    public double IGST { get; set; }
    public string HSNAC { get; set; }
}


public class DepotMsg
{
    public DepotMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int DepotId { get; set; }
    public string DepotName { get; set; }
    public string DepotCode { get; set; }
    public string DepotShortName { get; set; }
    public string DepotAddress1 { get; set; }
    public string LocationResult { get; set; }

    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public string LocationCode { get; set; }
    public string LocationType { get; set; }
    public string CompanyCode { get; set; }

    public int EmpCode { get; set; }
    public int KG { get; set; }
    public bool IsActive { get; set; }
    public int DeptId { get; set; }
    public string DeptName { get; set; }
    public string DeptCode { get; set; }
    public DateTime FromDate { get; set; }
    public string Result { get; set; }
   
}
public class ControlProcessMsg
{
    public ControlProcessMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Int64 ControlProcessingId { get; set; }
    public int CompanyId { get; set; }

    public string CompanyName { get; set; }
    public string YYYYMM { get; set; }
    public DateTime ClosingDate { get; set; }
    public string CreatedBy { get; set; }

    public DateTime ESIPeriodFrom { get; set; }
    public DateTime ESIPeriodTo { get; set; }
    public DateTime OverTimePeriodFrom { get; set; }
    public DateTime OverTimePeriodTo { get; set; }
    public DateTime PFPeriodFrom { get; set; }
    public DateTime PFPeriodTo { get; set; }
    public int WorkingDays { get; set; }
    public string PaySlipProcess { get; set; }
    public string YearMonth { get; set; }
    public bool IsActive { get; set; }

    public Int64 PaySlipYearMonth { get; set; }


}

public class AdvanceMsg
{
    public AdvanceMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Int64 PKTAdvanceId { get; set; }
    public string CompanyName { get; set; }
    public string LocationName { get; set; }
    public string BuyerName { get; set; }

    public int CompanyId { get; set; }
    public int BuyerId { get; set; }
    public string LocationCode { get; set; }
    public string BankReference { get; set; }
    public double AdvanceAmount { get; set; }
    public DateTime ReceivedDate { get; set; }
    public string CreatedBy { get; set; }
   
}

public class AttCorrectionMsg
{
    public AttCorrectionMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int TypeId { get; set; }
    public string TypeName { get; set; }
    public int EmpNo { get; set; }
    public int SNo { get; set; }
    public int EmpCode { get; set; }
    public DateTime AttDate { get; set; }
    public int Division { get; set; }
    public int HandKg { get; set; }
    public int ShearKg { get; set; }
    public double PlDay { get; set; }
    public string JobCode { get; set; }
    public double SuDay { get; set; }
    public int Hours { get; set; }
    public int PlKg { get; set; }
    public string Result { get; set; }
}