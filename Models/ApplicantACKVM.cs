namespace CommercialAgriEnterprise.Models
{
    public class ApplicantACKVM
    {
        public string ReferenceNumber { get; set; }
        public string Photo { get; set; }
        public string DateOfApplication { get; set; }
        public string AppliedTo { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
        public string GP { get; set; }
        public string Village { get; set; }
        public string FarmerID { get; set; }
        public string ApplicantType { get; set; }
        public string ApplicantName { get; set; }
        public string FatherorHusbandName { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string MobileNumber { get; set; }
        public string GroupType { get; set; }
        public int? GroupTypeCode { get; set; }
        public string FirmName { get; set; }
        public int? GMNumber { get; set; }
        public string EducationalQualification { get; set; }
        public string EducationalCertificate { get; set; }
        public decimal AnnualIncome { get; set; }
        public string PresentOccupation { get; set; }
        public string PreviousExperience { get; set; }
        public string EmailID { get; set; }
        public string Pincode { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Department { get; set; }
        public string Project { get; set; }
        //public string IntegratedProjects { get; set; }
        public string ProductsProduced { get; set; }
        public decimal? Capacity { get; set; }
        public string CapacityUnit { get; set; }
        public string FinanceType { get; set; }
        public string BankConsentLeter { get; set; }
        public int BankCode { get; set; }
        public string Bank { get; set; }
        public int? BranchCode { get; set; }
        public string Branch { get; set; }
        public string IsCISPreviouslyAvailed { get; set; }
        public string CISProject { get; set; }
        public string CISLocation { get; set; }
        public string CISYear { get; set; }
        public string CISFinanceType { get; set; }
        public decimal? CISAmount { get; set; }
        public string CISBankConsentLeter { get; set; }
        public string ApproximateCost { get; set; }
        public decimal OwnContribution { get; set; }
        public decimal? BankLoan { get; set; }
        public decimal? TotalCost { get; set; }
        public string IdentityProof { get; set; }
        public string Signature { get; set; }
    }
}