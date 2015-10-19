using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImpRec
{
    /// <summary>
    /// Binary serializer used for the knowledge base.
    /// </summary>
    public class Serializer
    {
        public Serializer()
        {
        }

        public void SerializeObject(string filename, KnowlBase objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public KnowlBase DeSerializeObject(string filename)
        {
            KnowlBase objectToSerialize;
            Stream stream = null; ;
            try
            {
                stream = File.Open(filename, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                objectToSerialize = (KnowlBase)bFormatter.Deserialize(stream);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                stream.Close();
            }
            
            return objectToSerialize;
        }
    }

}
