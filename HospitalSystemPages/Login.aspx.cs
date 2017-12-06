using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using HospitalSystemRepo;
//added these for data manip
using System.Data.Entity;
using System.Linq;

public partial class Account_Login : Page
{
    HospitalSystemDatabaseEntities dbcontext = new HospitalSystemDatabaseEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = new UserManager();
                ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                if (user != null)
                {
                    IdentityHelper.SignIn(manager, user, RememberMe.Checked);

                //Add current username to session - used to validate what infomation to view
                //Yes, this is terrible security practice

                //check if UserName is contained w/in either doctor or patient tables
                bool patientTruth = dbcontext.PatientTables.Where(type => type.PatientUserName.Equals(UserName.Text)).Any();
                bool doctorTruth = dbcontext.DoctorTables.Where(type => type.DoctorUserName.Equals(UserName.Text)).Any();

                if ( patientTruth )
                {
                    Session.Add("UserType", "Patient");
                }
                else if( doctorTruth )
                {
                    Session.Add("UserType", "Doctor");
                }
                else
                {
                    Session.Add("UserType", "Unknown");
                }

                Session.Add("Username", UserName.Text);
                //Roses are red,
                //Violets are blue,
                //If you hardcore your tokens,
                //I'll use them too!

                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
            else
                {
                    FailureText.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                }
            }
        }
}