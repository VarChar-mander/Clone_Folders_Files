using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

//written by vprabh01
namespace Clone_Folders_Files
{
    public partial class Form1 : Form
    {
        String sourceD;
        String sDefault = "\\\\vmdevdsm5.711dev.pvt\\c$\\Documents and Settings\\vprabh01\\Desktop\\TEST_source";
        String clonedD;
        String cDefault = "\\\\vmdevdsm5.711dev.pvt\\c$\\Documents and Settings\\vprabh01\\Desktop\\TEST_destination";
        String[] fTypes;
        Boolean sourceChanged = false;
        Boolean destinationChanged = false;
        Boolean extChanged = false;
        public Form1()
        {
            InitializeComponent();
            btnBrowse.Select();
            ToolTip from = new ToolTip();
            from.ShowAlways = true;
            from.SetToolTip(txtFilepath, "Enter the directory to clone FROM here. Ex: " + sDefault);
            ToolTip to = new ToolTip();
            to.ShowAlways = true;
            to.SetToolTip(buildHere, "Enter the directory to clone TO here. Ex: " + cDefault);
            ToolTip ext = new ToolTip();
            ext.ShowAlways = true;
            ext.SetToolTip(fileExt, "Enter the desired file type (e.g. '.pl' or '.txt'). To list multiple extensions, place a comma between each. Leave the field blank to copy all files.");
            ToolTip noFiles = new ToolTip();
            noFiles.ShowAlways = true;
            noFiles.SetToolTip(directoriesOnly, "Check box to copy only folders and not files");
            ToolTip noFolders = new ToolTip();
            noFolders.ShowAlways = true;
            noFolders.SetToolTip(filesOnly, "Check box to copy all files and none of the folders");
            ToolTip noEmptyF = new ToolTip();
            noEmptyF.ShowAlways = true;
            noEmptyF.SetToolTip(noEmpty, "Check box to not allow for any empty clone directories. When copying both files and folders, no empty folders are kept, even if the source directory wasn't empty. When copying only directories, empty source directories are not cloned.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!destinationChanged || !sourceChanged || txtFilepath.Text.ToString() == "" || buildHere.Text.ToString() == "")
                {
                    MessageBox.Show("Please select both a source and a destination directory");
                    return;
                }
                
                DirectoryInfo di = new DirectoryInfo(sourceD);
                DirectoryInfo clone = new DirectoryInfo(clonedD);
                setExtension();
                String continueMSG = "";

                if (directoriesOnly.Checked)
                    continueMSG = "Click 'OK' to clone all folders, but not any files, from '" + sourceD + "' into '" + clonedD + "'";
                else
                {
                    if (filesOnly.Checked)
                        if (fTypes[0] == "*")
                            continueMSG = "Click 'OK' to clone ALL files and no folders from '" + sourceD + "' into '" + clonedD + "'";
                        else
                            continueMSG = "Click 'OK' to clone the selected file type(s) and no folders from '" + sourceD + "' into '" + clonedD + "'";
                    else
                        if (fTypes[0] == "*")
                            continueMSG = "Click 'OK' to clone ALL files and folders from '" + sourceD + "' into '" + clonedD + "'";
                        else
                            continueMSG = "Click 'OK' to clone the selected file type(s) and all folders from '" + sourceD + "' into '" + clonedD + "'";
                }
                if (noEmpty.Checked == true)
                    continueMSG += ". Empty directories will not be cloned.";
                if (MessageBox.Show(continueMSG, "Continue?", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;

                if (directoriesOnly.Checked)
                {
                    if (noEmpty.Checked)
                        allDirnotEmpty(di, clone);
                    else
                        createAllDir(di, clone);
                    }
                else
                {
                    if (filesOnly.Checked)
                        copyFilesOnly(di, clone);
                    else
                        createAllFiles(di, clone);
                    if (noEmpty.Checked)
                        deleteEmpty(clone);
                }
                MessageBox.Show("Complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void createAllFiles(DirectoryInfo sourceInfo, DirectoryInfo clone)
        {
            try
            {
                DirectoryInfo di = sourceInfo;
                DirectoryInfo newDir;
                DirectoryInfo[] allSub = di.GetDirectories();
                FileInfo[] allFiles;
                foreach (String extension in fTypes)
                {
                    allFiles = di.GetFiles(extension);
                    foreach (FileInfo foundFile in allFiles)
                        File.Copy(foundFile.FullName, clone.FullName + "\\" + foundFile.Name);
                }
                
                foreach (DirectoryInfo subDir in allSub)
                {
                    if (!subDir.FullName.Equals(clonedD.ToString()))
                    {
                        newDir = clone.CreateSubdirectory(subDir.Name);
                        createAllFiles(subDir, newDir);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void createAllDir(DirectoryInfo info, DirectoryInfo clone)
        {
            try
            {
                DirectoryInfo di = info;
                DirectoryInfo newDir;
                DirectoryInfo[] allSub = di.GetDirectories();
                foreach (DirectoryInfo subDir in allSub)
                {
                    if (!subDir.FullName.Equals(clonedD.ToString()))
                    {
                        newDir = clone.CreateSubdirectory(subDir.Name);
                        createAllDir(subDir, newDir);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void copyFilesOnly(DirectoryInfo sourceInfo, DirectoryInfo clone)
        {
            try
            {
                DirectoryInfo di = sourceInfo;
                DirectoryInfo[] allSub = di.GetDirectories();
                FileInfo[] allFiles;
                foreach (String extension in fTypes)
                {
                    allFiles = di.GetFiles(extension);
                    foreach (FileInfo foundFile in allFiles)
                        if (!File.Exists(clone.FullName + "\\" + foundFile.Name))
                            File.Copy(foundFile.FullName, clone.FullName + "\\" + foundFile.Name);
                        else
                        {
                            /*
                             * Need to handle when All files are being searched
                             * when regular ".xxx" files are searched
                             * and when files with no extension are encountered
                             */
                            int suffix = 1;
                            //String saveThis = "";
                            //String trimExt = extension.Replace("*", "");
                            String[] temp1; // = foundFile.Name.Split(new String[] { trimExt }, System.StringSplitOptions.RemoveEmptyEntries);
                            String temp; // = foundFile.Name.Split(new String[] {extension.Replace("*.", "")}, System.StringSplitOptions.RemoveEmptyEntries)[0] + "_";
                            if (!extension.Equals("*"))
                            {
                                String trimExt = extension.Replace("*", "");
                                temp1 = foundFile.Name.Split(new String[] { trimExt }, System.StringSplitOptions.RemoveEmptyEntries);
                                temp = temp1[0];
                                while (File.Exists(clone.FullName + "\\" + temp + " (" + suffix + ")" + trimExt))
                                    suffix++;
                                File.Copy(foundFile.FullName, clone.FullName + "\\" + temp + " (" + suffix + ")" + trimExt);
                            }
                            //if (extension.Equals("*"))
                            else
                            {
                                //Pulls the extension of each file so it can properly add the suffix to give it a unique name.
                                String saveThis = "";
                                temp1 = foundFile.Name.Split(new String[] { "." }, System.StringSplitOptions.RemoveEmptyEntries);
                                if (temp1.Length > 1)
                                {
                                    temp = foundFile.Name.Split(new String[] { "." + temp1[temp1.Length - 1] }, System.StringSplitOptions.RemoveEmptyEntries)[0];
                                    saveThis = clone.FullName + "\\" + temp + " (" + suffix + ")." + temp1[temp1.Length - 1];
                                    while (File.Exists(saveThis))
                                    {
                                        suffix++;
                                        saveThis = clone.FullName + "\\" + temp + " (" + suffix + ")." + temp1[temp1.Length - 1];
                                    }
                                    File.Copy(foundFile.FullName, saveThis);
                                }
                                else
                                {
                                    //handles any files that are missing an extension (Files whose type is "File")
                                    while (File.Exists(clone.FullName + "\\" + foundFile.Name + " (" + suffix + ")"))
                                        suffix++;
                                    File.Copy(foundFile.FullName, clone.FullName + "\\" + foundFile.Name + " (" + suffix + ")");
                                }
                            }
                            //File.Copy(foundfile.FullName, saveThis);
                            //File.Copy(foundFile.FullName, clone.FullName + "\\" + temp + suffix + extension);
                        }
                }
                foreach (DirectoryInfo subDir in allSub)
                {
                    if (!subDir.FullName.Equals(clonedD.ToString()))
                        copyFilesOnly(subDir, clone);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void setExtension()
        {
            if (!extChanged || fileExt.Text == "")
                fTypes = new String[]{"*"};
            else
            {
                fTypes = fileExt.Text.Split(new char[] { ',' });
                for (int trim = 0; trim < fTypes.Length; trim++)
                    fTypes[trim] = fTypes[trim].Trim();
                for (int j = 0; j < fTypes.Length; j++)
                    if (fTypes[j].ToCharArray(0, 1)[0] == '.')
                        fTypes[j] = "*" + fTypes[j];
                    else
                        fTypes[j] = "*." + fTypes[j];
            }
        }

        private void deleteEmpty(DirectoryInfo sourceInfo)
        {
            try
            {
                DirectoryInfo di = sourceInfo;
                DirectoryInfo[] allSub = di.GetDirectories();
                foreach (DirectoryInfo subDir in allSub)
                    deleteEmpty(subDir);
                allSub = di.GetDirectories();
                FileInfo[] allFiles = di.GetFiles();
                if (allFiles.Length == 0 && allSub.Length == 0)
                    di.Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }
         
         private void allDirnotEmpty(DirectoryInfo info, DirectoryInfo clone)
        {
            try
            {
                DirectoryInfo di = info;
                DirectoryInfo newDir;
                DirectoryInfo[] allSub = di.GetDirectories();
                foreach (DirectoryInfo subDir in allSub)
                {
                    if (!subDir.FullName.Equals(clonedD.ToString()))
                    {
                        newDir = clone.CreateSubdirectory(subDir.Name);
                        allDirnotEmpty(subDir, newDir);
                    }
                }
                if(di.GetFiles().Length == 0 && clone.GetDirectories().Length == 0)
                    clone.Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }
        
        private void fileExt_TextChanged(object sender, EventArgs e)
        {
            extChanged = true;
        }

        private void buildHere_TextChanged(object sender, EventArgs e)
        {
            clonedD = buildHere.Text;
            destinationChanged = true;
        }

        private void txtFilepath_TextChanged(object sender, EventArgs e)
        {
            sourceD = txtFilepath.Text;
            sourceChanged = true;
        }

        private void txtFilepath_Click(object sender, EventArgs e)
        {
            if (!sourceChanged)
                txtFilepath.Clear();
            txtFilepath.ForeColor = Color.Black;
            sourceChanged = true;
        }

        private void buildHere_Click(object sender, EventArgs e)
        {
            if (!destinationChanged)
                buildHere.Clear();
            buildHere.ForeColor = Color.Black;
            destinationChanged = true;
        }

        private void fileExt_Click(object sender, System.EventArgs e)
        {
            if (!extChanged)
                fileExt.Clear();
            fileExt.ForeColor = Color.Black;
            extChanged = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = folderBrowserDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    txtFilepath.ForeColor = Color.Black;
                    txtFilepath.Text = folderBrowserDialog1.SelectedPath;
                    sourceChanged = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = folderBrowserDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    buildHere.ForeColor = Color.Black;
                    buildHere.Text = folderBrowserDialog1.SelectedPath;
                    destinationChanged = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void filesOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (filesOnly.Checked)
            {
                directoriesOnly.Enabled = false;
                noEmpty.Enabled = false;
                fileExt.Enabled = true;
            }
            else
            {
                directoriesOnly.Enabled = true;
                noEmpty.Enabled = true;
            }
        }

        private void directoriesOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (directoriesOnly.Checked)
            {
                filesOnly.Enabled = false;
                fileExt.Enabled = false;
            }
            else
            {
                filesOnly.Enabled = true;
                fileExt.Enabled = true;
            }
        }

        private void noEmpty_CheckedChanged(object sender, EventArgs e)
        {
            if (noEmpty.Checked == true)
                filesOnly.Enabled = false;
            else
                filesOnly.Enabled = true;
        }
    }
}
