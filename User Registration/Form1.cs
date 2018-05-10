using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace User_Registration
{
    public partial class frmUserRegistration : Form
    {
        string connectionString = @"Data Source = (local)\sqlexpress; Initial Catalog = UserRegistrationDemoDB; Integrated Security=True;";

        public frmUserRegistration()
        {
            InitializeComponent();

            //sets max length of passwords 
            txtPassword.MaxLength = 15;
            txtConfirmPassword.MaxLength = 15;

            //sets the character used to mask the characters of the 
            //passwords entered in the single-line textbox controls.
            txtPassword.PasswordChar = '*';
            txtConfirmPassword.PasswordChar = '*';
        }

        /// <summary>
        /// Handles the click event of the button btnSubmit.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //Check for mandatory fields. 
            if (String.IsNullOrEmpty(txtUsename.Text)|| String.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please fill Mandatory Fields.");
            }
            else if (txtPassword.Text != txtConfirmPassword.Text) {
                MessageBox.Show("Passwords do not match.");
            }
            else
            {
                //Inserts data at Database.
                InsertData();

                //Clears text from the textboxes.
                ClearTextBoxes();
            }
        }

        /// <summary>
        /// Inserts Data to the Database at tblUser table 
        /// through the Stored Procedure 'UserAdd'.
        /// </summary>
        private void InsertData()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                //open connection with DB.
                sqlCon.Open();

                //sets store procedure to be executed against DB.
                SqlCommand sqlCmd = new SqlCommand("UserAdd", sqlCon);

                //sets teh command type of the sql command.
                sqlCmd.CommandType = CommandType.StoredProcedure;

                //gets the parameters of the store procedure.
                sqlCmd.Parameters.AddWithValue("@_FirstName", txtFirstName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@_LastName", txtLastName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@_Phone", txtPhoneNumber.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@_Address", txtAddress.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@_Username", txtUsename.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@_Password", txtPassword.Text.Trim());

                //execute stored procedure
                sqlCmd.ExecuteNonQuery();

                MessageBox.Show("Registration is successfull");
            }
        }

        /// <summary>
        /// Clears all the text form the textbox controls.
        /// </summary>
        void ClearTextBoxes()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            txtUsename.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
        }
    }
}
