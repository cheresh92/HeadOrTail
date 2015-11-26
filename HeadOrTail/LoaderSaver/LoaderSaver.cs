using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Popups;

namespace HeadOrTail.LoaderSaver
{
    class LoaderSaver
    {
        private string filename = "HeadsOrTailsDataSetting.xml";

        virtual public void Save(HeadsOrTailsData setting)
        {
            var t = Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(filename,
                CreationCollisionOption.ReplaceExisting).GetAwaiter();

            while (!t.IsCompleted) { }
            StorageFile file = t.GetResult();

            using (var stream = file.OpenStreamForWriteAsync().Result)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(HeadsOrTailsData));
                serializer.Serialize(stream, setting);
            }
        }

        virtual public HeadsOrTailsData Load()
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            Stream stream = null;
            HeadsOrTailsData setting;
            try
            {
                try
                {
                    stream = local.OpenStreamForReadAsync(filename).Result;
                }
                catch (Exception)
                {
                    StorageFile file = local.CreateFileAsync(filename).GetResults();
                    stream = local.OpenStreamForReadAsync(filename).Result;
                }
                

                using (StreamReader reader = new StreamReader(stream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(HeadsOrTailsData));
                    try
                    {
                        setting = (HeadsOrTailsData)serializer.Deserialize(reader);
                    }
                    catch (InvalidOperationException exception)
                    {
                        setting = new HeadsOrTailsData();
                        setting.AllCoinThrowCount = 0;
                        setting.CorrectlyGuessCoinCount = 0;
                        setting.GuessCoinCount = 0;
                    }
                    
                }
            }
            catch (IOException exception)
            {
                setting = new HeadsOrTailsData();
            }
            return setting;
        }

    }
}
