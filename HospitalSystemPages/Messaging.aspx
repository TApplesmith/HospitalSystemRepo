<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Messaging.aspx.cs" Inherits="HospitalSystemPages_Messaging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        &nbsp;</p>
    <p>
        <asp:TextBox ID="FilterBox" runat="server"></asp:TextBox>
        <asp:Button ID="FilterButton" runat="server" OnClick="FilterButton_Click" Text="Filter" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" OnClick="Button2_Click" Text="Reset Filter" />
        <br />
    </p>
    <p>
        To:
        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Name] FROM [PatientTable]"></asp:SqlDataSource>
    </p>
    <p>
        <asp:TextBox ID="TextBox1" runat="server" Height="60px" Width="720px" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Send" />
    </p>
    <p>
        <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
    </p>
    <p>
    </p>
</asp:Content>

