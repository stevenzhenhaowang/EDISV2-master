using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Edis.Db.ExpenseRecords;
using Shared;

namespace Edis.Db
{

    //This is a line to test github
    public class Adviser
    {
        [Key]
        public string AdviserId { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; }

        [Required]
        public string AdviserNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int VerifiedId { get; set; }


        //Personal Details
        public string ABNACN { get; set;}
        public string Fax { get; set;}
        public string Mobile { get; set;}
        public string Phone { get; set;}
        public string CompanyName { get; set;}
        public string CurrentTitle { get; set;}
        public string Gender { get; set;}
        public DateTime? ExperienceStartDate { get; set;}
        public string MiddleName { get; set;}
        public string Title { get; set;}
        public double? Lng { get; set; }
        public double? Lat { get; set; }
        public DateTime? LastUpdate { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }


        //Business Details
        public DateTime? IndustryExperienceStartDate { get; set; }
        public string BusinessPhone { get; set; }
        public string BusinessMobile { get; set; }
        public string BusinessFax { get; set; }
        public string AddressLn1 { get; set; }
        public string AddressLn2 { get; set; }
        public string AddressLn3 { get; set; }
        public string State { get; set; }
        public string Suburb { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }

        //Dealer Group Details
        public string RoleAndServicesSummary { get; set; }
        public string DealerGroupName { get; set; }
        public string Asfl { get; set; }
        public string DAddressLine1 { get; set; }
        public string DAddressLine2 { get; set; }
        public string DAddressLine3 { get; set; }
        public string DSuburb { get; set; }
        public string DState { get; set; }
        public string DPostcode { get; set; }
        public string DCountry { get; set; }
        public bool DealerGroupHasDerivativesLicense { get; set; }

        //Asset and Investment Details
        public bool IsAuthorizedRepresentative { get; set; }
        public string AuthorizedRepresentativeNumber { get; set; }
        public string TotalAssetUnderManagement { get; set; }
        public string TotalInvestmentUndermanagement { get; set; }
        public string TotalDirectAustralianEquitiesUnderManagement { get; set; }
        public string TotalDirectInterantionalEquitiesUnderManagement { get; set; }
        public string TotalDirectFixedInterestUnderManagement { get; set; }
        public string TotalDirectLendingBookInterestUnderManagement { get; set; }
        public string ApproximateNumberOfClients { get; set; }
        
        //Education Details
        public string Institution { get; set; }
        public int EducationLevelId { get; set; }
        public string CourseTitle { get; set; }
        public bool CourseStatus { get; set; } 

        //Client Management Settings
        public string ProfessiontypeId { get; set; }
        
        public int NewsLetterServiceId { get; set; }
        public string NewsLetterServiceName { get; set; }
        public bool NewsLetterSelected { get; set; }

        public string GroupName { get; set; }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool Providing { get; set; }

        public int CAFId { get; set; }
        public bool CAFSelected { get; set; }
        public string CAFDescription { get; set; }

        public bool RemunerationMethodSpecified { get; set; }
        public string RemunerationMethod { get; set; }
        public int NumberOfClientsId { get; set; }
        public int AnnualIncomeLevelId { get; set; }
        public int InvestibleAssetLevel { get; set; }
        public int TotalAssetLevelId { get; set; }
        public string TotalAssetLevel { get; set; }





        public virtual ICollection<TransactionExpense> TransactionExpenses { get; set; }
        public virtual ICollection<ConsultancyExpense> ConsultancyExpenses { get; set; }



    }
}