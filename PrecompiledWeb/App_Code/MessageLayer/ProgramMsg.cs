using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProgramMsg
/// </summary>
public class ProgramMsg
{
	public ProgramMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int ProgramId { get; set; }
    public string ProgramName { get; set; }
    public string ProgramAccessPath { get; set; }
    public string MainMenu { get; set; }
    public string SubMenu { get; set; }
    public string ChildMenu { get; set; }
    public int ProgramSequence { get; set; }
    public bool CanAccess { get; set; }
    public bool CanCreate { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
    public bool CanPrint { get; set; }
    public string Result { get; set; }
}