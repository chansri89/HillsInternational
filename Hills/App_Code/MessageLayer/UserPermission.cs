using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ganini.Lib;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for UserPermission
/// </summary>
public enum UserPermission
{
    CanAccess = 0,
    CanCreate = 1,
    CanEdit = 2,
    CanDelete = 4,
    CanPrint = 5,
}

public class UserAccess
{
    public UserAccess()
    {

    }
    BaseClass AccessBase = new BaseClass();
    
    public bool HasPermission(string ProgramName, string Permission)
    {
        bool Result = false;
        if (AccessBase.ProgramMsgList != null && AccessBase.ProgramMsgList.Count > 0)
        {
            switch (Permission)
            {
                case "CanAccess":
                    var Access = (from programMsg in AccessBase.ProgramMsgList
                                  where programMsg.ProgramAccessPath.Contains(ProgramName)
                                  select programMsg.CanAccess).ToList();
                    Result = Access[0];
                    break;
                case "CanCreate":
                    var Create = (from programMsg in AccessBase.ProgramMsgList
                                  where programMsg.ProgramAccessPath.Contains(ProgramName)
                                  select programMsg.CanCreate).ToList();
                    Result = Create[0];
                    break;
                case "CanEdit":
                    var Edit = (from programMsg in AccessBase.ProgramMsgList
                                where programMsg.ProgramAccessPath.Contains(ProgramName)
                                select programMsg.CanEdit).ToList();
                    Result = Edit[0];
                    break;
                case "CanDelete":
                    var Delete = (from programMsg in AccessBase.ProgramMsgList
                                  where programMsg.ProgramAccessPath.Contains(ProgramName)
                                  select programMsg.CanDelete).ToList();
                    Result = Delete[0];
                    break;
                case "CanPrint":
                    var Print = (from programMsg in AccessBase.ProgramMsgList
                                 where programMsg.ProgramAccessPath.Contains(ProgramName)
                                 select programMsg.CanPrint).ToList();
                    Result = Print[0];
                    break;
            }
        }
        return Result;
    }
}
