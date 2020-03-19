
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Models
{
    public class BenefitsResponse
    {
        public BenefitsResponse() { }
        public string EmployeeId { get; set; }
        public ESPP ESPP { get; set; }
        public StayFit StayFit { get; set; }
        public HealthInsurance HealthInsurance { get; set; }
    
        
    }

    
}



/*?xml version="1.0" encoding="ISO-8859-1"?>
<BenefitsOverviewResult xmlns="http://schemas.datacontract.org/2004/07/MS.IT.HRE.Benefits.DataPoint.BenefitsOverview" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
<ErrorMessage i:nil="true"/>
<Overview>
<DentalPlanDescription/>
<ESPP>
<ESPPContributionPercent>0.15</ESPPContributionPercent>
<NextESPPEnrollmentDate>2018-3-01T00:00:00</NextESPPEnrollmentDate>
</ESPP>
<EmployeeId>999989</EmployeeId>
<FSA>
<AnnualContribution>0</AnnualContribution>
<ClaimSpendingAmount>0</ClaimSpendingAmount>
<DependentCareAmount>0</DependentCareAmount>
<FlexibleSpendingAmount>0</FlexibleSpendingAmount>
<LimitedPurposeAmount>0</LimitedPurposeAmount>
<RolloverBalance>0</RolloverBalance>
</FSA>
<HasGroupLegal>false</HasGroupLegal>
<HealthInsurancePlan>
<CoinsuranceMax>2500</CoinsuranceMax>
<CoinsuranceUsed>0</CoinsuranceUsed>
<DeductableMax>3750</DeductableMax>
<DeductibleUsed>124.25</DeductibleUsed>
<OutOfPocketLeft>306.59</OutOfPocketLeft>
<OutOfPocketMax>6250</OutOfPocketMax>
<PlanDescription/>
</HealthInsurancePlan>
<HealthSavingsAccount>
<AnnualIRSLimit>0</AnnualIRSLimit>
<Balance>14214.99</Balance>
<EmployeeContribution>0</EmployeeContribution>
<EmployerContribution>0</EmployerContribution>
</HealthSavingsAccount>
<LifeInsurance>
<AccidentalDeath>N/A</AccidentalDeath>
<Child>0</Child>
<ElectionAmount>N/A</ElectionAmount>
<LongTermDisability>0</LongTermDisability>
<PartnerAmount>0</PartnerAmount>
</LifeInsurance>
<MemberKey>100109999-01</MemberKey>
<Retirement>
<AnnualMaxContribution>32500</AnnualMaxContribution>
<Balance>734522.85</Balance>
<CompensationBonusContribution>0</CompensationBonusContribution>
<CompensationPlanBalance>0</CompensationPlanBalance>
<CompensationPlanContribution>0</CompensationPlanContribution>
<ContributionPercent>0.06</ContributionPercent>
<YtdEmployeeContribution>2133.59</YtdEmployeeContribution>
<YtdMsContribution>2014.8</YtdMsContribution>
</Retirement>
<SnapshotReleasedFlag>true</SnapshotReleasedFlag>
<StayFitPlan>
<Balance>800</Balance>
<Credit>0</Credit>
<GymName>N/A</GymName>
<Reimbursement>0</Reimbursement>
<TotalAmount>800</TotalAmount>
</StayFitPlan>
<VisionPlan>N/A</VisionPlan>
</Overview>
</BenefitsOverviewResult>*/
