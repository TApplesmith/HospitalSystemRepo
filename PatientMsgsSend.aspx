<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PatientMsgsSend.aspx.cs" Inherits="EWork_PatientMsgsSend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <br />
    </p>
    <p>
        <asp:TextBox ID="FilterBox" runat="server"></asp:TextBox>
&nbsp;
        <asp:Button ID="FilterButton" runat="server" OnClick="FilterButton_Click" Text="Filter" />
&nbsp;
        <asp:Button ID="ResetFilterButton" runat="server" OnClick="ResetFilterButton_Click" Text="Reset Filter" />
    </p>
    <p>
        To:
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    </p>
    <p>
        <asp:TextBox ID="MsgText" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="SendButton" runat="server" OnClick="SendButton_Click" Text="Send"/>
    </p>
    <p>
        <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>

