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
namespace Nehta.VendorLibrary.PCEHR.PrescriptionAndDispenseView {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://ns.electronichealth.net.au/pcehr/xsd/interfaces/PrescriptionAndDispenseVie" +
        "w/1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://ns.electronichealth.net.au/pcehr/xsd/interfaces/PrescriptionAndDispenseVie" +
        "w/1.0", IsNullable=false)]
    public partial class prescriptionAndDispenseView {
        
        private string versionNumberField;
        
        private System.DateTime fromDateField;
        
        private System.DateTime toDateField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime fromDate {
            get {
                return this.fromDateField;
            }
            set {
                this.fromDateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime toDate {
            get {
                return this.toDateField;
            }
            set {
                this.toDateField = value;
            }
        }
    }
}
