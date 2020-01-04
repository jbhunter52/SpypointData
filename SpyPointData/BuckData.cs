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

        public BuckData()
        {
            IDs = new List<BuckID>();
        }
        public void AddConnection(string name, Photo p)
        {
            //First find if photo is already connected and remove
            foreach (BuckID id in IDs)
            {
                id.Photos.RemoveAll(i => i.PhotoID.Equals(p.id));
            }
            
            //Now add connection     
            BuckID buckID = IDs.Find(id => id.Name.Equals(name));
            buckID.Photos.Add(new BuckIDPhoto(p.id));
        }
        public string CheckForPhoto(string photoID)
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
