using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RoleMsg
/// </summary>
public class RoleMsg
{
	public RoleMsg()    {	}
    public string RoleName { get; set; }
    public int RoleId { get; set; }
    public int AsgRoleId { get; set; }
    public int AvRoleId { get; set; }
}

public class RoleProgramsMsg
{
    public RoleProgramsMsg() { }
    public string RoleName { get; set; }
    public int ProgramId { get; set; }
    public int RoleId { get; set; }
    public string ProgramName { get; set; }
    public bool CanAccess { get; set; }
    public bool CanCreate { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
    public bool CanPrint { get; set; }
    public string CreatedBy { get; set; }
    public string Flag { get; set; }
    public string Result { get; set; }
    public string MainMenu { get; set; }
}