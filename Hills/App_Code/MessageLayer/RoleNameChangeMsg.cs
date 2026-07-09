using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StateMasterMsg
/// </summary>
public class RoleNameChangeMsg
{
    public RoleNameChangeMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string CreatedBy { get; set; }
    public string Flag { get; set; }
    public string RoleResult { get; set; }

}
public class DSRNumberChangeMsg
{
    public DSRNumberChangeMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int DSRId { get; set; }
    public string DSRNumber { get; set; }
    public string CreatedBy { get; set; }
    public string DSRResult { get; set; }

}