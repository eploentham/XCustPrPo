﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCustPr
{
    class XCustPrToCloud:Form
    {
        int gapLine = 5;
        int grd0 = 0, grd1 = 100, grd2 = 240, grd3 = 320, grd4 = 570, grd5 = 700, grd51 = 700, grd6 = 820, grd7 = 900, grd8 = 1070, grd9 = 1200;
        int line1 = 35, line2 = 27, line3 = 85, line4 = 105, line41 = 120, line5 = 270, ControlHeight = 21, lineGap = 5;

        int formwidth = 860, formheight = 600;

        MaterialLabel lb1;
        MaterialSingleLineTextField txtFileName;
        MaterialFlatButton btnRead, btnPrepare, btnWebService, btnFTP, btnEmail;
        MaterialListView lv1;
        
        Color cTxtL, cTxtE, cForm;

        ControlRDPO cRDPO;
        String[] filePO;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XCustPrToCloud
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "XCustPrToCloud";
            this.ResumeLayout(false);
        }
        
        public XCustPrToCloud(ControlRDPO crdpo)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(formwidth, formheight);
            this.StartPosition = FormStartPosition.CenterScreen;
            cRDPO = crdpo;
            
            initConfig();
            cTxtL = txtFileName.BackColor;
            cTxtE = Color.Yellow;
        }
        private void initConfig()
        {
            initCompoment();
            //txtFileName.Text = cRDPO.initC.PathInitial + "PR03102017.txt";
            txtFileName.Text = cRDPO.initC.PathInitial ;
            cRDPO.CreateIfMissing(cRDPO.initC.PathArchive);
            cRDPO.CreateIfMissing(cRDPO.initC.PathError);
            cRDPO.CreateIfMissing(cRDPO.initC.PathInitial);
            cRDPO.CreateIfMissing(cRDPO.initC.PathProcess);

            lv1.Columns.Add("List File", formwidth - 40 - 100, HorizontalAlignment.Left);
            lv1.Columns.Add("   process   ", 100, HorizontalAlignment.Center);
            //lv1.Columns.Add(" Azimuth ", 100, HorizontalAlignment.Center);

            filePO = cRDPO.getFileinFolder(cRDPO.initC.PathInitial);
            foreach(string aa in filePO)
            {
                lv1.Items.Add(aa);
            }
        }
        private void initCompoment()
        {
            line1 = 35 + gapLine;
            line2 = 57 + gapLine;
            line3 = 85 + gapLine;
            line4 = 125 + gapLine;
            line41 = 120 + gapLine;
            line5 = 270 + gapLine;

            lb1 = new MaterialLabel();
            lb1.Font = cRDPO.fV1;
            lb1.Text = "Text File";
            lb1.AutoSize = true;
            Controls.Add(lb1);
            lb1.Location = new System.Drawing.Point(cRDPO.formFirstLineX, cRDPO.formFirstLineY + gapLine);

            txtFileName = new MaterialSingleLineTextField();
            txtFileName.Font = cRDPO.fV1;
            txtFileName.Text = "";
            txtFileName.Size = new System.Drawing.Size(800 - grd1-20-30, ControlHeight);
            Controls.Add(txtFileName);
            txtFileName.Location = new System.Drawing.Point(grd1, cRDPO.formFirstLineY + gapLine);
            txtFileName.Hint = lb1.Text;
            txtFileName.Enter += txtFileName_Enter; ;
            txtFileName.Leave += txtFileName_Leave;


            btnRead = new MaterialFlatButton();
            btnRead.Font = cRDPO.fV1;
            btnRead.Text = "1. mod up Read Text";
            btnRead.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnRead);
            btnRead.Location = new System.Drawing.Point(grd1 , line1);
            btnRead.Click += btnRead_Click;

            btnPrepare = new MaterialFlatButton();
            btnPrepare.Font = cRDPO.fV1;
            btnPrepare.Text = "2. prepare Data, zip file";
            btnPrepare.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnPrepare);
            btnPrepare.Location = new System.Drawing.Point(grd3, line1);
            btnPrepare.Click += btnPrepare_Click;

            btnWebService = new MaterialFlatButton();
            btnWebService.Font = cRDPO.fV1;
            btnWebService.Text = "3. Web Service";
            btnWebService.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnWebService);
            btnWebService.Location = new System.Drawing.Point(grd4, line1);
            btnWebService.Click += btnWebService_Click;

            btnFTP = new MaterialFlatButton();
            btnFTP.Font = cRDPO.fV1;
            btnFTP.Text = "4. FTP to linfox";
            btnFTP.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnFTP);
            btnFTP.Location = new System.Drawing.Point(grd5, line1);
            btnFTP.Click += btnFTP_Click;

            btnEmail = new MaterialFlatButton();
            btnEmail.Font = cRDPO.fV1;
            btnEmail.Text = "5. Send email ";
            btnEmail.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnEmail);
            btnEmail.Location = new System.Drawing.Point(grd1, line3);
            btnEmail.Click += btnEmail_Click;

            lv1 = new MaterialListView();
            lv1.Font =cRDPO.fV1;
            lv1.FullRowSelect = true;
            lv1.Size = new System.Drawing.Size(formwidth-40, formheight- line3-80);
            lv1.Location = new System.Drawing.Point(cRDPO.formFirstLineX+5, line4);
            lv1.FullRowSelect = true;
            lv1.View = View.Details;
            //lv1.Dock = System.Windows.Forms.DockStyle.Fill;
            lv1.BorderStyle = System.Windows.Forms.BorderStyle.None;

            Controls.Add(lv1);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            // move file
            cRDPO.processRDPO(filePO);
            
        }
        private void btnPrepare_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            // move file
            //cRDPO.processRDPO(filePO);
            //string zipPath = @cRDPO.initC.PathZip;
            //string extractPath = @cRDPO.initC.PathZipExtract;
            var allFiles = Directory.GetFiles(@cRDPO.initC.PathZip, "*.zip", SearchOption.AllDirectories);
            foreach(String file in allFiles)
            {
                using (ZipArchive archive = ZipFile.OpenRead(file))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            entry.ExtractToFile(Path.Combine(@cRDPO.initC.PathZipExtract, entry.FullName));
                        }
                    }
                }
            }
        }
        private void btnWebService_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            // move file
            //cRDPO.processRDPO(filePO);
            FtpWebRequest request;
        }
        private void btnFTP_Click(object sender, EventArgs e)
        {
            var allFiles = Directory.GetFiles(@cRDPO.initC.PathZip, "*.zip", SearchOption.AllDirectories);
            foreach (String file in allFiles)
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://" + cRDPO.initC.FTPServer+"/"+ file.Replace(cRDPO.initC.PathZip,""));
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.Credentials = new NetworkCredential("pop", "pop");
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;

                StreamReader sourceStream = new StreamReader(file);
                //byte[] fileContents = sourceStream.ReadToEnd();
                byte[] fileContents = File.ReadAllBytes(file);
                sourceStream.Close();
                ftpRequest.ContentLength = fileContents.Length;

                Stream requestStream = ftpRequest.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                //ftpRequest.Close();
                response.Close();
            }
            //ftpRequest.cl
        }
        private void btnEmail_Click(object sender, EventArgs e)
        {
            var email = new MailMessage();
            email.From = new MailAddress(cRDPO.initC.EmailSender);
            email.Subject = "Test send Email";
            email.To.Add(new MailAddress(cRDPO.initC.APPROVER_EMAIL));
            email.IsBodyHtml = true;
            email.BodyEncoding = System.Text.Encoding.UTF8;

            email.Body = "Body Test .....";

            var smtp = new SmtpClient(cRDPO.initC.EmailHost);
            var credential = new NetworkCredential(cRDPO.initC.EmailUsername, cRDPO.initC.EmailPassword);
            smtp.Port = int.Parse(cRDPO.initC.EmailPort);
            smtp.Port = 465;
            //smtp.Credentials = new NetworkCredential(cRDPO.initC.EmailUsername, cRDPO.initC.EmailPassword);
            smtp.Credentials = credential;
            smtp.Host = cRDPO.initC.EmailHost; // SMTP
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;



            smtp.Send(email);
            smtp.Dispose();
            email.Dispose();
        }
        private void txtFileName_Leave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtFileName.BackColor = cTxtL;
        }

        private void txtFileName_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtFileName.BackColor = cTxtE;
        }
    }
}
