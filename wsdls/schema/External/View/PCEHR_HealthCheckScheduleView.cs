﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.8009
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 
namespace Nehta.VendorLibrary.PCEHR.HealthCheckScheduleView {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://ns.electronichealth.net.au/pcehr/xsd/interfaces/HealthCheckScheduleView/1." +
        "0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://ns.electronichealth.net.au/pcehr/xsd/interfaces/HealthCheckScheduleView/1." +
        "0", IsNullable=false)]
    public partial class healthCheckScheduleView {
        
        private string versionNumberField;
        
        private healthCheckScheduleViewJurisdiction jurisdictionField;
        
        /// <remarks/>
        public string versionNumber {
            get {
                return this.versionNumberField;
            }
            set {
                this.versionNumberField = value;
            }
        }
        
        /// <remarks/>
        public healthCheckScheduleViewJurisdiction jurisdiction {
            get {
                return this.jurisdictionField;
            }
            set {
                this.jurisdictionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://ns.electronichealth.net.au/pcehr/xsd/interfaces/HealthCheckScheduleView/1." +
        "0")]
    public enum healthCheckScheduleViewJurisdiction {
        
        /// <remarks/>
        NSW,
        
        /// <remarks/>
        QLD,
        
        /// <remarks/>
        ACT,
        
        /// <remarks/>
        NT,
        
        /// <remarks/>
        VIC,
        
        /// <remarks/>
        WA,
        
        /// <remarks/>
        TAS,
        
        /// <remarks/>
        SA,
    }
}
