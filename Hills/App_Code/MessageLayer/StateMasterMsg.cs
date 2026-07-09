using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StateMasterMsg
/// </summary>
public class StateMasterMsg
{
	public StateMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
    public int StateId { get; set; }
    public string StateName { get; set; }
    public string StateShortName { get; set; }
    public string Flag { get; set; }
    public string StateResult { get; set; }
}
public class PackageMasterMsg
{
    public PackageMasterMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    
    public string PackageName { get; set; }
    public string PackageCode { get; set; }
    public string Flag { get; set; }
    public string PackageResult { get; set; }
}

public class TradeMasterMsg
{
    public TradeMasterMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public string TradeName { get; set; }
    public string TradeCode { get; set; }
    public string Flag { get; set; }
    public string TradeResult { get; set; }
}

public class ElementMasterMsg
{
    public ElementMasterMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public string ElementName { get; set; }
    public string ElementCode { get; set; }
    public string Flag { get; set; }
    public string ElementResult { get; set; }
}
//public class MakeMasterMsg
//{
//    public MakeMasterMsg()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }


//    public string MakeName { get; set; }
//    public string MakeCode { get; set; }
//    public string Flag { get; set; }
//    public string Result { get; set; }
//}
public class CRAMIOWGroupMsg
{
    public CRAMIOWGroupMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
    public Int32 CRAMGroupId { get; set; }
    public string GroupCode { get; set; }
    public string GroupName { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}
public class CRAMIOWSubGroupMsg
{
    public CRAMIOWSubGroupMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
    public Int32 CRAMSubGroupId { get; set; }
    public string GroupCode { get; set; }
    public string SubGroupCode { get; set; }
    public string SubGroupName { get; set; }
    public string GroupName { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}
public class CRAMIOWCodeMsg
{
    public CRAMIOWCodeMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
    public Int64 CRAMIOWHeadDtlId { get; set; }
    public Int64 ClientProjectId { get; set; }
    public string GroupCode { get; set; }
    public string GroupName { get; set; }
    public string SubGroupCode { get; set; }
    public string SubGroupName { get; set; }

    public string IOWHeadCode { get; set; }
    public string IOWCode { get; set; }
    public string IOWDescription { get; set; }
    public string IOWUOM { get; set; }
    public string IOWHeadDescription { get; set; }

    public string L1Code { get; set; }
    public string L1Desc { get; set; }
    public string L2Code { get; set; }
    public string L2Desc { get; set; }
    public string L3Code { get; set; }
    public string L3Desc { get; set; }
    public string L4Code { get; set; }
    public string L4Desc { get; set; }

    public bool IsTemproryIOW { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
    public Int16 TenderIOWMapped { get; set; }
    public Int64 TenderMapId { get; set; }
}
public class ItemCategoryMsg
{
    public ItemCategoryMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
    public Int32 ItemCategoryId { get; set; }
    public string ItemCategoryCode { get; set; }
    public string ItemCategoryName { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string Result { get; set; }
}
public class ItemSubCategoryMsg
{
    public ItemSubCategoryMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
    public string ItemCategoryCode { get; set; }
    public Int32 ItemCategoryId { get; set; }
    public Int32 ItemSubCategoryId { get; set; }
    public string ItemSubCategoryCode { get; set; }
    public string ItemSubCategoryName { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string Result { get; set; }
}
public class ItemGroupMsg
{
    public ItemGroupMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   
    public Int32 CompanyId { get; set; }
    public Int32 ItemGroupId { get; set; }
    public string ItemGroupCode { get; set; }
    public string ItemGroupName { get; set; }
    public string ItemCategoryName { get; set; }
    public Int32 ItemCategoryId { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}

public class ItemMsg
{
    public ItemMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
    public Int32 ItemId { get; set; }
    public Int32 ItemCategoryId { get; set; }
    public Int32 ItemSubCategoryId { get; set; }
    public Int32 ItemGroupId { get; set; }
    public string ItemCategoryName { get; set; }
    public string ItemSubCategoryName { get; set; }
    public string ItemGroupName { get; set; }

    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public string ItemUOM { get; set; }
    public string ItemMake { get; set; }
    public bool IsImported { get; set; }

    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}

public class ItemRateMsg
{
    public ItemRateMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
    public Int32 ItemCategoryId { get; set; }
    public Int32 ItemGroupId { get; set; }
    public Int32 ItemSubCategoryId { get; set; }

    public string ItemCategoryName { get; set; }
    public string ItemSubCategoryName { get; set; }
    public string ItemGroupName { get; set; }
    public Int32 ItemRateId { get; set; }
    public Int32 ItemId { get; set; }


    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public string ItemUOM { get; set; }
    public string ItemMake { get; set; }
    public bool IsImported { get; set; }
    
    public decimal ItemRate { get; set; }
    public string Region { get; set; }
    public Int32 StateId { get; set; }
    public string StateName { get; set; }
    public DateTime RateDate { get; set; }
    public string SourceOfRate { get; set; }
    public decimal Discount { get; set; }
    public Int64 RateYearMonth { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}
 public class CRAMIOWDtlMsg
  {
    public CRAMIOWDtlMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
     public Int64 CRAMIOWHeadId { get; set; }
     public string CRAMIOWHeadCode { get; set; }
      public string PreviousIOWLevel { get; set; }
      public Int64 CRAMIOWHeadDtlId { get; set; }
      public string IOWCode { get; set; }
    public string IOWDescription { get; set; }
    public string IOWUOM { get; set; }
    public string CRAMIOWShortName { get; set; }
    public double IOWQuantity { get; set; }
    public bool IsTemproryIOW { get; set; }
    public bool IsCommonFactorsApplicable { get; set; }

    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
      //public string PackageCode { get; set; }
    //public string TradeCode { get; set; }
    //public string ElementCode { get; set; }

    //public string PackageName { get; set; }
    //public string TradeName { get; set; }
    //public string ElementName { get; set; }
 }
public class CRAMIOWHeadMsg
{
    public CRAMIOWHeadMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
    public string GroupCode { get; set; }
    public string SubGroupCode { get; set; }
    public string GroupName { get; set; }
    public string SubGroupName { get; set; }
    public string IOWHeadCode { get; set; }
    public string IOWHeadName { get; set; }
    public string IOWLevel1 { get; set; }
    public string IOWLevel2 { get; set; }
    public string IOWLevel3 { get; set; }
    public string IOWLevel4 { get; set; }
   
    public string StarItem { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}
public class IOWItemMsg
{
    public IOWItemMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
  
    public CRAMIOWHeadMsg IOWMas { get; set; }
    public Int32 CompanyId { get; set; }
    public Int64 CRAMIOWHeadDtlId { get; set; }
    public string IOWCode { get; set; }
    public string IOWName { get; set; }
    public string CRAMIOWCode { get; set; }
    public string CRAMIOWName { get; set; }
    public string IOWUOM { get; set; }
    public bool IsTemproryIOW { get; set; }
    public string Context { get; set; }
    public string ContextDisplay { get; set; }
    public string ContextSrlNo { get; set; }

    public double IOWQuantity { get; set; }
    
    public Int64 ItemId { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public Int64 IOWHeadDtlId { get; set; }
    public string IOWHeadDtlDescription { get; set; }
    public string ItemUOM { get; set; }
    public double ItemQuantity { get; set; }
    public double Wastage { get; set; }
    public string ItemMake { get; set; }
    public bool IsImported { get; set; }
    public bool IsCommonFactorApplicable { get; set; }

    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}
public class RateYearMsg
{
    public RateYearMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
   
    public Int64 RateYearMonth { get; set; }
    
}
public class GroupMsg
{
    public GroupMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Int32 GroupId { get; set; }
    public Int32 CompanyId { get; set; }
    public string GroupCode { get; set; }
    public string GroupName { get; set; }
  
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}
public class SubGroupMsg
{
    public SubGroupMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Int32 SubGroupId { get; set; }
    public Int32 CompanyId { get; set; }
    public string GroupCode { get; set; }
    public string SubGroupCode { get; set; }
    public string SubGroupName { get; set; }

    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}
public class IOWHeadMsg
{
    public IOWHeadMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Int32 IOWHeadId { get; set; }
    public Int32 CompanyId { get; set; }
    public string IOWHeadCode { get; set; }
    public string IOWHeadDescription { get; set; }
    public string IOWLevel1 { get; set; }
    public string IOWLevel2 { get; set; }
    public string IOWLevel3 { get; set; }
    public string IOWLevel4 { get; set; }
    public string GroupCode { get; set; }
  
    public string SubGroupCode { get; set; }
    public string StarItem { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}
 public class CRAMCommonFactorMsg
{
    public CRAMCommonFactorMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Int32 CompanyId { get; set; }
    public Int32 IOWCommonFactorId { get; set; }
    public string IOWCommonFactor { get; set; }
    public double FactorPercentage { get; set; }
    public int SequenceNumber { get; set; }
    public int SequenceGroup { get; set; }
    public double EffectivePercentage { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string CreatedBy { get; set; }
    public string Result { get; set; }
}
 public class ProjectInputCRAMCommonFactorMsg
 {
     public ProjectInputCRAMCommonFactorMsg()
     {
         //
         // TODO: Add constructor logic here
         //
     }

     public Int32 CompanyId { get; set; }
     public string ClientCode { get; set; }
     public string ProjectCode { get; set; }
     public Int64 ForYearMonth { get; set; }
     public Int32 IOWCommonFactorId { get; set; }
     public string IOWCommonFactor { get; set; }
     public double FactorPercentage { get; set; }
     public int SequenceNumber { get; set; }
     public int SequenceGroup { get; set; }
     public double EffectivePercentage { get; set; }
     public bool IsActive { get; set; }
     public string Flag { get; set; }
     public string CreatedBy { get; set; }
     public string Result { get; set; }
 }

