using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAdvantage.Classes;
using VAdvantage.Common;
using VAdvantage.Process;
using VAdvantage.Model;
using VAdvantage.DataBase;
using VAdvantage.SqlExec;
using VAdvantage.Utility;
using System.Windows.Forms;
using VAdvantage.ProcessEngine;

using System.Data;
using System.Data.SqlClient;
using VAdvantage.Logging;

namespace VAdvantage.Process
{
    public class ReSendMailText : ProcessEngine.SvrProcess
    {		
        //	Mail Text				
        private MMailText _MailText = null;
        //	From (sender)			
        private int _AD_User_ID = -1;
        // Client Info				
        private MClient _client = null;
        //	From					
        private MUser _from = null;
        private List<int> _list = new List<int>();
        private int _counter = 0;
        private int _errors = 0;

        protected override void Prepare()
        {
            ProcessInfoParameter[] para = GetParameter();
            for (int i = 0; i < para.Length; i++)
            {
                String name = para[i].GetParameterName();
                if (para[i].GetParameter() == null)
                {
                    ;
                }
                else
                {
                    log.Log(Level.SEVERE, "Unknown Parameter: " + name);
                }
            }
        }

        protected override String DoIt()
        {
            MUserMail Usermail = new MUserMail(GetCtx(), GetRecord_ID(), Get_Trx());
            log.Info("R_MailText_ID=" + Usermail.GetR_MailText_ID());
            //	Mail Test
            _MailText = new MMailText(GetCtx(), Usermail.GetR_MailText_ID(), Get_TrxName());
            if (_MailText.GetR_MailText_ID() == 0)
            {
                throw new Exception("Not found @R_MailText_ID@=" + Usermail.GetR_MailText_ID());
            }
            //	Client Info
            _client = MClient.Get(GetCtx());
            if (_client.GetAD_Client_ID() == 0)
            {
                throw new Exception("Not found @AD_Client_ID@");
            }
            if (_client.GetSmtpHost() == null || _client.GetSmtpHost().Length == 0)
            {
                throw new Exception("No SMTP Host found");
            }
            //
            if (Usermail.GetCreatedBy() > 0)
            {
                _from = new MUser(GetCtx(), Usermail.GetCreatedBy(), Get_TrxName());
                if (_from.GetAD_User_ID() == 0)
                {
                    throw new Exception("No found @AD_User_ID@=" + Usermail.GetCreatedBy());
                }
            }
            log.Fine("From " + _from);
            long start = CommonFunctions.CurrentTimeMillis();

            Boolean ok = SendIndividualMail(Usermail.GetAD_User_ID(), Usermail.GetMailText());
            if (ok)
            {
                _counter++;
            }
            else
            {
                _errors++;
            }

            return "@Created@=" + _counter + ", @Errors@=" + _errors + " - "
                + (CommonFunctions.CurrentTimeMillis() - start) + "ms";
        }

        private Boolean SendIndividualMail(int AD_User_ID, String message)
        {
            //	Prevent two email
            int ii = AD_User_ID;
            if (_list.Contains(ii))
            {
                //return null;
                return false;
            }
            _list.Add(ii);
            //
            MUser to = new MUser(GetCtx(), AD_User_ID, null);
            if (to.IsEMailBounced())			//	ignore bounces
            {
                //return null;
                return false;
            }
            _MailText.SetUser(AD_User_ID);		//	parse context
            EMail email = _client.CreateEMail(_from, to, _MailText.GetMailHeader(), message);
            if (email == null)
            {
                //return Boolean.FALSE;
                return false;
            }
            if (_MailText.IsHtml())
            {
                email.SetMessageHTML(_MailText.GetMailHeader(), message);
            }
            else
            {
                email.SetSubject(_MailText.GetMailHeader());
                email.SetMessageText(message);
            }
            if (!email.IsValid() && !email.IsValid(true))
            {
                log.Warning("NOT VALID - " + email);
                to.SetIsActive(false);
                to.AddDescription("Invalid EMail");
                to.Save();
                //return Boolean.FALSE;
                return false;
            }
            Boolean OK = EMail.SENT_OK.Equals(email.Send());
            if (OK)
            {
                string str = "UPDATE AD_UserMail SET IsDelivered='Y' WHERE AD_UserMail_ID = " + GetRecord_ID();
                int no = Util.GetValueOfInt(DB.ExecuteQuery(str, null, Get_Trx()));
                log.Fine(to.GetEMail());
            }
            else
            {
                log.Warning("FAILURE - " + to.GetEMail());
            }
            AddLog(0, null, null, (OK ? "@OK@" : "@ERROR@") + " - " + to.GetEMail());
            return OK;
        }
    }
}
