using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class EWork_PatientMsgsSend : System.Web.UI.Page
{
    HospitalSystemDatabaseEntities dbcon = new HospitalSystemDatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        ListBox1.Visible = true;
        resetDropDown();

        if (IsPostBack != true)
        {

        }
    }

    private void resetDropDown()
    {
        DropDownList1.DataSourceID = null;
        if (Session.Count != 0)
        {
            ListBox1.Items.Add("Session Count != 0");
            string userType = Session["UserType"].ToString();
            if (userType != null)
            {
                ListBox1.Items.Add("userType != null");
                if (userType.Equals("Doctor"))
                {
                    resetToPatients();
                }
                else if (userType.Equals("Patient"))
                {
                    resetToDoctors();
                }
            }
        }
        ListBox1.Items.Add("End DropDown reset.");
    }

    private void resetToPatients()
    {
        ListBox1.Items.Add("Populated by patients");
        DropDownList1.Items.Clear();

        List<PatientTable> patientList = new List<PatientTable>();
        patientList = dbcon.PatientTables.ToList();

        foreach (var person in patientList)
        {
            DropDownList1.Items.Add(person.Name);
        }
        //DEBUG
        ListBox1.Items.Add("Doctor");
    }

    private void resetToDoctors()
    {
        ListBox1.Items.Add("Populated by Doctors");
        DropDownList1.Items.Clear();

        List<DoctorTable> doctorList = new List<DoctorTable>();
        doctorList = dbcon.DoctorTables.ToList();

        foreach (var person in doctorList)
        {
            DropDownList1.Items.Add(person.Name);
        }
        //DEBUG
        ListBox1.Items.Add("Patient");
    }

    protected void FilterButton_Click(object sender, EventArgs e)
    {
        //Filter Button
        ListBox1.Items.Add("Filtering Begun: ");

        string userType = Session["UserType"].ToString();
        if (userType != null)
        {
            string selection = FilterBox.Text;
            ListBox1.Items.Add("      " + selection);

            if (userType.Equals("Doctor"))
            {
                DropDownList1.Items.Clear();

                //updates patients based on text
                var Patient = from tempPatient in dbcon.PatientTables
                              where tempPatient.Name.ToLower().Contains(selection.ToLower())
                              select tempPatient;
                foreach (var person in Patient)
                {
                    DropDownList1.Items.Add(person.Name);
                }
            }
            else if (userType.Equals("Patient"))
            {
                DropDownList1.Items.Clear();

                //updates doctors based on text
                var Doctor = from tempDoc in dbcon.DoctorTables
                             where tempDoc.Name.ToLower().Contains(selection.ToLower())
                             select tempDoc;
                foreach (var person in Doctor)
                {
                    DropDownList1.Items.Add(person.Name);
                }
            }
        }
    }

    protected void ResetFilterButton_Click(object sender, EventArgs e)
    {
        FilterBox.Text = "";
        ListBox1.Items.Add("clicked");
        resetDropDown();
    }

    protected void SendButton_Click(object sender, EventArgs e)
    {
        //testing with listbox
        ListBox1.Items.Add(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString());
        ListBox1.Items.Add(new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).ToString());
        ListBox1.Items.Add(MsgText.Text);
        ListBox1.Items.Add(Session["DoctorUsername"].ToString());
        ListBox1.Items.Add(Session["Username"].ToString());
        ListBox1.Items.Add(Session["Username"].ToString());
        ListBox1.Items.Add(Convert.ToInt32(Session["DoctorID"]).ToString());
        ListBox1.Items.Add(Convert.ToInt32(Session["PatientID"]).ToString());

        EmailTable myEmail = new EmailTable();

        myEmail.EmailDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        myEmail.EmailTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        myEmail.EmailText = MsgText.Text;
        myEmail.DoctorId = Convert.ToInt32(Session["DoctorID"]);
        myEmail.PatientId = Convert.ToInt32(Session["PatientID"]);

        int doctorIDForPatient = Convert.ToInt32(Session["DoctorID"]);

        //add stuff for patient username and doctor username

        //grab doctor for current patient's doctor
        var result = (from x in dbcon.DoctorTables
                      where x.DoctorId == doctorIDForPatient
                      select x).First();

        Session.Add("DoctorUsername", result.DoctorUserName);

        //get doctor username
        myEmail.DoctorUserName = Session["DoctorUsername"].ToString();
        myEmail.PatientUserName = Session["Username"].ToString();
        myEmail.Sender = Session["Username"].ToString();

        dbcon.EmailTables.Add(myEmail);
        dbcon.SaveChanges();

        // Response.Redirect("PatientMsgsConfirm.aspx");
    }
}