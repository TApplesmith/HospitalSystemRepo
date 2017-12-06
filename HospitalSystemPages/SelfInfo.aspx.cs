using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class HospitalSystemPages_SelfInfo : System.Web.UI.Page
{
    HospitalSystemDatabaseEntities dbcontext = new HospitalSystemDatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["UserType"] != null)
        {
            Label1.Text = Session["UserType"].ToString();
            string username = Session["UserName"].ToString();
            if (Session["UserType"].Equals("Patient"))
            {
                //dbcontext.PatientTables.Where(patient => patient.PatientUserName.Equals(Session["UserName"])).Load();
                dbcontext.PatientTables.Where(patient => patient.PatientUserName.Equals(username)).Load();
                GridView1.DataSource = dbcontext.PatientTables.Local;
            }
            else
            {
                dbcontext.DoctorTables.Where(doctor => doctor.DoctorUserName.Equals(username)).Load();
                GridView1.DataSource = dbcontext.DoctorTables.Local;
            }
            GridView1.DataBind();
        }



        //DEBUG
        ListBox1.Visible = false;

        ListBox1.Items.Add(Session.Count.ToString());
        foreach (string key in Session.Keys)
        {
            ListBox1.Items.Add(Session[key].ToString());
        }
    }
}