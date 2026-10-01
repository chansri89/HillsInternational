using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReportMsg
/// </summary>
public class ReportMsg
{
    public ReportMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int CompanyId { get; set; }
    public Int64 ClientProjectId { get; set; }
    
    public string ProjectCode { get; set; }
    public string PackageCode { get; set; }
    public Int32 ItemCategoryId { get; set; }
    public Int32 ItemSubCategoryId { get; set; }
    public string ClientCode { get; set; }
    public string TransType { get; set; }
    public int EmployeeCode { get; set; }
    public string CreatedBy { get; set; }
    public int NoOfMonths { get; set; }
    public string GroupCode { get; set; }
    public string SubGroupCode { get; set; }
    public string IOWHeadCode { get; set; }
    public Int64 ForYearMonth { get; set; }
    public string Region { get; set; } //scs241221 
}
public class RptMsg
{
    public RptMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string ProcessCode { get; set; }
    public string ProductionUnitCode { get; set; }
    public int CompanyId { get; set; }
}
public class ParameterMsg
{
    public ParameterMsg()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public string Parameters { get; set; }
    public int ParameterId { get; set; }
    public bool IsAreaApplicable { get; set; }

}
public class RegionMsg
{
    public RegionMsg()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public string Region { get; set; }
}

public class UserMsg
{
    public UserMsg()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public Int32 UserId { get; set; }
    public Int32 CompanyId { get; set; }

    public string UserName { get; set; }
    public string EmailId { get; set; }
    

}
public class CompanyMsg
{
    public CompanyMsg()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public Int32 CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyShortName { get; set; }
    public string CompanyFlag { get; set; }
    //below 4 added by Vignesh 201211
    public string BankCode { get; set; }
    public string BankName { get; set; }
    public string CompanyBank { get; set; }
    public string NameofBank { get; set; }
    public string NameofDept { get; set; }
    public string DeptCode { get; set; }
    public string CompanyDept { get; set; }
    public string LoginUserName { get; set; }
    public int WeekNumber { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    // vignesh abv
}
public class FieldMsg
{
    public FieldMsg()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public Int32 FieldId { get; set; }
    public string FieldName { get; set; }
    public string FieldCode { get; set; }
    public Int32 DivisionId { get; set; }
    public string DivisionCode { get; set; }
    public double Area { get; set; }

}
public class TransactionCodeFromBankMasterMsg
{
    public TransactionCodeFromBankMasterMsg()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public string TransactionName { get; set; }
    public string TransactionCode { get; set; }

}
public class PFEStablishmentMsg
{
    public PFEStablishmentMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string PFEStablishmentcode { get; set; }

}


public class FASExcelImportMaster
{
    public FASExcelImportMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string Result { get; set; }
    public string RKount { get; set; }
    public int SrlNo { get; set; }
    public string VoucherNo { get; set; }
    public string CompanyCode { get; set; }
    public string BankCode { get; set; }
    public DateTime VoucherDate { get; set; }
    public string CostCenterCode { get; set; }
    public string AccountCode { get; set; }
    public double Debit { get; set; }
    public double Credit { get; set; }
    public string PartyName { get; set; }
    public string Particulars { get; set; }
    public string ManDays { get; set; }
    public string JobCode { get; set; }
    public string SubCode { get; set; }

}

public class MonthMsg
{
    public MonthMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public Int16 MonthNum { get; set; }
    public Int16 FinancialMonthNum { get; set; }
    public string MonthName { get; set; }


}
public class UploadResultMsg
{
    public UploadResultMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string Result { get; set; }
    public Int64 WParameterId { get; set; }
    public string Result1 { get; set; }
    public string Result2 { get; set; }
}
public class IndicatorMsg
{
    public IndicatorMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public string Id { get; set; }
    public string IndicatorName { get; set; }


}

public class DeptMsg
{
    public DeptMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public Int64 DepartmentId { get; set; }
    public string DepartmentCode { get; set; }
    public string DepartmentName { get; set; }


}

public class CategoryMsg
{
    public CategoryMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public Int64 CategoryId { get; set; }
    public string CategoryName { get; set; }


}

public class DesigMsg
{
    public DesigMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public Int64 DesignationId { get; set; }
    public string Designation { get; set; }


}

public class PayYearMonthMsg
{
    public PayYearMonthMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public Int64 CompanyID { get; set; }
    public string CompanyName { get; set; }
    public string YearMonth { get; set; }
    public string Result { get; set; }


}

public class HolidayRateMsg
{
    public HolidayRateMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public Int64 CompanyID { get; set; }
    public string CompanyName { get; set; }
    public string YearMonth { get; set; }
    public string HolidayFlag { get; set; }
    public double JobDiffRate { get; set; }
    public double HolidayRate { get; set; }
    public string Result { get; set; }


}