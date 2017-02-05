﻿<%@ Page Title="" Language="C#" MasterPageFile="~/View/Store/Clerk/Clerk.master" AutoEventWireup="true" CodeBehind="DisplaySupplierPOPeriod.aspx.cs" Inherits="LogicUniversityStore.View.Store.Reports.DisplaySupplierPOPeriod" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="clrkCssBlock" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="clrkMainHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="clrkMainContentBlock" runat="server">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="50px" ReportSourceID="SuppPOPeriod" ToolbarImagesFolderUrl="" ToolPanelWidth="200px" Width="350px" />
    <CR:CrystalReportSource ID="SuppPOPeriod" runat="server">
        <Report FileName="View\Store\Reports\PO_Supplier_Period.rpt">
        </Report>
    </CR:CrystalReportSource>
    <br />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="clrkMainJs" runat="server">
</asp:Content>
