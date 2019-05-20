﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using VAdvantage.Model;
using VAdvantage.Utility;
using VIS.Models;

namespace VIS.Controllers
{
    public class MOrderLineController:Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetOrderLine(string fields)
        {
           
            string retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetOrderLine(ctx, fields));
            }         
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetNotReserved(string fields)
        {
            
            string retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetNotReserved(ctx, fields));
            }           
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTax(string fields)
        {

            String retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetTax(ctx, fields));
            }
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }

        // change by amit 4-6-2016
        public JsonResult GetPrices(string fields)
        {

            String retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetPrices(ctx, fields));
            }
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }

        //when we change qtyOrder, product , QtyEntered
        public JsonResult GetPricesOnChange(string fields)
        {

            String retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetPricesOnChange(ctx, fields));
            }
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }

        //when we chage the UOM
        public JsonResult GetPricesOnUomChange(string fields)
        {

            String retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetPricesOnUomChange(ctx, fields));
            }
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductPriceOnUomChange(string fields)
        {

            String retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetProductPriceOnUomChange(ctx, fields));
            }
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }

        //product selection
        public JsonResult GetPricesOnProductChange(string fields)
        {

            String retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetPricesOnProductChange(ctx, fields));
            }
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }

        // Get Tax ID
        public JsonResult GetTaxId(string fields)
        {

            String retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetTaxId(ctx, fields));
            }
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }

        // product info
        public JsonResult GetProductInfo(string fields)
        {

            String retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetProductInfo(ctx, fields));
            }
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }

        // Changes by Mohit to remove client side queries - 16 May 2017
        //public JsonResult GetChargeAmt(string fields)
        //{
        //    String retJSON = "";
        //    if (Session["ctx"] != null)
        //    {
        //        VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
        //        MOrderLineModel objOrderLine = new MOrderLineModel();
        //        retJSON = JsonConvert.SerializeObject(objOrderLine.GetChargeAmount(ctx, fields));
        //    }
        //    return Json(retJSON, JsonRequestBehavior.AllowGet);
        //}

        //// Get enforce price limit from price list
        //public JsonResult GetEnfPriceLimit(string fields)
        //{
        //    String retJSON = "";
        //    if (Session["ctx"] != null)
        //    {
        //        VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
        //        MOrderLineModel objOrderLine = new MOrderLineModel();
        //        retJSON = JsonConvert.SerializeObject(objOrderLine.GetEnforcePriceLimit(fields));
        //    }
        //    return Json(retJSON, JsonRequestBehavior.AllowGet);
        //}

        //// get Attribute from product
        //public JsonResult GetProductASI(string fields)
        //{
        //    String retJSON = "";
        //    if (Session["ctx"] != null)
        //    {
        //        VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
        //        MOrderLineModel objOrderLine = new MOrderLineModel();
        //        retJSON = JsonConvert.SerializeObject(objOrderLine.GetProdAttributeSetInstance(fields));
        //    }
        //    return Json(retJSON, JsonRequestBehavior.AllowGet);
        //}

        //// Get Prices From price list
        //public JsonResult GetProductPrices(string fields)
        //{
        //    String retJSON = "";
        //    if (Session["ctx"] != null)
        //    {
        //        VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
        //        MOrderLineModel objOrderLine = new MOrderLineModel();
        //        retJSON = JsonConvert.SerializeObject(objOrderLine.GetProductprices(fields));
        //    }
        //    return Json(retJSON, JsonRequestBehavior.AllowGet);
        //}

        //Get Product Cost
        public JsonResult GetProductCost(int fields)
        {

            String retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetProductCost(ctx, fields));
            }
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }

        //Get No of Months--Neha
        public JsonResult GetNoOfMonths(string fields)
        {

            String retJSON = "";
            if (Session["ctx"] != null)
            {
                VAdvantage.Utility.Ctx ctx = Session["ctx"] as Ctx;
                MOrderLineModel objOrderLine = new MOrderLineModel();
                retJSON = JsonConvert.SerializeObject(objOrderLine.GetNoOfMonths(Util.GetValueOfInt(fields)));
            }
            return Json(retJSON, JsonRequestBehavior.AllowGet);
        }

    }
}