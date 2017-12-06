using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class HospitalSystemPages_Messaging : System.Web.UI.Page
{
    HospitalSystemDatabaseEntities dbcontext = new HospitalSystemDatabaseEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        ListBox1.Visible = false;
        resetDropDown();
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
    }

    private void resetToPatients()
    {
        ListBox1.Items.Add("Populated by patients");
        DropDownList1.Items.Clear();
        //DEBUG
        ListBox1.Items.Add("Doctor");

        List<PatientTable> patientList = new List<PatientTable>();
        patientList = dbcontext.PatientTables.ToList();

        foreach (var person in patientList)
        {
            DropDownList1.Items.Add(person.Name);
        }
    }

    private void resetToDoctors()
    {
        ListBox1.Items.Add("Populated by Doctors");
        DropDownList1.Items.Clear();
        //DEBUG
        ListBox1.Items.Add("Patient");

        List<DoctorTable> doctorList = new List<DoctorTable>();
        doctorList = dbcontext.DoctorTables.ToList();

        foreach (var person in doctorList)
        {
            DropDownList1.Items.Add(person.Name);
        }
    }

    /*
    private void updatePatient(int selection)
    {
        //updates patient based on DoctorID
        DropDownList1.DataSourceID = null;
        dbcontext.PatientTables.Where(depart => depart.DoctorId == selection).Load();
        DropDownList1.DataSource = dbcontext.DepartmentTables.Local;
        DropDownList1.DataBind();
    }
    */

    /*
    private List<PatientTable> patientNames()
    {
        List<PatientTable> patName = new List<PatientTable>();

        patName = dbcontext.PatientTables.Local.ToList();

        return patName;
    }
    */
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        /*
        string userType = Session["UserType"].ToString();
        if (Session["UserType"] != null)
        {
            string username = Session["UserName"].ToString();

        }*/
    }

    /*
    protected void TestingSubmission()
    {
        AppointmentTable myApp = new AppointmentTable();

        myApp.AppointmentDate = new DateTime(2006, 12, 1);
        myApp.AppointmentTime = new TimeSpan(12, 12, 12);
        myApp.Description = "test";
        myApp.DoctorId = 3;
        myApp.DoctorUserName = "BDees";
        myApp.PatientId = 10;
        myApp.PatientUserName = "PJans";


        dbcontext.AppointmentTables.Add(myApp);
        dbcontext.SaveChanges();
    }
    */

    protected void FilterButton_Click(object sender, EventArgs e)
    {
        string userType = Session["UserType"].ToString();
        if (userType != null)
        {
            string selection = FilterBox.Text;
            if (userType.Equals("Doctor"))
            {
                DropDownList1.Items.Clear();

                //updates patients based on text
                var Patient = from tempPatient in dbcontext.PatientTables
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
                var Doctor = from tempDoc in dbcontext.DoctorTables
                             where tempDoc.Name.ToLower().Contains(selection.ToLower())
                             select tempDoc;
                foreach (var person in Doctor)
                {
                    DropDownList1.Items.Add(person.Name);
                }
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        FilterBox.Text = "";
        resetDropDown();
    }
}