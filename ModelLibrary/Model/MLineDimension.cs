﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAdvantage.DataBase;
using VAdvantage.Utility;
using ViennaAdvantage.Model;

namespace VAdvantage.Model
{
    public class MLineDimension : X_GL_LineDimension 
    {
        public MLineDimension(Ctx ctx, int GL_LineDimension_ID, Trx trxName)
             : base(ctx, GL_LineDimension_ID, trxName)
        { 
        }

        public MLineDimension(Ctx ctx, DataRow dr, Trx trxName) 
             : base(ctx, dr, trxName)
        { 
        }

        private int count = 0;
        protected override bool BeforeSave(bool newRecord)
        {
            MJournalLine obj = new MJournalLine(GetCtx(), GetGL_JournalLine_ID(), Get_Trx());
            string val = "";

            if (obj.GetAmtSourceDr() > 0)
            {
                val = " AmtSourceDr ";
            }
            else
            {
                val = " AmtSourceCr ";
            }

            string sql = "SELECT SUM(amount) FROM Gl_Linedimension WHERE GL_JournalLine_ID=" + Get_Value("GL_JournalLine_ID") + " AND Gl_Linedimension_ID NOT IN( " + GetGL_LineDimension_ID() + ")";
            int count = Util.GetValueOfInt(DB.ExecuteScalar(sql, null, Get_Trx()));
            count += GetAmount();

            string sqlQry = "SELECT " + val + " FROM GL_JournalLine WHERE GL_JournalLine_ID=" + Get_Value("GL_JournalLine_ID");
            int amtcount = Util.GetValueOfInt(DB.ExecuteScalar(sqlQry, null, Get_Trx()));

            if (count > amtcount)
            {
                log.SaveWarning("AmoutCheck", "");
                return false;
            }

          return true;
        }

       


    }
}
