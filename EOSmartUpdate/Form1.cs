using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;

namespace EOSmartUpdate
{
    public partial class frmMain : Form
    {
        // The client used to download text
        private static readonly HttpClient client = new HttpClient();
        // The client used to download files
        private static WebClient download = new WebClient();
        // A regex to check for config files - this is to ensure that Preserve Settings ignores config files
        private static string configRegex = @"^config/";
        // The base URL where the EOSmartUpdateServer is located
        private static string baseURL = "https://dev.skyline969.ca/eo/";
        // The executable to run when the Play button is clicked
        private static string clientExecutable = "Endless.exe";
        // The name of the game - this can be changed for another private server, application, etc
        private static string clientName = "EndlessOnline";

        private bool updateRunning = false;
        private string latestVersion = "";
        private Dictionary<string, string> updateFiles;

        public frmMain()
        {
            InitializeComponent();
            this.Text = clientName + " Updater";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            GetLatestUpdate();
        }

        /// <summary>
        /// This function will retrieve the latest update from the server.
        /// After retrieving the latest update, it will check local files against the server's files
        /// and download files as needed.
        /// </summary>
        private async void GetLatestUpdate()
        {
            updateRunning = true;

            txtStatus.AppendText("Fetching latest update...." + Environment.NewLine);

            // Lock input
            latestVersion = "";
            bool errors = false;
            chkPreserveSettings.Enabled = false;
            btnForceUpdate.Enabled = false;
            btnForceUpdate.BackColor = Color.Gray;
            btnPlay.Enabled = false;
            btnPlay.BackColor = Color.Gray;

            // Get the latest version
            try
            {
                latestVersion = await client.GetStringAsync(baseURL + "updates/latest.txt");
            }
            catch (HttpRequestException ex)
            {
                txtStatus.AppendText("Failed to get latest version!" + Environment.NewLine);
                txtStatus.AppendText(ex.Message + Environment.NewLine);
            }

            // Only proceed if we did get the latest version
            if (latestVersion != "")
            {
                txtStatus.AppendText("Latest update on server is " + latestVersion + Environment.NewLine);
                txtStatus.AppendText("Fetching file list...." + Environment.NewLine);

                string updatesJSON = "";

                // Get the md5list for the latest version - this contains a list of files and their MD5 hashes
                try
                {
                    updatesJSON = await client.GetStringAsync(baseURL + "updates/" + latestVersion + ".md5list");
                }
                catch (HttpRequestException ex)
                {
                    errors = true;
                    txtStatus.AppendText("Failed to get latest version file list!" + Environment.NewLine);
                    txtStatus.AppendText(ex.Message + Environment.NewLine);
                }

                // Only proceed if we managed to get the md5hash
                if (updatesJSON != "")
                {
                    updateFiles = JsonConvert.DeserializeObject<Dictionary<string, string>>(updatesJSON);
                    foreach (KeyValuePair<string, string> file in updateFiles)
                    {
                        // Check the local file's MD5 against the remote MD5
                        // Local file: file.Key
                        // Remote MD5: file.Value
                        if (!File.Exists(file.Key))
                        {
                            txtStatus.AppendText("Downloading missing file " + file.Key + Environment.NewLine);

                            // Download files one at a time - wait if the client is currently downloading a file
                            try
                            {
                                while (download.IsBusy) { await Task.Delay(100); }
                                DownloadFromServer(baseURL + "updates/" + latestVersion + "/" + file.Key, file.Key);
                            }
                            catch (WebException ex)
                            {
                                errors = true;
                                txtStatus.AppendText("Failed to download file " + baseURL + "updates/" + latestVersion + "/" + file.Key + "!" + Environment.NewLine + ex.Message + Environment.NewLine);
                            }
                        }
                        else
                        {
                            // Calculate MD5 of local file
                            string localMD5 = "";
                            using (var md5 = MD5.Create())
                            {
                                using (var stream = File.OpenRead(file.Key))
                                {
                                    byte[] hash = md5.ComputeHash(stream);
                                    localMD5 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                                }
                            }

                            if (localMD5 == "")
                            {
                                errors = true;
                                txtStatus.AppendText("ERROR: Failed to compute MD5 of local file " + file.Key + Environment.NewLine);
                            }
                            else
                            {
                                if (localMD5 != file.Value)
                                {
                                    if (chkPreserveSettings.Checked)
                                    {
                                        // If Preserve Settings is checked, ignore any config/ files.
                                        Regex r = new Regex(configRegex, RegexOptions.IgnoreCase);
                                        Match m = r.Match(file.Key);
                                        if (!m.Success)
                                        {
                                            txtStatus.AppendText("Updating file " + file.Key + Environment.NewLine);

                                            // Download file
                                            try
                                            {
                                                // We can only download one file at a time, so wait for the current download to finish before moving on to the next
                                                // This method of doing things does not lock up the UI
                                                while (download.IsBusy) { await Task.Delay(100); }
                                                DownloadFromServer(baseURL + "updates/" + latestVersion + "/" + file.Key, file.Key);
                                            }
                                            catch (WebException ex)
                                            {
                                                errors = true;
                                                txtStatus.AppendText("Failed to download file " + baseURL + "updates/" + latestVersion + "/" + file.Key + "!" + Environment.NewLine + ex.Message + Environment.NewLine);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        txtStatus.AppendText("Updating file " + file.Key + Environment.NewLine);

                                        // Download file
                                        try
                                        {
                                            // We can only download one file at a time, so wait for the current download to finish before moving on to the next
                                            // This method of doing things does not lock up the UI
                                            while (download.IsBusy) { await Task.Delay(100); }
                                            DownloadFromServer(baseURL + "updates/" + latestVersion + "/" + file.Key, file.Key);
                                        }
                                        catch (WebException ex)
                                        {
                                            errors = true;
                                            txtStatus.AppendText("Failed to download file " + baseURL + "updates/" + latestVersion + "/" + file.Key + "!" + Environment.NewLine + ex.Message + Environment.NewLine);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!errors)
                txtStatus.AppendText("Update to " + latestVersion + " complete! Click Play to get started." + Environment.NewLine);
            else
                txtStatus.AppendText("One or more errors occurred during update. Review the log before continuing." + Environment.NewLine);

            // Unlock input
            updateRunning = false;
            chkPreserveSettings.Enabled = true;
            btnForceUpdate.Enabled = true;
            btnForceUpdate.BackColor = Color.Silver;
            btnPlay.Enabled = true;
            btnPlay.BackColor = Color.Silver;
        }

        /// <summary>
        /// This function will download a file from the server.
        /// </summary>
        /// <param name="remotePath">The remote file to download</param>
        /// <param name="localPath">The local location to save the file</param>
        private void DownloadFromServer(string remotePath, string localPath)
        {
            // Create the destination directory if it does not exist
            string dir = Path.GetDirectoryName(localPath);
            if (dir != "")
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            // Once we have the directory, download the file
            download.DownloadFileAsync(new Uri(remotePath), localPath);
        }

        private void btnForceUpdate_Click(object sender, EventArgs e)
        {
            if (!updateRunning)
                GetLatestUpdate();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!updateRunning)
            {
                if (File.Exists(clientExecutable))
                {
                    Process.Start(clientExecutable);
                    this.Close();
                }
                else
                    txtStatus.AppendText("Game is not installed! Click on Force Update and wait for it to complete. If the issue persists, contact the developer immediately." + Environment.NewLine);
            }
        }
    }
}
