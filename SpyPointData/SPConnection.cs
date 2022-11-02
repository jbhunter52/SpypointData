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
using System.Reflection;
using SpyPointSettings;

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
        [JsonIgnore]
        public Dictionary<string,Photo> LocationPics { get; set; }
        public string Dir { get; set; }
        public string DataFile { get; set; }
        public delegate void ProgressUpdate(string s);
        public event ProgressUpdate OnProgressUpdate;

        public DateTime LastPicture;

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

        public List<Photo> GetFilteredPhotos(FilterCriteria fc, BuckData buckData)
        {
            List<Photo> photos = new List<Photo>();
            foreach (KeyValuePair<string, CameraPics> kvp in CameraPictures)
            {
                CameraPics cp = kvp.Value;
                photos.AddRange(cp.GetFilteredPhotos(fc, buckData));
            }
            return photos;
        }
        public void GetAllPicInfo()
        {
            foreach (var kvp in CameraInfoList)
            {
                CameraInfo ci = kvp.Value;
                if (ci.ManualDisable)
                    continue;
                try
                {
                    GetPicInfoFromCamera(ci);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        public void UpdateFirstPics()
        {
            foreach (var kvp in CameraInfoList)
            {
                CameraInfo ci = kvp.Value;
                if (ci.ManualDisable)
                    continue;
                var pics = GetFirstPicInfoFromCamera(ci);
                if (pics.photos.Count > 0)
                {
                    DateTime last = pics.photos.Max(p => p.originDate);
                    ci.lastPhotoDate = last;
                }
            }
        }

        public string GetNodeName(Photo p)
        {
            return p.originDate.ToShortDateString() + ", " + p.originDate.ToString("hh:mm:ss tt");
        }
        private class PhotoOrg
        {
            public Photo P;
            public string CamId;
            public PhotoOrg(Photo p, string camid)
            {
                P = p;
                CamId = camid;
            }
        }
        public TreeNode GetNodes(FilterCriteria fc, BuckData buckData, OrganizeMethod method)
        {
            TreeNode user = new TreeNode();
            user.Text = this.UserLogin.Username;

            if (method == OrganizeMethod.Camera)
            {
                foreach (var kvp in CameraPictures)
                {
                    CameraPics cp = kvp.Value;
                    string name = CameraInfoList[kvp.Key].config.name;
                    TreeNode camNode = new TreeNode(name);
                    camNode.Tag = cp.cameraId;
                    foreach (Photo p in cp.photos)
                    {
                        TreeNode n = new TreeNode(GetNodeName(p));
                        n.Tag = p.id;
                        //if (p.Deer == null)
                        //    p.Deer = false;
                        //if (p.Buck == null)
                        //    p.Buck = false;
                        if (p.Buck)
                            n.ForeColor = System.Drawing.Color.DarkGreen;
                        if (p.Deer)
                            n.BackColor = System.Drawing.Color.LightGray;
                        //Apply filters
                        if (p.CheckFilter(fc, buckData.CheckForPhoto(p.id)))
                            camNode.Nodes.Add(n);
                    }
                    
                    user.Nodes.Add(camNode);
                    camNode.Text += ", " + camNode.Nodes.Count.ToString() + " pics";
                }
            }
            if (method == OrganizeMethod.Location)
            {
                List<PhotoOrg> org = new List<PhotoOrg>();
                List<string> locations = new List<string>();
                foreach (var kvp in CameraPictures)
                {
                    CameraPics cp = kvp.Value;
                    
                    foreach (Photo p in cp.photos)
                    {
                        org.Add(new PhotoOrg(p, cp.cameraId));
                        locations.Add(p.CameraName);
                    }
                }

                org = org.OrderBy(o => o.P.originDate).Reverse().ToList();
                locations = locations.Distinct().ToList();

                foreach (string loc in locations)
                {
                    TreeNode locNode = new TreeNode(loc);

                    List<PhotoOrg> allLoc = org.FindAll(po => po.P.CameraName.Equals(loc)).ToList();

                    foreach (PhotoOrg photoOrg in allLoc)
                    {
                        Photo p = photoOrg.P;
                        TreeNode n = new TreeNode(GetNodeName(p));
                        n.Tag = p.id;

                        if (p.Buck)
                            n.ForeColor = System.Drawing.Color.DarkGreen;
                        if (p.Deer)
                            n.BackColor = System.Drawing.Color.LightGray;
                        //Apply filters
                        if (p.CheckFilter(fc, buckData.CheckForPhoto(p.id)))
                            locNode.Nodes.Add(n);
                    }
                    if (locNode.Nodes.Count > 0)
                    {
                        user.Nodes.Add(locNode);
                        locNode.Text += ", " + locNode.Nodes.Count.ToString() + " pics";
                    }
                }
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

        public bool Login()
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
                try
                {
                    response = client.UploadString(new Uri("https://restapi.spypoint.com/api/v3/user/login"), postData);
                }
                catch(WebException ex)
                {
                    return false;
                }

                
            }
            Regex regex = new Regex("\"(.*?)\"");
            MatchCollection matches = regex.Matches(response);
            this.uuid = matches[1].Value.Replace("\"", "");
            this.token = matches[3].Value.Replace("\"", "");
            return true;
        }

        public void GetCameraInfo()
        {
            try
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
                Debug(response, false);

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;

                List<CameraInfo> list = JsonConvert.DeserializeObject<List<CameraInfo>>(response, settings);
                foreach (CameraInfo ci in list)
                {
                    string dir = Path.Combine(Dir, ci.id);
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    if (CameraInfoList.ContainsKey(ci.id))
                    {
                        CameraInfoList[ci.id] = ci;
                    }
                    else
                        CameraInfoList.Add(ci.id, ci);
                };
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error updating camera lists for user:{0}", this.UserLogin.Username), ex);
            }
        }
        public void SetCameraSettings(CameraInfo ci, DelayOptions delay, MutiShotOptions multishot)
        {
            if (ci.status.model.ToLower().StartsWith("flex"))
            {
                //skip flex update
                return;
            }

            string response = "";
            string url = "https://restapi.spypoint.com/api/v3/camera/settings/" + ci.id;

            try
            {
                using (var client = new CookieAwareWebClient()) // WebClient class inherits IDisposable
                {
                    client.Method = "PUT";
                    client.Headers.Add("Referer", "https://webapp.spypoint.com/camera/" + ci.id + "/settings");
                    client.Headers.Add("Authorization", "bearer " + token);
                    client.Headers.Add("Accept", "application/json, text/plain, */*");
                    client.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                    client.Headers.Add("Content-Type", "application/json;charset=utf-8");
                    response = client.UploadString(url, "PUT", delay.GetJson(ci.status.model));
                }

                using (var client = new CookieAwareWebClient()) // WebClient class inherits IDisposable
                {
                    client.Method = "PUT";
                    client.Headers.Add("Referer", "https://webapp.spypoint.com/camera/" + ci.id + "/settings");
                    client.Headers.Add("Authorization", "bearer " + token);
                    client.Headers.Add("Accept", "application/json, text/plain, */*");
                    client.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                    client.Headers.Add("Content-Type", "application/json;charset=utf-8");
                    response = client.UploadString(url, "PUT", multishot.GetJson(ci.status.model));
                }
            }
            catch (WebException ex)
            {
                string mess = "Error updating " + ci.config.name;
                System.Diagnostics.Debug.WriteLine(mess);

            }
        }
        public CameraPics GetFirstPicInfoFromCamera(CameraInfo ci)
        {
            try
            {
                CameraPics allPics = new CameraPics(ci.id);
                Debug("Getting, " + ci.config.name);
                string response = "";
                string url = "https://restapi.spypoint.com/api/v3/photo/all";
                bool more = true;
                string date = "2100-01-01T00:00:00.000Z";
                string postdata = "{\"dateEnd\":\"" + date + "\"}";
                postdata = JsonConvert.SerializeObject(new PostRequest_AllCamPic(ci.id, date));

                using (var client = new CookieAwareWebClient()) // WebClient class inherits IDisposable
                {
                    client.Method = "POST";
                    client.Headers.Add("Referer", "https://webapp.spypoint.com/");
                    client.Headers.Add("Authorization", "bearer " + token);
                    Debug(date);
                    response = client.UploadString(url, postdata);
                }
                //Debug(response);
                CameraPics pics = JsonConvert.DeserializeObject<CameraPics>(response);
                Debug(pics.countPhotos.ToString() + ", pictures");
                return pics;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error update camera pics for {0}", ci.config.name), ex);
            }
        }

        public void GetPicInfoFromCamera(CameraInfo ci)
        {
            CameraPics allPics = new CameraPics(ci.id);
            Debug("Getting, " + ci.config.name);
            string response = "";
            string url = "https://restapi.spypoint.com/api/v3/photo/all";
            bool more = true;
            string date = "2100-01-01T00:00:00.000Z";
            string postdata = "{\"dateEnd\":\"" + date + "\"}";
            postdata = JsonConvert.SerializeObject(new PostRequest_AllCamPic(ci.id, date));
            int tries = 0;
            while (more)
            {
                tries++;
                try
                {
                    using (var client = new CookieAwareWebClient()) // WebClient class inherits IDisposable
                    {
                        client.Method = "POST";
                        client.Headers.Add("Referer", "https://webapp.spypoint.com/");
                        client.Headers.Add("Authorization", "bearer " + token);
                        Debug(date);
                        response = client.UploadString(url, postdata);
                    }
                }
                catch (WebException ex)
                {
                    Debug(String.Format("Status={0}, {1}, on {2}", ex.Status, ex.Message, ci.config.name));
                    System.Threading.Thread.Sleep((tries+1)*100);
                    if (tries == 3)
                        throw ex;
                    else
                        continue;
                }
                //Debug(response);
                CameraPics pics = JsonConvert.DeserializeObject<CameraPics>(response);
                Debug(pics.countPhotos.ToString() + ", pictures");

                if (pics.countPhotos == 0)
                    more = false;
                else
                {
                    allPics.Add(pics);
                    DateTime d = pics.photos[pics.photos.Count - 1].originDate;
                    d = d.Subtract(new TimeSpan(0, 0, 0, 0, 100));
                    date = d.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    postdata = "{\"dateEnd\":\"" + date + "\"}";
                    postdata = JsonConvert.SerializeObject(new PostRequest_AllCamPic(ci.id, date));
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
                if (ci.ManualDisable)
                    continue;
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

            //if (p.hd.host.Length > 1)
            if (p.hd != null)
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
                Debug("Error getting image\n" + ex.Message);
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
            Debug("Merging");
            int cntHd = 0;
            int cntNew = 0;

            foreach (var kvp in sp.CameraInfoList)
            {
                
                //Directory for any new cameras should already exist
                CameraInfo ci = kvp.Value;
                Debug("Merging, " + ci.config.name);
                if (this.CameraInfoList.ContainsKey(kvp.Key))
                {
                    //Camera already exists
                    this.CameraInfoList[kvp.Key] = kvp.Value; //update the CameraInfo
                    CameraPics cp = sp.CameraPictures[kvp.Key];
                    CameraPics cpOld = this.CameraPictures[kvp.Key];

                    Debug("Old count, " + cpOld.photos.Count.ToString());
                    Debug("New count, " + cp.photos.Count.ToString());

                    foreach (Photo p in cp.photos) //cycle through new pictures
                    {
                        Photo old = cpOld.photos.Find(x => x.id.Equals(p.id));
                        if (old != null)
                        {
                            //Photo already exists, check if hd exisits
                            if (p.hd != null)
                            {
                                //New photo contains hd
                                if (old.hd == null)
                                {
                                    Debug("Updating to hd, " + p.originDate.ToString());
                                    //Old photo no hd
                                    p.Buck = old.Buck;
                                    p.Deer = old.Deer;
                                    p.HaveLocation = old.HaveLocation;
                                    p.Latitude = old.Latitude;
                                    p.Longitude = old.Longitude;
                                    p.New = true;
                                    GetPhotoAndSave(p, ci);
                                    int ind = cpOld.photos.IndexOf(old);
                                    cpOld.photos[ind] = p; //update old database picture
                                    cntHd++;
                                }
                            }
                        }
                        else
                        {
                            //Photo does not exist, download it and add it
                            Debug("Adding, " + p.originDate.ToString());
                            GetPhotoAndSave(p, ci);
                            p.New = true;
                            //set pic coordinates to current camera location
                            if (cpOld.HaveLocation)
                            {
                                p.HaveLocation = true;
                                p.Latitude = cpOld.Latitude;
                                p.Longitude = cpOld.Longitude;
                            }
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
                    Debug("New camera detected, getting all photos");
                    this.CameraInfoList.Add(ci.id, ci); //Add cameraInfo
                    CameraPics cp = sp.CameraPictures[ci.id];
                    this.CameraPictures.Add(cp.cameraId, cp); //Add camera picture info
                    this.DownloadPhotosFromCamera(ci);

                }
            }

            Debug("Sorting");
            foreach (var kvp in this.CameraPictures)
            {
                CameraPics cp = kvp.Value;

                cp.photos.Sort((x, y) => y.originDate.CompareTo(x.originDate));
            }


            Debug("HD added, " + cntHd.ToString());
            Debug("New added, " + cntNew.ToString());
            Debug("Merge Done");
        }
        private void Debug(string s, bool report = true)
        {
            System.Diagnostics.Debug.WriteLine(s);

            if (report)
            {
                if (OnProgressUpdate != null)
                    OnProgressUpdate(s);
            }
        }

        private PropertyInfo[] _PropertyInfos = null;
        public override string ToString()
        {
            if (_PropertyInfos == null)
                _PropertyInfos = this.GetType().GetProperties();

            var sb = new StringBuilder();

            foreach (var info in _PropertyInfos)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }

            sb.AppendLine(String.Format("CameraInfoList, found {0} CameraInfos", CameraInfoList.Count));
            sb.AppendLine(String.Format("CameraPictures, found {0} CameraPictures", CameraPictures.Count));

            sb.AppendLine("\nCameras:");
            foreach (var ci in CameraInfoList)
            {
                sb.AppendLine(ci.ToString());
            }

            sb.AppendLine("\nLooping CameraPictures Objects");
            foreach (var kvp in this.CameraPictures)
            {
                var cp = kvp.Value;
                sb.AppendLine(String.Format("Key: {0}, count:{1}",kvp.Key,cp.photos.Count));
                string dir = Path.Combine(this.Dir, kvp.Key);
                string[] files = Directory.GetFiles(dir);
                sb.AppendLine(String.Format("Directory pic count: {0}", files.Length));
                sb.AppendLine("Looking for missing files:");

                foreach (string f in files)
                {
                    string fn = Path.GetFileNameWithoutExtension(f);
                    Photo found;
                    if (fn.ToLower().Contains("campic"))
                    {
                        //hd pic
                        string id = fn.Split('_')[0];
                        found = cp.photos.Find(p => p.id == id);
                        if (found != null)
                        {
                            if (found.CardPicFilename != f)
                            {
                                sb.AppendLine(String.Format("Missing cam pic, {0}", f));
                                sb.AppendLine("Fixing");
                                found.HaveCardPic = true;
                                found.CardPicFilename = f;
                            }
                        }
                    }
                    else
                    {
                        found = cp.photos.Find(p => p.FileName == f);
                        if (found == null)
                        {
                            sb.AppendLine(f);
                        }
                    }
                }
            }

            return sb.ToString();
        }

    }

    public class PostRequest_AllCamPic
    {
        public List<string> camera;
        public string dateEnd;
        public bool favorite;
        public bool hd;
        public int limit;
        public List<string> tag;
        
        public PostRequest_AllCamPic(string cid, string dateend)
        {
            camera = new List<string>();
            camera.Add(cid);
            dateEnd = dateend;
            favorite = false;
            hd = false;
            limit = 100;
            tag = new List<string>();
        }
    }
   


}
