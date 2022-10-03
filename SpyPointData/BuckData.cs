using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SpyPointData
{
    public class BuckData
    {
        public List<BuckID> IDs { get; set; }
        [JsonIgnore]
        public HashSet<string> pIDs { get; set; }
        [JsonIgnore]
        public Dictionary<string, Photo> IDPhotoDict {get;set;}

        public BuckData()
        {
            IDs = new List<BuckID>();
        }

        public void InitializeIDPhotoDict(DataCollection data)
        {
            IDPhotoDict = new Dictionary<string, Photo>();
            foreach (var buckid in IDs)
            {
                foreach (var p in buckid.Photos)
                {
                    Photo photo = data.FindPhoto(p.PhotoID);
                    if (photo != null)
                    {
                        if (!IDPhotoDict.ContainsKey(p.PhotoID))
                        {
                            IDPhotoDict.Add(p.PhotoID, photo);
                            continue;
                        }
                    }

                    photo = data.ManualPics.FindPhoto(p.PhotoID);
                    if (photo != null)
                    {
                        if (!IDPhotoDict.ContainsKey(p.PhotoID))
                        {
                            IDPhotoDict.Add(p.PhotoID, photo);
                            continue;
                        }
                    }
                }
            }
        }
        public Photo GetPhoto(string id)
        {
            if (IDPhotoDict.ContainsKey(id))
                return IDPhotoDict[id];
            else
                return null;
        }

        public void AddConnection(string name, Photo p)
        {
            //First find if photo is already connected and remove
            foreach (BuckID id in IDs)
            {
                id.Photos.RemoveAll(i => i.PhotoID.Equals(p.id));
                pIDs.Remove(p.id);
            }

            //If name is blank then it has already been removed above, return from function
            if (name.Equals(""))
                return;

            //Now add connection     
            BuckID buckID = IDs.Find(id => id.Name.Equals(name));
            buckID.Photos.Add(new BuckIDPhoto(p.id));
            pIDs.Add(p.id);
        }
        public string CheckForPhoto(string photoID)
        {
            if (pIDs == null)
            {
                pIDs = new HashSet<string>();
                foreach (BuckID buckID in IDs)
                {
                    foreach (var buckIDPhoto in buckID.Photos)
                    {
                        if (!pIDs.Contains(buckIDPhoto.PhotoID))
                            pIDs.Add(buckIDPhoto.PhotoID);
                    }
                }
            }

            if (pIDs.Contains(photoID))
            {
                foreach (BuckID buckID in IDs)
                {
                    foreach (var buckIDPhoto in buckID.Photos)
                    {
                        if (buckIDPhoto.PhotoID.Equals(photoID))
                        {
                            return buckID.Name;
                        }
                    }
                }
            }
            return null;
        }
    }


    public class BuckID
    {
        public List<BuckIDPhoto> Photos { get; set; }
        public string Name { get; set; }

        public BuckID(string name)
        {
            Name = name;
            Photos = new List<BuckIDPhoto>();
        }
    }
    public class BuckIDPhoto
    {
        public string PhotoID { get; set; }

        public BuckIDPhoto(string photoID)
        {
            PhotoID = photoID;
        }
    }


}
