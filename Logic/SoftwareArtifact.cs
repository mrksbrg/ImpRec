using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImpRec
{
    /// <summary>
    /// Abstract super class describing general information about software artifacts.
    /// </summary>
    [Serializable()]
    public abstract class SoftwareArtifact : ISerializable
    {
        protected string artifactID;
        protected string title;
        protected string type;
        protected int nbrInLinks;
        protected double centrality;

        public SoftwareArtifact()
        {
        }

        protected SoftwareArtifact(string artifactID, string type)
        {
            this.artifactID = artifactID;
            this.title = "";
            this.type = type;

            if (artifactID == null)
            {
                artifactID = "UnknownID";
            }
        }

        #region Serialization
        protected SoftwareArtifact(SerializationInfo info, StreamingContext ctxt)
        {
            this.artifactID = (String)info.GetValue("ArtifactID", typeof(String));
            this.title = (String)info.GetValue("Title", typeof(String));
            this.type = (String)info.GetValue("Type", typeof(String));
            this.nbrInLinks = (int)info.GetValue("NbrInLinks", typeof(int));
            this.centrality = (double)info.GetValue("Centrality", typeof(double));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("ArtifactID", this.artifactID);
            info.AddValue("Title", this.title);
            info.AddValue("Type", this.type);
            info.AddValue("NbrInLinks", this.nbrInLinks);
            info.AddValue("Centrality", this.centrality);
        }
        #endregion
 
        public string ArtifactID
        {
            get { return artifactID; }
            set { artifactID = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }  
        
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
   
        public int NbrInLinks
        {
            get { return nbrInLinks; }
            set { nbrInLinks = value; }
        }

        public double Centrality
        {
            get { return centrality; }
            set { centrality = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            SoftwareArtifact sa = obj as SoftwareArtifact;
            if ((System.Object)sa == null)
            {
                return false;
            }
            return this.artifactID.Equals(sa.artifactID);
        }

        public abstract String ToXmlElement();

    }
}
