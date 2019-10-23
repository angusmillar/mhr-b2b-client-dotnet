/*
 * Copyright 2012 NEHTA
 *
 * Licensed under the NEHTA Open Source (Apache) License; you may not use this
 * file except in compliance with the License. A copy of the License is in the
 * 'license.txt' file, which should be provided with this work.
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.PCEHR;
using Nehta.VendorLibrary.PCEHR.PCEHRProfile;
using System.Net;
using System.Net.Security;

namespace PCEHR.Sample
{
    /// <summary>
    /// Sample demonstrating how to use the 'GainPCEHRAccess' client.
    /// </summary>
    /// 
    /// See the Record Access Logical Service Specification and the Technical Service Specification
    /// https://www.digitalhealth.gov.au/implementation-resources/national-infrastructure/EP-2109-2015
    /// 
    public class GainPCEHRAccessClientSample
    {
        public void Sample()
        {
            // Obtain the certificate for use with TLS and signing
            X509Certificate2 cert = X509CertificateUtil.GetCertificate(
                "06fba6",
                X509FindType.FindBySerialNumber,
                StoreName.My,
                StoreLocation.CurrentUser,
                true
                );

<<<<<<< HEAD
            // Create the PCEHR header
            PCEHRHeader pcehrHeader = new PCEHRHeader();

            // IHI is always 16 digits long starting 800360.
            pcehrHeader.ihiNumber = "8003608666885561";

            // User Id may be a HPI-I for those who are HPI-I eligible (AHPRA registered)
            // HPI-I is always 16 digits long starting 800361
            // For other users such as administration and support staff user ID is set to their local ID
            pcehrHeader.User = new PCEHRHeaderUser();
            pcehrHeader.User.ID = "AngusM";
            // Set User.IDType to PCEHRHeaderUserIDType.LocalSystemIdentifier if the user.ID is a local ID rather than a HPI-I
            pcehrHeader.User.IDType = PCEHRHeaderUserIDType.LocalSystemIdentifier;
            pcehrHeader.User.userName = "Angus Millar";

            pcehrHeader.accessingOrganisation = new PCEHRHeaderAccessingOrganisation();
            pcehrHeader.accessingOrganisation.organisationName = "AngusADHA";
            // HPI-O is always 16 digits long starting 800362
            pcehrHeader.accessingOrganisation.organisationID = "8003629900019338";

            pcehrHeader.clientSystemType = PCEHRHeaderClientSystemType.CIS;
            // The below information can be found in the My Health Record Vendor Product 
            // Details Form that you filled out and submitted
            pcehrHeader.productType = new PCEHRHeaderProductType();
            pcehrHeader.productType.platform = "Windows";
            pcehrHeader.productType.productName = "NEHTA HIPS";
            pcehrHeader.productType.productVersion = "6.1";
            pcehrHeader.productType.vendor = "HIPS0001";
=======
            // Create PCEHR header
            CommonPcehrHeader header = PcehrHeaderHelper.CreateHeader();
            // Override this value to the current patient's IHI.
            header.IhiNumber = "IHI";
>>>>>>> ADHA/dependabot/nuget/src/PCEHR/DotNetZip-1.11.0

            // Instantiate the client
            // SVT endpoint is "https://b2b.ehealthvendortest.health.gov.au/gainPCEHRAccess"
            // production endpoint is "https://services.ehealth.gov.au/gainPCEHRAccess"
            GainPCEHRAccessClient gainPcehrAccessClient = new GainPCEHRAccessClient(new Uri("https://b2b.ehealthvendortest.health.gov.au/gainPCEHRAccess"), cert, cert);

            // Add server certificate validation callback
            ServicePointManager.ServerCertificateValidationCallback += ValidateServiceCertificate;

            // Create the access request
            gainPCEHRAccessPCEHRRecord accessRequest = new gainPCEHRAccessPCEHRRecord();
            // if gaining access without a code, authorisationDetails is not required
            // if gaining access with a code, include authorisationDetails, accessCode and accessType
            // if gaining emergency access, include authorisationDetails and set accessType to “EmergencyAccess”
            // Only include the below to pass an access code or gain emergency access
            accessRequest.authorisationDetails = new gainPCEHRAccessPCEHRRecordAuthorisationDetails();
<<<<<<< HEAD
            // "patient access code" is not required if the patient has open access for there record
            accessRequest.authorisationDetails.accessCode = "";
=======
            accessRequest.authorisationDetails.accessCode = "patient access code";
>>>>>>> ADHA/dependabot/nuget/src/PCEHR/DotNetZip-1.11.0
            accessRequest.authorisationDetails.accessType = gainPCEHRAccessPCEHRRecordAuthorisationDetailsAccessType.AccessCode;

      
             gainPCEHRAccessResponseIndividual individual = new gainPCEHRAccessResponseIndividual();
            
            try
            {
                // Invoke the service
                responseStatusType responseStatus = gainPcehrAccessClient.GainPCEHRAccess(header, accessRequest, out individual);

                // Get the soap request and response
                string soapRequest = gainPcehrAccessClient.SoapMessages.SoapRequest;
                string soapResponse = gainPcehrAccessClient.SoapMessages.SoapResponse;
            }
            catch (FaultException fex)
            {
                // Handle any errors
            }            
        }

        private bool ValidateServiceCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Checks can be done here to validate the service certificate.
            // If the service certificate contains any problems or is invalid, return false. Otherwise, return true.
            // This example returns true to indicate that the service certificate is valid.
            return true;
        }
    }
}
