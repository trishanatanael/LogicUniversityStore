﻿using LogicUniversityStore.Controller;
using LogicUniversityStore.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LogicUniversityStore.View.Department.Hod
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        ApproveRejectReqController reqController = new ApproveRejectReqController();
        int reqId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //reqId = Convert.ToInt32(Request.QueryString["id"]);
                //gvRequisitionDetails.DataSource=reqController.getRequisitionItemList(reqId);
                //gvRequisitionDetails.DataBind();

                reqId = Convert.ToInt32(Request.QueryString["id"]);
                List<RequisitionItem> items = reqController.getRequisitionItemList(reqId);

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("ItemName"), new DataColumn("NeededQuantity")});
                foreach(RequisitionItem i in items)
                {
                    DataRow r = dt.NewRow();
                    r["ItemName"] = i.SupplierItem.Item.ItemName;
                    r["NeededQuantity"] = i.NeededQuantity;
                    dt.Rows.Add(r);
                }
                gvRequisitionDetails.DataSource = dt;
                gvRequisitionDetails.DataBind();
            }

        }

        

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            reqId= Convert.ToInt32(Request.QueryString["id"]);
            reqController.approveRequisition(reqId);
            Response.Redirect("ApproveReject.aspx");
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            reqId = Convert.ToInt32(Request.QueryString["id"]);
            reqController.rejectRequisition(reqId);
            Response.Redirect("ApproveReject.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApproveReject.aspx");
        }
    }
}