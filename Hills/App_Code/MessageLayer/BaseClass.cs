using System.Collections.Generic;

/// <summary>
/// Summary description for BaseClass
/// </summary>
public class BaseClass : System.Web.UI.Page
{
    public BaseClass()
    {
    }
    public void clear()
    {
        Session["UserId"] = null;
        Session["EmployeeCode"] = null;
        Session["EmployeeName"] = null;
        Session["HitCounter"] = null;
        Session["CompanyId"] = null;
        Session["DepartmentId"] = null; //scs 210224 to identify the division he belongs
        Session["IsAdmin"] = null; //scs 210224 to identify the division he belongs
        Session["IsSuperUser"] = null; //scs 210806 to identify to the unit and related locations
        Session["CompanyCode"] = null;
        Session["CompanyName"] = null;
        Session["EmployeeMailId"] = null;
        Session["ProgramMsgList"] = null;
        Session["IsHomePageAction"] = null;
        Session["ProgramMsgList"] = null; // scs added since this list is causing problem sometimes 180814
        
    }

    public string UserId
    {
        get
        {
            return Session["UserId"].ToString();
        }
        set
        {
            Session["UserId"] = value;
        }
    }
    public bool IsAdmin //scs 210224
    {
        get
        {
            if (Session["IsAdmin"] == null || (bool)Session["IsAdmin"] == false)
            {
                Session["IsAdmin"] = false;
            }
            else
            {
                Session["IsAdmin"] = true;
            }
            return (bool)Session["IsAdmin"];
        } 
        set
        {
            Session["IsAdmin"] = value;
        }
    }
    public bool IsSuperUser //scs 210806 identify a suoer user for the unit
    {
        get
        {
            if (Session["IsSuperUser"] == null || (bool)Session["IsSuperUser"] == false)
            {
                Session["IsSuperUser"] = false;
            }
            else
            {
                Session["IsSuperUser"] = true;
            }
            return (bool)Session["IsSuperUser"];
        }
        set
        {
            Session["IsSuperUser"] = value;
        }
    }
    public string EmployeeCode
    {
        get
        {
            return Session["EmployeeCode"] == null ? "" : Session["EmployeeCode"].ToString();
        }
        set
        {
            Session["EmployeeCode"] = value;
        }
    }

    public string IsHomePageAction
    {
        get
        {
            return Session["IsHomePageAction"] == null ? "" : Session["IsHomePageAction"].ToString();
        }
        set
        {
            Session["IsHomePageAction"] = value;
        }
    }

    public string EmployeeName
    {
        get
        {
            return Session["EmployeeName"] == null ? "" : Session["EmployeeName"].ToString();
        }
        set
        {
            Session["EmployeeName"] = value;
        }
    }
    public string EmployeeMailId
    {
        get
        {
            return Session["EmployeeMailId"] == null ? "" : Session["EmployeeMailId"].ToString();
        }
        set
        {
            Session["EmployeeMailId"] = value;
        }
    }
    public string HitCounter
    {
        get
        {
            return Session["HitCounter"] == null ? "" : Session["HitCounter"].ToString();
        }
        set
        {
            Session["HitCounter"] = value;
        }
    }
    public int CompanyId
    {
        get
        {
            if (Session["CompanyId"] == null )
            {
                Session["CompanyId"] = 0;
            }

            return (int)Session["CompanyId"];
          //  return (int) Session["CompanyId"] == null ? 0 : (int)Session["CompanyId"];
        }
        set
        {
            Session["CompanyId"] = value;
        }
    }
    public string DepartmentId
    {
        get
        {
            return Session["DepartmentId"] == null ? "" : Session["DepartmentId"].ToString();
        }
        set
        {
            Session["DepartmentId"] = value;
        }
    }
    public string CompanyCode
    {
        get
        {
            return Session["CompanyCode"] == null ? "" : Session["CompanyCode"].ToString();
        }
        set
        {
            Session["CompanyCode"] = value;
        }
    }
    public string CompanyName
    {
        get
        {
            return Session["CompanyName"] == null ? "" : Session["CompanyName"].ToString();
        }
        set
        {
            Session["CompanyName"] = value;
        }
    }

    public int UserSessionId
    {
        get
        {
            return (int)Session["UserSessionId"] == null ? 0 : (int)Session["UserSessionId"];
        }
        set
        {
            Session["UserSessionId"] = value;
        }
    }
    public string LoginResult
    {
        get
        {
            return Session["LoginResult"] == null ? "" : Session["LoginResult"].ToString();
        }
        set
        {
            Session["LoginResult"] = value;
        }
    }

    //Added by Madhavi for fetch  RoleName  in Login Page --160513
    public string RoleName
    {
        get
        {
            return Session["RoleName"] == null ? "" : Session["RoleName"].ToString();
        }
        set
        {
            Session["RoleName"] = value;
        }
    }
    public List<ProgramMsg> ProgramMsgList
    {
        get
        {
            return (List<ProgramMsg>)Session["ProgramMsgList"];
        }
        set
        {
            Session["ProgramMsgList"] = value;
        }
    }

    public string SessionVar1
    {
        get
        {
            return Session["SessionVar1"] == null ? "" : Session["SessionVar1"].ToString();
        }
        set
        {
            Session["SessionVar1"] = value;
        }
    }
}
