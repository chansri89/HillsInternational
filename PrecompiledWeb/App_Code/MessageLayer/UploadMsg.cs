using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SaleNoteInvoiceUploadMsg
/// </summary>
public class UploadMsg
{
    public UploadMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int FromDate { get; set; }

    public int ToDate { get; set; }
    public int MusterDate { get; set; }
    public string Type { get; set; }
    public string CompanyCode { get; set; }
    public int NoOfWeighments { get; set; }
    //public string WarehouseCode { get; set; }
    //public string WarehouseName { get; set; }
    public string UserId { get; set; }
    public string InputFileName { get; set; }
    public string UploadResult { get; set; }
    public int FromYear { get; set; }
    public int ToYear { get; set; }
    public string DBFFilename1 { get; set; }


}
