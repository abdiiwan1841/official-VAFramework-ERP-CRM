namespace VAdvantage.Model{
/** Generated Model - DO NOT CHANGE */
using System;using System.Text;using VAdvantage.DataBase;using VAdvantage.Common;using VAdvantage.Classes;using VAdvantage.Process;using VAdvantage.Model;using VAdvantage.Utility;using System.Data;/** Generated Model for VAF_GridLayoutItems
 *  @author Raghu (Updated) 
 *  @version Vienna Framework 1.1.1 - $Id$ */
public class X_VAF_GridLayoutItems : PO{public X_VAF_GridLayoutItems (Context ctx, int VAF_GridLayoutItems_ID, Trx trxName) : base (ctx, VAF_GridLayoutItems_ID, trxName){/** if (VAF_GridLayoutItems_ID == 0){SetVAF_GridLayoutItems_ID (0);SetVAF_GridLayout_ID (0);} */
}public X_VAF_GridLayoutItems (Ctx ctx, int VAF_GridLayoutItems_ID, Trx trxName) : base (ctx, VAF_GridLayoutItems_ID, trxName){/** if (VAF_GridLayoutItems_ID == 0){SetVAF_GridLayoutItems_ID (0);SetVAF_GridLayout_ID (0);} */
}/** Load Constructor 
@param ctx context
@param rs result set 
@param trxName transaction
*/
public X_VAF_GridLayoutItems (Context ctx, DataRow rs, Trx trxName) : base(ctx, rs, trxName){}/** Load Constructor 
@param ctx context
@param rs result set 
@param trxName transaction
*/
public X_VAF_GridLayoutItems (Ctx ctx, DataRow rs, Trx trxName) : base(ctx, rs, trxName){}/** Load Constructor 
@param ctx context
@param rs result set 
@param trxName transaction
*/
public X_VAF_GridLayoutItems (Ctx ctx, IDataReader dr, Trx trxName) : base(ctx, dr, trxName){}/** Static Constructor 
 Set Table ID By Table Name
 added by ->Harwinder */
static X_VAF_GridLayoutItems(){ Table_ID = Get_Table_ID(Table_Name); model = new KeyNamePair(Table_ID,Table_Name);}/** Serial Version No */
static long serialVersionUID = 27858187933414L;/** Last Updated Timestamp 12/11/2019 4:40:16 PM */
public static long updatedMS = 1576062616625L;/** VAF_TableView_ID=1000539 */
public static int Table_ID; // =1000539;
/** TableName=VAF_GridLayoutItems */
public static String Table_Name="VAF_GridLayoutItems";
protected static KeyNamePair model;protected Decimal accessLevel = new Decimal(4);/** AccessLevel
@return 4 - System 
*/
protected override int Get_AccessLevel(){return Convert.ToInt32(accessLevel.ToString());}/** Load Meta Data
@param ctx context
@return PO Info
*/
protected override POInfo InitPO (Context ctx){POInfo poi = POInfo.GetPOInfo (ctx, Table_ID);return poi;}/** Load Meta Data
@param ctx context
@return PO Info
*/
protected override POInfo InitPO (Ctx ctx){POInfo poi = POInfo.GetPOInfo (ctx, Table_ID);return poi;}/** Info
@return info
*/
public override String ToString(){StringBuilder sb = new StringBuilder ("X_VAF_GridLayoutItems[").Append(Get_ID()).Append("]");return sb.ToString();}/** Set VAF_GridLayoutItems_ID.
@param VAF_GridLayoutItems_ID VAF_GridLayoutItems_ID */
public void SetVAF_GridLayoutItems_ID (int VAF_GridLayoutItems_ID){if (VAF_GridLayoutItems_ID < 1) throw new ArgumentException ("VAF_GridLayoutItems_ID is mandatory.");Set_ValueNoCheck ("VAF_GridLayoutItems_ID", VAF_GridLayoutItems_ID);}/** Get VAF_GridLayoutItems_ID.
@return VAF_GridLayoutItems_ID */
public int GetVAF_GridLayoutItems_ID() {Object ii = Get_Value("VAF_GridLayoutItems_ID");if (ii == null) return 0;return Convert.ToInt32(ii);}/** Set Grid Layout.
@param VAF_GridLayout_ID Grid Layout */
public void SetVAF_GridLayout_ID (int VAF_GridLayout_ID){if (VAF_GridLayout_ID < 1) throw new ArgumentException ("VAF_GridLayout_ID is mandatory.");Set_ValueNoCheck ("VAF_GridLayout_ID", VAF_GridLayout_ID);}/** Get Grid Layout.
@return Grid Layout */
public int GetVAF_GridLayout_ID() {Object ii = Get_Value("VAF_GridLayout_ID");if (ii == null) return 0;return Convert.ToInt32(ii);}
/** AlignItems VAF_Control_Ref_ID=1000224 */
public static int ALIGNITEMS_VAF_Control_Ref_ID=1000224;/** Bottom = B */
public static String ALIGNITEMS_Bottom = "B";/** Center = C */
public static String ALIGNITEMS_Center = "C";/** Top = T */
public static String ALIGNITEMS_Top = "T";/** Is test a valid value.
@param test testvalue
@returns true if valid **/
public bool IsAlignItemsValid (String test){return test == null || test.Equals("B") || test.Equals("C") || test.Equals("T");}/** Set Align Items.
@param AlignItems Align Items of panel block vertically. */
public void SetAlignItems (String AlignItems){if (!IsAlignItemsValid(AlignItems))
throw new ArgumentException ("AlignItems Invalid value - " + AlignItems + " - Reference_ID=1000224 - B - C - T");if (AlignItems != null && AlignItems.Length > 1){log.Warning("Length > 1 - truncated");AlignItems = AlignItems.Substring(0,1);}Set_Value ("AlignItems", AlignItems);}/** Get Align Items.
@return Align Items of panel block vertically. */
public String GetAlignItems() {return (String)Get_Value("AlignItems");}/** Set Background Color.
@param BackgroundColor Background Color of header panel */
public void SetBackgroundColor (String BackgroundColor){if (BackgroundColor != null && BackgroundColor.Length > 50){log.Warning("Length > 50 - truncated");BackgroundColor = BackgroundColor.Substring(0,50);}Set_Value ("BackgroundColor", BackgroundColor);}/** Get Background Color.
@return Background Color of header panel */
public String GetBackgroundColor() {return (String)Get_Value("BackgroundColor");}/** Set Column Span.
@param ColumnSpan Column span of item */
public void SetColumnSpan (int ColumnSpan){Set_Value ("ColumnSpan", ColumnSpan);}/** Get Column Span.
@return Column span of item */
public int GetColumnSpan() {Object ii = Get_Value("ColumnSpan");if (ii == null) return 0;return Convert.ToInt32(ii);}/** Set Export.
@param Export_ID Export */
public void SetExport_ID (String Export_ID){if (Export_ID != null && Export_ID.Length > 50){log.Warning("Length > 50 - truncated");Export_ID = Export_ID.Substring(0,50);}Set_Value ("Export_ID", Export_ID);}/** Get Export.
@return Export */
public String GetExport_ID() {return (String)Get_Value("Export_ID");}/** Set Font Color.
@param FontColor Font Color of item */
public void SetFontColor (String FontColor){if (FontColor != null && FontColor.Length > 50){log.Warning("Length > 50 - truncated");FontColor = FontColor.Substring(0,50);}Set_Value ("FontColor", FontColor);}/** Get Font Color.
@return Font Color of item */
public String GetFontColor() {return (String)Get_Value("FontColor");}/** Set Font Size.
@param FontSize Font Size */
public void SetFontSize (String FontSize){if (FontSize != null && FontSize.Length > 50){log.Warning("Length > 50 - truncated");FontSize = FontSize.Substring(0,50);}Set_Value ("FontSize", FontSize);}/** Get Font Size.
@return Font Size */
public String GetFontSize() {return (String)Get_Value("FontSize");}
/** JustifyItems VAF_Control_Ref_ID=1000225 */
public static int JUSTIFYITEMS_VAF_Control_Ref_ID=1000225;/** Center = C */
public static String JUSTIFYITEMS_Center = "C";/** Left = L */
public static String JUSTIFYITEMS_Left = "L";/** Right = R */
public static String JUSTIFYITEMS_Right = "R";/** Is test a valid value.
@param test testvalue
@returns true if valid **/
public bool IsJustifyItemsValid (String test){return test == null || test.Equals("C") || test.Equals("L") || test.Equals("R");}/** Set Justify Items.
@param JustifyItems Justify Items */
public void SetJustifyItems (String JustifyItems){if (!IsJustifyItemsValid(JustifyItems))
throw new ArgumentException ("JustifyItems Invalid value - " + JustifyItems + " - Reference_ID=1000225 - C - L - R");if (JustifyItems != null && JustifyItems.Length > 1){log.Warning("Length > 1 - truncated");JustifyItems = JustifyItems.Substring(0,1);}Set_Value ("JustifyItems", JustifyItems);}/** Get Justify Items.
@return Justify Items */
public String GetJustifyItems() {return (String)Get_Value("JustifyItems");}/** Set Rowspan.
@param Rowspan Rowspan */
public void SetRowspan (int Rowspan){Set_Value ("Rowspan", Rowspan);}/** Get Rowspan.
@return Rowspan */
public int GetRowspan() {Object ii = Get_Value("Rowspan");if (ii == null) return 0;return Convert.ToInt32(ii);}/** Set Sequence.
@param SeqNo Method of ordering elements; lowest number comes first */
public void SetSeqNo (Decimal? SeqNo){Set_Value ("SeqNo", (Decimal?)SeqNo);}/** Get Sequence.
@return Method of ordering elements; lowest number comes first */
public Decimal GetSeqNo() {Object bd =Get_Value("SeqNo");if (bd == null) return Env.ZERO;return  Convert.ToDecimal(bd);}/** Set Start Column.
@param StartColumn Start Column */
public void SetStartColumn (int StartColumn){Set_Value ("StartColumn", StartColumn);}/** Get Start Column.
@return Start Column */
public int GetStartColumn() {Object ii = Get_Value("StartColumn");if (ii == null) return 0;return Convert.ToInt32(ii);}/** Set Start At Row.
@param StartRow Start At Row */
public void SetStartRow (int StartRow){Set_Value ("StartRow", StartRow);}/** Get Start At Row.
@return Start At Row */
public int GetStartRow() {Object ii = Get_Value("StartRow");if (ii == null) return 0;return Convert.ToInt32(ii);}}
}