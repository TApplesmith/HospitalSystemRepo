using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

public partial class MyWork_SearchForPatient : System.Web.UI.Page
{
    private HospitalSystemDatabaseEntities dbcontext = new HospitalSystemDatabaseEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        dbcontext.PatientTables.Load();
        //Load table into page
        GridView1.DataSourceID = null;
        GridView1.DataSource = dbcontext.PatientTables.Local;
        GridView1.DataBind();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string search = TextBox1.Text;
        //Does not do anything if Textbox1 is empty
        if (search.Equals(""))
            return;

        //Searches PatientTable for patients by ID
        var patient = dbcontext.PatientTables.Local.FirstOrDefault();
        try
        {
            patient = dbcontext.PatientTables.Local.FirstOrDefault((p) => p.PatientId == Convert.ToInt32(search));
        }
        catch (System.FormatException)
        {
            // patient = dbcontext.PatientTables.Local.FirstOrDefault((p) => p.Name.Contains(search));   //Todo: Select multiple by lastname, lookup SelectMany()
            //var Patient = from tempPatient in dbcontext.PatientTables.Local
            //          where tempPatient.Name.Contains(search)
            //          select tempPatient;
        }

        // patient = dbcontext.PatientTables.Local.FirstOrDefault((p) => p.Name.Contains(search));   //Todo: Select multiple by lastname, lookup SelectMany()
        var Patient = from tempPatient in dbcontext.PatientTables.Local
                      where tempPatient.Name.ToLower().Contains(search.ToLower())
                      select tempPatient;

        //Displays in ListBox relevant patients
        if (patient == null)
        {
            //True only if try-catch works but finds no patient
            ListBox1.Items.Clear();
            ListBox1.Items.Add("No patients found.");
        }
        else if (Patient.Any() != false)
        {
            //True only if letter input

            ListBox1.Items.Clear();
            //ListBox1.Items.Add(patient.Name+"; "+patient.PatientUserName);

            List<PatientTable> patientList = Patient.ToList();
            foreach ( var person in patientList)
            {
                ListBox1.Items.Add(person.PatientId+"; "+person.Name + "; " + person.PatientUserName);
                //patientList.Remove(person);
            }


           // foreach( var name in Patient)
           // {
           //     ListBox1.Items.Add(Patient.First().Name);
           //     Patient.
           // }
        }
        else
        {
            //True only if patient != null & was not searching by name
            ListBox1.Items.Clear();
            ListBox1.Items.Add(patient.PatientId+"; "+patient.Name + "; " + patient.PatientUserName);
        }
    }

    //Response.Redirect("Website.aspx");

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}


/* Previous Work
 * 
 * 
 * 
 * public partial class MyWork_address : System.Web.UI.Page
{
    AddressBookEntities dbcontext = new AddressBookEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowAll();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //dbcontext.Addresses.Where(item => item.LastName.StartsWith(TextBox1.Text)).Load();
        dbcontext.Addresses.Where(item => item.LastName.Trim().Equals(TextBox1.Text.Trim())).Load();
        //add data to the Gridview
        GridView1.DataSource = dbcontext.Addresses.Local;
        GridView1.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ShowAll();
    }

    private void ShowAll()
    {
        dbcontext.Addresses.Load();
        //add data to the Gridview
        GridView1.DataSource = dbcontext.Addresses.Local;
        GridView1.DataBind();
    }
}
*/