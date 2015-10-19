﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImpRec.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ImpRec.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;TrackerCase&gt;&lt;ID&gt;33734&lt;/ID&gt;
        ///&lt;IA&gt;
        ///** Impact Analysis&quot;,&quot;asadashi&quot;,&quot;2008-05-28&quot;,&quot;05:14:05&quot;,&quot;2008-05-28&quot;,&quot;05:14:05&quot;,&quot;1)  Q: Is the reported problem Safety Critical?
        ///    A: No. S3964R is an interface free code and cannot be run in a HI controller.
        ///2a) Q: Describe how the problem shall be solved.
        ///    A: The present implementation sets the Thread id to 0 in Threadwork function when a hot removal is detected and on Hot
        ///insert it is recreated.
        ///       The change done is not to set the thread id to 0 in threadwork fun [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string impactAnalyses {
            get {
                return ResourceManager.GetString("impactAnalyses", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 20000
        ///ControlLib: A printed warning has to occur when the oscillation is limited.
        ///A printed warning has to occur when the selected Max relay amplitude limits the oscillation. The calculated parameters may not get relevant values. And the controller may get worse parameters than before the tuning efforts. INFORMER: Bengt Hansson
        ///
        ///20001
        ///Unfavourable behaviour after power fail.
        ///&quot;Unfavourable behaviour after power fail. The control loop shall go to backtracking mode and backtrack the I/O-value for one sca [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string issueText {
            get {
                return ResourceManager.GetString("issueText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;root&gt;
        ///&lt;TEST ID=&quot;3bse031209&quot;/&gt;
        ///&lt;REQ ID=&quot;msm-dof-002&quot;/&gt;
        ///&lt;UNSPECIFIED ID=&quot;3bse036351&quot;/&gt;
        ///&lt;REQ ID=&quot;pr-dlv-050&quot;/&gt;
        ///&lt;UNSPECIFIED ID=&quot;3bse038691&quot;/&gt;
        ///&lt;REQ ID=&quot;sac-dof-102&quot;/&gt;
        ///&lt;REQ ID=&quot;pa-fct-503&quot;/&gt;
        ///&lt;REQ ID=&quot;sac-dof-10&quot;/&gt;
        ///&lt;UNSPECIFIED ID=&quot;3bse032063&quot;/&gt;
        ///&lt;HARDWARE ID=&quot;h_s800iomodulebushwlib&quot;/&gt;
        ///&lt;UNSPECIFIED ID=&quot;3bse049734&quot;/&gt;
        ///&lt;TEST ID=&quot;3bse025115&quot;/&gt;
        ///&lt;REQ ID=&quot;ccp-dof-001&quot;/&gt;
        ///&lt;REQ ID=&quot;hio-dof-0700&quot;/&gt;
        ///&lt;REQ ID=&quot;ccp-dof-007&quot;/&gt;
        ///&lt;UNSPECIFIED ID=&quot;3bse032357&quot;/&gt;
        ///&lt;REQ ID=&quot;sfc-do [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string semanticNetwork {
            get {
                return ResourceManager.GetString("semanticNetwork", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 3bse030453,DoF Modulebus Protocol Handler
        ///3bse030454,DoF Modulebus in SafetyModule
        ///3bse030691,PTTD Global Test
        ///3bse030775,DoF Modulebus in AC800M
        ///3bse030898,FTTD Basic Library
        ///3bse030937,FTTD IOAccess in IOC Framework
        ///3bse030940,FTTD Config in IOC Framework
        ///3bse030942,DTD HWIOAccess Config
        ///3bse031163,FTTD SM Protocol Handler
        ///3bse031312,Design Test Description CRC
        ///3bse031350,DTD Controller Configuration Integrity
        ///3bse031352,DTD Overrun and Latency
        ///3bse031484,DTD Virtual Machine Test and Configura [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string translation {
            get {
                return ResourceManager.GetString("translation", resourceCulture);
            }
        }
    }
}
