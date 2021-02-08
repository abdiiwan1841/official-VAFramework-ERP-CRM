﻿/********************************************************
 * Project Name   : VAdvantage
 * Class Name     : LeadProject
 * Purpose        : Create Project from Lead
 * Class Used     : ProcessEngine.SvrProcess
 * Chronological    Development
 * Deepak           09-Dec-2009
  ******************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAdvantage.Classes;
using VAdvantage.Common;
using VAdvantage.Process;
//using System.Windows.Forms;
using VAdvantage.Model;
using VAdvantage.DataBase;
using VAdvantage.SqlExec;
using VAdvantage.Utility;
using System.Data;
using VAdvantage.Logging;

using VAdvantage.ProcessEngine;namespace VAdvantage.Process
{
    public class LeadProject : ProcessEngine.SvrProcess
    {
   /** Project Type		*/
	private int _VAB_ProjectType_ID = 0;
	/** Lead				*/
	private int _VAB_Lead_ID = 0;
	
	/// <summary>
	/// Prepare
	/// </summary>
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
			else if (name.Equals("VAB_ProjectType_ID"))
            {
				_VAB_ProjectType_ID = para[i].GetParameterAsInt();
            }
			else
            {
				log.Log(Level.SEVERE, "Unknown Parameter: " + name);
            }
		}
		_VAB_Lead_ID = GetRecord_ID();
	}	//	prepare

	/// <summary>
	/// Process
	/// </summary>
	/// <returns>summary</returns>
	protected override String DoIt()
	{
		log.Info("VAB_Lead_ID=" + _VAB_Lead_ID + ",VAB_ProjectType_ID=" + _VAB_ProjectType_ID);
        if (_VAB_Lead_ID == 0)
        {
            throw new Exception("@VAB_Lead_ID@ ID=0");
        }
        if (_VAB_ProjectType_ID == 0)
        {
            throw new Exception("@VAB_ProjectType_ID@ ID=0");

        }

		MVABLead lead = new MVABLead (GetCtx(), _VAB_Lead_ID, Get_TrxName());
        if (lead.Get_ID() != _VAB_Lead_ID)
        {
            throw new Exception("@NotFound@: @VAB_Lead_ID@ ID=" + _VAB_Lead_ID);
        }
		//
		String retValue = lead.CreateProject(_VAB_ProjectType_ID);
        if (retValue != null)
        {
            throw new SystemException(retValue);
        }
		lead.Save();
		MProject project = lead.GetProject();
		//
		return "@VAB_Project_ID@ " + project.GetName();
	}	//	doIt

}	//	LeadProject

}
