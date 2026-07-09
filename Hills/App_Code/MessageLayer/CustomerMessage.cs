using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for CompanyMessage
/// </summary>
public class ClientMsg
{
    public ClientMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ClientCode { get; set; }
    public string ParentClientCode { get; set; }
    public string ClientName { get; set; }
    public string ClientShortName { get; set; }
    public string ClientType { get; set; }
    public int CompanyId { get; set; } // Naren Mail dt 22/02/21
    public int CustomerTypeId { get; set; }
      
    public string BuildingName { get; set; }
    public string Addr1 { get; set; }
    public string Addr2 { get; set; }
    public string City { get; set; }
    public string PinCode { get; set; }
    public int StateId { get; set; }
    public string StateName { get; set; }
    public string EmailId { get; set; }
    public string CustomerResult { get; set; }
    public Int64 CreatedById { get; set; }
    public Int64 ModifiedById { get; set; }
   
    public string Flag { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public string ClientResult { get; set; }
    //public string GSTIN { get; set; }
    //public string CIN { get; set; }
    //public string PAN { get; set; }
    // public string CustomerFlag { get; set; } 
    //public string WebSite { get; set; }
    //public string TelePhoneNumber { get; set; }
    //public string Code { get; set; }

    //public bool CustomerStatus { get; set; }
    //public string EmployeeCode { get; set; }
}
public class ClientTypeMsg
{
    public ClientTypeMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ClientType { get; set; }
}
public class SectorMsg
{
    public SectorMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int ProjectSectorGroupId { get; set; } 
    public int ProjectSectorSubGroupId { get; set; } // Naren Mail dt 22/02/21
    public string ProjectSectorGroupName { get; set; }
    public string ProjectSectorClass { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string Result { get; set; }
}

public class ProjectMsg
{
    public ProjectMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int CompanyId { get; set; } // Naren Mail dt 22/02/21
    public string Flag { get; set; }
    public string ClientCode { get; set; }
    public string ClientName { get; set; }
    public int StateId { get; set; }
    public string StateName { get; set; }

    public Int32 ClientProjectId { get; set; }
    public string ProjectCode { get; set; }
    public string ProjectName { get; set; }
    public string ProjectLocation { get; set; }
    public string ProjectCity { get; set; }
    public Int32 DeviationMonths { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime TenderPeriod { get; set; }
    public DateTime ConstructionStart { get; set; }
    public DateTime ConstructionCompleted { get; set; }
    public int RevisionNumber { get; set; }
      
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public string ProjectResult { get; set; }

    public int ProjectSectorSubGroupId { get; set; } // Hills mail 260326
    public string ProjectSectorClass { get; set; }
    public string ProjectSectorGroupName { get; set; } 

}
public class GSTINState
{
    public GSTINState()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string State { get; set; }
    public string GSTINCode { get; set; }
}

public class SupplierTypeMsg
{
    public SupplierTypeMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string SupplierType  { get; set; }
    public Int32 SupplierTypeId { get; set; }
}
public class CustomerTypeMsg
{
    public CustomerTypeMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string CustomerType { get; set; }
    public Int32 CustomerTypeId { get; set; }
}

public class CustomerInvoiceKnockoff
{
    public CustomerInvoiceKnockoff()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Int64 CollectionOthersHdrId { get; set; }
    public int CompanyId { get; set; }
    public int CompanyCode { get; set; }
    public string LocationName { get; set; }
    public string LocationCode { get; set; }
    public DateTime InvoicefromDate { get; set; }
    public DateTime InvoiceToDate { get; set; }
    public string HDRRemarks { get; set; }
    public string CustomerCode { get; set; }
    public string BuyerName { get; set; }
    public string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public double NetKg { get; set; }
    public double InvoiceValue { get; set; }
    public double NetAmount { get; set; }
    public double AmountCollected { get; set; }
    public string TransactionNumber { get; set; }
    public DateTime TransactionDate { get; set; }
    public double CollectedAmount { get; set; }
    public double OpBalAmt { get; set; } //SCS 210403
    public string CreatedBy { get; set; }
    public string DtlRemarks { get; set; }
    public string Result { get; set; }  //
}

public class TenderMsg
{
    public TenderMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int CompanyId { get; set; } // Naren Mail dt 22/02/21
    public int StateId { get; set; }
    public Int32 ClientProjectId { get; set; }
    public Int64 ClientProjectTenderId { get; set; }
    public string ProjectCode { get; set; }
    public string ProjectName { get; set; }
    public string StateName { get; set; }
    public string ClientCode { get; set; }
    public string ClientName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime ConstructionCompleted { get; set; }
    public string ExcelSheetName { get; set; }
    public int ExcelRowNumber { get; set; }
    public string SrlNo { get; set; }
    public string Description { get; set; }
    public string UOM { get; set; }
    public double Quantity { get; set; }
    public int SubmissionNumber { get; set; }
    public int RevisionNumber { get; set; }
    public double SupplyRate { get; set; }
    public double InstallRate { get; set; }
    public double SupplyAmount { get; set; }
    public double InstallAmount { get; set; }
    public double Itemtotal { get; set; }
    public bool BudgetRate { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; } 
    public string Remarks { get; set; }
    public int SupplyOnly { get; set; }
    public int TenderIOWMapped { get; set; }
}
