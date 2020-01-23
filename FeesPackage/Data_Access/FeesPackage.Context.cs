﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FeesPackage.Data_Access
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FeesPackageEntities : DbContext
    {
        public FeesPackageEntities()
            : base("name=FeesPackageEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ReferralwithClient> ReferralwithClients { get; set; }
        public virtual DbSet<TBL_PAYMENT> TBL_PAYMENTS { get; set; }
        public virtual DbSet<tbl_PMTS_with_Comment> tbl_PMTS_with_Comments { get; set; }
        public virtual DbSet<tblAttorney> tblAttorneys { get; set; }
        public virtual DbSet<tblAttyDesc> tblAttyDescs { get; set; }
        public virtual DbSet<tblC_R> tblC_R { get; set; }
        public virtual DbSet<tblClaim> tblClaims { get; set; }
        public virtual DbSet<tblClient> tblClients { get; set; }
        public virtual DbSet<tblClientReferral> tblClientReferrals { get; set; }
        public virtual DbSet<tblCounty> tblCounties { get; set; }
        public virtual DbSet<tblPayment> tblPayments { get; set; }
        public virtual DbSet<tblReferral> tblReferrals { get; set; }
        public virtual DbSet<tblAttorneyCombo> tblAttorneyCombos { get; set; }
        public virtual DbSet<Banks_H__Martin__C__Pd_thru_date> Banks_H__Martin__C__Pd_thru_date { get; set; }
        public virtual DbSet<qryAttorneyListing> qryAttorneyListings { get; set; }
        public virtual DbSet<qryClaim_Without_Matching_tblPayment> qryClaim_Without_Matching_tblPayments { get; set; }
        public virtual DbSet<qryClaimInfo> qryClaimInfoes { get; set; }
        public virtual DbSet<qryClaimPaymentTotal> qryClaimPaymentTotals { get; set; }
        public virtual DbSet<qryClaims_30Days> qryClaims_30Days { get; set; }
        public virtual DbSet<qryClaims_30Days2> qryClaims_30Days2 { get; set; }
        public virtual DbSet<qryClaimsDormant> qryClaimsDormants { get; set; }
        public virtual DbSet<qryClaimsDormant2> qryClaimsDormant2 { get; set; }
        public virtual DbSet<qryClaimTotal> qryClaimTotals { get; set; }
        public virtual DbSet<qryClientClaimList> qryClientClaimLists { get; set; }
        public virtual DbSet<qryClientClaimListteststatu> qryClientClaimListteststatus { get; set; }
        public virtual DbSet<qryClientClaimListtstatuswith_referal_Query> qryClientClaimListtstatuswith_referal_Queries { get; set; }
        public virtual DbSet<qryClientList> qryClientLists { get; set; }
        public virtual DbSet<qryDailyTotal> qryDailyTotals { get; set; }
        public virtual DbSet<qryDeposit> qryDeposits { get; set; }
        public virtual DbSet<qryMonthlyIncome__AJS_Weeklies> qryMonthlyIncome__AJS_Weeklies { get; set; }
        public virtual DbSet<qryMonthlyIncome__AJS_Weeklies__no_posted_yet> qryMonthlyIncome__AJS_Weeklies__no_posted_yet { get; set; }
        public virtual DbSet<qryPaymentsView> qryPaymentsViews { get; set; }
        public virtual DbSet<qryShowAllClient> qryShowAllClients { get; set; }
        public virtual DbSet<qrySpecialSearch> qrySpecialSearches { get; set; }
        public virtual DbSet<Query_Hand_Credit_Atty> Query_Hand_Credit_Atty { get; set; }
        public virtual DbSet<tblClient_Query__PL_HANDLING> tblClient_Query__PL_HANDLING { get; set; }
        public virtual DbSet<tblClient_Without_Matching_tblClientReferral> tblClient_Without_Matching_tblClientReferrals { get; set; }
        public virtual DbSet<tblInsurance> tblInsurances { get; set; }
    }
}
