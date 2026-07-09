using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DepartmentMsg
/// </summary>
public class ValidationMsg
{
    public ValidationMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string FactoryCode { get; set; }
    public string InputFileName { get; set; }
    public int CreatedbyId { get; set; }
    public DateTime CollectionDate { get; set; }
    //public DateTime ReceiptDate { get; set; }
    //public int MachineNo { get; set; }
   
}