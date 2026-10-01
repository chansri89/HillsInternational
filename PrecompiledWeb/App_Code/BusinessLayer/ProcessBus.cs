using System;
using System.Collections.Generic;
using Ganini.Lib;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Web.UI;
using GeneratingMail.Messages;
using System.Xml;

/// <summary>
/// Summary description for ProcessBus
/// </summary>
public class ProcessBus
{
    public ProcessBus()
    {
        string TestFileName = Config.GetAppsetting("TestFile");
    }
    Validation Isvalid = new Validation();
    string TestFileName = Config.GetAppsetting("TestFile");
    #region Connection

    private ConnectionClass MconnectionClass = null;
    private ConnectionClass Connection
    {
        get
        {
            if (null == MconnectionClass)
            {
                MconnectionClass = new Ganini.Lib.ConnectionClass();
            }
            return MconnectionClass;
        }

    }

    #endregion

    #region MasterSelect
    //public List<CompanyMsg> CompanyMasterSelect()
    //{
    //    List<CompanyMsg> CList = new List<CompanyMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {

    //            Connection.cmd.CommandText = "MasCompanyMasterSelectSp";
    //            Connection.cmd.CommandType = CommandType.StoredProcedure;
    //            Connection.cmd.Connection = Connection.con;
    //            using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //            {
    //                while (sdr.Read())
    //                {
    //                    CompanyMsg Comp = new CompanyMsg();
    //                    Comp.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString());
    //                    Comp.CompanyCode = (sdr["CompanyCode"].ToString());
    //                    Comp.CompanyName = sdr["CompanyName"].ToString();
    //                    // Comp.CompanyFlag = sdr["CompanyFlag"].ToString();
    //                    CList.Add(Comp);
    //                }
    //            }
    //        }

    //        catch (Exception ex)
    //        {
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
    //            return null;
    //        }
    //        finally
    //        {
    //            Connection.cmd.Dispose();
    //            Connection.cmd.Parameters.Clear();
    //        }
    //        return CList;
    //    }
    //}
    public List<CompanyMessage> CompanyMasterSelect(EmployeeMasterMsg Emp)
    {
        List<CompanyMessage> CompanyList = new List<CompanyMessage>();
        using (Connection.con)
        {

            try
            {

                Connection.cmd.CommandText = "MasCompanyMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CompanyMessage CompanyMessage = new CompanyMessage();
                        CompanyMessage.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim()); // scs 200403 Added as only companyCode is being taken
                        CompanyMessage.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                        CompanyMessage.CompanyName = sdr["CompanyName"].ToString().Trim();
                        CompanyMessage.CompanyShortName = sdr["CompanyShortName"].ToString().Trim();
                        CompanyMessage.CompanyFlag = sdr["CompanyFlag"].ToString().Trim();
                        CompanyMessage.ParentCompanyCode = sdr["ParentCompanyCode"].ToString().Trim();
                        CompanyMessage.ParentCompanyName = sdr["ParentCompanyName"].ToString().Trim();
                        CompanyList.Add(CompanyMessage);
                    }
                }


            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return CompanyList;

    }
    public List<EmployeeMasterMsg> EmployeeMasterSelect(EmployeeMasterMsg Emp)
    {
        List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "MasEmployeeMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@LoginEmployeeCode", Emp.LoginEmployeeCode);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        EmployeeMasterMsg Employee = new EmployeeMasterMsg();
                        Employee.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                        Employee.CompanyName = sdr["CompanyName"].ToString().Trim();
                        Employee.EmployeeCode = sdr["EmployeeCode"].ToString().Trim();
                        Employee.EmployeeName = sdr["EmployeeName"].ToString().Trim();
                        Employee.EmailId = sdr["EmailId"].ToString().Trim();
                        Employee.Password = sdr["PassWord"].ToString().Trim();
                        Employee.ManagerCode = sdr["ManagerCode"].ToString().Trim();
                        Employee.ManagerName = sdr["ManagerName"].ToString().Trim();
                        Employee.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        //Employee.EmployeeDesignation = sdr["ActivityDesignation"].ToString().Trim();-- sai 15/05/15 for connection agiler
                        Employee.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                        Employee.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                        Employee.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                        Employee.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                        EmpList.Add(Employee);
                    }
                }


            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return EmpList;
        }
    }
    public List<StateMasterMsg> StateMasterSelect(EmployeeMasterMsg Emp)
    {
        List<StateMasterMsg> StateList = new List<StateMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasStateMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        StateMasterMsg St = new StateMasterMsg();
                        St.StateId = Convert.ToInt32(sdr["StateId"].ToString().Trim());
                        St.StateName = sdr["StateName"].ToString().Trim();
                        St.StateShortName = sdr["StateShortName"].ToString().Trim();
                        StateList.Add(St);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return StateList;
        }
    }
    public List<EnterpriseMasterMsg> EnterpriseMasterSelect(EmployeeMasterMsg Emp)
    {
        List<EnterpriseMasterMsg> EnterpriseList = new List<EnterpriseMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasEnterpriseMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        EnterpriseMasterMsg Ep = new EnterpriseMasterMsg();
                        Ep.EnterpriseId = Convert.ToInt32(sdr["EnterpriseId"].ToString().Trim());
                        Ep.EnterpriseName = sdr["EnterpriseName"].ToString().Trim();
                        Ep.Addr1 = sdr["Addr1"].ToString().Trim();
                        Ep.Addr2 = sdr["Addr2"].ToString().Trim();
                        Ep.Addr3 = sdr["Addr3"].ToString().Trim();
                        Ep.Addr4 = sdr["Addr4"].ToString().Trim(); // sai 280714
                        Ep.Phone1 = Convert.ToInt64(sdr["Phone1"].ToString().Trim());
                        //Ep.Phone2 = Convert.ToInt32(sdr["Phone2"].ToString().Trim()); // sai 280714
                        //Ep.Fax = Convert.ToInt32(sdr["Fax"].ToString().Trim());
                        //Ep.Email = sdr["Email"].ToString().Trim(); // sai 280714
                        Ep.Website = sdr["Website"].ToString().Trim();
                        Ep.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        EnterpriseList.Add(Ep);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return EnterpriseList;
        }
    }
    public List<FiscalYearMsg> FiscalYearSelectSp(FiscalYearMsg FiscalYearMsg)
    {
        List<FiscalYearMsg> FiscalYearList = new List<FiscalYearMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "FiscalYearSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        FiscalYearMsg FiscalYrMsg = new FiscalYearMsg();
                        FiscalYrMsg.FiscalYear = Convert.ToInt32(sdr["FiscalYear"].ToString().Trim());
                        FiscalYrMsg.FiscalYearId = Convert.ToInt32(sdr["FiscalYearId"].ToString().Trim());
                        FiscalYearList.Add(FiscalYrMsg);
                    }
                }
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                //return null;
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return FiscalYearList;
    }
    public List<DesignationMsg> DesignationSelectSp(DesignationMsg DesignationMsg)
    {
        List<DesignationMsg> DesignationList = new List<DesignationMsg>();
        using (Connection.con)
        {

            try
            {

                Connection.cmd.CommandText = "MasDesignationSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        DesignationMsg Designation = new DesignationMsg();
                        Designation.ActivityDesignation = sdr["ActivityDesignation"].ToString().Trim();
                        DesignationList.Add(Designation);
                    }
                }


            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                //return null;
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return DesignationList;
    }
    public List<DepartmentMsg> DepartmentMasterSelect(string LoginUserName)
    {
        List<DepartmentMsg> DepList = new List<DepartmentMsg>();
        using (Connection.con)
        {

            try
            {
                Connection.cmd.CommandText = "MasDepartmentMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();

                Connection.cmd.Parameters.AddWithValue("@LoginEmployeeCode", LoginUserName);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        DepartmentMsg Depmsg = new DepartmentMsg();
                        Depmsg.DepartmentId = Convert.ToInt32(sdr["DepartmentId"].ToString().Trim());
                        Depmsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim()); //SCS 210807 since dep is linked to Company
                        Depmsg.DepartmentName = sdr["DepartmentName"].ToString().Trim();
                        //Depmsg.CostCenterId = Convert.ToInt32(sdr["CostCenterId"].ToString().Trim()); //SCS 230122 tbldepartment to have costcenter in it as per disc vj
                        Depmsg.IsFactory = Convert.ToBoolean(sdr["IsFactory"].ToString().Trim()); //scs 230427 to get factory
                        DepList.Add(Depmsg);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return DepList;

    }
  
    //public List<GSTINState> GSTINStateSelect()
    //{
    //    List<GSTINState> GSTSTList = new List<GSTINState>();
    //    using (Connection.con)
    //    {

    //        try
    //        {
    //            Connection.cmd.CommandText = "GSTINStateSelectSp";
    //            Connection.cmd.CommandType = CommandType.StoredProcedure;
    //            Connection.cmd.Connection = Connection.con;
    //            Connection.cmd.Parameters.Clear();
    //            using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //            {
    //                while (sdr.Read())
    //                {
    //                    GSTINState SMsg = new GSTINState();
    //                    SMsg.State = (sdr["State"].ToString().Trim());
    //                    SMsg.GSTINCode = sdr["GSTINCode"].ToString().Trim();
    //                    GSTSTList.Add(SMsg);
    //                }
    //            }
    //        }

    //        catch (Exception ex)
    //        {
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
    //            return null;
    //        }
    //        finally
    //        {
    //            Connection.cmd.Dispose();
    //            Connection.cmd.Parameters.Clear();
    //        }
    //    }
    //    return GSTSTList;

    //}
    public List<CustomerTypeMsg> MasCustomerType()
    {
        List<CustomerTypeMsg> CustTypeLst = new List<CustomerTypeMsg>();
        using (Connection.con)
        {

            try
            {
                Connection.cmd.CommandText = "MasCustomerTypeSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CustomerTypeMsg SMsg = new CustomerTypeMsg();
                        SMsg.CustomerType = (sdr["CustomerType"].ToString().Trim());
                        SMsg.CustomerTypeId = Convert.ToInt32(sdr["CustomerTypeId"].ToString().Trim());
                        CustTypeLst.Add(SMsg);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return CustTypeLst;

    }
    public List<ControlProcessMsg> SelectControlProcessing(int CompanyId, string CreatedBy)
    {
        List<ControlProcessMsg> ControlProcess = new List<ControlProcessMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "ADMSelectControlProcessingSp";
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                using (SqlDataReader Sdr = Connection.cmd.ExecuteReader())
                {
                    while (Sdr.Read())
                    {
                        ControlProcessMsg ControlProcessingMsg = new ControlProcessMsg();
                        ControlProcessingMsg.CompanyId = Convert.ToInt32(Sdr["CompanyId"].ToString());
                        ControlProcessingMsg.ESIPeriodFrom = Convert.ToDateTime(Sdr["ESIPeriodFrom"].ToString());
                        ControlProcessingMsg.ESIPeriodTo = Convert.ToDateTime(Sdr["ESIPeriodTo"].ToString());
                        ControlProcessingMsg.OverTimePeriodFrom = Convert.ToDateTime(Sdr["OverTimePeriodFrom"].ToString());
                        ControlProcessingMsg.OverTimePeriodTo = Convert.ToDateTime(Sdr["OverTimePeriodTo"].ToString());
                        ControlProcessingMsg.PFPeriodFrom = Convert.ToDateTime(Sdr["PFYearFrom"].ToString());
                        ControlProcessingMsg.PFPeriodTo = Convert.ToDateTime(Sdr["PFYearTo"].ToString());
                        ControlProcessingMsg.WorkingDays = Convert.ToInt32(Sdr["WorkingDays"].ToString());
                        ControlProcessingMsg.PaySlipProcess = Convert.ToString(Sdr["PaySlipProcessStatus"].ToString());
                        ControlProcessingMsg.PaySlipYearMonth = Convert.ToInt64(Sdr["PaySlipYearMonth"].ToString());
                        ControlProcessingMsg.ControlProcessingId = Convert.ToInt64(Sdr["ControlProcessingId"].ToString());
                        ControlProcess.Add(ControlProcessingMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }
        }
        return ControlProcess;
    }
    public List<ClientMsg> NewClientMasterSelect(Int32 CompId)
    {
        List<ClientMsg> DepList = new List<ClientMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "MasClientMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                //Connection.cmd.Parameters.AddWithValue("@EmployeeCode", EmpCode);
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompId);
                //Connection.cmd.Parameters.AddWithValue("@UploadFlag", UploadFlag);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ClientMsg depMsg = new ClientMsg();
                        depMsg.ClientCode = (sdr["ClientCode"].ToString().Trim());
                        depMsg.ClientName = sdr["ClientName"].ToString().Trim();
                        depMsg.ClientType = sdr["ClientType"].ToString().Trim();
                        depMsg.ClientShortName = (sdr["ClientShortName"].ToString().Trim());
                        depMsg.ParentClientCode = sdr["ParentClientCode"].ToString().Trim(); // scs 210201 
                        depMsg.BuildingName = (sdr["BuildingName"].ToString().Trim());
                        depMsg.Addr1 = (sdr["Addr1"].ToString().Trim());
                        depMsg.Addr2 = (sdr["Addr2"].ToString().Trim());
                        depMsg.City = (sdr["City"].ToString().Trim());
                        depMsg.PinCode = (sdr["PinCode"].ToString().Trim());
                        depMsg.StateId = Convert.ToInt32(sdr["StateId"].ToString().Trim());
                        depMsg.StateName = (sdr["StateName"].ToString().Trim());
                        depMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        DepList.Add(depMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return DepList;
        }
    }
    //MasClientProjectSelectSp
    public List<ProjectMsg> MasClientProjectSelect(Int32 CompId)
    {
        List<ProjectMsg> ProjList = new List<ProjectMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "MasClientProjectSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ProjectMsg proj = new ProjectMsg();
                        proj.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                        proj.ClientProjectId = Convert.ToInt32(sdr["ClientProjectId"].ToString().Trim());
                        proj.ClientCode = (sdr["ClientCode"].ToString().Trim());
                        proj.ClientName = sdr["ClientName"].ToString().Trim();
                        proj.ProjectCode = sdr["ProjectCode"].ToString().Trim();
                        proj.ProjectName = (sdr["ProjectName"].ToString().Trim());
                        proj.RevisionNumber = Convert.ToInt32(sdr["RevisionNumber"].ToString().Trim());
                        proj.StateName = sdr["StateName"].ToString().Trim(); // scs 210201 
                        proj.StateId = Convert.ToInt32(sdr["StateId"].ToString().Trim());
                        proj.StartDate = Convert.ToDateTime(sdr["StartDate"].ToString().Trim());
                        proj.ConstructionCompleted = Convert.ToDateTime(sdr["ConstructionCompleted"].ToString().Trim());

                        ProjList.Add(proj);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return ProjList;
        }
    }
    public List<TenderMsg> MasClientProjectTenderSelect(Int32 CompId, string ClientCode, Int64 ClientProjectId,string TenderFilter)
    {
        List<TenderMsg> TendList = new List<TenderMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "MasClientProjectTenderSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompId);
                Connection.cmd.Parameters.AddWithValue("@ClientCode", ClientCode);
                Connection.cmd.Parameters.AddWithValue("@ClientProjectId", ClientProjectId);
                Connection.cmd.Parameters.AddWithValue("@TenderFilter", TenderFilter);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        TenderMsg tend = new TenderMsg();
                        tend.ClientProjectTenderId = Convert.ToInt64(sdr["ClientProjectTenderId"].ToString().Trim());
                        tend.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                        tend.ClientProjectId = Convert.ToInt32(sdr["ClientProjectId"].ToString().Trim());
                        tend.ClientCode = (sdr["ClientCode"].ToString().Trim());
                        //tend.ClientName = sdr["ClientName"].ToString().Trim();
                        tend.ProjectCode = sdr["ProjectCode"].ToString().Trim();
                        tend.ProjectName = (sdr["ProjectName"].ToString().Trim());
                        tend.RevisionNumber = Convert.ToInt32(sdr["RevisionNumber"].ToString().Trim());
                        
                        tend.ExcelSheetName = sdr["ExcelSheetName"].ToString().Trim(); // scs 210201 
                        tend.ExcelRowNumber = Convert.ToInt32(sdr["ExcelRowNumber"].ToString().Trim());
                        tend.SrlNo = sdr["SrlNo"].ToString().Trim();  
                        tend.Description = sdr["Description"].ToString().Trim();
                        tend.UOM = sdr["UOM"].ToString().Trim();
                        tend.Quantity = Convert.ToDouble(sdr["Quantity"].ToString().Trim());
                        tend.Remarks = sdr["Remarks"].ToString().Trim();
                        tend.SupplyOnly = Convert.ToInt16(sdr["SupplyOnly"].ToString().Trim());
                        tend.TenderIOWMapped = Convert.ToInt16(sdr["TenderIOWMapped"].ToString().Trim());
                        TendList.Add(tend);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return TendList;
        }
    }
    public List<CRAMIOWGroupMsg> MasCRAMGroupSelect(Int32 CompId)
    {
        List<CRAMIOWGroupMsg> GRPlist = new List<CRAMIOWGroupMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "MasCRAMGroupSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CRAMIOWGroupMsg grpMsg = new CRAMIOWGroupMsg();
                        grpMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                        grpMsg.CRAMGroupId = Convert.ToInt32(sdr["CRAMGroupId"].ToString().Trim());
                        grpMsg.GroupCode = (sdr["GroupCode"].ToString().Trim());
                        grpMsg.GroupName = (sdr["GroupName"].ToString().Trim());
                       // grpMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        GRPlist.Add(grpMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return GRPlist;
        }
    }
    public List<CRAMIOWSubGroupMsg> MasCRAMSubGroupSelect(Int32 CompId)
    {
        List<CRAMIOWSubGroupMsg> SGRPlist = new List<CRAMIOWSubGroupMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "MasCRAMSubGroupSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CRAMIOWSubGroupMsg sgrpMsg = new CRAMIOWSubGroupMsg();
                        sgrpMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                        sgrpMsg.CRAMSubGroupId = Convert.ToInt32(sdr["CRAMSubGroupId"].ToString().Trim());
                        sgrpMsg.GroupCode = (sdr["GroupCode"].ToString().Trim());
                        sgrpMsg.SubGroupCode = (sdr["SubGroupCode"].ToString().Trim());
                        sgrpMsg.SubGroupName = (sdr["SubGroupName"].ToString().Trim());
                        //sgrpMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        SGRPlist.Add(sgrpMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return SGRPlist;
        }
    }
    public List<CRAMIOWCodeMsg> IOWCodeForTenderMappingSelect(Int32 CompId, Int64 ClientProjectTenderId, string GroupCode, string SubGroupCode, string IOWFilter)
    {
        List<CRAMIOWCodeMsg> IOWList = new List<CRAMIOWCodeMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "IOWCodeForTenderMappingSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompId);
                Connection.cmd.Parameters.AddWithValue("@ClientProjectTenderId", ClientProjectTenderId);
                Connection.cmd.Parameters.AddWithValue("@GroupCode", GroupCode);
                Connection.cmd.Parameters.AddWithValue("@SubGroupCode", SubGroupCode);
                Connection.cmd.Parameters.AddWithValue("@IOWFilter", IOWFilter);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CRAMIOWCodeMsg IOWMsg = new CRAMIOWCodeMsg();
                        IOWMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                        IOWMsg.CRAMIOWHeadDtlId = Convert.ToInt32(sdr["CRAMIOWHeadDtlId"].ToString().Trim());
                        IOWMsg.GroupCode = (sdr["GroupCode"].ToString().Trim());
                         IOWMsg.GroupName = (sdr["GroupName"].ToString().Trim());
                        IOWMsg.SubGroupCode = (sdr["SubGroupCode"].ToString().Trim());
                        IOWMsg.SubGroupName = (sdr["SubGroupName"].ToString().Trim());

                        IOWMsg.IOWHeadCode = (sdr["IOWHeadCode"].ToString().Trim());
                        IOWMsg.IOWHeadDescription = (sdr["IOWHeadDescription"].ToString().Trim());
                        IOWMsg.IOWCode = (sdr["IOWCode"].ToString().Trim());
                        IOWMsg.IOWDescription = (sdr["IOWDescription"].ToString().Trim());
                        IOWMsg.IOWUOM = (sdr["IOWUOM"].ToString().Trim());
                        IOWMsg.IsTemproryIOW = Convert.ToBoolean(sdr["IsTemproryIOW"].ToString().Trim());
                        IOWMsg.TenderIOWMapped = Convert.ToInt16(sdr["TenderIOWMapped"].ToString().Trim());
                        IOWList.Add(IOWMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return IOWList;
        }
    }

    public List<CRAMIOWCodeMsg> IOWHeadSelect(Int32 CompId, string IOWCode)
    {
        List<CRAMIOWCodeMsg> IOWList = new List<CRAMIOWCodeMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "IOWHeadSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompId);
                Connection.cmd.Parameters.AddWithValue("@IOWCode", IOWCode);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CRAMIOWCodeMsg IOWMsg = new CRAMIOWCodeMsg();
                        //IOWMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                        IOWMsg.GroupCode = (sdr["GroupCode"].ToString().Trim());
                        IOWMsg.GroupName = (sdr["GroupName"].ToString().Trim());
                        IOWMsg.SubGroupCode = (sdr["SubGroupCode"].ToString().Trim());
                        IOWMsg.SubGroupName = (sdr["SubGroupName"].ToString().Trim());

                        IOWMsg.L1Code = (sdr["L1Code"].ToString().Trim());
                        IOWMsg.L1Desc = (sdr["L1Desc"].ToString().Trim());
                        IOWMsg.L2Code = (sdr["L2Code"].ToString().Trim());
                        IOWMsg.L2Desc = (sdr["L2Desc"].ToString().Trim());
                        IOWMsg.L3Code = (sdr["L3Code"].ToString().Trim());
                        IOWMsg.L3Desc = (sdr["L3Desc"].ToString().Trim());
                        IOWMsg.L4Code = (sdr["L4Code"].ToString().Trim());
                        IOWMsg.L4Desc = (sdr["L4Desc"].ToString().Trim());

                        IOWMsg.IOWCode = (sdr["IOWCode"].ToString().Trim());
                        IOWMsg.IOWDescription = (sdr["IOWDescription"].ToString().Trim());
                        IOWMsg.IsTemproryIOW = Convert.ToBoolean(sdr["IsTemproryIOW"].ToString().Trim());
                        
                        IOWList.Add(IOWMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return IOWList;
        }
    }

    //--------
    //public List<IOWMsg> MasIOWMasterSelect(Int32 CompId)
    //{
    //    List<IOWMsg> Iowlst = new List<IOWMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {
    //            Connection.cmd.CommandText = "MasIOWMasterSelectSp";
    //            Connection.cmd.CommandType = CommandType.StoredProcedure;
    //            Connection.cmd.Connection = Connection.con;
    //            Connection.cmd.Parameters.Clear();
    //            Connection.cmd.Parameters.AddWithValue("@CompanyId", CompId);
    //            using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //            {
    //                while (sdr.Read())
    //                {
    //                    IOWMsg IOWMas = new IOWMsg();
    //                    IOWMas.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
    //                    IOWMas.PackageCode = (sdr["PackageCode"].ToString().Trim());
    //                    IOWMas.PackageName = (sdr["PackageName"].ToString().Trim());
    //                    IOWMas.TradeCode = sdr["TradeCode"].ToString().Trim();
    //                    IOWMas.TradeName = sdr["TradeName"].ToString().Trim();
    //                    IOWMas.ElementCode = sdr["ElementCode"].ToString().Trim();
    //                    IOWMas.ElementName = sdr["ElementName"].ToString().Trim();

    //                    IOWMas.IOWCode = (sdr["IOWCode"].ToString().Trim());
    //                    IOWMas.IOWName = (sdr["IOWName"].ToString().Trim());
    //                    IOWMas.IOWShortName = sdr["IOWShortName"].ToString().Trim();
    //                    IOWMas.IOWUOM = sdr["IOWUOM"].ToString().Trim();
    //                    IOWMas.IOWQuantity = Convert.ToDouble(sdr["IOWQuantity"].ToString().Trim());
    //                    IOWMas.IsTemproryIOW = Convert.ToBoolean(sdr["IOWUOM"].ToString().Trim());
    //                    IOWMas.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
    //                    IOWMas.IsCommonFactorsApplicable = Convert.ToBoolean(sdr["IsCommonFactorsApplicable"].ToString().Trim());

    //                    Iowlst.Add(IOWMas);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
    //            return null;
    //        }
    //        finally
    //        {
    //            Connection.cmd.Dispose();
    //            Connection.cmd.Parameters.Clear();
    //        }
    //        return Iowlst;
    //    }
    //}
    public List<RateYearMsg> MasItemRateYearMonthSelect(Int32 CompanyId)
    {
        List<RateYearMsg> RYMList = new List<RateYearMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "MasItemRateYearMonthSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        RateYearMsg rym = new RateYearMsg();
                        rym.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());

                        rym.RateYearMonth = Convert.ToInt64(sdr["RateYearMonth"].ToString().Trim());


                        RYMList.Add(rym);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return RYMList;
        }
    }

    //public string CRAMIOWRateInsert(Int32 CompanyId,string IOWCode, Int64 YearMonth)
    //{
    //    string Result = ""; SqlTransaction transaction = null;
    //    using (Connection.con)
    //    {
    //        try
    //        {
    //            transaction = Connection.con.BeginTransaction();
    //            Connection.cmd.Transaction = transaction;
    //            Connection.cmd.Connection = Connection.con;

    //            Connection.cmd.CommandText = "CRAMIOWRateInsertSp";
    //            Connection.cmd.CommandType = CommandType.StoredProcedure;
    //            Connection.cmd.Connection = Connection.con;
    //            Connection.cmd.Parameters.Clear();
    //            Connection.cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
    //            Connection.cmd.Parameters.AddWithValue("@IOWCode", IOWCode);
    //            Connection.cmd.Parameters.AddWithValue("@YearMonth", YearMonth);
    //            Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
    //            Connection.cmd.ExecuteNonQuery();
    //            Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
    //            if (Result.Substring(0, 1) == "0")
    //            {
    //                transaction.Commit();
    //            }
    //            else
    //            {
    //                transaction.Rollback();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
    //            Result = "Try Catch Error.." + ex.ToString();
    //            return Result;
    //        }
    //        finally
    //        {
    //            Connection.cmd.Dispose();
    //            Connection.cmd.Parameters.Clear();
    //        }
    //        return Result;
    //    }
    //}
    //TenderIOWMappingCostSp
    public string TenderIOWMappingCost(Int32 CompanyId  ,Int64 ClientProjectId, Int64 ForYearMonth,string Region)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        //List<GroupMsg> pkList = new List<GroupMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;

                Connection.cmd.CommandText = "TenderIOWMappingCostSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;

                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                Connection.cmd.Parameters.AddWithValue("@ClientProjectId",ClientProjectId );
                Connection.cmd.Parameters.AddWithValue("@ForYearMonth", ForYearMonth);
                Connection.cmd.Parameters.AddWithValue("@Region", Region);
                // Connection.cmd.Parameters.AddWithValue("@IsActive", pkmsg.IsActive);
                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                Connection.cmd.ExecuteNonQuery();
                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());

                if (Result.Substring(0, 1) == "0")
                {
                    transaction.Commit();
                    //Connection.cmd.Parameters.Clear();
                    //Connection.con.Close();
                }
                else
                {
                    transaction.Rollback();
                }


            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
                //Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }

            return Result;
        }
    }

    public string TenderCRAMIOWMapInsert(List<CRAMIOWCodeMsg> IOWlst, string CreatedBy)
    {
        SqlTransaction transaction = null; Int32 FirstRecord = 1;
        string Result = "0"; //string WIOWCode = ""; Int32 WCompanyId = 0;
        //List<IOWItemMsg> IOWItLst = new List<IOWItemMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                foreach (CRAMIOWCodeMsg iowm in IOWlst)
                {
                    //WCompanyId = iowm.CompanyId;
                    //WIOWCode = iowm.IOWCode;
                    //if (iowm.Flag != "R")
                    //{
                    Connection.cmd.CommandText = "TenderCRAMIOWMapInsertsp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;

                        Connection.cmd.Parameters.Clear();
                        // Connection.cmd.Parameters.AddWithValue("@Flag", iowm.Flag);
                        Connection.cmd.Parameters.AddWithValue("@FirstRecord", FirstRecord);
                        Connection.cmd.Parameters.AddWithValue("@CompanyId", iowm.CompanyId);
                        Connection.cmd.Parameters.AddWithValue("@ClientProjectId", iowm.ClientProjectId);
                        Connection.cmd.Parameters.AddWithValue("@IOWCode", iowm.IOWCode);
                        Connection.cmd.Parameters.AddWithValue("@CRAMIOWHeadDtlId", iowm.CRAMIOWHeadDtlId);
                        Connection.cmd.Parameters.AddWithValue("@CRAMGroupCode", iowm.GroupCode);
                        Connection.cmd.Parameters.AddWithValue("@CRAMSubGroupCode", iowm.SubGroupCode);
                        Connection.cmd.Parameters.AddWithValue("@ClientProjectTenderId", iowm.TenderMapId);
                        Connection.cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                        Connection.cmd.ExecuteNonQuery();
                        Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                        if (Result.Substring(0, 1) != "0")
                        {
                           // transaction.Rollback();
                            break;
                        }
                        FirstRecord = FirstRecord + 1;
                    //}
                }
                if (Result.Substring(0, 1) != "0")
                {
                    transaction.Rollback();
                }
                else
                {
                    transaction.Commit();
                }  
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Result = Result+ " Try catch error in TenderCRAMIOWMapInsertsp ..";
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
                
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return Result;
        }
    }

    #endregion

    #region Master Insert,Update,Delete
    public List<ClientMsg> MasClientInsertUpdateandDelete(ClientMsg Cust)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ClientMsg> CustList = new List<ClientMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Cust.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasClientMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Cust.Flag);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", Cust.CompanyId); //SCS 210301 incl companyId

                    Connection.cmd.Parameters.AddWithValue("@ClientCode", Cust.ClientCode);
                    Connection.cmd.Parameters.AddWithValue("@ClientName", Cust.ClientName);
                    Connection.cmd.Parameters.AddWithValue("@ClientShortName", Cust.ClientShortName);
                    Connection.cmd.Parameters.AddWithValue("@Addr1", Cust.Addr1);
                    Connection.cmd.Parameters.AddWithValue("@Addr2", Cust.Addr2);
                    Connection.cmd.Parameters.AddWithValue("@BuildingName", Cust.BuildingName);
                    Connection.cmd.Parameters.AddWithValue("@City", Cust.City);
                    Connection.cmd.Parameters.AddWithValue("@PinCode", Cust.PinCode);
                    Connection.cmd.Parameters.AddWithValue("@ClientType", Cust.ClientType); //scs 230221
                    Connection.cmd.Parameters.AddWithValue("@StateId", Cust.StateId);
                    Connection.cmd.Parameters.AddWithValue("@StateName", Cust.StateName);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Cust.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Cust.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasClientMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", Cust.CompanyId); //scs 201110 not required in agiler lite
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ClientMsg custMsg = new ClientMsg();
                            custMsg.ClientResult = Result;
                            custMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString()); //SCS 210301

                            custMsg.ClientCode = sdr["ClientCode"].ToString().Trim();
                            custMsg.ClientName = sdr["ClientName"].ToString();
                            custMsg.ClientShortName = sdr["ClientShortName"].ToString();
                            custMsg.Addr1 = sdr["Addr1"].ToString();
                            custMsg.Addr2 = sdr["Addr2"].ToString();
                            custMsg.BuildingName = sdr["BuildingName"].ToString();
                            custMsg.City = sdr["City"].ToString();
                            custMsg.StateId = Convert.ToInt32(sdr["StateId"].ToString());
                            custMsg.StateName = sdr["StateName"].ToString();
                            custMsg.PinCode = sdr["PinCode"].ToString();
                            custMsg.ClientType = sdr["ClientType"].ToString();
                           
                            custMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString());
                            //custMsg.CreatedBy = sdr["CreatedBy"].ToString();
                            CustList.Add(custMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ClientMsg cMsg = new ClientMsg();
                    cMsg.ClientResult = Result;
                    CustList.Add(cMsg);
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ClientMsg cmp = new ClientMsg();
                cmp.CustomerResult = "Try CAtch Exception - MasClentInsertUpdateandDelete " + ex.ToString();
                CustList.Add(cmp);
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return CustList;
        }
    }
    public List<ProjectMsg> MasClientProjectMasterInsertandUpdate(ProjectMsg Cust)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ProjectMsg> ProjList = new List<ProjectMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Cust.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasClientProjectMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Cust.Flag);
                    Connection.cmd.Parameters.AddWithValue("@ClientProjectId", Cust.ClientProjectId); //SCS 210301 incl companyId
                    Connection.cmd.Parameters.AddWithValue("@ProjectSectorSubGroupId", Cust.ProjectSectorSubGroupId); //SCS 260330 
                    Connection.cmd.Parameters.AddWithValue("@ClientCode", Cust.ClientCode);
                    Connection.cmd.Parameters.AddWithValue("@ProjectCode", Cust.ProjectCode);
                    Connection.cmd.Parameters.AddWithValue("@ProjectName", Cust.ProjectName);
                    Connection.cmd.Parameters.AddWithValue("@ProjectLocation", Cust.ProjectLocation);
                    Connection.cmd.Parameters.AddWithValue("@ProjectCity", Cust.ProjectCity);
                    Connection.cmd.Parameters.AddWithValue("@StartDate", Cust.StartDate);
                    Connection.cmd.Parameters.AddWithValue("@EndDate", Cust.EndDate);
                    Connection.cmd.Parameters.AddWithValue("@TenderPeriod", Cust.TenderPeriod);
                    Connection.cmd.Parameters.AddWithValue("@DeviationMonths", Cust.DeviationMonths); //scs 230221
                    Connection.cmd.Parameters.AddWithValue("@StateId", Cust.StateId);
                    Connection.cmd.Parameters.AddWithValue("@ConstructionStart", Cust.ConstructionStart);
                    Connection.cmd.Parameters.AddWithValue("@ConstructionCompleted", Cust.ConstructionCompleted);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Cust.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Cust.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasClientProjectMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", Cust.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@ClientCode", Cust.ClientCode);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ProjectMsg ProjMsg = new ProjectMsg();
                            ProjMsg.ProjectResult = Result;
                            ProjMsg.ClientCode = sdr["ClientCode"].ToString().Trim();
                            ProjMsg.ClientName = sdr["ClientName"].ToString();
                            ProjMsg.ClientProjectId = Convert.ToInt32(sdr["ClientProjectId"].ToString());
                            ProjMsg.ProjectCode = sdr["ProjectCode"].ToString();
                            ProjMsg.ProjectName = sdr["ProjectName"].ToString();
                            ProjMsg.ConstructionCompleted = Convert.ToDateTime(sdr["ConstructionCompleted"].ToString());
                            ProjMsg.ConstructionStart = Convert.ToDateTime(sdr["ConstructionStart"].ToString());
                            ProjMsg.TenderPeriod = Convert.ToDateTime(sdr["TenderPeriod"].ToString());
                            ProjMsg.StartDate = Convert.ToDateTime(sdr["StartDate"].ToString());
                            ProjMsg.EndDate = Convert.ToDateTime(sdr["EndDate"].ToString());
                            ProjMsg.DeviationMonths = Convert.ToInt32(sdr["DeviationMonths"].ToString());
                            ProjMsg.ProjectLocation = sdr["ProjectLocation"].ToString();
                            ProjMsg.ProjectCity = sdr["ProjectCity"].ToString();
                            ProjMsg.StateId = Convert.ToInt32(sdr["StateId"].ToString());
                            ProjMsg.StateName = sdr["StateName"].ToString();

                            ProjMsg.ProjectSectorSubGroupId = Convert.ToInt32(sdr["ProjectSectorSubGroupId"].ToString());
                            ProjMsg.ProjectSectorGroupName = sdr["ProjectSectorGroupName"].ToString();
                            ProjMsg.ProjectSectorClass = sdr["ProjectSectorClass"].ToString();

                            ProjMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString());
                            //custMsg.CreatedBy = sdr["CreatedBy"].ToString();
                            ProjList.Add(ProjMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ProjectMsg cMsg = new ProjectMsg();
                    cMsg.ProjectResult = Result;
                    ProjList.Add(cMsg);
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ProjectMsg cmp = new ProjectMsg();
                cmp.ProjectResult = "Try CAtch Exception - MasClentInsertUpdateandDelete " + ex.ToString();
                ProjList.Add(cmp);
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return ProjList;
        }
    }
    public List<CompanyMessage> MasCompanyInsertUpdateandDelete(CompanyMessage Company, EmployeeMasterMsg EmpMsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<CompanyMessage> CompanyList = new List<CompanyMessage>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Company.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasCompanyMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Company.Flag);
                    Connection.cmd.Parameters.AddWithValue("@EnterpriseId", Company.EnterpriseId);
                    Connection.cmd.Parameters.AddWithValue("@CompanyCode", Company.CompanyCode);
                    Connection.cmd.Parameters.AddWithValue("@CompanyName", Company.CompanyName);
                    Connection.cmd.Parameters.AddWithValue("@CompanyShortName", Company.CompanyShortName);
                    Connection.cmd.Parameters.AddWithValue("@CompanyFlag", Company.CompanyFlag);
                    Connection.cmd.Parameters.AddWithValue("@ParentCompanyCode", Company.ParentCompanyCode);
                    Connection.cmd.Parameters.AddWithValue("@StateId", Company.StateID);
                    //Connection.cmd.Parameters.AddWithValue("@LocationId", Company.LocationID);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Company.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Company.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasCompanyMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", EmpMsg.EmployeeCode); //scs 201110 not required in agiler lite
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            CompanyMessage CompanyMessage = new CompanyMessage();
                            CompanyMessage.CompanyResult = Result;
                            CompanyMessage.EnterpriseId = Convert.ToInt32(sdr["EnterpriseId"].ToString());
                            CompanyMessage.EnterpriseName = sdr["EnterpriseName"].ToString().Trim();
                            CompanyMessage.CompanyCode = sdr["CompanyCode"].ToString();
                            CompanyMessage.CompanyName = sdr["CompanyName"].ToString();
                            CompanyMessage.CompanyShortName = sdr["CompanyShortName"].ToString();
                            CompanyMessage.CompanyFlag = sdr["CompanyFlag"].ToString();
                            CompanyMessage.ParentCompanyCode = sdr["ParentCompanyCode"].ToString();
                            CompanyMessage.ParentCompanyName = sdr["ParentCompanyName"].ToString();
                            CompanyMessage.StateID = Convert.ToInt32(sdr["StateID"].ToString());
                            CompanyMessage.StateName = sdr["StateName"].ToString();
                            CompanyMessage.StateShortName = sdr["StateShortName"].ToString();
                            //CompanyMessage.LocationID = Convert.ToInt32(sdr["LocationID"].ToString());
                            //CompanyMessage.LocationName = sdr["LocationName"].ToString();
                            CompanyMessage.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString());
                            CompanyMessage.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString());
                            CompanyMessage.CreatedBy = sdr["CreatedBy"].ToString();
                            CompanyList.Add(CompanyMessage);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    CompanyMessage CompanyMessage = new CompanyMessage();
                    CompanyMessage.CompanyResult = Result;
                    CompanyList.Add(CompanyMessage);
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return CompanyList;
        }
    }
    public List<LoginUserMsg> MasLoginUserInsertUpdate(LoginUserMsg Login)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<LoginUserMsg> EmpList = new List<LoginUserMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Login.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasLoginUserInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Login.Flag);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", Login.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@DepartmentId", Login.DepartmentId);
                    Connection.cmd.Parameters.AddWithValue("@LoginUserId", Login.LoginUserId);
                    Connection.cmd.Parameters.AddWithValue("@UserName", Login.UserName);
                    Connection.cmd.Parameters.AddWithValue("@Password", Login.Password);
                    Connection.cmd.Parameters.AddWithValue("@IsAdmin", Login.IsAdmin);
                    Connection.cmd.Parameters.AddWithValue("@IsSuperUser", Login.IsSuperUser);
                    Connection.cmd.Parameters.AddWithValue("@EmailId", Login.EmailId);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Login.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Login.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasLoginUserSelectSp";
                    Connection.cmd.Parameters.Clear();
                   // Connection.cmd.Parameters.AddWithValue("@LoginEmployeeCode", Login.LoginEmployeeCode);
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            LoginUserMsg LMsg = new LoginUserMsg();
                            LMsg.LoginResult = Result;
                            LMsg.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                            LMsg.CompanyName = sdr["CompanyName"].ToString().Trim();
                            LMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                            LMsg.DepartmentId = Convert.ToInt32(sdr["DepartmentId"].ToString().Trim());
                            LMsg.DepartmentName = sdr["DepartmentName"].ToString().Trim();
                            LMsg.DepartmentCode = sdr["DepartmentCode"].ToString().Trim();
                            LMsg.DepartmentId = Convert.ToInt32(sdr["DepartmentId"].ToString().Trim());
                            LMsg.LoginUserId = Convert.ToInt32(sdr["LoginUserId"].ToString().Trim());
                            LMsg.UserName = sdr["UserName"].ToString().Trim();
                            LMsg.EmailId = sdr["EmailId"].ToString().Trim();
                            //LMsg.Password = sdr["PassWord"].ToString().Trim();
                            LMsg.IsAdmin = Convert.ToBoolean(sdr["IsAdmin"].ToString().Trim());
                            LMsg.IsSuperUser = Convert.ToBoolean(sdr["IsSuperUser"].ToString().Trim());
                            LMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            //LMsg.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            //LMsg.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                            //LMsg.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                            //LMsg.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                            EmpList.Add(LMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    LoginUserMsg Lmsg = new LoginUserMsg();
                    Lmsg.LoginResult = Result;
                    EmpList.Add(Lmsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
                    transaction.Dispose();
            }

            return EmpList;
        }
    }
    public List<EmployeeMasterMsg> MasEmployeeInsertUpdateandDelete(EmployeeMasterMsg Employee)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Employee.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasEmployeeMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Employee.Flag);
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Employee.EmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@EmployeeName", Employee.EmployeeName);
                    Connection.cmd.Parameters.AddWithValue("@EmailId", Employee.EmailId);
                    Connection.cmd.Parameters.AddWithValue("@PassWord", Employee.Password);
                    Connection.cmd.Parameters.AddWithValue("@CompanyCode", Employee.CompanyCode);
                    Connection.cmd.Parameters.AddWithValue("@ManagerCode", Employee.ManagerCode);
                    Connection.cmd.Parameters.AddWithValue("@ActivityDesignation", Employee.EmployeeDesignation);
                    Connection.cmd.Parameters.AddWithValue("@IsAuditor", Employee.IsAuditor);
                    Connection.cmd.Parameters.AddWithValue("@IsCompanyAdmin", Employee.IsCompanyAdmin);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Employee.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@RoleId", Employee.RoleId);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Employee.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasEmployeeMasterSelectSp";
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@LoginEmployeeCode", Employee.LoginEmployeeCode);
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            EmployeeMasterMsg EmpMessage = new EmployeeMasterMsg();
                            EmpMessage.EmployeeResult = Result;
                            EmpMessage.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                            EmpMessage.CompanyName = sdr["CompanyName"].ToString().Trim();
                            EmpMessage.EmployeeCode = sdr["EmployeeCode"].ToString().Trim();
                            EmpMessage.EmployeeName = sdr["EmployeeName"].ToString().Trim();
                            EmpMessage.EmailId = sdr["EmailId"].ToString().Trim();
                            EmpMessage.Password = sdr["PassWord"].ToString().Trim();
                            EmpMessage.ManagerCode = sdr["ManagerCode"].ToString().Trim();
                            EmpMessage.ManagerName = sdr["ManagerName"].ToString().Trim();
                           // EmpMessage.IsCompanyAdmin = Convert.ToBoolean(sdr["IsCompanyAdmin"].ToString().Trim());
                            EmpMessage.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            //EmpMessage.IsAuditor = Convert.ToBoolean(sdr["IsAuditor"].ToString().Trim());
                           // EmpMessage.EmployeeDesignation = sdr["ActivityDesignation"].ToString().Trim();
                            EmpMessage.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            EmpMessage.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                            EmpMessage.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                            EmpMessage.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                           // EmpMessage.DepartmentId = Convert.ToInt32(sdr["DepartmentId"].ToString().Trim());
                            EmpList.Add(EmpMessage);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    EmployeeMasterMsg EmpMessage = new EmployeeMasterMsg();
                    EmpMessage.EmployeeResult = Result;
                    EmpList.Add(EmpMessage);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return EmpList;
        }
    }
    public List<StateMasterMsg> MasStateInsertUpdateandDelete(StateMasterMsg State)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<StateMasterMsg> StateList = new List<StateMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (State.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasStateMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", State.Flag);
                    Connection.cmd.Parameters.AddWithValue("@StateId", State.StateId);
                    Connection.cmd.Parameters.AddWithValue("@StateName", State.StateName);
                    Connection.cmd.Parameters.AddWithValue("@StateShortName", State.StateShortName);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasStateMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            StateMasterMsg StateMsg = new StateMasterMsg();
                            StateMsg.StateResult = Result;
                            StateMsg.StateId = Convert.ToInt32(sdr["StateId"].ToString().Trim());
                            StateMsg.StateName = sdr["StateName"].ToString().Trim();
                            StateMsg.StateShortName = sdr["StateShortName"].ToString().Trim();
                            StateList.Add(StateMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    StateMasterMsg StateMsg = new StateMasterMsg();
                    StateMsg.StateResult = Result;
                    StateList.Add(StateMsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return StateList;
        }
    }
    public List<SectorMsg> MasProjectSectorGroupInsertandUpdate(SectorMsg StMsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<SectorMsg> SecList = new List<SectorMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (StMsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasSectorGroupInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", StMsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@ProjectSectorGroupId", StMsg.ProjectSectorGroupId);
                    Connection.cmd.Parameters.AddWithValue("@ProjectSectorGroupName", StMsg.ProjectSectorGroupName);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasSectorGroupSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            SectorMsg SecMsg = new SectorMsg();
                            SecMsg.Result = Result;
                            SecMsg.ProjectSectorGroupId = Convert.ToInt32(sdr["ProjectSectorGroupId"].ToString().Trim());
                            SecMsg.ProjectSectorGroupName = sdr["ProjectSectorGroupName"].ToString().Trim();
                            SecMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            SecList.Add(SecMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    SectorMsg SeMsg = new SectorMsg();
                    SeMsg.Result = Result;
                    SecList.Add(SeMsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return SecList;
        }
    }
    public List<SectorMsg> MasProjectSectorClassInsertandUpdate(SectorMsg StMsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<SectorMsg> SecList = new List<SectorMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (StMsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasSectorClassInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", StMsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@ProjectSectorGroupId", StMsg.ProjectSectorGroupId);
                    Connection.cmd.Parameters.AddWithValue("@ProjectSectorGroupName", StMsg.ProjectSectorGroupName);
                    Connection.cmd.Parameters.AddWithValue("@ProjectSectorSubGroupId", StMsg.ProjectSectorSubGroupId);
                    Connection.cmd.Parameters.AddWithValue("@ProjectSectorClass", StMsg.ProjectSectorClass);
                     Connection.cmd.Parameters.AddWithValue("@IsActive", StMsg.IsActive);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasSectorClassSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            SectorMsg SecMsg = new SectorMsg();
                            SecMsg.Result = Result;
                            SecMsg.ProjectSectorGroupId = Convert.ToInt32(sdr["ProjectSectorGroupId"].ToString().Trim());
                            SecMsg.ProjectSectorGroupName = sdr["ProjectSectorGroupName"].ToString().Trim();
                             SecMsg.ProjectSectorSubGroupId = Convert.ToInt32(sdr["ProjectSectorSubGroupId"].ToString().Trim());
                            SecMsg.ProjectSectorClass = sdr["ProjectSectorClass"].ToString().Trim();
                            SecMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            SecList.Add(SecMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    SectorMsg SeMsg = new SectorMsg();
                    SeMsg.Result = Result;
                    SecList.Add(SeMsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return SecList;
        }
    }


    //public List<PackageMasterMsg> MasCRAMPackageInsertandUpdate(PackageMasterMsg pkmsg)
    //{
    //    SqlTransaction transaction = null;
    //    string Result = "0";
    //    List<PackageMasterMsg> pkList = new List<PackageMasterMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {

    //            transaction = Connection.con.BeginTransaction();
    //            Connection.cmd.Transaction = transaction;
    //            Connection.cmd.Connection = Connection.con;
    //            if (pkmsg.Flag != "R")
    //            {
    //                Connection.cmd.CommandText = "MasCRAMPackageInsertandUpdateSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;

    //                Connection.cmd.Parameters.Clear();
    //                Connection.cmd.Parameters.AddWithValue("@Flag", pkmsg.Flag);
    //                Connection.cmd.Parameters.AddWithValue("@PackageCode", pkmsg.PackageCode);
    //                Connection.cmd.Parameters.AddWithValue("@PackageName", pkmsg.PackageName);
                    
    //                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
    //                Connection.cmd.ExecuteNonQuery();
    //                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
    //            }
    //            if (Result == "0")
    //            {
    //                Connection.cmd.CommandText = "MasCRAMPackageSelectSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;
    //                Connection.cmd.Parameters.Clear();
    //                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //                {
    //                    while (sdr.Read())
    //                    {

    //                        PackageMasterMsg pkMsg = new PackageMasterMsg();
    //                        pkMsg.PackageResult = Result;
    //                        pkMsg.PackageCode = (sdr["PackageCode"].ToString().Trim());
    //                        pkMsg.PackageName = sdr["PackageName"].ToString().Trim();

    //                        pkList.Add(pkMsg);
    //                    }

    //                }
    //                transaction.Commit();
    //                Connection.cmd.Parameters.Clear();
    //                Connection.con.Close();
    //            }
    //            else
    //            {
    //                PackageMasterMsg Pkmsg = new PackageMasterMsg();
    //                Pkmsg.PackageResult = Result;
    //                pkList.Add(Pkmsg);

    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            if (transaction != null)
    //            {
    //                transaction.Rollback();
    //            }
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

    //        }
    //        finally
    //        {
    //            Connection.cmd.Parameters.Clear();
    //        }

    //        return pkList;
    //    }
    //}
    //public List<TradeMasterMsg> MasCRAMTradeInsertUpdate(TradeMasterMsg pkmsg)
    //{
    //    SqlTransaction transaction = null;
    //    string Result = "0";
    //    List<TradeMasterMsg> pkList = new List<TradeMasterMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {

    //            transaction = Connection.con.BeginTransaction();
    //            Connection.cmd.Transaction = transaction;
    //            Connection.cmd.Connection = Connection.con;
    //            if (pkmsg.Flag != "R")
    //            {
    //                Connection.cmd.CommandText = "MasCRAMTradeInsertandUpdateSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;

    //                Connection.cmd.Parameters.Clear();
    //                Connection.cmd.Parameters.AddWithValue("@Flag", pkmsg.Flag);
    //                Connection.cmd.Parameters.AddWithValue("@TradeCode", pkmsg.TradeCode);
    //                Connection.cmd.Parameters.AddWithValue("@TradeName", pkmsg.TradeName);

    //                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
    //                Connection.cmd.ExecuteNonQuery();
    //                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
    //            }
    //            if (Result == "0")
    //            {
    //                Connection.cmd.CommandText = "MasCRAMTradeSelectSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;
    //                Connection.cmd.Parameters.Clear();
    //                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //                {
    //                    while (sdr.Read())
    //                    {

    //                        TradeMasterMsg pkMsg = new TradeMasterMsg();
    //                        pkMsg.TradeResult = Result;
    //                        pkMsg.TradeCode = (sdr["TradeCode"].ToString().Trim());
    //                        pkMsg.TradeName = sdr["TradeName"].ToString().Trim();

    //                        pkList.Add(pkMsg);
    //                    }

    //                }
    //                transaction.Commit();
    //                Connection.cmd.Parameters.Clear();
    //                Connection.con.Close();
    //            }
    //            else
    //            {
    //                TradeMasterMsg Pkmsg = new TradeMasterMsg();
    //                Pkmsg.TradeResult = Result;
    //                pkList.Add(Pkmsg);

    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            if (transaction != null)
    //            {
    //                transaction.Rollback();
    //            }
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

    //        }
    //        finally
    //        {
    //            Connection.cmd.Parameters.Clear();
    //        }

    //        return pkList;
    //    }
    //}
    //public List<ElementMasterMsg> MasCRAMElementInsertUpdate(ElementMasterMsg pkmsg)
    //{
    //    SqlTransaction transaction = null;
    //    string Result = "0";
    //    List<ElementMasterMsg> pkList = new List<ElementMasterMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {

    //            transaction = Connection.con.BeginTransaction();
    //            Connection.cmd.Transaction = transaction;
    //            Connection.cmd.Connection = Connection.con;
    //            if (pkmsg.Flag != "R")
    //            {
    //                Connection.cmd.CommandText = "MasCRAMElementInsertandUpdateSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;

    //                Connection.cmd.Parameters.Clear();
    //                Connection.cmd.Parameters.AddWithValue("@Flag", pkmsg.Flag);
    //                Connection.cmd.Parameters.AddWithValue("@ElementCode", pkmsg.ElementCode);
    //                Connection.cmd.Parameters.AddWithValue("@ElementName", pkmsg.ElementName);

    //                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
    //                Connection.cmd.ExecuteNonQuery();
    //                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
    //            }
    //            if (Result == "0")
    //            {
    //                Connection.cmd.CommandText = "MasCRAMElementSelectSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;
    //                Connection.cmd.Parameters.Clear();
    //                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //                {
    //                    while (sdr.Read())
    //                    {

    //                        ElementMasterMsg pkMsg = new ElementMasterMsg();
    //                        pkMsg.ElementResult = Result;
    //                        pkMsg.ElementCode = (sdr["ElementCode"].ToString().Trim());
    //                        pkMsg.ElementName = sdr["ElementName"].ToString().Trim();

    //                        pkList.Add(pkMsg);
    //                    }

    //                }
    //                transaction.Commit();
    //                Connection.cmd.Parameters.Clear();
    //                Connection.con.Close();
    //            }
    //            else
    //            {
    //                ElementMasterMsg Pkmsg = new ElementMasterMsg();
    //                Pkmsg.ElementResult = Result;
    //                pkList.Add(Pkmsg);

    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            if (transaction != null)
    //            {
    //                transaction.Rollback();
    //            }
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

    //        }
    //        finally
    //        {
    //            Connection.cmd.Parameters.Clear();
    //        }

    //        return pkList;
    //    }
    //}

    public List<CRAMCommonFactorMsg> MasIOWCommonFactorInsertUpdate(CRAMCommonFactorMsg cfmsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<CRAMCommonFactorMsg> cfList = new List<CRAMCommonFactorMsg>();
        CRAMCommonFactorMsg cfg = new CRAMCommonFactorMsg();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (cfmsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasCRAMCommonFactorInsertUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", cfmsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", cfmsg.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@IOWCommonFactorId", cfmsg.IOWCommonFactorId);
                    Connection.cmd.Parameters.AddWithValue("@IOWCommonFactor", cfmsg.IOWCommonFactor);
                    Connection.cmd.Parameters.AddWithValue("@SequenceNumber", cfmsg.SequenceNumber);
                   
                    Connection.cmd.Parameters.AddWithValue("@FactorPercentage", cfmsg.FactorPercentage);
                    Connection.cmd.Parameters.AddWithValue("@EffectivePercentage", cfmsg.EffectivePercentage);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", cfmsg.CreatedBy);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", cfmsg.IsActive);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasCRAMCommonFactorSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", cfmsg.CompanyId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            CRAMCommonFactorMsg cfMsg = new CRAMCommonFactorMsg();
                            cfMsg.Result = Result;
                            cfMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                            cfMsg.IOWCommonFactorId = Convert.ToInt32(sdr["IOWCommonFactorId"].ToString().Trim());
                            cfMsg.IOWCommonFactor = sdr["IOWCommonFactor"].ToString().Trim();
                            cfMsg.SequenceNumber = Convert.ToInt32(sdr["SequenceNumber"].ToString().Trim());
                            cfMsg.SequenceGroup = Convert.ToInt32(sdr["SequenceGroup"].ToString().Trim());
                            cfMsg.FactorPercentage = Convert.ToDouble(sdr["FactorPercentage"].ToString().Trim());
                            cfMsg.EffectivePercentage = Convert.ToDouble(sdr["EffectivePercentage"].ToString().Trim());
                            cfMsg.CreatedBy = (sdr["CreatedBy"].ToString().Trim());
                            cfMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            cfList.Add(cfMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                   
                    cfg.Result = Result;
                    cfList.Add(cfg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                cfg.Result = ex.ToString();
                cfList.Add(cfg);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return cfList;
        }
    }
    public List<ProjectInputCRAMCommonFactorMsg> ProjectInputIOWCommonFactorInsertUpdate(ProjectInputCRAMCommonFactorMsg cfmsg)
    {
        SqlTransaction transaction = null;
        string Result = "0"; //Int32 FirstRecord = 1;
        List<ProjectInputCRAMCommonFactorMsg> cfList = new List<ProjectInputCRAMCommonFactorMsg>();
        ProjectInputCRAMCommonFactorMsg cfg = new ProjectInputCRAMCommonFactorMsg();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (cfmsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "ProjectInputIOWCommonFactorInsertUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", cfmsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", cfmsg.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@ClientCode", cfmsg.ClientCode);
                    Connection.cmd.Parameters.AddWithValue("@ProjectCode", cfmsg.ProjectCode);
                    Connection.cmd.Parameters.AddWithValue("@ForYearMonth", cfmsg.ForYearMonth);
                   // Connection.cmd.Parameters.AddWithValue("@FirstRecord", FirstRecord);
                    Connection.cmd.Parameters.AddWithValue("@IOWCommonFactorId", cfmsg.IOWCommonFactorId);
                    Connection.cmd.Parameters.AddWithValue("@IOWCommonFactor", cfmsg.IOWCommonFactor);
                    Connection.cmd.Parameters.AddWithValue("@SequenceNumber", cfmsg.SequenceNumber);

                    Connection.cmd.Parameters.AddWithValue("@FactorPercentage", cfmsg.FactorPercentage);
                    Connection.cmd.Parameters.AddWithValue("@EffectivePercentage", cfmsg.EffectivePercentage);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", cfmsg.CreatedBy);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", cfmsg.IsActive);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                    //FirstRecord = FirstRecord + 1;
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "ProjectInputIOWCommonFactorSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", cfmsg.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@ClientCode", cfmsg.ClientCode);
                    Connection.cmd.Parameters.AddWithValue("@ProjectCode", cfmsg.ProjectCode);
                    Connection.cmd.Parameters.AddWithValue("@ForYearMonth", cfmsg.ForYearMonth);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ProjectInputCRAMCommonFactorMsg cfMsg = new ProjectInputCRAMCommonFactorMsg();
                            cfMsg.Result = Result;
                            cfMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                            cfMsg.ClientCode = (sdr["ClientCode"].ToString().Trim());
                            cfMsg.ProjectCode = (sdr["ProjectCode"].ToString().Trim());
                            cfMsg.ForYearMonth = Convert.ToInt64(sdr["ForYearMonth"].ToString().Trim());
                            cfMsg.IOWCommonFactorId = Convert.ToInt32(sdr["IOWCommonFactorId"].ToString().Trim());
                            cfMsg.IOWCommonFactor = sdr["IOWCommonFactor"].ToString().Trim();
                            cfMsg.SequenceNumber = Convert.ToInt32(sdr["SequenceNumber"].ToString().Trim());
                            cfMsg.SequenceGroup = Convert.ToInt32(sdr["SequenceGroup"].ToString().Trim());
                            cfMsg.FactorPercentage = Convert.ToDouble(sdr["FactorPercentage"].ToString().Trim());
                            cfMsg.EffectivePercentage = Convert.ToDouble(sdr["EffectivePercentage"].ToString().Trim());
                            cfMsg.CreatedBy = (sdr["CreatedBy"].ToString().Trim());
                            cfMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            cfList.Add(cfMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {

                    cfg.Result = Result;
                    cfList.Add(cfg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                cfg.Result = ex.ToString();
                cfList.Add(cfg);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return cfList;
        }
    }


    public List<CRAMIOWHeadMsg> MasCRAMIOWHeadInsertUpdate(CRAMIOWHeadMsg iowm)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<CRAMIOWHeadMsg> IOWLst = new List<CRAMIOWHeadMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (iowm.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasCRAMIOWHeadInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", iowm.Flag);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", iowm.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@GroupCode", iowm.GroupCode);
                    Connection.cmd.Parameters.AddWithValue("@SubGroupCode", iowm.SubGroupCode);
                    Connection.cmd.Parameters.AddWithValue("@IOWHeadCode", iowm.IOWHeadCode);
                    Connection.cmd.Parameters.AddWithValue("@IOWHeadName", iowm.IOWHeadName);
                    // Connection.cmd.Parameters.AddWithValue("@IsActive", iowm.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", iowm.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasCRAMIOWHeadSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", iowm.CompanyId);

                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CRAMIOWHeadMsg IOWMas = new CRAMIOWHeadMsg();
                            IOWMas.Result = Result;
                            IOWMas.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                            IOWMas.GroupCode = (sdr["GroupCode"].ToString().Trim());
                            IOWMas.GroupName = (sdr["GroupName"].ToString().Trim());
                            IOWMas.SubGroupCode = sdr["SubGroupCode"].ToString().Trim();
                            IOWMas.SubGroupName = sdr["SubGroupName"].ToString().Trim();
                            IOWMas.IOWHeadCode = (sdr["IOWHeadCode"].ToString().Trim());
                            IOWMas.IOWHeadName = (sdr["IOWHeadName"].ToString().Trim());
                            IOWMas.IOWLevel1 = sdr["IOWLevel1"].ToString().Trim();
                            IOWMas.IOWLevel2 = sdr["IOWLevel2"].ToString().Trim();
                            IOWMas.IOWLevel3 = (sdr["IOWLevel3"].ToString().Trim());
                            IOWMas.IOWLevel4 = (sdr["IOWLevel4"].ToString().Trim());
                            IOWLst.Add(IOWMas);

                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    CRAMIOWHeadMsg IOW = new CRAMIOWHeadMsg();
                    IOW.Result = Result;
                    IOWLst.Add(IOW);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                CRAMIOWHeadMsg IOW = new CRAMIOWHeadMsg();
                IOW.Result = "Try Catch MasIOWHeadInsertUpdate" + ex.ToString().Substring(1, 100);
                IOWLst.Add(IOW);
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return IOWLst;
        }
    }
    public List<CRAMIOWDtlMsg> MasCRAMIOWDtlInsertUpdate(CRAMIOWDtlMsg iowm)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<CRAMIOWDtlMsg> IOWLst = new List<CRAMIOWDtlMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "MasCRAMIOWDtlInsertUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    if (iowm.Flag != "R")
                    {
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.Parameters.AddWithValue("@Flag", iowm.Flag);
                        Connection.cmd.Parameters.AddWithValue("@CompanyId", iowm.CompanyId);
                        Connection.cmd.Parameters.AddWithValue("@PreviousIOWLevel", iowm.PreviousIOWLevel);
                        Connection.cmd.Parameters.AddWithValue("@IOWCode", iowm.IOWCode);
                        Connection.cmd.Parameters.AddWithValue("@IOWDescription", iowm.IOWDescription);
                        Connection.cmd.Parameters.AddWithValue("@IOWQuantity", iowm.IOWQuantity);
                        Connection.cmd.Parameters.AddWithValue("@IOWUOM", iowm.IOWUOM);
                        Connection.cmd.Parameters.AddWithValue("@IsTemproryIOW", iowm.IsTemproryIOW);
                        Connection.cmd.Parameters.AddWithValue("@CreatedBy", iowm.CreatedBy);
                        Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                        Connection.cmd.ExecuteNonQuery();
                        Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                    }
                    if (Result == "0")
                    {
                        Connection.cmd.CommandText = "MasCRAMIOWHeadDtlSelectSp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.Parameters.AddWithValue("@CompanyId", iowm.CompanyId);
                        Connection.cmd.Parameters.AddWithValue("@PreviousIOWLevel", iowm.PreviousIOWLevel);

                        using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                CRAMIOWDtlMsg IOWMas = new CRAMIOWDtlMsg();
                                IOWMas.Result = Result;
                                IOWMas.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                                IOWMas.CRAMIOWHeadDtlId = Convert.ToInt64(sdr["CRAMIOWHeadDtlId"].ToString().Trim());
                                IOWMas.PreviousIOWLevel = (sdr["PreviousIOWLevel"].ToString().Trim());
                                IOWMas.IOWCode = sdr["IOWCode"].ToString().Trim();
                                IOWMas.IOWDescription = sdr["IOWDescription"].ToString().Trim();
                                IOWMas.IOWUOM = (sdr["IOWUOM"].ToString().Trim());
                                IOWMas.IOWQuantity = Convert.ToDouble(sdr["IOWQuantity"].ToString().Trim());
                                IOWMas.IsTemproryIOW =Convert.ToBoolean(sdr["IsTemproryIOW"].ToString().Trim());
                                IOWLst.Add(IOWMas);

                            }

                        }
                        transaction.Commit();
                        Connection.cmd.Parameters.Clear();
                        Connection.con.Close();
                    }
               else
               {
                   CRAMIOWDtlMsg IOWMas = new CRAMIOWDtlMsg();
                   IOWMas.Result = Result;
                   IOWLst.Add(IOWMas);
                   transaction.Rollback();
               }
                
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                Result = "Try Catch MasCRAMIOWDtlInsertUpdateSp" + ex.ToString().Substring(1, 100);
               
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return IOWLst;
        }
    }
    //public List<CRAMIOWDtlMsg> MasCRAMIOWDtlInsertUpdate(CRAMIOWDtlMsg iowm)
    //{
    //    SqlTransaction transaction = null;
    //    string Result = "0";
    //    List<CRAMIOWDtlMsg> IOWLst = new List<CRAMIOWDtlMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {

    //            transaction = Connection.con.BeginTransaction();
    //            Connection.cmd.Transaction = transaction;
    //            Connection.cmd.Connection = Connection.con;
    //            Connection.cmd.CommandText = "MasCRAMIOWDtlInsertUpdateSp";
    //            Connection.cmd.CommandType = CommandType.StoredProcedure;
    //            if (iowm.Flag != "R")
    //            {
    //                Connection.cmd.Parameters.Clear();
    //                Connection.cmd.Parameters.AddWithValue("@Flag", iowm.Flag);
    //                Connection.cmd.Parameters.AddWithValue("@CompanyId", iowm.CompanyId);
    //                Connection.cmd.Parameters.AddWithValue("@PreviousIOWLevel", iowm.PreviousIOWLevel);
    //                Connection.cmd.Parameters.AddWithValue("@IOWCode", iowm.IOWCode);
    //                Connection.cmd.Parameters.AddWithValue("@IOWDescription", iowm.IOWDescription);
    //                Connection.cmd.Parameters.AddWithValue("@IOWQuantity", iowm.IOWQuantity);
    //                Connection.cmd.Parameters.AddWithValue("@IOWUOM", iowm.IOWUOM);
    //                Connection.cmd.Parameters.AddWithValue("@IsTemproryIOW", iowm.IsTemproryIOW);
    //                Connection.cmd.Parameters.AddWithValue("@CreatedBy", iowm.CreatedBy);
    //                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
    //                Connection.cmd.ExecuteNonQuery();
    //                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
    //            }
    //            if (Result == "0")
    //            {
    //                Connection.cmd.CommandText = "MasCRAMIOWHeadDtlSelectSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;
    //                Connection.cmd.Parameters.Clear();
    //                Connection.cmd.Parameters.AddWithValue("@CompanyId", iowm.CompanyId);
    //                Connection.cmd.Parameters.AddWithValue("@PreviousIOWLevel", iowm.PreviousIOWLevel);

    //                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //                {
    //                    while (sdr.Read())
    //                    {
    //                        CRAMIOWDtlMsg IOWMas = new CRAMIOWDtlMsg();
    //                        IOWMas.Result = Result;
    //                        IOWMas.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
    //                        IOWMas.CRAMIOWHeadDtlId = Convert.ToInt64(sdr["CRAMIOWHeadDtlId"].ToString().Trim());
    //                        IOWMas.PreviousIOWLevel = (sdr["PreviousIOWLevel"].ToString().Trim());
    //                        IOWMas.IOWCode = sdr["IOWCode"].ToString().Trim();
    //                        IOWMas.IOWDescription = sdr["IOWDescription"].ToString().Trim();
    //                        IOWMas.IOWUOM = (sdr["IOWUOM"].ToString().Trim());
    //                        IOWMas.IOWQuantity = Convert.ToDouble(sdr["IOWQuantity"].ToString().Trim());
    //                        IOWMas.IsTemproryIOW = Convert.ToBoolean(sdr["IsTemproryIOW"].ToString().Trim());
    //                        IOWLst.Add(IOWMas);

    //                    }

    //                }
    //                transaction.Commit();
    //                Connection.cmd.Parameters.Clear();
    //                Connection.con.Close();
    //            }
    //            else
    //            {
    //                CRAMIOWDtlMsg IOWMas = new CRAMIOWDtlMsg();
    //                IOWMas.Result = Result;
    //                IOWLst.Add(IOWMas);
    //                transaction.Rollback();
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            if (transaction != null)
    //            {
    //                transaction.Rollback();
    //            }

    //            Result = "Try Catch MasCRAMIOWDtlInsertUpdateSp" + ex.ToString().Substring(1, 100);

    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

    //        }
    //        finally
    //        {
    //            Connection.cmd.Parameters.Clear();
    //        }

    //        return IOWLst;
    //    }
    //}
    public List<IOWItemMsg> MasCRAMIOWItemInsertUpdate(List<IOWItemMsg> iowmit, string CreatedBy)
    {
        SqlTransaction transaction = null; Int32 FirstRecord = 1;
        string Result = "0"; string WIOWCode = ""; Int32 WCompanyId = 0;
        List<IOWItemMsg> IOWItLst = new List<IOWItemMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                foreach (IOWItemMsg iowm in iowmit)
                {
                    WCompanyId = iowm.CompanyId;
                    WIOWCode = iowm.IOWCode;
                    if (iowm.Flag != "R")
                    {
                        Connection.cmd.CommandText = "MasCRAMIOWItemInsertandUpdateSp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;

                        Connection.cmd.Parameters.Clear();
                        // Connection.cmd.Parameters.AddWithValue("@Flag", iowm.Flag);
                        Connection.cmd.Parameters.AddWithValue("@FirstRecord", FirstRecord);
                        Connection.cmd.Parameters.AddWithValue("@CompanyId", iowm.CompanyId);
                        //Connection.cmd.Parameters.AddWithValue("@IOWCode", iowm.IOWCode); // done prior to introduction of context hence commented
                        Connection.cmd.Parameters.AddWithValue("@IOWCode", iowm.CRAMIOWCode); //introduction of context and item
                        Connection.cmd.Parameters.AddWithValue("@Context", iowm.Context); //introduction of context and item
                        Connection.cmd.Parameters.AddWithValue("@ContextSrlNo", iowm.ContextSrlNo); //introduction of context and item
                        Connection.cmd.Parameters.AddWithValue("@IOWHeadDtlId", iowm.IOWHeadDtlId); 
                        Connection.cmd.Parameters.AddWithValue("@ItemId", iowm.ItemId);
                        Connection.cmd.Parameters.AddWithValue("@ItemUOM", iowm.ItemUOM);
                        Connection.cmd.Parameters.AddWithValue("@ItemQuantity", iowm.ItemQuantity);
                        Connection.cmd.Parameters.AddWithValue("@Wastage", iowm.Wastage);
                        Connection.cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                        Connection.cmd.ExecuteNonQuery();
                        Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                        if (Result.Substring(0, 1) != "0")
                        {
                            transaction.Rollback();
                            break;
                        }
                        FirstRecord = FirstRecord + 1;
                    }
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasCRAMIOWHeadDtlContextSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", WCompanyId);
                    Connection.cmd.Parameters.AddWithValue("@IOWCode", WIOWCode);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            IOWItemMsg IOWMas = new IOWItemMsg();
                            IOWMas.Result = "0";
                            IOWMas.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                            IOWMas.CRAMIOWHeadDtlId = Convert.ToInt64(sdr["CRAMIOWHeadDtlId"].ToString().Trim());
                            IOWMas.CRAMIOWCode = (sdr["CRAMIOWCode"].ToString().Trim());
                            IOWMas.CRAMIOWName = (sdr["CRAMIOWName"].ToString().Trim());
                            IOWMas.IOWQuantity = Convert.ToDouble(sdr["IOWQuantity"].ToString().Trim());
                            IOWMas.IOWUOM = (sdr["IOWUOM"].ToString().Trim());
                            IOWMas.IsTemproryIOW = Convert.ToBoolean(sdr["IsTemproryIOW"].ToString().Trim());

                            IOWMas.Context = sdr["Context"].ToString().Trim();
                            IOWMas.ContextSrlNo = sdr["ContextSrlNo"].ToString().Trim();
                            IOWMas.ContextDisplay = sdr["ContextDisplay"].ToString().Trim();

                            IOWMas.ItemId = Convert.ToInt32(sdr["ItemID"].ToString().Trim());
                            IOWMas.IOWHeadDtlId = Convert.ToInt32(sdr["IOWHeadDtlId"].ToString().Trim());
                            IOWMas.ItemCode = sdr["ItemCode"].ToString().Trim();
                            IOWMas.ItemName = sdr["ItemName"].ToString().Trim();
                            IOWMas.ItemUOM = (sdr["ItemUOM"].ToString().Trim());
                            //IOWMas.ItemMake = (sdr["ItemMake"].ToString().Trim());
                            IOWMas.ItemQuantity = Convert.ToDouble(sdr["ItemQuantity"].ToString().Trim());
                            IOWMas.Wastage = Convert.ToDouble(sdr["Wastage"].ToString().Trim());
                            IOWMas.IsImported = Convert.ToBoolean(sdr["IsImported"].ToString().Trim());
                            IOWMas.IOWCode = (sdr["IOWCode"].ToString().Trim()); // used for displaying the IOWItemSelected for the IOW

                            IOWItLst.Add(IOWMas);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    IOWItemMsg IOW = new IOWItemMsg();
                    IOW.Result = Result;
                    IOWItLst.Add(IOW);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                IOWItemMsg IOW = new IOWItemMsg();
                IOW.Result = "TryCatch MasIOWItemInsertUpdate" + ex.ToString().Substring(1, 100);
                IOWItLst.Add(IOW);
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return IOWItLst;
        }
    }
    //public List<IOWItemMsg> MasCRAMIOWItemInsertUpdate(List<IOWItemMsg> iowmit,string CreatedBy)
    //{
    //    SqlTransaction transaction = null; Int32 FirstRecord = 1;
    //    string Result = "0"; string WIOWCode = ""; Int32 WCompanyId = 0;
    //    List<IOWItemMsg> IOWItLst = new List<IOWItemMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {

    //            transaction = Connection.con.BeginTransaction();
    //            Connection.cmd.Transaction = transaction;
    //            Connection.cmd.Connection = Connection.con;
    //            foreach (IOWItemMsg iowm in iowmit)
    //            {
    //                WCompanyId = iowm.CompanyId;
    //                WIOWCode = iowm.IOWCode;
    //                if (iowm.Flag != "R")
    //                {
    //                    Connection.cmd.CommandText = "MasCRAMIOWItemInsertandUpdateSp";
    //                    Connection.cmd.CommandType = CommandType.StoredProcedure;

    //                    Connection.cmd.Parameters.Clear();
    //                   // Connection.cmd.Parameters.AddWithValue("@Flag", iowm.Flag);
    //                    Connection.cmd.Parameters.AddWithValue("@FirstRecord", FirstRecord);
    //                    Connection.cmd.Parameters.AddWithValue("@CompanyId", iowm.CompanyId);
    //                    Connection.cmd.Parameters.AddWithValue("@IOWCode", iowm.IOWCode);
    //                    Connection.cmd.Parameters.AddWithValue("@ItemId", iowm.ItemId);
    //                    Connection.cmd.Parameters.AddWithValue("@ItemUOM", iowm.ItemUOM);
    //                    Connection.cmd.Parameters.AddWithValue("@ItemQuantity", iowm.ItemQuantity);
    //                    Connection.cmd.Parameters.AddWithValue("@Wastage", iowm.Wastage);
    //                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
    //                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
    //                    Connection.cmd.ExecuteNonQuery();
    //                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
    //                    if (Result.Substring(0, 1) != "0")
    //                    {
    //                        transaction.Rollback();
    //                    }
    //                    FirstRecord = FirstRecord + 1;
    //                }
    //            }
    //            if (Result == "0")
    //            {
    //                Connection.cmd.CommandText = "MasCRAMIOWItemSelectSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;
    //                Connection.cmd.Connection = Connection.con;
    //                Connection.cmd.Parameters.Clear();
    //                Connection.cmd.Parameters.AddWithValue("@CompanyId", WCompanyId);
    //                Connection.cmd.Parameters.AddWithValue("@IOWCode", WIOWCode);
    //                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //                {
    //                        while (sdr.Read())
    //                        {
    //                            IOWItemMsg IOWMas = new IOWItemMsg();
    //                            IOWMas.Result = "0";
    //                            IOWMas.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
    //                            IOWMas.IOWCode = (sdr["IOWCode"].ToString().Trim());
    //                            IOWMas.IOWName = (sdr["IOWName"].ToString().Trim());
    //                            IOWMas.IOWQuantity = Convert.ToDouble(sdr["IOWQuantity"].ToString().Trim());
    //                            IOWMas.ItemId = Convert.ToInt32(sdr["ItemID"].ToString().Trim());
    //                            IOWMas.ItemCode = sdr["ItemCode"].ToString().Trim();
    //                            IOWMas.ItemName = sdr["ItemName"].ToString().Trim();

    //                            IOWMas.ItemUOM = (sdr["ItemUOM"].ToString().Trim());
    //                            IOWMas.ItemMake = (sdr["ItemMake"].ToString().Trim());
    //                            IOWMas.IsImported = Convert.ToBoolean(sdr["IsImported"].ToString().Trim());
                      
    //                            IOWMas.ItemQuantity = Convert.ToDouble(sdr["ItemQuantity"].ToString().Trim());
    //                            IOWMas.Wastage = Convert.ToDouble(sdr["Wastage"].ToString().Trim());
    //                            //IOWMas.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
    //                           // IOWMas.IsCommonFactorsApplicable = Convert.ToBoolean(sdr["IsCommonFactorsApplicable"].ToString().Trim());

    //                            IOWItLst.Add(IOWMas);
    //                        }
                
    //                    }
    //                transaction.Commit();
    //                Connection.cmd.Parameters.Clear();
    //                Connection.con.Close();
    //            }
    //            else
    //            {
    //                IOWItemMsg IOW = new IOWItemMsg();
    //                IOW.Result = Result;
    //                IOWItLst.Add(IOW);

    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            if (transaction != null)
    //            {
    //                transaction.Rollback();
    //            }
    //            IOWItemMsg IOW = new IOWItemMsg();
    //            IOW.Result = "TryCatch MasIOWItemInsertUpdate" + ex.ToString().Substring(1, 100);
    //            IOWItLst.Add(IOW);
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

    //        }
    //        finally
    //        {
    //            Connection.cmd.Parameters.Clear();
    //        }

    //        return IOWItLst;
    //    }
    //}

    public List<IOWItemMsg> MasCRAMIOWItemSelectSp(Int32 CompId, string IOWCode)
    {
        List<IOWItemMsg> IowItlst = new List<IOWItemMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "MasCRAMIOWItemSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompId);
                Connection.cmd.Parameters.AddWithValue("@IOWCode", IOWCode);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        IOWItemMsg IOWMas = new IOWItemMsg();
                        IOWMas.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                        IOWMas.IOWCode = (sdr["IOWCode"].ToString().Trim());
                        IOWMas.IOWName = (sdr["IOWName"].ToString().Trim());
                        IOWMas.IOWQuantity = Convert.ToDouble(sdr["IOWQuantity"].ToString().Trim());
                        IOWMas.ItemId = Convert.ToInt32(sdr["ItemID"].ToString().Trim());
                        IOWMas.ItemCode = sdr["ItemCode"].ToString().Trim();
                        IOWMas.ItemName = sdr["ItemName"].ToString().Trim();

                        IOWMas.ItemUOM = (sdr["ItemUOM"].ToString().Trim());
                        IOWMas.ItemMake = (sdr["ItemMake"].ToString().Trim());
                        IOWMas.IsImported = Convert.ToBoolean(sdr["IsImported"].ToString().Trim());
                      
                        IOWMas.ItemQuantity = Convert.ToDouble(sdr["ItemQuantity"].ToString().Trim());
                        IOWMas.Wastage = Convert.ToDouble(sdr["Wastage"].ToString().Trim());
                        //IOWMas.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                       // IOWMas.IsCommonFactorsApplicable = Convert.ToBoolean(sdr["IsCommonFactorsApplicable"].ToString().Trim());

                        IowItlst.Add(IOWMas);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return IowItlst;
        }
    }
    public List<ItemCategoryMsg> MasItemCategoryInsertUpdate(ItemCategoryMsg pkmsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ItemCategoryMsg> pkList = new List<ItemCategoryMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (pkmsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasItemCategoryInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", pkmsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@ItemCategoryId", pkmsg.ItemCategoryId);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", pkmsg.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@ItemCategoryCode", pkmsg.ItemCategoryCode);
                    Connection.cmd.Parameters.AddWithValue("@ItemCategoryName", pkmsg.ItemCategoryName);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", pkmsg.IsActive);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasItemCategorySelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", pkmsg.CompanyId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ItemCategoryMsg pkMsg = new ItemCategoryMsg();
                            pkMsg.Result = Result;
                            pkMsg.ItemCategoryId = Convert.ToInt32(sdr["ItemCategoryId"].ToString().Trim());
                            pkMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                            pkMsg.ItemCategoryCode = (sdr["ItemCategoryCode"].ToString().Trim());
                            pkMsg.ItemCategoryName = sdr["ItemCategoryName"].ToString().Trim();
                            pkMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());

                            pkList.Add(pkMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ItemCategoryMsg Pkmsg = new ItemCategoryMsg();
                    Pkmsg.Result = Result;
                    pkList.Add(Pkmsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return pkList;
        }
    }
    public List<ItemSubCategoryMsg> MasItemSubCategoryInsertUpdate(ItemSubCategoryMsg pkmsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ItemSubCategoryMsg> pkSubList = new List<ItemSubCategoryMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (pkmsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasItemSubCategoryInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", pkmsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@ItemSubCategoryId", pkmsg.ItemSubCategoryId);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", pkmsg.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@ItemSubCategoryCode", pkmsg.ItemSubCategoryCode);
                    Connection.cmd.Parameters.AddWithValue("@ItemSubCategoryName", pkmsg.ItemSubCategoryName);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", pkmsg.IsActive);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasItemSubCategorySelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", pkmsg.CompanyId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ItemSubCategoryMsg pkMsg = new ItemSubCategoryMsg();
                            pkMsg.Result = Result;
                            pkMsg.ItemSubCategoryId = Convert.ToInt32(sdr["ItemSubCategoryId"].ToString().Trim());
                            pkMsg.ItemCategoryId = Convert.ToInt32(sdr["ItemCategoryId"].ToString().Trim());
                            pkMsg.ItemCategoryCode = (sdr["ItemCategoryCode"].ToString().Trim());
                            pkMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                            pkMsg.ItemSubCategoryCode = (sdr["ItemSubCategoryCode"].ToString().Trim());
                            pkMsg.ItemSubCategoryName = sdr["ItemSubCategoryName"].ToString().Trim();
                            pkMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());

                            pkSubList.Add(pkMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ItemSubCategoryMsg Pkmsg = new ItemSubCategoryMsg();
                    Pkmsg.Result = Result;
                    pkSubList.Add(Pkmsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return pkSubList;
        }
    }

    public List<GroupMsg> MasGroupInsertUpdate(GroupMsg pkmsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<GroupMsg> pkList = new List<GroupMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (pkmsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasGroupInsertUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", pkmsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@GroupId", pkmsg.GroupId);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", pkmsg.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@GroupCode", pkmsg.GroupCode);
                    Connection.cmd.Parameters.AddWithValue("@GroupName", pkmsg.GroupName);
                   // Connection.cmd.Parameters.AddWithValue("@IsActive", pkmsg.IsActive);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasGroupSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", pkmsg.CompanyId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            GroupMsg pkMsg = new GroupMsg();
                            pkMsg.Result = Result;
                            pkMsg.GroupId = Convert.ToInt32(sdr["GroupId"].ToString().Trim());
                            pkMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                            pkMsg.GroupCode = (sdr["GroupCode"].ToString().Trim());
                            pkMsg.GroupName = sdr["GroupName"].ToString().Trim();
                            //pkMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());

                            pkList.Add(pkMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    GroupMsg Pkmsg = new GroupMsg();
                    Pkmsg.Result = Result;
                    pkList.Add(Pkmsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return pkList;
        }
    }
    public string IOWRateInsert(ReportMsg rpt)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        //List<GroupMsg> pkList = new List<GroupMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;

                Connection.cmd.CommandText = "A_IOWRateInsertSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", rpt.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@ForYearMonth", rpt.ForYearMonth);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", rpt.CreatedBy);
                    Connection.cmd.Parameters.AddWithValue("@Region", rpt.Region);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    Result = " Try catch Failure.. " + ex.ToString();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return Result;
        }
    }

    public string A_ProjectInputIOWRateInsert(ReportMsg rpt)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        //List<GroupMsg> pkList = new List<GroupMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;

                Connection.cmd.CommandText = "A_ProjectInputIOWRateInsertSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;

                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@CompanyId", rpt.CompanyId);
                Connection.cmd.Parameters.AddWithValue("@ForYearMonth", rpt.ForYearMonth);
                Connection.cmd.Parameters.AddWithValue("@CreatedBy", rpt.CreatedBy);
                Connection.cmd.Parameters.AddWithValue("@ClientCode", rpt.ClientCode);
                Connection.cmd.Parameters.AddWithValue("@ProjectCode", rpt.ProjectCode);
                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                Connection.cmd.ExecuteNonQuery();
                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                if (Result == "0")
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();


            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    Result = " Try catch Failure.. " + ex.ToString();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return Result;
        }
    }
    public List<SubGroupMsg> MasSubGroupInsertUpdate(SubGroupMsg pkmsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<SubGroupMsg> pkList = new List<SubGroupMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (pkmsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasSubGroupInsertUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", pkmsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@SubGroupId", pkmsg.SubGroupId);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", pkmsg.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@GroupCode", pkmsg.GroupCode);
                    Connection.cmd.Parameters.AddWithValue("@SubGroupCode", pkmsg.SubGroupCode);
                    Connection.cmd.Parameters.AddWithValue("@SubGroupName", pkmsg.SubGroupName);
                    // Connection.cmd.Parameters.AddWithValue("@IsActive", pkmsg.IsActive);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasSubGroupSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", pkmsg.CompanyId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            SubGroupMsg pkMsg = new SubGroupMsg();
                            pkMsg.Result = Result;
                            pkMsg.SubGroupId = Convert.ToInt32(sdr["SubGroupId"].ToString().Trim());
                            pkMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                            pkMsg.GroupCode = (sdr["GroupCode"].ToString().Trim());
                            pkMsg.SubGroupCode = (sdr["SubGroupCode"].ToString().Trim());
                            pkMsg.SubGroupName = sdr["SubGroupName"].ToString().Trim();
                            //pkMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());

                            pkList.Add(pkMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    SubGroupMsg Pkmsg = new SubGroupMsg();
                    Pkmsg.Result = Result;
                    pkList.Add(Pkmsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return pkList;
        }
    }
    public List<IOWHeadMsg> MasIOWHeadInsertUpdate(IOWHeadMsg pkmsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<IOWHeadMsg> pkList = new List<IOWHeadMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (pkmsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasIOWHeadInsertUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", pkmsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@GroupCode", pkmsg.GroupCode);
                    Connection.cmd.Parameters.AddWithValue("@SubGroupCode", pkmsg.SubGroupCode);
                    Connection.cmd.Parameters.AddWithValue("@IOWHeadId", pkmsg.IOWHeadId);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", pkmsg.CompanyId);
                    Connection.cmd.Parameters.AddWithValue("@IOWHeadCode", pkmsg.IOWHeadCode);
                    Connection.cmd.Parameters.AddWithValue("@IOWHeadDescription", pkmsg.IOWHeadDescription);
                    Connection.cmd.Parameters.AddWithValue("@IOWLevel1", pkmsg.IOWLevel1);
                    Connection.cmd.Parameters.AddWithValue("@IOWLevel2", pkmsg.IOWLevel2);
                    Connection.cmd.Parameters.AddWithValue("@IOWLevel3", pkmsg.IOWLevel3);
                    Connection.cmd.Parameters.AddWithValue("@IOWLevel4", pkmsg.IOWLevel4);
                    // Connection.cmd.Parameters.AddWithValue("@IsActive", pkmsg.IsActive);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasIOWHeadSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", pkmsg.CompanyId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            IOWHeadMsg pkMsg = new IOWHeadMsg();
                            pkMsg.Result = Result;
                            pkMsg.IOWHeadId = Convert.ToInt32(sdr["IOWHeadId"].ToString().Trim());
                            pkMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString().Trim());
                            pkMsg.GroupCode = (sdr["GroupCode"].ToString().Trim());
                            pkMsg.SubGroupCode = (sdr["SubGroupCode"].ToString().Trim());
                            pkMsg.IOWHeadCode = (sdr["IOWHeadCode"].ToString().Trim());
                            pkMsg.IOWHeadDescription = sdr["IOWHeadDescription"].ToString().Trim();
                            pkMsg.IOWLevel1 = sdr["IOWLevel1"].ToString().Trim();
                            pkMsg.IOWLevel2 = sdr["IOWLevel2"].ToString().Trim();
                            pkMsg.IOWLevel3 = sdr["IOWLevel3"].ToString().Trim();
                            pkMsg.IOWLevel4 = sdr["IOWLevel4"].ToString().Trim();
                           // pkMsg.IOWLevel1 = sdr["IOWLevel1"].ToString().Trim();

                            //pkMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());

                            pkList.Add(pkMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    IOWHeadMsg Pkmsg = new IOWHeadMsg();
                    Pkmsg.Result = Result;
                    pkList.Add(Pkmsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return pkList;
        }
    }
    public List<CRAMIOWGroupMsg> MasCRAMGroupInsertUpdate(CRAMIOWGroupMsg tig)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<CRAMIOWGroupMsg> tigList = new List<CRAMIOWGroupMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (tig.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasCRAMGroupInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", tig.Flag);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", tig.CompanyId); //SCS 210301 incl companyId

                    Connection.cmd.Parameters.AddWithValue("@CRAMGroupId", tig.CRAMGroupId);
                    Connection.cmd.Parameters.AddWithValue("@GroupName", tig.GroupName);
                    Connection.cmd.Parameters.AddWithValue("@GroupCode", tig.GroupCode);
                    // Connection.cmd.Parameters.AddWithValue("@ItemCategoryId", tig.ItemCategoryId);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", tig.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", tig.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasCRAMGroupSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", tig.CompanyId); //scs 201110 not required in agiler lite
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            CRAMIOWGroupMsg tigMsg = new CRAMIOWGroupMsg();
                            tigMsg.Result = Result;
                            tigMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString()); //SCS 210301
                            tigMsg.CRAMGroupId = Convert.ToInt32(sdr["CRAMGroupId"].ToString()); //SCS 210301
                            //  tigMsg.ItemCategoryId = Convert.ToInt32(sdr["ItemCategoryId"].ToString()); //SCS 210301

                            tigMsg.GroupCode = sdr["GroupCode"].ToString().Trim();
                            tigMsg.GroupName = sdr["GroupName"].ToString();
                            // tigMsg.ItemCategoryName = sdr["ItemCategoryName"].ToString();
                            //tigMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString());
                            //tigMsg.CreatedBy = sdr["CreatedBy"].ToString();
                            tigList.Add(tigMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    CRAMIOWGroupMsg cMsg = new CRAMIOWGroupMsg();
                    cMsg.Result = Result;
                    tigList.Add(cMsg);
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                CRAMIOWGroupMsg cmp = new CRAMIOWGroupMsg();
                cmp.Result = "Try Catch Exception - MasIOWGroupInsertUpdate " + ex.ToString();
                tigList.Add(cmp);
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return tigList;
        }
    }
    public List<CRAMIOWSubGroupMsg> MasCRAMSubGroupInsertUpdate(CRAMIOWSubGroupMsg tig)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<CRAMIOWSubGroupMsg> tigList = new List<CRAMIOWSubGroupMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (tig.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasCRAMSubGroupInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", tig.Flag);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", tig.CompanyId); //SCS 210301 incl companyId

                    Connection.cmd.Parameters.AddWithValue("@CRAMSubGroupId", tig.CRAMSubGroupId);
                    Connection.cmd.Parameters.AddWithValue("@SubGroupName", tig.SubGroupName);
                    Connection.cmd.Parameters.AddWithValue("@GroupCode", tig.GroupCode);
                    Connection.cmd.Parameters.AddWithValue("@SubGroupCode", tig.SubGroupCode);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", tig.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", tig.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasCRAMSubGroupSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", tig.CompanyId); //scs 201110 not required in agiler lite
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            CRAMIOWSubGroupMsg tigMsg = new CRAMIOWSubGroupMsg();
                            tigMsg.Result = Result;
                            tigMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString()); //SCS 210301
                            tigMsg.CRAMSubGroupId = Convert.ToInt32(sdr["CRAMSubGroupId"].ToString()); //SCS 210301
                            tigMsg.GroupCode = sdr["GroupCode"].ToString().Trim();
                            tigMsg.SubGroupCode = sdr["SubGroupCode"].ToString().Trim();
                            tigMsg.SubGroupName = sdr["SubGroupName"].ToString();
                            tigMsg.GroupName = sdr["GroupName"].ToString();
                            tigList.Add(tigMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    CRAMIOWSubGroupMsg cMsg = new CRAMIOWSubGroupMsg();
                    cMsg.Result = Result;
                    tigList.Add(cMsg);
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                CRAMIOWSubGroupMsg cmp = new CRAMIOWSubGroupMsg();
                cmp.Result = "Try Catch Exception - MasIOWGroupInsertUpdate " + ex.ToString();
                tigList.Add(cmp);
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return tigList;
        }
    }

    public List<ItemGroupMsg> MasItemGroupInsertUpdateandDelete(ItemGroupMsg tig)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ItemGroupMsg> tigList = new List<ItemGroupMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (tig.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasItemGroupInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", tig.Flag);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", tig.CompanyId); //SCS 210301 incl companyId

                    Connection.cmd.Parameters.AddWithValue("@ItemGroupId", tig.ItemGroupId);
                    Connection.cmd.Parameters.AddWithValue("@ItemGroupName", tig.ItemGroupName);
                    Connection.cmd.Parameters.AddWithValue("@ItemGroupCode", tig.ItemGroupCode);
                   // Connection.cmd.Parameters.AddWithValue("@ItemCategoryId", tig.ItemCategoryId);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", tig.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", tig.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasItemGroupMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", tig.CompanyId); //scs 201110 not required in agiler lite
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ItemGroupMsg tigMsg = new ItemGroupMsg();
                            tigMsg.Result = Result;
                            tigMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString()); //SCS 210301
                            tigMsg.ItemGroupId = Convert.ToInt32(sdr["ItemGroupId"].ToString()); //SCS 210301
                          //  tigMsg.ItemCategoryId = Convert.ToInt32(sdr["ItemCategoryId"].ToString()); //SCS 210301

                            tigMsg.ItemGroupCode = sdr["ItemGroupCode"].ToString().Trim();
                            tigMsg.ItemGroupName = sdr["ItemGroupName"].ToString();
                           // tigMsg.ItemCategoryName = sdr["ItemCategoryName"].ToString();
                            tigMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString());
                            //tigMsg.CreatedBy = sdr["CreatedBy"].ToString();
                            tigList.Add(tigMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ItemGroupMsg cMsg = new ItemGroupMsg();
                    cMsg.Result = Result;
                    tigList.Add(cMsg);
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ItemGroupMsg cmp = new ItemGroupMsg();
                cmp.Result = "Try CAtch Exception - MasItemGroupInsertUpdateandDelete " + ex.ToString();
                tigList.Add(cmp);
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return tigList;
        }
    }

    public List<ItemMsg> MasItemMasterInsertUpdateandDelete(ItemMsg tig)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ItemMsg> tigList = new List<ItemMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (tig.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasItemMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", tig.Flag);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", tig.CompanyId); //SCS 210301 incl companyId
                    Connection.cmd.Parameters.AddWithValue("@ItemCategoryId", tig.ItemCategoryId);
                    Connection.cmd.Parameters.AddWithValue("@ItemSubCategoryId", tig.ItemSubCategoryId);
                    Connection.cmd.Parameters.AddWithValue("@ItemGroupId", tig.ItemGroupId);

                    Connection.cmd.Parameters.AddWithValue("@ItemId", tig.ItemId);
                    Connection.cmd.Parameters.AddWithValue("@ItemCode", tig.ItemCode);
                    Connection.cmd.Parameters.AddWithValue("@ItemName", tig.ItemName);
                    Connection.cmd.Parameters.AddWithValue("@ItemUOM", tig.ItemUOM);
                    Connection.cmd.Parameters.AddWithValue("@ItemMake", tig.ItemMake);
                    Connection.cmd.Parameters.AddWithValue("@IsImported", tig.IsImported);

                    Connection.cmd.Parameters.AddWithValue("@IsActive", tig.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", tig.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasItemMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", tig.CompanyId); //scs 201110 not required in agiler lite
                    Connection.cmd.Parameters.AddWithValue("@ItemCategoryId", tig.ItemCategoryId); //scs 201110 not required in agiler lite
                    Connection.cmd.Parameters.AddWithValue("@ItemSubCategoryId", tig.ItemSubCategoryId); //scs 201110 not required in agiler lite
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ItemMsg tigMsg = new ItemMsg();
                            tigMsg.Result = Result;
                            tigMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString()); //SCS 210301

                            tigMsg.ItemGroupId = Convert.ToInt32(sdr["ItemGroupId"].ToString()); //SCS 210301
                            tigMsg.ItemCategoryId = Convert.ToInt32(sdr["ItemCategoryId"].ToString()); //SCS 210301
                            tigMsg.ItemSubCategoryId = Convert.ToInt32(sdr["ItemSubCategoryId"].ToString()); //SCS 210301
                            tigMsg.ItemCategoryName = sdr["ItemCategoryName"].ToString();
                            tigMsg.ItemSubCategoryName = sdr["ItemSubCategoryName"].ToString();
                            tigMsg.ItemGroupName = sdr["ItemGroupName"].ToString();

                            tigMsg.ItemId = Convert.ToInt32(sdr["ItemId"].ToString()); //SCS 210301
                            tigMsg.ItemCode = sdr["ItemCode"].ToString().Trim();
                            tigMsg.ItemName = sdr["ItemName"].ToString();
                            tigMsg.ItemUOM = sdr["ItemUOM"].ToString();
                            tigMsg.ItemMake = sdr["ItemMake"].ToString();

                            tigMsg.IsImported = Convert.ToBoolean(sdr["IsImported"].ToString());
                            tigMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString());
                            //tigMsg.CreatedBy = sdr["CreatedBy"].ToString();
                            tigList.Add(tigMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ItemMsg cMsg = new ItemMsg();
                    cMsg.Result = Result;
                    tigList.Add(cMsg);
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ItemMsg cmp = new ItemMsg();
                cmp.Result = "Try CAtch Exception - MasItemGroupInsertUpdateandDelete " + ex.ToString();
                tigList.Add(cmp);
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return tigList;
        }
    }
    public List<ItemRateMsg> MasItemRateInsertUpdateandDelete(ItemRateMsg tig)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ItemRateMsg> tigList = new List<ItemRateMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (tig.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasItemRateInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", tig.Flag);
                    Connection.cmd.Parameters.AddWithValue("@ItemRateId", tig.ItemRateId);
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", tig.CompanyId); //SCS 210301 incl companyId
                    Connection.cmd.Parameters.AddWithValue("@ItemId", tig.ItemId);
                    Connection.cmd.Parameters.AddWithValue("@ItemRate", tig.ItemRate);
                    Connection.cmd.Parameters.AddWithValue("@Region", tig.Region);
                    Connection.cmd.Parameters.AddWithValue("@StateId", tig.StateId);
                    Connection.cmd.Parameters.AddWithValue("@RateDate", tig.RateDate);
                    Connection.cmd.Parameters.AddWithValue("@Discount", tig.Discount);
                    Connection.cmd.Parameters.AddWithValue("@SourceOfRate", tig.SourceOfRate);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", tig.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", tig.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasItemRateSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyId", tig.CompanyId); //scs 201110 not required in agiler lite
                    Connection.cmd.Parameters.AddWithValue("@ItemCategoryId", tig.ItemCategoryId); //scs 201110 not required in agiler lite
                    Connection.cmd.Parameters.AddWithValue("@ItemSubCategoryId", tig.ItemSubCategoryId); //scs 201110 not required in agiler lite
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ItemRateMsg tigMsg = new ItemRateMsg();
                            tigMsg.Result = Result;
                            tigMsg.CompanyId = Convert.ToInt32(sdr["CompanyId"].ToString()); //SCS 210301
                            tigMsg.ItemRateId = Convert.ToInt32(sdr["ItemRateId"].ToString()); //SCS 210301
                            tigMsg.ItemCategoryName = sdr["ItemCategoryName"].ToString();
                            tigMsg.ItemSubCategoryName = sdr["ItemSubCategoryName"].ToString();
                            tigMsg.ItemGroupName = sdr["ItemGroupName"].ToString();
                            tigMsg.ItemGroupId = Convert.ToInt32(sdr["ItemGroupId"].ToString()); //SCS 210301

                            tigMsg.ItemId = Convert.ToInt32(sdr["ItemId"].ToString()); //SCS 210301
                            tigMsg.ItemCode = sdr["ItemCode"].ToString().Trim();
                            tigMsg.ItemName = sdr["ItemName"].ToString();
                            tigMsg.ItemUOM = sdr["ItemUOM"].ToString();
                            tigMsg.ItemMake = sdr["ItemMake"].ToString();
                            tigMsg.ItemRate = Convert.ToDecimal(sdr["ItemRate"].ToString()); //SCS 210301
                            tigMsg.Discount = Convert.ToDecimal(sdr["Discount"].ToString()); //SCS 210301
                            tigMsg.Region = sdr["Region"].ToString();
                            tigMsg.SourceOfRate = sdr["SourceOfRate"].ToString();
                            tigMsg.StateName = sdr["StateName"].ToString();
                            tigMsg.IsImported = Convert.ToBoolean(sdr["IsImported"].ToString());
                            tigMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString());

                            tigMsg.StateId = Convert.ToInt32(sdr["StateId"].ToString()); //SCS 210301
                            //tigMsg.ItemSubCategoryId = Convert.ToInt32(sdr["ItemSubCategoryId"].ToString()); //SCS 210301
                           

                           
                           

                           
                            //tigMsg.CreatedBy = sdr["CreatedBy"].ToString();
                            tigList.Add(tigMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ItemRateMsg cMsg = new ItemRateMsg();
                    cMsg.Result = Result;
                    tigList.Add(cMsg);
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ItemRateMsg cmp = new ItemRateMsg();
                cmp.Result = "Try CAtch Exception - MasItemGroupInsertUpdateandDelete " + ex.ToString();
                tigList.Add(cmp);
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return tigList;
        }
    }


    public List<EnterpriseMasterMsg> MasEnterpriseInsertUpdateandDelete(EnterpriseMasterMsg Enterprise)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<EnterpriseMasterMsg> EnterpriseList = new List<EnterpriseMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Enterprise.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasEnterpriseMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Enterprise.Flag);
                    Connection.cmd.Parameters.AddWithValue("@EnterpriseId", Enterprise.EnterpriseId);
                    Connection.cmd.Parameters.AddWithValue("@EnterpriseName", Enterprise.EnterpriseName);
                    Connection.cmd.Parameters.AddWithValue("@Addr1", Enterprise.Addr1);
                    Connection.cmd.Parameters.AddWithValue("@Addr2", Enterprise.Addr2);
                    Connection.cmd.Parameters.AddWithValue("@Addr3", Enterprise.Addr3);
                    //Connection.cmd.Parameters.AddWithValue("@Addr4", Enterprise.Addr4);
                    Connection.cmd.Parameters.AddWithValue("@Phone1", Enterprise.Phone1);
                    //Connection.cmd.Parameters.AddWithValue("@Phone2", Enterprise.Phone2);
                    Connection.cmd.Parameters.AddWithValue("@Fax", Enterprise.Fax);
                    Connection.cmd.Parameters.AddWithValue("@Website", Enterprise.Website);
                    //Connection.cmd.Parameters.AddWithValue("@Email", Enterprise.Email);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Enterprise.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Enterprise.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasEnterpriseMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            EnterpriseMasterMsg EnterpriseMsg = new EnterpriseMasterMsg();
                            EnterpriseMsg.EnterpriseResult = Result;
                            EnterpriseMsg.EnterpriseId = Convert.ToInt32(sdr["EnterpriseId"].ToString().Trim());
                            EnterpriseMsg.EnterpriseName = sdr["EnterpriseName"].ToString().Trim();
                            EnterpriseMsg.Addr1 = sdr["Addr1"].ToString().Trim();
                            EnterpriseMsg.Addr2 = sdr["Addr2"].ToString().Trim();
                            EnterpriseMsg.Addr3 = sdr["Addr3"].ToString().Trim();
                           // EnterpriseMsg.Addr4 = sdr["Addr4"].ToString().Trim();
                            EnterpriseMsg.Phone1 = Convert.ToInt32(sdr["Phone1"].ToString().Trim());
                           // EnterpriseMsg.Phone2 = Convert.ToInt32(sdr["Phone2"].ToString().Trim());
                            EnterpriseMsg.Fax = Convert.ToInt32(sdr["Fax"].ToString().Trim());
                            //EnterpriseMsg.Email = sdr["Email"].ToString().Trim();
                            EnterpriseMsg.Website = sdr["Website"].ToString().Trim();
                            EnterpriseMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            EnterpriseList.Add(EnterpriseMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    EnterpriseMasterMsg EnterpriseMsg = new EnterpriseMasterMsg();
                    EnterpriseMsg.EnterpriseResult = Result;
                    EnterpriseList.Add(EnterpriseMsg);
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return EnterpriseList;
        }
    }
    public string InsertControlProcessing(ControlProcessMsg Control)
    {
        string Result = ""; Int64 CtrlProcId = 0;
        SqlTransaction transaction = null;
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "ADMInsertProcessingControlSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;

                Connection.cmd.Parameters.AddWithValue("@ControlProcessingId", Control.ControlProcessingId);
                //Connection.cmd.Parameters.AddWithValue("@OverTimePeriodFrom", Control.OverTimePeriodFrom);
                //Connection.cmd.Parameters.AddWithValue("@OverTimePeriodTo", Control.OverTimePeriodTo);
                //Connection.cmd.Parameters.AddWithValue("@PayslipYearMonth", Control.YearMonth);
                //Connection.cmd.Parameters.AddWithValue("@PaySlipProcessStatus", Control.PaySlipProcess);
                //Connection.cmd.Parameters.AddWithValue("@ESIPeriodFrom", Control.ESIPeriodFrom);
                //Connection.cmd.Parameters.AddWithValue("@ESIPeriodTo", Control.ESIPeriodTo);
                //Connection.cmd.Parameters.AddWithValue("@PFYearFrom", Control.PFPeriodFrom);
                //Connection.cmd.Parameters.AddWithValue("@PFYearTo", Control.PFPeriodTo);
                //Connection.cmd.Parameters.AddWithValue("@IsActive", Control.IsActive);
                Connection.cmd.Parameters.AddWithValue("@WorkingDays", Control.WorkingDays);
                Connection.cmd.Parameters.AddWithValue("@CreatedBy", Control.CreatedBy);
                //Connection.cmd.Parameters.AddWithValue("@CreatedDate", Control.CreatedDate);
                //Connection.cmd.Parameters.AddWithValue("@ModifiedDate", Control.ModifiedDate);
                //Connection.cmd.Parameters.AddWithValue("@ModifiedById", Control.ModifiedId);
                Connection.cmd.Parameters.AddWithValue("@Id", 0).Direction = ParameterDirection.Output;
                Connection.cmd.ExecuteNonQuery();

                CtrlProcId = Convert.ToInt64(Connection.cmd.Parameters["@Id"].Value.ToString());
                if (CtrlProcId == Control.ControlProcessingId)
                {
                    transaction.Commit();
                    Result = "S";
                }
                else
                {
                    transaction.Rollback();
                    Result = "E";
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }
            return Result;
        }
    }

    public List<FinancialYearMasterMsg> MasFinancialYearInsertUpdate(FinancialYearMasterMsg Financial)
    {
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";

        SqlTransaction transaction = null;
        string Result = "0";
        List<FinancialYearMasterMsg> FinancialyearList = new List<FinancialYearMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Financial.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasFinancialYearMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Financial.Flag);
                    Connection.cmd.Parameters.AddWithValue("@FinancialYearId", Financial.FinancialYearId);
                    Connection.cmd.Parameters.AddWithValue("@FromDate", Financial.FromDate);
                    Connection.cmd.Parameters.AddWithValue("@ToDate", Financial.ToDate);
                    Connection.cmd.Parameters.AddWithValue("@FiscalYear", Financial.FiscalYear);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Financial.CreatedBy);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Financial.IsActive);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasFinancialYearMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            FinancialYearMasterMsg FinancialYr = new FinancialYearMasterMsg();
                            FinancialYr.FinancialYearResult = Result;
                            FinancialYr.FinancialYearId = Convert.ToInt32(sdr["FinancialYearId"].ToString());
                            FinancialYr.FromDate = Convert.ToDateTime(sdr["FromDate"].ToString());
                            FinancialYr.ToDate = Convert.ToDateTime(sdr["ToDate"].ToString());
                            FinancialYr.FiscalYear = Convert.ToInt32(sdr["FiscalYear"].ToString());
                            FinancialyearList.Add(FinancialYr);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    FinancialYearMasterMsg FinancialYr = new FinancialYearMasterMsg();
                    FinancialYr.FinancialYearResult = Result;
                    FinancialyearList.Add(FinancialYr);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return FinancialyearList;
        }
    }
    #endregion
    #region Login Admin
    public LoginInfoMsg CheckLogin(LoginInfoMsg LoginInfo)
    {
        string Result = "0";
        LoginInfoMsg LoginInfoResult = new LoginInfoMsg();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "AdmChkLoginSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", LoginInfo.UserName);
                Connection.cmd.Parameters.AddWithValue("@Password", LoginInfo.Password);
                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                Connection.cmd.ExecuteNonQuery();
                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                if (Result == "0" || Result == "1")//added by abinayaa 230513 for db Size cheking "|| Result == "1""
                {
                    Connection.cmd.CommandText = "AdmUserInfoSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", LoginInfo.UserName);
                    Connection.cmd.Parameters.AddWithValue("@MachineIp", LoginInfo.MachineIP);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            LoginInfoResult.Result = Result;
                            LoginInfoResult.EmployeeName = sdr["EmployeeName"].ToString().Trim();
                            LoginInfoResult.EmployeeCode = sdr["EmployeeCode"].ToString().Trim(); // //scs 210224
                            LoginInfoResult.CompanyName = sdr["CompanyName"].ToString().Trim();
                            LoginInfoResult.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                            LoginInfoResult.CompanyId = sdr["CompanyId"].ToString().Trim();
                            LoginInfoResult.DepartmentId = Convert.ToInt32(sdr["DepartmentID"].ToString().Trim()); //scs 210224
                            LoginInfoResult.IsAdmin = Convert.ToBoolean(sdr["IsAdmin"].ToString().Trim()); //scs 210224
                            LoginInfoResult.IsSuperUser = Convert.ToBoolean(sdr["IsSuperUser"].ToString().Trim()); //scs 210224
                            LoginInfoResult.UserSessionId = Convert.ToInt32(sdr["UserSessionId"].ToString().Trim());
                        }
                    }
                    Connection.cmd.Parameters.Clear();
                    //Connection.con.Close();
                }
                else
                {
                    LoginInfoResult.Result = Result;
                }
            }
            catch (Exception ex)
            {
                Connection.con.Close();
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            return LoginInfoResult;
        }
    }
    public void UpdateUserLogoffInfo(LoginInfoMsg LoginInfo)
    {
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "AdmUserLogOffInfoSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", LoginInfo.UserName);
                Connection.cmd.Parameters.AddWithValue("@UserSessionId", LoginInfo.UserSessionId);
                Connection.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Connection.con.Close();
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }
    }
    public List<ChangePasswordMsg> AdmChangePaswordUpdateSp(ChangePasswordMsg ChangePwd)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ChangePasswordMsg> ChangePwdList = new List<ChangePasswordMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                //if (State.Flag != "R")
                {
                    Connection.cmd.CommandText = "AdmChangePaswordUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@NewPassword", ChangePwd.NewPassword);
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", ChangePwd.EmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@OldPassword", ChangePwd.OldPassword);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                transaction.Commit();
                Connection.cmd.Parameters.Clear();
               // Connection.con.Close();
                ChangePasswordMsg ChangePwdMsg = new ChangePasswordMsg();
                ChangePwdMsg.ChangePwdResult = Result;
                ChangePwdList.Add(ChangePwdMsg);
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                Connection.con.Close();
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }

            return ChangePwdList;
        }
    }
    public string AdmRoleUpdateSp(RoleNameChangeMsg Role )
    {
        //SqlTransaction transaction = null;
        string Result = "";
        List<RoleNameChangeMsg> RoleList = new List<RoleNameChangeMsg>();
        using (Connection.con)
        {
            try
            {

               
                Connection.cmd.Connection = Connection.con;                
                if (Role.Flag != "R")
                {
                    Connection.cmd.CommandText = "AdmRoleUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Role.Flag);
                    Connection.cmd.Parameters.AddWithValue("@RoleId", Role.RoleId);
                    Connection.cmd.Parameters.AddWithValue("@RoleName", Role.RoleName);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Role.CreatedBy);                    
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                    
                }
               

            }
            catch (Exception ex)
            {
                Connection.con.Close();        
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }

            return Result;
        }
    }
    public List<RoleProgramsMsg> AdmRoleProgramsInsertUpdateandDelete(RoleProgramsMsg roleProgramsMsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        int NewCount = 1;
        List<RoleProgramsMsg> RoleProgramsList = new List<RoleProgramsMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;

                if (roleProgramsMsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "AdmProgramsToRoleInsertSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", roleProgramsMsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@RoleId", roleProgramsMsg.RoleId);
                    Connection.cmd.Parameters.AddWithValue("@RoleName", roleProgramsMsg.RoleName);
                    Connection.cmd.Parameters.AddWithValue("@ProgramId", roleProgramsMsg.ProgramId);
                    Connection.cmd.Parameters.AddWithValue("@CanAccess", roleProgramsMsg.CanAccess);
                    Connection.cmd.Parameters.AddWithValue("@CanCreate", roleProgramsMsg.CanCreate);
                    Connection.cmd.Parameters.AddWithValue("@CanEdit", roleProgramsMsg.CanEdit);
                    Connection.cmd.Parameters.AddWithValue("@CanDelete", roleProgramsMsg.CanDelete);
                    Connection.cmd.Parameters.AddWithValue("@CanPrint", roleProgramsMsg.CanPrint);
                    Connection.cmd.Parameters.AddWithValue("@UserCode", roleProgramsMsg.CreatedBy);
                    Connection.cmd.Parameters.AddWithValue("@NewCount", NewCount);
                    Connection.cmd.Parameters.Add("@Return", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Return"].Value.ToString().Trim());

                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "AdmRoleProgramSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@RoleId", roleProgramsMsg.RoleId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RoleProgramsMsg rolePrograms = new RoleProgramsMsg();
                            rolePrograms.Result = Result;
                            rolePrograms.ProgramId = Convert.ToInt32(sdr["ProgramId"].ToString().Trim());
                            rolePrograms.ProgramName = sdr["ProgramName"].ToString().Trim();
                            rolePrograms.CanAccess = Convert.ToBoolean(sdr["CanAccess"].ToString().Trim());
                            rolePrograms.CanCreate = Convert.ToBoolean(sdr["CanCreate"].ToString().Trim());
                            rolePrograms.CanDelete = Convert.ToBoolean(sdr["CanDelete"].ToString().Trim());
                            rolePrograms.CanEdit = Convert.ToBoolean(sdr["CanEdit"].ToString().Trim());
                            rolePrograms.CanPrint = Convert.ToBoolean(sdr["CanPrint"].ToString().Trim());
                            rolePrograms.MainMenu = sdr["MainMenu"].ToString().Trim();
                            RoleProgramsList.Add(rolePrograms);
                        }
                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    RoleProgramsMsg roles = new RoleProgramsMsg();
                    roles.Result = Result;
                    RoleProgramsList.Add(roles);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Connection.con.Close();
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }

            return RoleProgramsList;
        }
    }
    public List<RoleProgramsMsg> AdmRoleProgramsInsertUpdateandDelete(List<RoleProgramsMsg> RoleProgramsList)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        int NewCount = 0;
        int RoleId = 0;
        bool IsSuccess = true;
        List<RoleProgramsMsg> rolePromgramMsgList = new List<RoleProgramsMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "AdmProgramsToRoleInsertSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                foreach (RoleProgramsMsg roleProgramsMsg in RoleProgramsList)
                {
                    if (IsSuccess == true)
                    {
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.Parameters.AddWithValue("@Flag", roleProgramsMsg.Flag);
                        Connection.cmd.Parameters.AddWithValue("@RoleId", RoleId);
                        Connection.cmd.Parameters.AddWithValue("@RoleName", roleProgramsMsg.RoleName);
                        Connection.cmd.Parameters.AddWithValue("@ProgramId", roleProgramsMsg.ProgramId);
                        Connection.cmd.Parameters.AddWithValue("@CanAccess", roleProgramsMsg.CanAccess);
                        Connection.cmd.Parameters.AddWithValue("@CanCreate", roleProgramsMsg.CanCreate);
                        Connection.cmd.Parameters.AddWithValue("@CanEdit", roleProgramsMsg.CanEdit);
                        Connection.cmd.Parameters.AddWithValue("@CanDelete", roleProgramsMsg.CanDelete);
                        Connection.cmd.Parameters.AddWithValue("@CanPrint", roleProgramsMsg.CanPrint);
                        Connection.cmd.Parameters.AddWithValue("@UserCode", roleProgramsMsg.CreatedBy);
                        if (roleProgramsMsg.RoleId == 0 && NewCount == 0)
                        {
                            Connection.cmd.Parameters.AddWithValue("@NewCount", NewCount);
                        }
                        else
                        {
                            NewCount = 1;
                            Connection.cmd.Parameters.AddWithValue("@NewCount", NewCount);
                        }
                        Connection.cmd.Parameters.Add("@Return", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                        Connection.cmd.ExecuteNonQuery();
                        if (RoleId == 0 && NewCount == 0)
                        {
                            if (Isvalid.IsIntegerAndPositive(Connection.cmd.Parameters["@Return"].Value.ToString().Trim()))
                            {
                                RoleId = Convert.ToInt32(Connection.cmd.Parameters["@Return"].Value.ToString().Trim());
                                NewCount = 1;
                            }
                            else
                            {
                                Result = Convert.ToString(Connection.cmd.Parameters["@Return"].Value.ToString().Trim());
                                IsSuccess = false;
                            }
                        }
                        else if (RoleId > 0)
                        {
                            Result = Convert.ToString(Connection.cmd.Parameters["@Return"].Value.ToString().Trim());
                            if (Result != "0")
                            {
                                IsSuccess = false;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (IsSuccess)
                {
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                    rolePromgramMsgList = AdmRoleProgramsSelect(RoleId);
                }
                else
                {
                    RoleProgramsMsg roles = new RoleProgramsMsg();
                    roles.Result = Result;
                    rolePromgramMsgList.Add(roles);
                    transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                transaction.Dispose();
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }
        return rolePromgramMsgList;
    }
    public string GetForgotPassword(EmployeeMasterMsg Emp)
    {
        string Result = "0";
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Connection = Connection.con;
                //if (State.Flag != "R")
                {
                    Connection.cmd.CommandText = "AdmForgotPaswordSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();

                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@LoginEmployeeCode", Emp.LoginEmployeeCode);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                }

                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            catch (Exception ex)
            {

                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }

            return Result;
        }
    }
    public List<RoleMsg> AdmAvailandAssignerRolesSelect(EmployeeMasterMsg emp)
    {
        List<RoleMsg> RolesList = new List<RoleMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "AdmUserAssignedRolesSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", emp.EmployeeCode);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        RoleMsg rolesMsg = new RoleMsg();
                        rolesMsg.AvRoleId = Convert.ToInt32(sdr["Id"].ToString().Trim());
                        rolesMsg.RoleName = sdr["RoleName"].ToString().Trim();
                        rolesMsg.AsgRoleId = Convert.ToInt32(sdr["RoleId"].ToString().Trim());
                        RolesList.Add(rolesMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }
        return RolesList;
    }
    public List<RoleMsg> AdmRolesSelect()
    {
        List<RoleMsg> RolesList = new List<RoleMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "AdmRolesSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        RoleMsg rolesMsg = new RoleMsg();
                        rolesMsg.RoleId = Convert.ToInt32(sdr["Id"].ToString().Trim());
                        rolesMsg.RoleName = sdr["RoleName"].ToString().Trim();
                        RolesList.Add(rolesMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }
        return RolesList;
    }
    public int AdmUserRolesInsert(string EmployeeCode, ArrayList RoleIdList)
    {
        SqlTransaction transaction = null;
        List<RoleMsg> RolesList = new List<RoleMsg>();
        int Result = 0;
        int NewCount = 0;
        bool IsSuccess = true;
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.CommandText = "AdmUserRolesInsertSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                foreach (string roleId in RoleIdList)
                {
                    if (IsSuccess)
                    {
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                        Connection.cmd.Parameters.AddWithValue("@RoleId", roleId);
                        Connection.cmd.Parameters.AddWithValue("@NewCount", NewCount);
                        Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                        Connection.cmd.ExecuteNonQuery();
                        Result = Convert.ToInt32(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                        NewCount = 1;
                        if (Result != 0)
                        {
                            IsSuccess = false;
                        }
                    }
                }
                if (IsSuccess)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return 1;
            }
            finally
            {
                transaction.Dispose();
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }
        return Result;
    }
    /// <summary>
    /// To Select Program for the Role
    /// </summary>
    /// <param name="Emp"></param>
    /// <returns></returns>
    public List<RoleProgramsMsg> AdmRoleProgramsSelect(int RoleId)
    {
        List<RoleProgramsMsg> RoleProgramsList = new List<RoleProgramsMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "AdmRoleProgramSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@RoleId", RoleId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        RoleProgramsMsg roleProgramsMsg = new RoleProgramsMsg();
                        roleProgramsMsg.Result = "0";
                        roleProgramsMsg.ProgramId = Convert.ToInt32(sdr["ProgramId"].ToString().Trim());
                        roleProgramsMsg.ProgramName = sdr["ProgramName"].ToString().Trim();
                        roleProgramsMsg.CanAccess = Convert.ToBoolean(sdr["CanAccess"].ToString().Trim());
                        roleProgramsMsg.CanCreate = Convert.ToBoolean(sdr["CanCreate"].ToString().Trim());
                        roleProgramsMsg.CanDelete = Convert.ToBoolean(sdr["CanDelete"].ToString().Trim());
                        roleProgramsMsg.CanEdit = Convert.ToBoolean(sdr["CanEdit"].ToString().Trim());
                        roleProgramsMsg.CanPrint = Convert.ToBoolean(sdr["CanPrint"].ToString().Trim());
                        roleProgramsMsg.MainMenu = sdr["MainMenu"].ToString().Trim(); //scs vj 230208 for filter
                        RoleProgramsList.Add(roleProgramsMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }
        return RoleProgramsList;
    }
    public List<ProgramMsg> AdmUserAccessProgramsSelect(EmployeeMasterMsg emp)
    {
        List<ProgramMsg> ProgramMsgList = new List<ProgramMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "AdmUserAccessProgramsSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", emp.EmployeeCode);
                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ProgramMsg programMsg = new ProgramMsg();
                        programMsg.Result = "0";
                        programMsg.ProgramId = Convert.ToInt32(sdr["Id"].ToString().Trim());
                        programMsg.ProgramName = sdr["ProgramName"].ToString().Trim();
                        programMsg.ProgramAccessPath = sdr["ProgramAccessPath"].ToString().Trim();
                        programMsg.MainMenu = sdr["MainMenu"].ToString().Trim();
                        programMsg.SubMenu = sdr["SubMenu"].ToString().Trim();
                        programMsg.ChildMenu = sdr["ChildMenu"].ToString().Trim();
                        programMsg.ProgramSequence = Convert.ToInt32(sdr["ProgramSequence"].ToString().Trim());
                        programMsg.CanAccess = Convert.ToBoolean(sdr["CanAccess"].ToString().Trim());
                        programMsg.CanCreate = Convert.ToBoolean(sdr["CanCreate"].ToString().Trim());
                        programMsg.CanDelete = Convert.ToBoolean(sdr["CanDelete"].ToString().Trim());
                        programMsg.CanEdit = Convert.ToBoolean(sdr["CanEdit"].ToString().Trim());
                        programMsg.CanPrint = Convert.ToBoolean(sdr["CanPrint"].ToString().Trim());
                        ProgramMsgList.Add(programMsg);
                    }
                }
                if (Connection.cmd.Parameters["@Result"].Value.ToString().Trim() != "0")
                {
                    ProgramMsgList = new List<ProgramMsg>();
                    ProgramMsg program = new ProgramMsg();
                    program.Result = Connection.cmd.Parameters["@Result"].Value.ToString().Trim();
                    ProgramMsgList.Add(program);
                    return ProgramMsgList;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }
        return ProgramMsgList;
    }
    #endregion
    #region ExcelUpload
    public UploadResultMsg A_ExcelUploadDSRTemp(List<ExcelUploadMsg> ExcelUploadMsgList, string TranType)
    {
        UploadResultMsg Upload = new UploadResultMsg();
        SqlTransaction tran = null; int DeleteCount = 0; Int64 WParameterId = 0;  //to take care of multiple uploads simultaneously scs 071014
        string Result = "0"; String Remarks = ""; bool IsSuccess = false;

        using (Connection.con)
        {
            IsSuccess = true;
            try
            {

                tran = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = tran;
                //cmd.Transaction = tran;
                Connection.cmd.Connection = Connection.con;
                //cmd.Connection = scon1;
                foreach (ExcelUploadMsg excelUploadMsg in ExcelUploadMsgList)
                {
                    if (IsSuccess)
                    {
                        Connection.cmd.CommandText = "A_ExcelUploadDSRTempSp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.CommandTimeout = 1200;
                        Connection.cmd.Parameters.AddWithValue("@SrlNo", excelUploadMsg.Column1); //Sr No
                        Connection.cmd.Parameters.AddWithValue("@ColA", excelUploadMsg.Column2); //
                        Connection.cmd.Parameters.AddWithValue("@ColB", excelUploadMsg.Column3); //
                        Connection.cmd.Parameters.AddWithValue("@ColC", excelUploadMsg.Column4); //
                        Connection.cmd.Parameters.AddWithValue("@ColD", excelUploadMsg.Column5); //
                        Connection.cmd.Parameters.AddWithValue("@ColE", excelUploadMsg.Column6); //
                        Connection.cmd.Parameters.AddWithValue("@ColF", excelUploadMsg.Column7);
                        Connection.cmd.Parameters.AddWithValue("@ColG", excelUploadMsg.Column8);
                        Connection.cmd.Parameters.AddWithValue("@ColH", excelUploadMsg.Column9);
                        Connection.cmd.Parameters.AddWithValue("@ColI", excelUploadMsg.Column10);

                        Connection.cmd.Parameters.AddWithValue("@ColJ", excelUploadMsg.Column11);
                        Connection.cmd.Parameters.AddWithValue("@ColK", excelUploadMsg.Column12);
                        Connection.cmd.Parameters.AddWithValue("@ColL", excelUploadMsg.Column13);
                        Connection.cmd.Parameters.AddWithValue("@ColM", excelUploadMsg.Column14);
                        Connection.cmd.Parameters.AddWithValue("@ColN", excelUploadMsg.Column15);
                        Connection.cmd.Parameters.AddWithValue("@ColO", excelUploadMsg.Column16);
                        Connection.cmd.Parameters.AddWithValue("@ColP", excelUploadMsg.Column17);
                        Connection.cmd.Parameters.AddWithValue("@ColQ", excelUploadMsg.Column18);
                        Connection.cmd.Parameters.AddWithValue("@ColR", excelUploadMsg.Column19);
                        Connection.cmd.Parameters.AddWithValue("@ColS", excelUploadMsg.Column20); // sheet name
                        #region not used
                        //Connection.cmd.Parameters.AddWithValue("@ColA", excelUploadMsg.Column21);
                        //Connection.cmd.Parameters.AddWithValue("@ColA", excelUploadMsg.Column22);
                        //Connection.cmd.Parameters.AddWithValue("@Column23", excelUploadMsg.Column23);
                        //Connection.cmd.Parameters.AddWithValue("@Column24", excelUploadMsg.Column24);
                        //Connection.cmd.Parameters.AddWithValue("@Column25", excelUploadMsg.Column25);
                        //Connection.cmd.Parameters.AddWithValue("@Column26", excelUploadMsg.Column26);
                        //Connection.cmd.Parameters.AddWithValue("@Column27", excelUploadMsg.Column27);
                        //Connection.cmd.Parameters.AddWithValue("@Column28", excelUploadMsg.Column28);//Net Amount
                        //Connection.cmd.Parameters.AddWithValue("@Column29", excelUploadMsg.Column29);//not used
                        //Connection.cmd.Parameters.AddWithValue("@Column30", excelUploadMsg.Column30); //

                        //Connection.cmd.Parameters.AddWithValue("@Column31", excelUploadMsg.Column31); //
                        //Connection.cmd.Parameters.AddWithValue("@Column32", excelUploadMsg.Column32); // 
                        //Connection.cmd.Parameters.AddWithValue("@Column33", excelUploadMsg.Column33);
                        //Connection.cmd.Parameters.AddWithValue("@Column34", excelUploadMsg.Column34);
                        //Connection.cmd.Parameters.AddWithValue("@Column35", excelUploadMsg.Column35);
                        //Connection.cmd.Parameters.AddWithValue("@Column36", excelUploadMsg.Column36);
                        //Connection.cmd.Parameters.AddWithValue("@Column37", excelUploadMsg.Column37);
                        //Connection.cmd.Parameters.AddWithValue("@Column38", excelUploadMsg.Column38);// 
                        //Connection.cmd.Parameters.AddWithValue("@Column39", excelUploadMsg.Column39);// 
                        //Connection.cmd.Parameters.AddWithValue("@Column40", excelUploadMsg.Column40); //

                        //Connection.cmd.Parameters.AddWithValue("@Column41", excelUploadMsg.Column41); //
                        //Connection.cmd.Parameters.AddWithValue("@Column42", excelUploadMsg.Column42); // 
                        //Connection.cmd.Parameters.AddWithValue("@Column43", excelUploadMsg.Column43);
                        //Connection.cmd.Parameters.AddWithValue("@Column44", excelUploadMsg.Column44);
                        //Connection.cmd.Parameters.AddWithValue("@Column45", excelUploadMsg.Column45);
                        //Connection.cmd.Parameters.AddWithValue("@Column46", excelUploadMsg.Column46);
                        //Connection.cmd.Parameters.AddWithValue("@Column47", excelUploadMsg.Column47);
                        //Connection.cmd.Parameters.AddWithValue("@Column48", excelUploadMsg.Column48);// 
                        //Connection.cmd.Parameters.AddWithValue("@Column49", excelUploadMsg.Column49);// 
                        //Connection.cmd.Parameters.AddWithValue("@Column50", excelUploadMsg.Column50); //
                        #endregion
                        Connection.cmd.Parameters.AddWithValue("@CompanyId", excelUploadMsg.Column51); //
                       // Connection.cmd.Parameters.AddWithValue("@Column52", excelUploadMsg.Column52); // 
                        //Connection.cmd.Parameters.AddWithValue("@Column53", excelUploadMsg.Column53);
                        //Connection.cmd.Parameters.AddWithValue("@Column54", excelUploadMsg.Column54);
                        Connection.cmd.Parameters.AddWithValue("@Column55", excelUploadMsg.Column55); // emp code scs 240909


                        Connection.cmd.Parameters.AddWithValue("@DeleteCount", DeleteCount); // first record
                        Connection.cmd.Parameters.AddWithValue("@WParameterId", WParameterId); // BatchNumber
                        Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                        Connection.cmd.Parameters.Add("@OutParameterId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        Connection.cmd.ExecuteNonQuery();
                        Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                        WParameterId = Convert.ToInt64(Connection.cmd.Parameters["@OutParameterId"].Value.ToString());
                        DeleteCount = 1;
                        if (Result != "0")
                        {
                            IsSuccess = false;
                            //Remarks = Remarks + " " + excelUploadMsg.Column1 + " " + excelUploadMsg.Column10 + " Not in Master. "; // No master found for the PArtyname or account
                        }
                    }
                }
                if (IsSuccess)
                {
                    Upload.WParameterId = WParameterId;
                    Upload.Result = "0";

                    tran.Commit();
                    //Connection.cmd.Parameters.Clear();
                    //Connection.con.Close();
                }
                else //if not success while Initial excel upload
                {
                    Upload.WParameterId = WParameterId;
                    Upload.Result = "1" + Remarks;
                    tran.Rollback();
                    //Connection.cmd.Parameters.Clear();
                    //Connection.con.Close();
                }

            }
            catch (Exception ex)
            {
                IsSuccess = false;
                if (tran != null)
                {
                    tran.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }

        return Upload;

    }
    public UploadResultMsg A_ExcelTempToTableInsert(Int64 WParameterId)
    {
        UploadResultMsg copUpload = new UploadResultMsg();
        SqlTransaction tran = null;
        using (Connection.con)
        {
            try
            {

                tran = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = tran;
                Connection.cmd.Connection = Connection.con;
                
                // Connection.cmd.CommandText = "NewExcelTempToTenderInsertSp"; //scs 231128 created a common sp from where it can branch to respective sp's
                Connection.cmd.CommandText = "A_ExcelTempToTableInsertSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@WParameterId", WParameterId);

                //SqlParameter Result = new SqlParameter("@Result", SqlDbType.VarChar, 256);
                //Result.Direction = ParameterDirection.Output;
                //Result.Size = 256; 
                //Connection.cmd.Parameters.Add(Result);
                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                Connection.cmd.ExecuteNonQuery();
                //copUpload.Result = Result.Value.ToString();
               copUpload.Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                //WeightUpload.Result = "0";
                copUpload.WParameterId = WParameterId;
                if (copUpload.Result.Substring(0, 1) != "0")
                {
                    copUpload.Result = copUpload.Result; //"Error while inserting data from Temp table";
                    copUpload.WParameterId = WParameterId;
                    tran.Rollback();
                }
                else
                {
                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                //IsSuccess = false;
                copUpload.Result = ex.ToString();
                copUpload.WParameterId = WParameterId;
                tran.Rollback();

                //copUpload.Result = ex.ToString();
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }

            finally
            {
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }
        return copUpload;
    }

    public UploadResultMsg NewExcelUploadTempForTender(List<ExcelUploadMsg> ExcelUploadMsgList, string TranType)
    {
        UploadResultMsg Upload = new UploadResultMsg();
        SqlTransaction tran = null; int DeleteCount = 0; Int64 WParameterId = 0;  //to take care of multiple uploads simultaneously scs 071014
        string Result = "0"; String Remarks = ""; bool IsSuccess = false;

        using (Connection.con)
        {
            IsSuccess = true;
            try
            {

                tran = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = tran;
                //cmd.Transaction = tran;
                Connection.cmd.Connection = Connection.con;
                //cmd.Connection = scon1;
                foreach (ExcelUploadMsg excelUploadMsg in ExcelUploadMsgList)
                {
                    if (IsSuccess)
                    {
                        Connection.cmd.CommandText = "NewExcelUploadTempForTenderSp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.CommandTimeout = 1200;
                        Connection.cmd.Parameters.AddWithValue("@Column1", excelUploadMsg.Column1); //Sr No
                        Connection.cmd.Parameters.AddWithValue("@Column2", excelUploadMsg.Column2); //
                        Connection.cmd.Parameters.AddWithValue("@Column3", excelUploadMsg.Column3); //
                        Connection.cmd.Parameters.AddWithValue("@Column4", excelUploadMsg.Column4); //
                        Connection.cmd.Parameters.AddWithValue("@Column5", excelUploadMsg.Column5); //
                        Connection.cmd.Parameters.AddWithValue("@Column6", excelUploadMsg.Column6); //
                        Connection.cmd.Parameters.AddWithValue("@Column7", excelUploadMsg.Column7);
                        Connection.cmd.Parameters.AddWithValue("@Column8", excelUploadMsg.Column8);
                        Connection.cmd.Parameters.AddWithValue("@Column9", excelUploadMsg.Column9);
                        Connection.cmd.Parameters.AddWithValue("@Column10", excelUploadMsg.Column10);

                        Connection.cmd.Parameters.AddWithValue("@Column11", excelUploadMsg.Column11);
                        Connection.cmd.Parameters.AddWithValue("@Column12", excelUploadMsg.Column12);
                        Connection.cmd.Parameters.AddWithValue("@Column13", excelUploadMsg.Column13);
                        Connection.cmd.Parameters.AddWithValue("@Column14", excelUploadMsg.Column14);
                        Connection.cmd.Parameters.AddWithValue("@Column15", excelUploadMsg.Column15);
                        Connection.cmd.Parameters.AddWithValue("@Column16", excelUploadMsg.Column16);
                        Connection.cmd.Parameters.AddWithValue("@Column17", excelUploadMsg.Column17);
                        Connection.cmd.Parameters.AddWithValue("@Column18", excelUploadMsg.Column18);
                        Connection.cmd.Parameters.AddWithValue("@Column19", excelUploadMsg.Column19);
                        Connection.cmd.Parameters.AddWithValue("@Column20", excelUploadMsg.Column20);

                        Connection.cmd.Parameters.AddWithValue("@Column21", excelUploadMsg.Column21);
                        Connection.cmd.Parameters.AddWithValue("@Column22", excelUploadMsg.Column22);
                        Connection.cmd.Parameters.AddWithValue("@Column23", excelUploadMsg.Column23);
                        Connection.cmd.Parameters.AddWithValue("@Column24", excelUploadMsg.Column24);
                        Connection.cmd.Parameters.AddWithValue("@Column25", excelUploadMsg.Column25);
                        Connection.cmd.Parameters.AddWithValue("@Column26", excelUploadMsg.Column26);
                        Connection.cmd.Parameters.AddWithValue("@Column27", excelUploadMsg.Column27);
                        Connection.cmd.Parameters.AddWithValue("@Column28", excelUploadMsg.Column28);//Net Amount
                        Connection.cmd.Parameters.AddWithValue("@Column29", excelUploadMsg.Column29);//not used
                        Connection.cmd.Parameters.AddWithValue("@Column30", excelUploadMsg.Column30); //

                        Connection.cmd.Parameters.AddWithValue("@Column31", excelUploadMsg.Column31); //
                        Connection.cmd.Parameters.AddWithValue("@Column32", excelUploadMsg.Column32); // 
                        Connection.cmd.Parameters.AddWithValue("@Column33", excelUploadMsg.Column33);
                        Connection.cmd.Parameters.AddWithValue("@Column34", excelUploadMsg.Column34);
                        Connection.cmd.Parameters.AddWithValue("@Column35", excelUploadMsg.Column35);
                        Connection.cmd.Parameters.AddWithValue("@Column36", excelUploadMsg.Column36);
                        Connection.cmd.Parameters.AddWithValue("@Column37", excelUploadMsg.Column37);
                        Connection.cmd.Parameters.AddWithValue("@Column38", excelUploadMsg.Column38);// 
                        Connection.cmd.Parameters.AddWithValue("@Column39", excelUploadMsg.Column39);// 
                        Connection.cmd.Parameters.AddWithValue("@Column40", excelUploadMsg.Column40); //

                        Connection.cmd.Parameters.AddWithValue("@Column41", excelUploadMsg.Column41); //
                        Connection.cmd.Parameters.AddWithValue("@Column42", excelUploadMsg.Column42); // 
                        Connection.cmd.Parameters.AddWithValue("@Column43", excelUploadMsg.Column43);
                        Connection.cmd.Parameters.AddWithValue("@Column44", excelUploadMsg.Column44);
                        Connection.cmd.Parameters.AddWithValue("@Column45", excelUploadMsg.Column45);
                        Connection.cmd.Parameters.AddWithValue("@Column46", excelUploadMsg.Column46);
                        Connection.cmd.Parameters.AddWithValue("@Column47", excelUploadMsg.Column47);
                        Connection.cmd.Parameters.AddWithValue("@Column48", excelUploadMsg.Column48);// 
                        Connection.cmd.Parameters.AddWithValue("@Column49", excelUploadMsg.Column49);// 
                        Connection.cmd.Parameters.AddWithValue("@Column50", excelUploadMsg.Column50); //

                        Connection.cmd.Parameters.AddWithValue("@Column51", excelUploadMsg.Column51); //
                        Connection.cmd.Parameters.AddWithValue("@Column52", excelUploadMsg.Column52); // 
                        Connection.cmd.Parameters.AddWithValue("@Column53", excelUploadMsg.Column53);
                        Connection.cmd.Parameters.AddWithValue("@Column54", excelUploadMsg.Column54);
                        Connection.cmd.Parameters.AddWithValue("@Column55", excelUploadMsg.Column55);


                        Connection.cmd.Parameters.AddWithValue("@DeleteCount", DeleteCount); // first record
                        Connection.cmd.Parameters.AddWithValue("@WParameterId", WParameterId); // BatchNumber
                        Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                        Connection.cmd.Parameters.Add("@OutParameterId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        Connection.cmd.ExecuteNonQuery();
                        Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                        WParameterId = Convert.ToInt64(Connection.cmd.Parameters["@OutParameterId"].Value.ToString());
                        DeleteCount = 1;
                        if (Result != "0")
                        {
                            IsSuccess = false;
                            //Remarks = Remarks + " " + excelUploadMsg.Column1 + " " + excelUploadMsg.Column10 + " Not in Master. "; // No master found for the PArtyname or account
                        }
                    }
                }
                if (IsSuccess)
                {
                    Upload.WParameterId = WParameterId;
                    Upload.Result = "0";

                    tran.Commit();
                    //Connection.cmd.Parameters.Clear();
                    //Connection.con.Close();
                }
                else //if not success while Initial excel upload
                {
                    Upload.WParameterId = WParameterId;
                    Upload.Result = "1" + Remarks;
                    tran.Rollback();
                    //Connection.cmd.Parameters.Clear();
                    //Connection.con.Close();
                }

            }
            catch (Exception ex)
            {
                IsSuccess = false;
                if (tran != null)
                {
                    tran.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }

        return Upload;

    }

    public UploadResultMsg NewExcelTempToTenderInsert(Int64 WParameterId)
    {
        UploadResultMsg copUpload = new UploadResultMsg();
        SqlTransaction tran = null;
        using (Connection.con)
        {
            try
            {

                tran = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = tran;
                Connection.cmd.Connection = Connection.con;

               // Connection.cmd.CommandText = "NewExcelTempToTenderInsertSp"; //scs 231128 created a common sp from where it can branch to respective sp's
                Connection.cmd.CommandText = "NewExcelTempToTableInsertSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@WParameterId", WParameterId);
                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                Connection.cmd.ExecuteNonQuery();
                //IsSuccess = true;
                copUpload.Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
                //WeightUpload.Result = "0";
                copUpload.WParameterId = WParameterId;
                if (copUpload.Result.Substring(0, 1) != "0")
                {
                    copUpload.Result = copUpload.Result; //"Error while inserting data from Temp table";
                    copUpload.WParameterId = WParameterId;
                    tran.Rollback();
                }
                else
                {
                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                //IsSuccess = false;
                copUpload.Result = ex.ToString();
                copUpload.WParameterId = WParameterId;
                tran.Rollback();

                //copUpload.Result = ex.ToString();
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }

            finally
            {
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }
        return copUpload;
    }

    #endregion
    #region reports
    public DataTable rptTenderListing(ReportMsg Rpt)
    {
        DataTable dt = new DataTable();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                Connection.cmd.CommandText = "rptTenderListingsp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@CompanyId", Rpt.CompanyId);
                Connection.cmd.Parameters.AddWithValue("@ProjectCode", Rpt.ProjectCode);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "rptTenderListingsp";
                sda.Fill(dt);
            }
            catch //(Exception Ex)
            {
                return null;
            }
            return dt;
        }
    }
    public DataTable rptTenderQuoteHistory(ReportMsg Rpt)
    {
        DataTable dt = new DataTable();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                Connection.cmd.CommandText = "rptTenderQuoteHistorysp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@CompanyId", Rpt.CompanyId);
                Connection.cmd.Parameters.AddWithValue("@ProjectCode", Rpt.ProjectCode);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "rptTenderListingsp";
                sda.Fill(dt);
            }
            catch //(Exception Ex)
            {
                return null;
            }
            return dt;
        }
    }
    public DataTable rptIOWRateSelect(ReportMsg Rpt)
    {
        DataTable dt = new DataTable();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                Connection.cmd.CommandText = "rptIOWRateSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@CompanyId", Rpt.CompanyId);
                Connection.cmd.Parameters.AddWithValue("@PackageCode", Rpt.PackageCode);
                Connection.cmd.Parameters.AddWithValue("@NoOfMonths", Rpt.NoOfMonths);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "rptIOWRateSelectSp";
                sda.Fill(dt);
            }
            catch //(Exception Ex)
            {
                return null;
            }
            return dt;
        }
    }

    public DataTable rptItemRateSelect(ReportMsg Rpt)
    {
        DataTable dt = new DataTable();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                Connection.cmd.CommandText = "rptItemRateSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@CompanyId", Rpt.CompanyId);
                Connection.cmd.Parameters.AddWithValue("@ItemCategoryId", Rpt.ItemCategoryId);
                Connection.cmd.Parameters.AddWithValue("@ItemSubCategoryId", Rpt.ItemSubCategoryId);
                Connection.cmd.Parameters.AddWithValue("@Region", Rpt.Region);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "rptItemRateSelectSp";
                sda.Fill(dt);
            }
            catch //(Exception Ex)
            {
                return null;
            }
            return dt;
        }
    }

    public DataTable CRAMForYearMonthSelect(int CompanyId)
    {
        DataTable dt = new DataTable();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                Connection.cmd.CommandText = "A_CRAMForYearMonthSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
               // Connection.cmd.Parameters.AddWithValue("@ItemCategoryId", Rpt.ItemCategoryId);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "CRAMForYearMonthSelectSp";
                sda.Fill(dt);
            }
            catch //(Exception Ex)
            {
                return null;
            }
            return dt;
        }
    }
    public DataTable ItemRateYearMonthSelect(int CompanyId)
    {
        DataTable dt = new DataTable();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                Connection.cmd.CommandText = "A_ItemRateYearMonthSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                // Connection.cmd.Parameters.AddWithValue("@ItemCategoryId", Rpt.ItemCategoryId);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "A_ItemRateYearMonthSelectSp";
                sda.Fill(dt);
            }
            catch //(Exception Ex)
            {
                return null;
            }
            return dt;
        }
    }
    public DataTable rptCRAMIOWCostListing(ReportMsg Rpt)
    {
        DataTable dt = new DataTable();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                if (Rpt.TransType == "2")
                {
                    Connection.cmd.CommandText = "rptCRAMIOWCostListingSp";
                }
                else
                {
                    Connection.cmd.CommandText = "rptCRAMIOWCostOnlyListingSp";
                }
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@CompanyId", Rpt.CompanyId);
                Connection.cmd.Parameters.AddWithValue("@GroupCode", Rpt.GroupCode);
                Connection.cmd.Parameters.AddWithValue("@SubGroupCode", Rpt.SubGroupCode);
                Connection.cmd.Parameters.AddWithValue("@IOWHeadCode", Rpt.IOWHeadCode);
                Connection.cmd.Parameters.AddWithValue("@ForYearMonth", Rpt.ForYearMonth);
                Connection.cmd.Parameters.AddWithValue("@Region", Rpt.Region);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "rptCRAMIOWCostListing";
                sda.Fill(dt);
            }
            catch //(Exception Ex)
            {
                return null;
            }
            return dt;
        }
    }
    //public DataTable rptTenderQuoteComparision(ReportMsg Rpt)
    //{
    //    DataTable dt = new DataTable();
    //    using (Connection.con)
    //    {
    //        try
    //        {
    //            Connection.cmd.Parameters.Clear();
    //            Connection.cmd.CommandText = "rptTenderQuoteComparisionsp";
    //            Connection.cmd.CommandType = CommandType.StoredProcedure;
    //            Connection.cmd.Connection = Connection.con;
    //            Connection.cmd.Parameters.AddWithValue("@CompanyId", Rpt.CompanyId);
    //            Connection.cmd.Parameters.AddWithValue("@ProjectCode", Rpt.ProjectCode);
    //            //Connection.cmd.ExecuteNonQuery();
    //            SqlDataAdapter sda = new SqlDataAdapter();
    //            sda.SelectCommand = Connection.cmd;
    //            sda.SelectCommand.ExecuteNonQuery();
    //            dt.TableName = "rptTenderQuoteComparisionsp";
    //            sda.Fill(dt);
    //        }
    //        catch //(Exception Ex)
    //        {
    //            return null;
    //        }
    //        return dt;
    //    }
    //}
    public DataTable rptTenderIOWItemQtySelect(Int32 CompanyId,Int64 ClientProjectId, Int64 ForYearMonth, string Region)
    { //for a tender get CRAM IOW Costing itemwise
        DataTable dt = new DataTable();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                Connection.cmd.CommandText = "rptTenderIOWItemQtySelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                Connection.cmd.Parameters.AddWithValue("@ClientProjectId", ClientProjectId);
                Connection.cmd.Parameters.AddWithValue("@ForYearMonth", ForYearMonth);
                Connection.cmd.Parameters.AddWithValue("@Region", Region);
                //Connection.cmd.Parameters.AddWithValue("@ForYearMonth", Rpt.ForYearMonth);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "rptTenderIOWItemQtySelectSp";
                sda.Fill(dt);
            }
            catch (Exception Ex)
            {
                return null;
            }
            return dt;
        }
    }
    public DataTable rptTenderRateComparisionSelect(Int32 CompanyId, Int64 ClientProjectId, Int64 ForYearMonth, string Region)
    { //for a tender get CRAM IOW Costing itemwise
        DataTable dt = new DataTable();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                Connection.cmd.CommandText = "rptTenderRateComparisionSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                Connection.cmd.Parameters.AddWithValue("@ClientProjectId", ClientProjectId);
                Connection.cmd.Parameters.AddWithValue("@ForYearMonth", ForYearMonth);
                Connection.cmd.Parameters.AddWithValue("@Region", Region);
                //Connection.cmd.Parameters.AddWithValue("@ForYearMonth", Rpt.ForYearMonth);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "rptTenderRateComparisionSelectSp";
                sda.Fill(dt);
            }
            catch (Exception Ex)
            {
                return null;
            }
            return dt;
        }
    }

    public DataTable rptTenderCRAMIOWCostDetail(Int64 ClientProjectId, Int64 FromYearMonth, Int64 ToYearMonth, string Region)
    { //for a tender get CRAM IOW Costing itemwise
        DataTable dt = new DataTable(); 
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                Connection.cmd.CommandText = "rptTenderCRAMIOWCostDetailsp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@ClientProjectId", ClientProjectId);
                Connection.cmd.Parameters.AddWithValue("@FromYearMonth", FromYearMonth);
                Connection.cmd.Parameters.AddWithValue("@ToYearMonth", ToYearMonth);
                Connection.cmd.Parameters.AddWithValue("@Region", Region);
                //Connection.cmd.Parameters.AddWithValue("@ForYearMonth", Rpt.ForYearMonth);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "rptTenderCRAMIOWCostDetailsp";
                sda.Fill(dt);
            }
            catch (Exception Ex)
            {
                return null;
            }   
            return dt;
        }
    }
    public DataTable rptTenderIOWCostDetailListing(Int64 ClientProjectId, Int64 FromYearMonth, Int64 ToYearMonth, string Region)
    {
        DataTable dt = new DataTable();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Parameters.Clear();
                Connection.cmd.CommandText = "rptTenderIOWCostDetailListingsp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@ClientProjectId", ClientProjectId);
                Connection.cmd.Parameters.AddWithValue("@FromYearMonth", FromYearMonth);
                Connection.cmd.Parameters.AddWithValue("@ToYearMonth", ToYearMonth);
                Connection.cmd.Parameters.AddWithValue("@Region", Region);
                //Connection.cmd.Parameters.AddWithValue("@ForYearMonth", Rpt.ForYearMonth);
                //Connection.cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                dt.TableName = "rptTenderIOWCostDetailListingsp";
                sda.Fill(dt);
            }
            catch //(Exception Ex)
            {
                return null;
            }
            return dt;
        }
    }


    #endregion
}
   