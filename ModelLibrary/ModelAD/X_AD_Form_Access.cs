namespace VAdvantage.Model
{

/** Generated Model - DO NOT CHANGE */
using System;
using System.Text;
using VAdvantage.DataBase;
using VAdvantage.Common;
using VAdvantage.Classes;
using VAdvantage.Process;
using VAdvantage.Model;
using VAdvantage.Utility;
using System.Data;
/** Generated Model for AD_Form_Access
 *  @author Jagmohan Bhatt (generated) 
 *  @version Vienna Framework 1.1.1 - $Id$ */
public class X_AD_Form_Access : PO
{
public X_AD_Form_Access (Context ctx, int AD_Form_Access_ID, Trx trxName) : base (ctx, AD_Form_Access_ID, trxName)
{
/** if (AD_Form_Access_ID == 0)
{
SetAD_Form_ID (0);
SetAD_Role_ID (0);
SetIsReadWrite (false);
}
 */
}
public X_AD_Form_Access (Ctx ctx, int AD_Form_Access_ID, Trx trxName) : base (ctx, AD_Form_Access_ID, trxName)
{
/** if (AD_Form_Access_ID == 0)
{
SetAD_Form_ID (0);
SetAD_Role_ID (0);
SetIsReadWrite (false);
}
 */
}
/** Load Constructor 
@param ctx context
@param rs result set 
@param trxName transaction
*/
public X_AD_Form_Access (Context ctx, DataRow rs, Trx trxName) : base(ctx, rs, trxName)
{
}
/** Load Constructor 
@param ctx context
@param rs result set 
@param trxName transaction
*/
public X_AD_Form_Access (Ctx ctx, DataRow rs, Trx trxName) : base(ctx, rs, trxName)
{
}
/** Load Constructor 
@param ctx context
@param rs result set 
@param trxName transaction
*/
public X_AD_Form_Access (Ctx ctx, IDataReader dr, Trx trxName) : base(ctx, dr, trxName)
{
}
/** Static Constructor 
 Set Table ID By Table Name
 added by ->Harwinder */
static X_AD_Form_Access()
{
 Table_ID = Get_Table_ID(Table_Name);
 model = new KeyNamePair(Table_ID,Table_Name);
}
/** Serial Version No */
//static long serialVersionUID = 27562514361420L;
/** Last Updated Timestamp 7/29/2010 1:07:24 PM */
public static long updatedMS = 1280389044631L;
/** AD_Table_ID=378 */
public static int Table_ID;
 // =378;

/** TableName=AD_Form_Access */
public static String Table_Name="AD_Form_Access";

protected static KeyNamePair model;
protected Decimal accessLevel = new Decimal(6);
/** AccessLevel
@return 6 - System - Client 
*/
protected override int Get_AccessLevel()
{
return Convert.ToInt32(accessLevel.ToString());
}
/** Load Meta Data
@param ctx context
@return PO Info
*/
protected override POInfo InitPO (Ctx ctx)
{
POInfo poi = POInfo.GetPOInfo (ctx, Table_ID);
return poi;
}
/** Load Meta Data
@param ctx context
@return PO Info
*/
protected override POInfo InitPO(Context ctx)
{
POInfo poi = POInfo.GetPOInfo (ctx, Table_ID);
return poi;
}
/** Info
@return info
*/
public override String ToString()
{
StringBuilder sb = new StringBuilder ("X_AD_Form_Access[").Append(Get_ID()).Append("]");
return sb.ToString();
}
/** Set Special Form.
@param AD_Form_ID Special Form */
public void SetAD_Form_ID (int AD_Form_ID)
{
if (AD_Form_ID < 1) throw new ArgumentException ("AD_Form_ID is mandatory.");
Set_ValueNoCheck ("AD_Form_ID", AD_Form_ID);
}
/** Get Special Form.
@return Special Form */
public int GetAD_Form_ID() 
{
Object ii = Get_Value("AD_Form_ID");
if (ii == null) return 0;
return Convert.ToInt32(ii);
}
/** Set Role.
@param AD_Role_ID Responsibility Role */
public void SetAD_Role_ID (int AD_Role_ID)
{
if (AD_Role_ID < 0) throw new ArgumentException ("AD_Role_ID is mandatory.");
Set_ValueNoCheck ("AD_Role_ID", AD_Role_ID);
}
/** Get Role.
@return Responsibility Role */
public int GetAD_Role_ID() 
{
Object ii = Get_Value("AD_Role_ID");
if (ii == null) return 0;
return Convert.ToInt32(ii);
}
/** Get Record ID/ColumnName
@return ID/ColumnName pair */
public KeyNamePair GetKeyNamePair() 
{
return new KeyNamePair(Get_ID(), GetAD_Role_ID().ToString());
}
/** Set Read Write.
@param IsReadWrite Field is read / write */
public void SetIsReadWrite (Boolean IsReadWrite)
{
Set_Value ("IsReadWrite", IsReadWrite);
}
/** Get Read Write.
@return Field is read / write */
public Boolean IsReadWrite() 
{
Object oo = Get_Value("IsReadWrite");
if (oo != null) 
{
 if (oo.GetType() == typeof(bool)) return Convert.ToBoolean(oo);
 return "Y".Equals(oo);
}
return false;
}
}

}
