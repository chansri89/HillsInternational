using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Ganini.Lib;


public partial class MasterPage3 : System.Web.UI.MasterPage
{
 #region Declaration
    ProcessBus Bus = new ProcessBus();
    EmployeeMasterMsg emp = new EmployeeMasterMsg();
    List<ProgramMsg> ProgramList = new List<ProgramMsg>();
    BaseClass AccessBaseClass = new BaseClass();
    public static string currValue="1";
    public static string wcurVal = "1";
    public static string Viwstateval = "1";
   private DataTable dtMenu
    {
        get
        {
            object objdt = ViewState["vsDt"];
            return (DataTable)objdt;
        }
        set
        {
            ViewState["vsDt"] = value;
        }
    }
#endregion
    #region Events and Operations
    protected void Page_Load(object sender, EventArgs e)
    {
        Uri u = Request.Url;
        if (AccessBaseClass.EmployeeName == string.Empty)
        {
            Response.Redirect(string.Format("~/Login.aspx?IsSessionTimeOutFlag={0}", "Y"));
        }
        lbluname.Text = AccessBaseClass.EmployeeName;
        lblDate.Text = Convert.ToString(System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm"));
        lblVersionNumber.Text = Config.GetAppsetting("VERSION");
        lblHeading.Text = "Tender Insight System - Version: " + lblVersionNumber.Text; //scs 230501 vj wants version in the top
        //Id = AccessBaseClass.UserId;
        //UserName = AccessBaseClass.UserFullName;
        //HitCounter = AccessBaseClass.HitCounter;
        //PlantId = AccessBaseClass.PlantId;
        //PlantName = AccessBaseClass.PlantName;
       if (!Page.IsPostBack)
        {
            Session["Sess1curVal"] = "";
            emp.EmployeeCode = AccessBaseClass.EmployeeCode;
            ProgramList = Bus.AdmUserAccessProgramsSelect(emp);
            AccessBaseClass.ProgramMsgList = ProgramList;
            //DataTable dt = new DataTable();

            //dt.Columns.Add("MainMenu");
            //dt.Columns.Add("ProgramAccessPath");
            //dt.Columns.Add("URL");
            //foreach (var item in ProgramList)
            //{
            //    var row = dt.NewRow();

            //    row["Name"] = item.MainMenu;
            //    row["Price"] = item.ProgramAccessPath;
            //    row["URL"] = item.;

            //    dt.Rows.Add(row);
            //}

            //dtMenu = ProgramList;
            LoadMenu(ProgramList);
        }
        //if (currValue != "1")
        if (AccessBaseClass.SessionVar1 != "")
        {
            Session["Sess1curVal"] = wcurVal;
            Session["Sess3curVal"] = wcurVal;
            AccessBaseClass.SessionVar1 = wcurVal;
        }
            //dtMenu = Bus.GetMenuItems(Id);
            //LoadMenu(dtMenu);
    }

    #region MenuLoading
    private void LoadMenu(List<ProgramMsg> programList)
    {
        litMenu.Text = BuildMenuMarkup(programList);
    }

    private string BuildMenuMarkup(IEnumerable<ProgramMsg> programList)
    {
        var topLevelGroups = (programList ?? Enumerable.Empty<ProgramMsg>())
            .Where(item => !string.IsNullOrWhiteSpace(item.MainMenu))
            .GroupBy(item => item.MainMenu)
            .OrderBy(group => group.Min(menuItem => menuItem.ProgramSequence))
            .ToList();

        var builder = new StringBuilder();
        builder.Append("<ul class=\"tis-menu\">");

        foreach (var group in topLevelGroups)
        {
            var mainMenuName = group.Key ?? string.Empty;
            var subMenuGroups = group.Where(item => !string.IsNullOrWhiteSpace(item.SubMenu))
                .GroupBy(item => item.SubMenu)
                .OrderBy(subGroup => subGroup.Min(menuItem => menuItem.ProgramSequence))
                .ToList();

            builder.Append("<li class=\"tis-menu__item\">");

            if (subMenuGroups.Count > 0)
            {
                builder.AppendFormat("<a href=\"javascript:void(0)\" class=\"tis-menu__link\">{0}</a>", HttpUtility.HtmlEncode(mainMenuName));
                builder.Append("<ul class=\"tis-menu__submenu\">");

                foreach (var subGroup in subMenuGroups)
                {
                    var subMenuName = subGroup.Key ?? string.Empty;
                    var childItems = subGroup.Where(item => !string.IsNullOrWhiteSpace(item.ChildMenu)).ToList();
                    builder.Append("<li class=\"tis-menu__submenu-item\">");

                    if (childItems.Count > 0)
                    {
                        builder.AppendFormat("<a href=\"javascript:void(0)\" class=\"tis-menu__sublink\">{0}</a>", HttpUtility.HtmlEncode(subMenuName));
                        builder.Append("<ul class=\"tis-menu__childmenu\">");

                        foreach (var child in childItems)
                        {
                            var link = string.IsNullOrWhiteSpace(child.ProgramAccessPath) ? "#" : child.ProgramAccessPath;
                            builder.AppendFormat("<li class=\"tis-menu__childitem\"><a href=\"{0}\" class=\"tis-menu__childlink\">{1}</a></li>",
                                HttpUtility.HtmlAttributeEncode(link),
                                HttpUtility.HtmlEncode(child.ChildMenu ?? string.Empty));
                        }

                        builder.Append("</ul>");
                    }
                    else
                    {
                        var fallbackLink = "#";
                        builder.AppendFormat("<a href=\"{0}\" class=\"tis-menu__sublink\">{1}</a>",
                            HttpUtility.HtmlAttributeEncode(fallbackLink),
                            HttpUtility.HtmlEncode(subMenuName));
                    }

                    builder.Append("</li>");
                }

                builder.Append("</ul>");
            }
            else
            {
                var firstChild = group.FirstOrDefault();
                var fallbackLink = "#";
                if (firstChild != null && !string.IsNullOrWhiteSpace(firstChild.ProgramAccessPath))
                {
                    fallbackLink = firstChild.ProgramAccessPath;
                }

                builder.AppendFormat("<a href=\"{0}\" class=\"tis-menu__link\">{1}</a>",
                    HttpUtility.HtmlAttributeEncode(fallbackLink),
                    HttpUtility.HtmlEncode(mainMenuName));
            }

            builder.Append("</li>");
        }

        builder.Append("</ul>");
        return builder.ToString();
    }

//        private void LoadMenu(DataTable dtMenu)
//        {
//            foreach (DataRow dr in dtMenu.Rows)
//            {
//                WMainMenu = dr["MainMenu"].ToString();
//                WSubMenu = dr["SubMenu"].ToString();
//                WMainSubMenu = WMainMenu + WSubMenu;
//                #region NewMainMenu
//                if (MainMenu != dr["MainMenu"].ToString()) // This is a new menu  //Add MainMenu Item
//                {
//                    MenuCount = MenuCount + 1;
//                    LoadMainMenuName(MenuCount, WMainMenu);   // Load MainMenu Name Based on MenuCount
//                    if (WMainSubMenu != WMainMenu) // There is a submenu item in this main menu
//                    {
//                        MenuItem Sub = new MenuItem();
//                        Sub.Text = WSubMenu;
//                        foreach (DataRow drSubTrans in dtMenu.Select("MainMenu ='" + WMainMenu + "' and SubMenu = '" + WSubMenu + "'"))
//                        {
//                            MenuItem SubSubMenu = new MenuItem();
//                            SubSubMenu.Text = drSubTrans["ChildMenu"].ToString();
//                            SubSubMenu.NavigateUrl = drSubTrans["ProgramAccessPath"].ToString();
//                            Sub.ChildItems.Add(SubSubMenu);
//                        }
//                        MainMenu = WMainMenu;
//                        MainSubMenu = WMainSubMenu;
//                        LoadSubMenu(MenuCount, Sub); // Load SubMenu Under the MainMenu Depending on MenuCount 
//                    }
//                    else //There's no SubMenu hence the submenu becomes the child menu for hyper linking
//                    {
//                        foreach (DataRow drSubTrans in dtMenu.Select("MainMenu = '" + WMainMenu + "'"))
//                        {
//                            MenuItem SubSubMenu = new MenuItem();
//                            SubSubMenu.Text = drSubTrans["ChildMenu"].ToString();
//                            SubSubMenu.NavigateUrl = drSubTrans["ProgramAccessPath"].ToString();
//                            LoadSubMenu(MenuCount, SubSubMenu); // Load SubSubMenu Under the MainMenu Depending on MenuCount
//                        }
//                        MainMenu = WMainMenu;
//                        MainSubMenu = WMainSubMenu;
//                    }
//                }
//                #endregion
//                #region Submenu
//                else // MainMenu Remains Same
//                {
//                    //If there is a new submenu for same main menu add it else SKIP
//                    if (MainSubMenu != WMainSubMenu) // There is a new Submenu hence proceed
//                    {
//                        if (WMainSubMenu != WMainMenu) // There is a submenu item in this main menu
//                        {
//                            MenuItem Sub = new MenuItem();
//                            Sub.Text = WSubMenu;
//                            foreach (DataRow drSub in dtMenu.Select("MainMenu ='" + WMainMenu + "' and SubMenu = '" + WSubMenu + "'"))
//                            {
//                                MenuItem SubSubMenu = new MenuItem();
//                                SubSubMenu.Text = drSub["ChildMenu"].ToString();
//                                SubSubMenu.NavigateUrl = drSub["ProgramAccessPath"].ToString();
//                                Sub.ChildItems.Add(SubSubMenu);
//                            }
//                            MainMenu = WMainMenu;
//                            MainSubMenu = WMainSubMenu;
//                            LoadSubMenu(MenuCount, Sub); // Load SubMenu Under the MainMenu Depending on MenuCount
//                        }
//                        else  // There is no submenu item in this main menu
//                        {
//                            foreach (DataRow drSub in dtMenu.Select("MainMenu = '" + WMainMenu + "'"))
//                            {
//                                MenuItem SubSubMenu = new MenuItem();
//                                SubSubMenu.Text = drSub["ChildMenu"].ToString();
//                                SubSubMenu.NavigateUrl = drSub["ProgramAccessPath"].ToString();
//                                LoadSubMenu(MenuCount, SubSubMenu); // Load SubSubMenu Under the MainMenu Depending on MenuCount  
//                            }
//                            MainMenu = WMainMenu;
//                            MainSubMenu = WMainSubMenu;
//                        }
//                    }
//                    else
//                    {
//                        //Skip
//                    }
//                }
//#endregion
//            }
          
//            //Loading All Menus in MasterPage
//            mnuMainMenu.Items.Add(Menu1);
//            mnuMainMenu.Items.Add(Menu2);
//            mnuMainMenu.Items.Add(Menu3);
//            mnuMainMenu.Items.Add(Menu4);
//            mnuMainMenu.Items.Add(Menu5);
//            mnuMainMenu.Items.Add(Menu6);
//            mnuMainMenu.Items.Add(Menu7);
//            mnuMainMenu.Items.Add(Menu8);
      
//        }
   #endregion
    protected void lnkbtnLogout_Click(object sender, EventArgs e)
    {
        //LogofDateTime = Bus.GetServerDate();
        //Bus.UpdateUserLogin(HitCounter, LogofDateTime);
        //dtMenu.Clear();
        //Response.Redirect("Login.aspx");
    }
    protected void Collapse_Click(object sender, EventArgs e)
    {
        Collapse();
    }
    private void Collapse()
    {
        if (LnkCollapse.Text == "Menu -")
        {
            Panel1.Visible = false;
            LnkCollapse.Text = "Menu +";
            
        }
        else
        {
            Panel1.Visible = true;
            LnkCollapse.Text = "Menu -";
        }
    }
    #endregion
    #region Methods
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        LoginInfoMsg Login=new LoginInfoMsg();
        Login.UserName=AccessBaseClass.EmployeeCode;
        Login.UserSessionId=AccessBaseClass.UserSessionId;
        Bus.UpdateUserLogoffInfo(Login);
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect("~/Login.aspx");
    }
    protected void lnkHome_Click(object sender, EventArgs e)
    {
       Response.Redirect("~/Default.aspx");
    }
    #endregion
}
