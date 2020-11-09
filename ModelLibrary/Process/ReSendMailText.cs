using System;
using System.Collections.Generic;
using VAdvantage.Classes;
using VAdvantage.DataBase;
using VAdvantage.Logging;
using VAdvantage.Model;
using VAdvantage.ProcessEngine;
using VAdvantage.Utility;

namespace VAdvantage.Process
{
    public class ReSendMailText : ProcessEngine.SvrProcess
    {		
        // Client Info				
        private MClient _client = null;
        //	From					
        private MUser _from = null;
        private List<int> _list = new List<int>();
        private string _msg;
        private int _errors = 0;
        private MUserMail Usermail = null;
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
            Usermail = new MUserMail(GetCtx(), GetRecord_ID(), Get_Trx());

            //	Client Info
            _client = MClient.Get(GetCtx());
            if (_client.GetAD_Client_ID() == 0)
            {
                return Msg.GetMessageText(GetCtx(),"Not found @AD_Client_ID@");
            }
            if (_client.GetSmtpHost() == null || _client.GetSmtpHost().Length == 0)
            {
                return Msg.GetMessageText(GetCtx(),"No SMTP Host found");
            }
            // From Mail Info
            if (Usermail.GetCreatedBy() > 0)
            {
                _from = new MUser(GetCtx(), Usermail.GetCreatedBy(), Get_TrxName());
            }
            log.Fine("From " + _from);

            Boolean ok = SendIndividualMail(Usermail.GetAD_User_ID(), Usermail.GetMailText());
            if (ok)
            {
                _msg = Msg.GetMessageText(GetCtx(), "Mail Sent");
            }
            else
            {
                _msg = Msg.GetMessageText(GetCtx(), "Mail Sending Failed");
            }
            return _msg;
        }

        private Boolean SendIndividualMail(int AD_User_ID, String message)
        {
            //	Prevent two email
            int ii = AD_User_ID;
            if (_list.Contains(ii))
            {
                return false;
            }
            _list.Add(ii);
            // To Mail Info
            MUser to = new MUser(GetCtx(), AD_User_ID, null);
            if (to.IsEMailBounced())			//	ignore bounces
            {
                return false;
            }
            EMail email = _client.CreateEMail(_from, to, Usermail.GetSubject(), message);
            if (email == null)
            {
                return false;
            }
            else
            {
                email.SetSubject(Usermail.GetSubject());
                email.SetMessageText(message);
            }
            if (!email.IsValid() && !email.IsValid(true))
            {
                log.Warning("NOT VALID - " + email);
                to.SetIsActive(false);
                to.AddDescription("Invalid EMail");
                to.Save();
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
