using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SpyPointData
{
    [Serializable]
    public class SPConnection
    {
        public string uuid { get; set; }
        public string token { get; set; }
        public LoginInfo UserLogin { get; set; }
        public Dictionary<string,CameraInfo> CameraInfoList { get; set; }
        public Dictionary<string, CameraPics> CameraPictures { get; set; }
        public string Dir { get; set; }
        public string DataFile { get; set; }

        public SPConnection(LoginInfo login)
        {
            UserLogin = login;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            CameraPictures = new Dictionary<string, CameraPics>();
            CameraInfoList = new Dictionary<string, CameraInfo>();
            Dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpyPoint");
            DataFile = Path.Combine(Dir, "Data.json");

            if (!Directory.Exists(Dir))
                Directory.CreateDirectory(Dir);
        }
        public void GetAllPicInfo()
        {
            foreach (var kvp in CameraInfoList)
            {
                CameraInfo ci = kvp.Value;
                GetPicInfoFromCamera(ci);
            }
        }
        public TreeNode GetNodes(bool deerFilter, bool buckFilter)
        {
            TreeNode user = new TreeNode();
            user.Text = this.uuid;
            foreach (var kvp in CameraPictures)
            {
                CameraPics cp = kvp.Value;
                string name = CameraInfoList[kvp.Key].config.name;
                TreeNode camNode = new TreeNode(name);
                camNode.Tag = cp.cameraId;

                foreach (Photo p in cp.photos)
                {
                    TreeNode n = new TreeNode(p.originDate.ToShortDateString() + ", " + p.originDate.ToShortTimeString());
                    n.Tag = p.id;

                    if (p.Deer == null)
                        p.Deer = false;
                    if (p.Buck == null)
                        p.Buck = false;

                    if (p.Buck)
                        n.ForeColor = System.Drawing.Color.DarkGreen;
                    if (p.Deer)
                        n.BackColor = System.Drawing.Color.LightGray;

                    //Apply filters
                    bool keep = true;

                    if (deerFilter)
                    {
                        if (p.Deer == false)
                            keep = false;
                    }
                    if (buckFilter)
                    {
                        if (p.Buck == false)
                            keep = false;
                    }

                    if (keep)
                        camNode.Nodes.Add(n);
                }
                user.Nodes.Add(camNode);
                camNode.Text += ", " + camNode.Nodes.Count.ToString() + " pics";
            }

            //count total pictures
            int cnt = 0;
            foreach (TreeNode child in user.Nodes)
            {
                cnt += child.Nodes.Count;
            }
            user.Text += ", " + cnt + " pics";

            return user;
        }

        public void Login()
        {
            string username = UserLogin.Username;
            string pass = UserLogin.Password;
            string LoginURL = "https://webapp.spypoint.com/login";

            string response = "";
            using (var client = new CookieAwareWebClient()) // WebClient class inherits IDisposable
            {
                response = client.DownloadString(LoginURL);

                // parse the 'authenticity_token' and cookie is auto handled by the cookieContainer
                string token = Regex.Match(response, "authenticity_token.+value=\"(.+)\"").Groups[1].Value;
                string encodedToken = System.Web.HttpUtility.UrlEncode(token);
                string postData = "{\"username\":\"" + username + "\",\"password\":\"" + pass + "\"}";

                client.Headers.Add("Referer", "https://webapp.spypoint.com/login");
                client.Method = "POST";
                response = client.UploadString(new Uri("https://restapi.spypoint.com/api/v3/user/login"), postData);


            }
            Regex regex = new Regex("\"(.*?)\"");
            MatchCollection matches = regex.Matches(response);
            this.uuid = matches[1].Value.Replace("\"", "");
            this.token = matches[3].Value.Replace("\"", "");
        }

        public void GetCameraInfo()
        {
            string response = "";
            string CameraAllURL = "https://restapi.spypoint.com/api/v3/camera/all";
            using (var client = new CookieAwareWebClient()) // WebClient class inherits IDisposable
            {
                client.Method = "GET";
                client.Headers.Add("Referer", "https://webapp.spypoint.com/");
                client.Headers.Add("Authorization", "bearer " + token);
                response = client.DownloadString(CameraAllURL);
            }
            System.Diagnostics.Debug.WriteLine(response);
            List<CameraInfo> list = JsonConvert.DeserializeObject<List<CameraInfo>>(response);
            foreach (CameraInfo ci in list)
            {
                string dir = Path.Combine(Dir, ci.id);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                CameraInfoList.Add(ci.id, ci);
            }
        }
        public void GetPicInfoFromCamera(CameraInfo ci)
        {
            CameraPics allPics = new CameraPics(ci.id);
            System.Diagnostics.Debug.WriteLine("Getting, " + ci.config.name);
            string response = "";
            string url = "https://restapi.spypoint.com/api/v3/photo/" + ci.id;
            bool more = true;
            string date = "2100-01-01T00:00:00.000Z";
            string postdata = "{\"dateEnd\":\"" + date + "\"}";
            while (more)
            {
                using (var client = new CookieAwareWebClient()) // WebClient class inherits IDisposable
                {
                    client.Method = "POST";
                    client.Headers.Add("Referer", "https://webapp.spypoint.com/camera/" + ci.id);
                    client.Headers.Add("Authorization", "bearer " + token);
                    System.Diagnostics.Debug.WriteLine(date);
                    response = client.UploadString(url, postdata);
                }
                //System.Diagnostics.Debug.WriteLine(response);
                CameraPics pics = JsonConvert.DeserializeObject<CameraPics>(response);
                System.Diagnostics.Debug.WriteLine(pics.countPhotos.ToString() + ", pictures");

                if (pics.countPhotos == 0)
                    more = false;
                else
                {
                    allPics.Add(pics);
                    DateTime d = pics.photos[pics.photos.Count - 1].originDate;
                    d = d.Subtract(new TimeSpan(0, 0, 0, 0, 100));
                    date = d.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    postdata = "{\"dateEnd\":\"" + date + "\"}";
                }
            }

            CameraPictures.Add(ci.id, allPics);
        }
        public void DownloadPhotosFromCamera(CameraInfo ci)
        {
            string camid = ci.id;
            CameraPics cp = CameraPictures[camid];
            //Get actual jpegs from each photo
            foreach (Photo p in cp.photos)
            {
                GetPhotoAndSave(p, ci);
            }
        }
        public void DownloadPhotosFromAllCameras()
        {
            foreach (var kvp in CameraInfoList)
            {
                CameraInfo ci = kvp.Value;
                DownloadPhotosFromCamera(ci);
            }
        }
        public System.Drawing.Image GetPhotoFromWeb(Photo p)
        {
            string host = p.large.host;
            string path = p.large.path;

            if (p.hd.host.Length > 1)
            {
                host = p.hd.host;
                path = p.hd.path;
            }

            using (var client = new CookieAwareWebClient()) // WebClient class inherits IDisposable
            {
                client.Method = "GET";
                Uri baseuri = new Uri("http://" + host);
                Uri uri = new Uri(baseuri, path);
                byte[] bytes = client.DownloadData(uri);
                MemoryStream ms = new MemoryStream(bytes);
                var image = System.Drawing.Image.FromStream(ms);
                return image;
            }
        }
        public void GetPhotoAndSave(Photo p, CameraInfo ci)
        {
            string host = p.large.host;
            string path = p.large.path;

            if (p.hd.host.Length > 1)
            {
                host = p.hd.host;
                path = p.hd.path;
            }

            try
            {
                using (var client = new CookieAwareWebClient()) // WebClient class inherits IDisposable
                {
                    client.Method = "GET";
                    Uri baseuri = new Uri("http://" + host);
                    Uri uri = new Uri(baseuri, path);
                    byte[] bytes = client.DownloadData(uri);
                    MemoryStream ms = new MemoryStream(bytes);
                    var image = System.Drawing.Image.FromStream(ms);
                    string file = Path.Combine(Dir, ci.id, p.id + ".jpg");
                    p.FileName = file;
                    image.Save(file);
                    DateTime time = p.originDate;
                    File.SetCreationTime(file, time);
                    File.SetLastWriteTime(file, time);
                    File.SetLastAccessTime(file, time);
                    p.CameraName = ci.config.name;

                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error getting image\n" + ex.Message);
            }
        }

        public Photo FindPhoto(string id)
        {
            foreach (var kvp  in CameraPictures)
            {
                CameraPics cp = kvp.Value;
                foreach (Photo p in cp.photos)
                {
                    if (p.id.Equals(id))
                        return p;
                }
            }
            return null;
        }
        public void Merge(SPConnection sp)
        {
            System.Diagnostics.Debug.WriteLine("Merging");
            int cntHd = 0;
            int cntNew = 0;

            foreach (var kvp in sp.CameraInfoList)
            {
                
                //Directory for any new cameras should already exist
                CameraInfo ci = kvp.Value;
                System.Diagnostics.Debug.WriteLine("Merging, " + ci.config.name);
                if (this.CameraInfoList.ContainsKey(kvp.Key))
                {
                    //Camera already exists
                    this.CameraInfoList[kvp.Key] = kvp.Value; //update the CameraInfo
                    CameraPics cp = sp.CameraPictures[kvp.Key];
                    CameraPics cpOld = this.CameraPictures[kvp.Key];

                    System.Diagnostics.Debug.WriteLine("Old count, " + cpOld.photos.Count.ToString());
                    System.Diagnostics.Debug.WriteLine("New count, " + cp.photos.Count.ToString());

                    foreach (Photo p in cp.photos) //cycle through new pictures
                    {
                        Photo old = cpOld.photos.Find(x => x.id.Equals(p.id));
                        if (old != null)
                        {
                            //Photo already exists, check if hd exisits
                            if (p.hd.path.Length > 1)
                            {
                                //New photo contains hd
                                if (old.hd.path.Length < 1)
                                {
                                    System.Diagnostics.Debug.WriteLine("Updating to hd, " + p.originDate.ToString());
                                    //Old photo no hd
                                    bool buck = old.Buck;
                                    bool deer = old.Deer;
                                    old = p; //update old database picture
                                    old.Buck = buck;
                                    old.Deer = deer;
                                    GetPhotoAndSave(old, ci);
                                    cntHd++;
                                }
                            }
                        }
                        else
                        {
                            //Photo does not exist, download it and add it
                            System.Diagnostics.Debug.WriteLine("Adding, " + p.originDate.ToString());
                            GetPhotoAndSave(p, ci);
                            cpOld.photos.Add(p);
                            cpOld.countPhotos = cpOld.photos.Count;
                            cntNew++;
                        }
                    }
                    this.CameraPictures[kvp.Key] = cpOld;
                }
                else
                {
                    //New camera detected
                    System.Diagnostics.Debug.WriteLine("New camera detected, getting all photos");
                    this.CameraInfoList.Add(ci.id, ci); //Add cameraInfo
                    CameraPics cp = sp.CameraPictures[ci.id];
                    this.CameraPictures.Add(cp.cameraId, cp); //Add camera picture info
                    this.DownloadPhotosFromCamera(ci);

                }
            }

            System.Diagnostics.Debug.WriteLine("Sorting");
            foreach (var kvp in this.CameraPictures)
            {
                CameraPics cp = kvp.Value;

                cp.photos.Sort((x, y) => y.originDate.CompareTo(x.originDate));
            }


            System.Diagnostics.Debug.WriteLine("HD added, " + cntHd.ToString());
            System.Diagnostics.Debug.WriteLine("New added, " + cntNew.ToString());
            System.Diagnostics.Debug.WriteLine("Merge Done");
        }


    }




}
