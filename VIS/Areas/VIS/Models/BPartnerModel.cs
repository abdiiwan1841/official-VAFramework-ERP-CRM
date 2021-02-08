﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using VAdvantage.DataBase;
using VAdvantage.Logging;
using VAdvantage.Model;
using VAdvantage.Utility;
using VIS.Classes;

namespace VIS.Models
{
    public class BPartnerModel
    {
        Ctx ctx = null;
        private const string CUSTOMER = "Customer";
        private const string VENDOR = "Vendor";
        private const string EMPLOYEE = "Employee";
        private const string PROSPECT = "Prospect";
        private const string CUSTOMERMASTER = "Customer Master";
        private const string VENDORMASTER = "Vendor Master";
        private const string EMPLOYEEMASTER = "Employee Master";
        private const string PROSPECTMASTER = "Prospects";
        public string whereClause = string.Empty;

        //private int _windowNo;
        private bool _readOnly = false;

        // private int _line;
        public List<BPInfo> _greeting;

        /*****************************************/
        public List<BPInfo> _bpGroup;
        public List<BPInfo> _bpRelation;
        public List<BPInfo> _bpLocation;
        /*****************************************/
        /**	Logger			*/

        //

        public string searchKey = string.Empty;
        public string name = string.Empty;
        public string name2 = string.Empty;
        public string phoneNo = string.Empty;
        public string userImage = string.Empty;
        public string phoneNo2 = string.Empty;
        public string mobile = string.Empty;
        public string fax = string.Empty;
        public string contact = string.Empty;
        public string title = string.Empty;
        public string email = string.Empty;
        public string greeting = string.Empty;
        public string greeting1 = string.Empty;
        public int location = 0;
        public int bpGroupID = 0;
        public string bpRelationID = string.Empty;
        public string WebUrl = string.Empty;
        //public string bpRelationID = string.Empty;
        public string bpLocationID = string.Empty;
        public bool isCustomer = false;
        public bool isVendor = false;
        public bool isEmployee = false;
        public int tableID = 0;
        int VAB_BusinessPartner_ID = 0;

        //private string BPtype = null;
        /// <summary>
        /// constructor with no parameter 
        /// </summary>
        public BPartnerModel()
        {
        }

        /// <summary>
        /// Constructor with parameter call when open business Partner 
        /// </summary>
        /// <param name="WinNo"></param>
        /// <param name="bPartnerID"></param>
        /// <param name="bpType"></param>
        public BPartnerModel(int WinNo, int bPartnerID, string bpType, Ctx context)
        {
            ctx = context;
            InitBPartner(WinNo, bPartnerID, bpType);

        }
        private void InitBPartner(int WinNo, int bPartnerID, string bpType)
        {

            VAB_BusinessPartner_ID = bPartnerID;
            bool ro = false;
            DataSet ds = null;


            log.Config("VAB_BusinessPartner_ID=" + bPartnerID);
            //  New bpartner
            if (bPartnerID == 0)
            {
                _partner = null;
                _pLocation = null;
                _user = null;
                _bprelation = null;
                _bpLocation = null;
                _bpGroup = null;
                //return true;
            }

            _partner = new MVABBusinessPartner(Env.GetCtx(), bPartnerID, null);
            if (_partner.Get_ID() != 0)
            {
                //	Contact - Load values
                _pLocation = _partner.GetLocation(
                    Env.GetCtx().GetContextAsInt(WinNo, "VAB_BPart_Location_ID"));
                _user = _partner.GetContact(
                   Env.GetCtx().GetContextAsInt(WinNo, "VAF_UserContact_ID"));
            }

            isCustomer = _partner.IsCustomer();
            isVendor = _partner.IsVendor();
            isEmployee = _partner.IsEmployee();
            _readOnly = !MVAFRole.GetDefault(Env.GetCtx()).CanUpdate(
                Env.GetCtx().GetVAF_Client_ID(), Env.GetCtx().GetVAF_Org_ID(),
                MVABBusinessPartner.Table_ID, 0, false);
            log.Info("R/O=" + _readOnly);

            //	Get Data
            _greeting = FillGreeting();

            /************************************/
            _bpGroup = FillBPGroup();
            _bpRelation = FillBPRelation();
            _bpLocation = FillBPLocation(0, ctx);
            /************************************/
            ro = _readOnly;
            if (!ro)
                ro = !MVAFRole.GetDefault(Env.GetCtx()).CanUpdate(
                    Env.GetCtx().GetVAF_Client_ID(), Env.GetCtx().GetVAF_Org_ID(),
                    MVABBPartLocation.Table_ID, 0, false);
            if (!ro)
                ro = !MVAFRole.GetDefault(Env.GetCtx()).CanUpdate(
                    Env.GetCtx().GetVAF_Client_ID(), Env.GetCtx().GetVAF_Org_ID(),
                    MVABAddress.Table_ID, 0, false);

            ds = DB.ExecuteDataset("Select VAB_BusinessPartnerRelation_ID, VAB_BusinessPartnerrelation_location_id from VAB_BPart_Relation where VAB_BusinessPartner_id=" + _partner.GetVAB_BusinessPartner_ID());

            LoadBPartner(VAB_BusinessPartner_ID, ds);

        }

        public bool LoadBPartner(int VAB_BusinessPartner_ID, DataSet ds)
        {
            log.Config("VAB_BusinessPartner_ID=" + VAB_BusinessPartner_ID);
            ////  New bpartner
            if (VAB_BusinessPartner_ID == 0)
            {
                _partner = null;
                _pLocation = null;
                _user = null;
                _bprelation = null;
                _bpLocation = null;
                _bpGroup = null;
                return true;
            }

            //_partner = new MBPartner(Env.GetCtx(), VAB_BusinessPartner_ID, null);
            if (_partner.Get_ID() == 0)
            {
                //Classes.ShowMessage.Error("BPartnerNotFound", null);

            }

            //	BPartner - Load values
            searchKey = _partner.GetValue() ?? "";
            greeting = GetGreeting(_partner.GetVAB_Greeting_ID());
            name = _partner.GetName() ?? "";
            name2 = _partner.GetName2() ?? "";
            WebUrl = _partner.GetURL();

            if (_pLocation != null)
            {
                location = _pLocation.GetVAB_Address_ID();

                //
                phoneNo = _pLocation.GetPhone() ?? "";
                phoneNo2 = _pLocation.GetPhone2() ?? "";
                fax = _pLocation.GetFax() ?? "";
            }
            //	User - Load values

            if (_user != null)
            {
                greeting1 = GetGreeting(_user.GetVAB_Greeting_ID());
                contact = _user.GetName() ?? "";
                title = _user.GetTitle() ?? "";
                email = _user.GetEMail() ?? "";
                //
                phoneNo = _user.GetPhone() ?? "";
                phoneNo2 = _user.GetPhone2() ?? "";
                mobile = _user.GetMobile() ?? "";
                fax = _user.GetFax() ?? "";
                userImage = GetUserImage(_user.GetVAF_Image_ID());

            }
            bpGroupID = _partner.GetVAB_BPart_Category_ID();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    bpRelationID = Convert.ToString(ds.Tables[0].Rows[i]["VAB_BusinessPartnerRelation_ID"]);
                    bpLocationID = Convert.ToString(ds.Tables[0].Rows[i]["VAB_BusinessPartnerrelation_location_id"]);
                }
            }


            return true;
        }


        private string GetUserImage(int imageID)
        {
            string image = string.Empty;
            if (imageID > 0)
            {
                MVAFImage objImage = new MVAFImage(ctx, imageID, null);
                // byte[] imageByte = objImage.GetThumbnailByte(320, 185);
                byte[] imageByte = objImage.GetThumbnailByte(320, 240);
                if (imageByte != null)
                {
                    image = "data:image/jpg;base64," + Convert.ToBase64String(imageByte);
                }
                else
                {
                    image = "Areas/VIS/Images/home/User.png";
                }
            }
            else
            { image = "Areas/VIS/Images/home/User.png"; }
            return image;
        }

        private string GetGreeting(int key)
        {
            for (int i = 0; i < _greeting.Count; i++)
            {
                if (_greeting[i].ID == key)
                    return Convert.ToString(_greeting[i].ID);
            }
            return "";
        }

        /// <summary>
        /// Get BPartner ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetBPartnerID(int userID)
        {
            int VAB_BusinessPartner_ID = 0;
            string sqlQuery = "select VAB_BusinessPartner_id from VAF_UserContact where VAF_UserContact_id =" + userID;
            VAB_BusinessPartner_ID = Util.GetValueOfInt(DB.ExecuteScalar(sqlQuery, null, null));
            return VAB_BusinessPartner_ID;
        }
        /// <summary>
        /// Get VAB_Order_id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetCOrderID(int userID)
        {
            int VAB_Order_ID = 0;
            string sqlQuery = "select VAB_ORDER_ID from VAB_ORDER where VAF_UserContact_id =" + userID;
            VAB_Order_ID = Util.GetValueOfInt(DB.ExecuteScalar(sqlQuery, null, null));
            return VAB_Order_ID;
        }
        public List<BPInfo> lstBPGroup = null;
        public List<BPInfo> FillBPGroup()
        {
            lstBPGroup = new List<BPInfo>();
            lstBPGroup.Add(new BPInfo()
            {
                ID = 0,
                Name = ""
            });
            DataSet ds = new DataSet();
            String sql = "select VAB_BPart_Category_id, Name  from VAB_BPart_Category WHERE IsActive='Y' ";
            sql = MVAFRole.GetDefault(ctx).AddAccessSQL(sql, "VAB_BPart_Category", MVAFRole.SQL_NOTQUALIFIED, MVAFRole.SQL_RO);
            sql += "ORDER BY 2";
            ds = DB.ExecuteDataset(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstBPGroup.Add(new BPInfo()
                    {
                        ID = Convert.ToInt32(ds.Tables[0].Rows[i]["VAB_BPART_CATEGORY_ID"]),
                        Name = Convert.ToString(ds.Tables[0].Rows[i]["NAME"])
                    });
                }
            }
            return lstBPGroup;
        }
        /// <summary>
        ///Fill Greeting
        /// </summary>
        /// <returns>Array of Greetings</returns>
        public List<BPInfo> lstBPRelation = null;
        /// <summary>
        /// Fill BPRelation
        /// </summary>
        /// <returns></returns>
        public List<BPInfo> FillBPRelation()
        {
            lstBPRelation = new List<BPInfo>();
            lstBPRelation.Add(new BPInfo()
            {
                ID = 0,
                Name = ""
            });
            DataSet ds = new DataSet();
            String sql = "select VAB_BusinessPartner_id, Name  from VAB_BusinessPartner WHERE IsActive='Y' ";
            sql = MVAFRole.GetDefault(ctx).AddAccessSQL(sql, "VAB_BusinessPartner", MVAFRole.SQL_NOTQUALIFIED, MVAFRole.SQL_RO);
            sql += "ORDER BY 2";
            ds = DB.ExecuteDataset(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstBPRelation.Add(new BPInfo()
                    {
                        ID = Convert.ToInt32(ds.Tables[0].Rows[i]["VAB_BusinessPartner_ID"]),
                        Name = Convert.ToString(ds.Tables[0].Rows[i]["NAME"])
                    });
                }
            }
            return lstBPRelation;
        }

        /// <summary>
        /// Fill BPLocation
        /// </summary>
        public List<BPInfo> lstBPLocation = null;
        public List<BPInfo> FillBPLocation(int VAB_BusinessPartner_id, Ctx ctx)
        {
            lstBPLocation = new List<BPInfo>();
            lstBPLocation.Add(new BPInfo()
            {
                ID = 0,
                Name = ""
            });
            DataSet ds = new DataSet();
            String sql = "select VAB_BPart_Location_id, Name  from VAB_BPart_Location WHERE IsActive='Y' and VAB_BusinessPartner_id=" + VAB_BusinessPartner_id;
            sql = MVAFRole.GetDefault(ctx).AddAccessSQL(sql, "VAB_BPart_Location", MVAFRole.SQL_NOTQUALIFIED, MVAFRole.SQL_RO);
            sql += "ORDER BY 2";
            ds = DB.ExecuteDataset(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstBPLocation.Add(new BPInfo()
                    {
                        ID = Convert.ToInt32(ds.Tables[0].Rows[i]["VAB_BPart_Location_ID"]),
                        Name = Convert.ToString(ds.Tables[0].Rows[i]["NAME"])
                    });
                }
            }
            return lstBPLocation;
        }
        public List<BPInfo> lstGreeting = null;
        /// <summary>
        ///Fill Greeting
        /// </summary>
        /// <returns>Array of Greetings</returns>
        public List<BPInfo> FillGreeting()
        {
            lstGreeting = new List<BPInfo>();
            lstGreeting.Add(new BPInfo()
            {
                ID = 0,
                Name = ""
            });
            DataSet ds = new DataSet();
            String sql = "SELECT VAB_Greeting_ID, Name FROM VAB_Greeting WHERE IsActive='Y' ";
            sql = MVAFRole.GetDefault(ctx).AddAccessSQL(sql, "VAB_Greeting", MVAFRole.SQL_NOTQUALIFIED, MVAFRole.SQL_RO);
            sql += "ORDER BY 2";
            ds = DB.ExecuteDataset(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstGreeting.Add(new BPInfo()
                    {
                        ID = Convert.ToInt32(ds.Tables[0].Rows[i]["VAB_GREETING_ID"]),
                        Name = Convert.ToString(ds.Tables[0].Rows[i]["NAME"])
                    });
                }
            }
            return lstGreeting;
        }



        private MVABBPartLocation _pLocation = null;
        private MVABBusinessPartner _partner = null;

        private MVAFUserContact _user = null;
        X_VAB_BPart_Relation _bprelation = null;
        private static VLogger log = VLogger.GetVLogger(typeof(BPartnerModel).FullName);
        /// <summary>
        /// Add Or Update Business Partner
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="name"></param>
        /// <param name="name2"></param>
        /// <param name="greeting"></param>
        /// <param name="bpGroup"></param>
        /// <param name="bpRelation"></param>
        /// <param name="bpLocation"></param>
        /// <param name="contact"></param>
        /// <param name="greeting1"></param>
        /// <param name="title"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="phoneNo"></param>
        /// <param name="phoneNo2"></param>
        /// <param name="fax"></param>
        /// <param name="ctx"></param>
        /// <param name="_windowNo"></param>
        /// <param name="BPtype"></param>
        /// <param name="VAB_BusinessPartner_ID"></param>
        /// <returns></returns>

        public string AddBPartner(string searchKey, string name, string name2, string greeting, string bpGroup, string bpRelation, string bpLocation, string contact, string greeting1, string title, string email, string address, string phoneNo, string phoneNo2, string fax, Ctx ctx, int _windowNo, string BPtype, int VAB_BusinessPartner_ID, bool isCustomer, bool isVendor, bool isProspect, string fileUrl, string umobile, string webUrl, bool isEmployee)
        {
            StringBuilder strError = new StringBuilder();
            int VAF_Client_ID = ctx.GetVAF_Client_ID();
            if (VAB_BusinessPartner_ID > 0)
            {
                _partner = new MVABBusinessPartner(ctx, VAB_BusinessPartner_ID, null);
            }
            else
            {
                _partner = MVABBusinessPartner.GetTemplate(ctx, VAF_Client_ID);
            }
            bool isSOTrx = ctx.IsSOTrx(_windowNo);
            _partner.SetIsCustomer(isSOTrx);
            _partner.SetIsVendor(!isSOTrx);
            // JID_1197 IN Business partner  updating Createdby,Updatedby,Created,Updated fields as per changed date
            _partner.Set_ValueNoCheck("CreatedBy", ctx.GetVAF_UserContact_ID());
            _partner.Set_ValueNoCheck("Created", DateTime.Now);
            _partner.Set_ValueNoCheck("Updated", DateTime.Now);
            _partner.Set_ValueNoCheck("UpdatedBy", ctx.GetVAF_UserContact_ID());
            if (BPtype != null && (!isCustomer && !isVendor))
            {
                if (BPtype.Contains("Customer"))
                {
                    _partner.SetIsCustomer(true);
                }
                if (BPtype.Contains("Employee"))
                {
                    _partner.SetIsEmployee(true);
                }
                if (BPtype.Contains("Vendor"))
                {
                    _partner.SetIsVendor(true);
                }
                if (BPtype.Contains("Prospect"))
                {
                    _partner.SetIsProspect(true);
                }
                /*
                if (BPtype == "Customer")
                {
                    _partner.SetIsCustomer(true);
                }
                else if (BPtype == "Employee")
                {
                    _partner.SetIsEmployee(true);
                }
                else if (BPtype == "Vendor")
                {
                    _partner.SetIsVendor(true);
                }
                else if (BPtype == "Prospect")
                {
                    _partner.SetIsProspect(true);
                }*/
            }
            if (isCustomer)
            {
                _partner.SetIsCustomer(true);
            }
            else
            {
                _partner.SetIsCustomer(false);
            }
            if (isVendor)
            {
                _partner.SetIsVendor(true);
            }
            else
            {
                _partner.SetIsVendor(false);
            }
            if (isProspect)
            {
                _partner.SetIsProspect(true);
            }
            else
            {
                _partner.SetIsProspect(false);
            }

            if (isEmployee)
            {
                _partner.SetIsEmployee(true);
            }
            else
            {
                _partner.SetIsEmployee(false);
            }

            if (searchKey == null || searchKey.Length == 0)
            {
                //	get Table Documet No
                searchKey = MVAFRecordSeq.GetDocumentNo(ctx.GetVAF_Client_ID(), "VAB_BusinessPartner", null, ctx);
                //Dispatcher.BeginInvoke(() => { txtValue.Text = value; });
            }
            _partner.SetValue(searchKey);
            //
            _partner.SetName(name);
            _partner.SetURL(webUrl);
            //  _partner.SetName2(name2);
            //KeyNamePair p = (KeyNamePair)cmbGreetingBP.SelectedItem;
            //if (greeting >0)
            //{
            //    _partner.SetVAB_Greeting_ID(greeting);
            //}
            //else
            //{
            //    _partner.SetVAB_Greeting_ID(0);
            //}
            if (greeting != string.Empty)
            {
                _partner.SetVAB_Greeting_ID(Convert.ToInt32(greeting));
            }
            else
            {
                _partner.SetVAB_Greeting_ID(0);
            }
            /***************************************************/
            _partner.SetVAB_BPart_Category_ID(Util.GetValueOfInt(bpGroup));
            /***************************************************/

            if (_partner.Save())
            {
                log.Fine("VAB_BusinessPartner_ID=" + _partner.GetVAB_BusinessPartner_ID());
            }
            else
            {
                // Classes.ShowMessage.Error("SearchKeyExist", null);
                strError.Append("SearchKeyExist");
                //this.Cursor = Cursors.Arrow;
                return strError.ToString();
            }

            //	***** Business Partner - Location *****
            if (_pLocation == null)
                if (VAB_BusinessPartner_ID > 0)
                {
                    _pLocation = new MVABBPartLocation(ctx, GetBPartnerLocationID(_partner.Get_ID()), null);
                    if (_pLocation.Get_ID() <= 0)
                    {
                        _pLocation = new MVABBPartLocation(_partner);
                    }
                }
                else
                {
                    _pLocation = new MVABBPartLocation(_partner);
                }
            if (address != string.Empty)
            {
                _pLocation.SetVAB_Address_ID(Convert.ToInt32(address));
            }

            //
            _pLocation.SetPhone(phoneNo);
            // _pLocation.SetPhone2(phoneNo2);
            _pLocation.SetFax(fax);
            if (_pLocation.Save())
            {
                log.Fine("VAB_BPart_Location_ID=" + _pLocation.GetVAB_BPart_Location_ID());
            }
            else
            {
                //   ADialog.error(m_WindowNo, this, "BPartnerNotSaved", Msg.translate(Env.getCtx(), "VAB_BPart_Location_ID"));
                // Classes.ShowMessage.Error("BPartnerNotSaved", null);
                //this.Cursor = Cursors.Arrow;
                strError.Append("BPartnerNotSaved");
                return strError.ToString();
            }

            //	***** Business Partner - User *****
            //String contact = txtContact.Text;
            //String email = txtEMail.Text;
            if (_user == null && (contact.Length > 0 || email.Length > 0))
                if (VAB_BusinessPartner_ID > 0)
                {
                    _user = new MVAFUserContact(ctx, GetUserID(_partner.Get_ID()), null);
                }
                else
                {
                    _user = new MVAFUserContact(_partner);
                }
            if (_user != null)
            {
                if (contact.Length == 0)
                    contact = name;
                _user.SetName(contact);
                _user.SetEMail(email);
                _user.SetTitle(title);
                _user.SetVAB_Address_ID(Convert.ToInt32(address));

                // = (KeyNamePair)cmbGreetingC.SelectedItem;

                //if (greeting1 >0)
                //    _user.SetVAB_Greeting_ID(greeting1);
                if (greeting1 != string.Empty)
                    _user.SetVAB_Greeting_ID(Convert.ToInt32(greeting1));
                else
                    _user.SetVAB_Greeting_ID(0);
                //
                _user.SetPhone(phoneNo);
                // _user.SetPhone2(phoneNo2);
                _user.SetMobile(umobile);
                _user.SetFax(fax);
                _user.SetVAB_BPart_Location_ID(_pLocation.GetVAB_BPart_Location_ID());
                if (_user.Save())
                {
                    if (fileUrl != null && fileUrl != string.Empty)
                    {
                        _user.SetVAF_Image_ID(SaveUserImage(ctx, fileUrl, _user.GetVAF_UserContact_ID()));
                    }
                    if (_user.Save())
                    {
                        log.Fine("VAF_UserContact_ID(VAF_Image_ID)=" + _user.GetVAF_UserContact_ID() + "(" + _user.GetVAF_Image_ID() + ")");
                    }
                    log.Fine("VAF_UserContact_ID=" + _user.GetVAF_UserContact_ID());
                }
                else
                {
                    //Classes.ShowMessage.Error("BPartnerNotSaved", null);
                    //this.Cursor = Cursors.Arrow;
                    strError.Append("BPartnerNotSaved");
                    return strError.ToString();
                }

                /*************************************************/
                if ((bpRelation != null && bpLocation != null) && (bpRelation != string.Empty && bpLocation != string.Empty))
                {
                    if (bpRelation.ToString().Trim() == "" || bpLocation.ToString().Trim() == "")
                    {
                        int dele = DB.ExecuteQuery("DELETE from VAB_BPart_Relation where VAB_BusinessPartner_id=" + _partner.GetVAB_BusinessPartner_ID(), null, null);
                        if (dele == -1)
                        {
                            log.SaveError("VAB_BPart_RelationNotDeleted", "VAB_BusinessPartner_id=" + _partner.GetVAB_BusinessPartner_ID());
                        }
                    }
                    else
                    {
                        //Business Partner Relation 
                        if (VAB_BusinessPartner_ID > 0)
                        {
                            _bprelation = new X_VAB_BPart_Relation(ctx, GetBPRelationID(_partner.Get_ID()), null);
                        }
                        else
                        {
                            _bprelation = new X_VAB_BPart_Relation(ctx, 0, null);
                        }
                        _bprelation.SetVAF_Client_ID(_partner.GetVAF_Client_ID());
                        _bprelation.SetVAF_Org_ID(_partner.GetVAF_Org_ID());
                        _bprelation.SetName(_partner.GetName());
                        _bprelation.SetDescription(_partner.GetDescription());
                        _bprelation.SetVAB_BusinessPartner_ID(_partner.GetVAB_BusinessPartner_ID());
                        _bprelation.SetVAB_BPart_Location_ID(_pLocation.GetVAB_BPart_Location_ID());
                        _bprelation.SetVAB_BPart_Relation_ID(Util.GetValueOfInt(bpRelation));
                        _bprelation.SetVAB_BusinessPartnerRelation_ID(Util.GetValueOfInt(bpRelation));
                        _bprelation.SetVAB_BusinessPartnerRelation_Location_ID(Util.GetValueOfInt(bpLocation));
                        _bprelation.SetIsBillTo(true);
                        if (_bprelation.Save())
                        {
                            log.Fine("VAB_BPart_Relation_ID=" + _bprelation.GetVAB_BPart_Relation_ID());
                        }
                        else
                        {
                            //Classes.ShowMessage.Error("BPRelationNotSaved", null);

                            //this.Cursor = Cursors.Arrow;
                            strError.Append("BPRelationNotSaved");
                            return strError.ToString();
                        }
                    }
                }
                /*************************************************/

            }
            return strError.ToString();

        }


        private int SaveUserImage(Ctx ctx, string fileUrl, int userID)
        {
            int imageID = 0;
            if (File.Exists(fileUrl))
            {
                byte[] byteArray = File.ReadAllBytes(fileUrl);
                string fileName = Path.GetFileName(fileUrl);
                File.Delete(fileUrl); //Delete Temporary file             
                imageID = CommonFunctions.SaveUserImage(ctx, byteArray, fileName, false, userID);
            }
            return imageID;
        }

        /// <summary>
        /// Get window ID
        /// </summary>
        /// <param name="windowName"></param>
        /// <returns></returns>
        public int GetWindowID(string windowName)
        {
            return Util.GetValueOfInt(DB.ExecuteScalar("SELECT VAF_SCREEN_ID FROM VAF_SCREEN WHERE NAME='" + windowName + "' AND ISACTIVE='Y'", null, null));

        }
        /// <summary>
        /// Get user ID
        /// </summary>
        /// <param name="VAB_BusinessPartner_ID"></param>
        /// <returns></returns>
        public int GetUserID(int VAB_BusinessPartner_ID)
        {
            return Util.GetValueOfInt(DB.ExecuteScalar("SELECT VAF_USERCONTACT_ID FROM VAF_USERCONTACT WHERE VAB_BUSINESSPARTNER_ID='" + VAB_BusinessPartner_ID + "' AND ISACTIVE='Y'", null, null));

        }

        /// <summary>
        /// Get BPartnerLocation Id
        /// </summary>
        /// <param name="VAB_BusinessPartner_ID"></param>
        /// <returns></returns>
        public int GetBPartnerLocationID(int VAB_BusinessPartner_ID)
        {
            return Util.GetValueOfInt(DB.ExecuteScalar("SELECT VAB_BPart_Location_ID FROM VAB_BPart_Location WHERE VAB_BUSINESSPARTNER_ID='" + VAB_BusinessPartner_ID + "' AND ISACTIVE='Y'", null, null));

        }
        /// <summary>
        /// Get BPRelation ID
        /// </summary>
        /// <param name="VAB_BusinessPartner_ID"></param>
        /// <returns></returns>
        public int GetBPRelationID(int VAB_BusinessPartner_ID)
        {
            return Util.GetValueOfInt(DB.ExecuteScalar("SELECT VAB_BPart_Relation_ID FROM VAB_BPart_Relation WHERE VAB_BUSINESSPARTNER_ID='" + VAB_BusinessPartner_ID + "' AND ISACTIVE='Y'", null, null));

        }

        /// <summary>
        /// Get table id
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int GetTableID(string TableName)
        {
            return Util.GetValueOfInt(DB.ExecuteScalar("SELECT VAF_TableView_ID FROM VAF_TableView WHERE TABLENAME='" + TableName + "' AND ISACTIVE='Y'", null, null));

        }
    }
    public class BPInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

}