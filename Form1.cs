using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows.Forms;
using HMS.Custom_Classes;
using HMS.Custom_Classes.Service_Classes;
namespace HMS
{
    public partial class Form1 : Form 
    {
        ClsRestService _ClsRestService = new ClsRestService();
        ClsGeneralLibrary _ClsGeneralLibrabry = new ClsGeneralLibrary();
        ClsDataAccess _clsDataAccess = new ClsDataAccess();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Button b = new Button();
            //b.Text = "test";
            //b.FlatStyle = FlatStyle.Flat;
           
            //panel1.Controls.Add(b);

         //   AddButtons();
           // dataGridView1.DataSource = _ClsRestService.GetHttpRequestDataTable("http://services.groupkt.com/state/get/IND/all");   
            //var dt = new DataTable();
            //dt = _ClsRestService.GetHttpRequestDataTable("http://default-environment.srjbvgwytq.us-east-1.elasticbeanstalk.com/rest/secured/location/states/101");
            //dataGridView1.DataSource = dt;
            //string temp = "http://default-environment.srjbvgwytq.us-east-1.elasticbeanstalk.com/rest/secured/roomtypes";

            //_ClsGeneralLibrabry.fillComboBox(dt, comboBox1, "name", "name");
            
            

            //IEnumerable<KeyValuePair<string, string>> Params = new List<KeyValuePair<string, string>>()
            //{
            //    new KeyValuePair<string,string> ("para1","val1"),
            //    new KeyValuePair<string,string> ("para2","val2"),
            //};

            //var dict = new Dictionary<string, string>();
            //dict.Add("id", "1");
            //dict.Add("name", "Standard");
            //dict.Add("pricePerNight", "1000");

           // var output = Newtonsoft.Json.JsonConvert.SerializeObject(dict);

          //  MessageBox.Show(output.ToString());
          //int f =  _ClsRestService.PutRequest(temp, dict);

          //if (f == 0)
          //{
          //    MessageBox.Show(_ClsRestService.globalResponseMessage);
          //}


            //var Surveys = new List<SurveytrackD>();

            //Surveys.Add(new SurveytrackD { id = "26", appsurvey = "1" });
            //Surveys.Add(new SurveytrackD { id = "27", appsurvey = "1" });


            //foreach (var survey in Surveys)
            //{
            //    survey.questions = new List<Question>();

            //    survey.questions.Add(new Question { questionid = "1", feedback = "0" });
            //    survey.questions.Add(new Question { questionid = "2", feedback = "1" });
            //}

            //var json = JsonConvert.SerializeObject(Surveys, Formatting.Indented);

           // SurveytrackD s = new SurveytrackD { id = "1", appsurvey = "test" };
           // Question q = new Question { questionid = "2", feedback = "tetsf" };
           // var dic = new Dictionary<string,object>();
           // var Surveys = new List<object>();
           //// //Surveys.Add(s);
           //// //Surveys.Add(q);
           // dic.Add("survey", s);
           // dic.Add("question", q);
           // var json = JsonConvert.SerializeObject(dic, Formatting.Indented);


            string temp = _clsDataAccess.GetConnectionString();
          
        }

        private void AddButtons()
        {
            int xPos = 0;
            int yPos = 0;
           
            // Declare and assign number of buttons = 26 
            System.Windows.Forms.Button[] btnArray = new System.Windows.Forms.Button[26];
            
            // Create (26) Buttons: 
            for (int i = 0; i < 26; i++)
            {
                // Initialize one variable 
                btnArray[i] = new System.Windows.Forms.Button();
            }
            int n = 0;
            int f = 0;
            int t = 0;
            while (n < 26)
            {
                btnArray[n].Tag = n + 1; // Tag of button 
                btnArray[n].Width = 60; // Width of button 
                btnArray[n].Height = 40; // Height of button 
                if (n%4==0) // Location of second line of buttons: 
                {
                    
                    xPos = 0;
                    
                    yPos = 40;
                    t = f;
                    f++;
                   
                    if ((f > t) )
                    {
                        yPos = (yPos * f);
                    }
                    
                }
                // Location of button: 
                btnArray[n].Left = xPos;
                btnArray[n].Top = yPos;
                btnArray[n].FlatStyle = FlatStyle.Flat;
                // Add buttons to a Panel: 
                panel1.Controls.Add(btnArray[n]);
                
                // Let panel hold the Buttons 
                xPos = xPos + btnArray[n].Width; // Left of next button 
                // Write English Character: 
                btnArray[n].Text = ((char)(n + 65)).ToString();
                toolTip1.SetToolTip(btnArray[n], btnArray[n].Text);
                /* **************************************************************** 
                You can use following code instead previous line 
                char[] Alphabet = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 
                'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 
                'W', 'X', 'Y', 'Z'}; btnArray[n].Text = Alphabet[n].ToString(); 
                //**************************************************************** */

                // the Event of click Button 
                btnArray[n].Click += new System.EventHandler(ClickButton);
                n++;
            }
          //  btnAddButton.Enabled = false; // not need now to this button now 
          //  label1.Visible = true;
        }

        // Result of (Click Button) event, get the text of button 
        public void ClickButton(Object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            MessageBox.Show("You clicked character [" + btn.Text + "]");
        }  
        private int incflag(int f)
        {
            return f++;
        }
     }    
}
