﻿using LogicUniversityStore.Controller;
using LogicUniversityStore.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LogicUniversityStore.View.Department.Employee
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        CancelUpdatePendingApprovalController reqController = new CancelUpdatePendingApprovalController();
        RequisitionItem ri = new RequisitionItem();
        DataTable dt;
        DataRow r;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int reqId = Convert.ToInt32(Request.QueryString["id"]);
                List<RequisitionItem> items = reqController.getRequisitionItemList(reqId);
                dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ItemName"), new DataColumn("NeededQuantity"), new DataColumn("UOM") });
                foreach (RequisitionItem i in items)
                {
                    r = dt.NewRow();
                    r["ItemName"] = i.SupplierItem.Item.ItemName;
                    r["NeededQuantity"] = i.NeededQuantity;
                    r["UOM"] = i.SupplierItem.Item.UOM;
                    dt.Rows.Add(r);
                }
                ViewState["dt"] = dt;
                bindGrid();
            }
        }

        protected void bindGrid()
        {
            gvRequisitionDetails.DataSource = (DataTable)ViewState["dt"];
            gvRequisitionDetails.DataBind();
        }

        protected void gvRequisitionDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int rowIndex = e.RowIndex;
            dt = (DataTable)ViewState["dt"];
            dt.Rows[rowIndex].Delete();
            ViewState["dt"] = dt;
            bindGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int reqId = Convert.ToInt32(Request.QueryString["id"]);
            String remark = txtRemark.Text;
            reqController.updateRemark(reqId, remark);
            reqController.removeRequisitionItems(reqId);
            int rowCount = gvRequisitionDetails.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                RequisitionItem ri = new RequisitionItem();
                ri.ReqID = reqId;
                String itemName = gvRequisitionDetails.Rows[i].Cells[1].Text;
                Item item = reqController.GetItem(itemName);
                int itemId = item.ItemID;
                ri.SupplierItemID = itemId;
                TextBox tb = gvRequisitionDetails.Rows[i].FindControl("txtQty") as TextBox;
                ri.NeededQuantity = Convert.ToInt32(tb.Text);
                reqController.addRequisitionItem(ri);
            }
            Response.Redirect("CancelUpdatePendingApproval.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("CancelUpdatePendingApproval.aspx");
        }
    }
}