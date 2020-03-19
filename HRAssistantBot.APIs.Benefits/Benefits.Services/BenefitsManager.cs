using MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MS.CFE.FHL.Hack.SmartAssistant.APIs.Benefits.Services
{
    public class BenefitsManager : IBenefitsManager
    {
        public BenefitsManager()
        {

        }
        public BenefitsResponse GetBenefitsAsync()
        {
            List<BenefitsResponse> benefitsData = new List<BenefitsResponse>();
            benefitsData.Add(new BenefitsResponse()
            {
                EmployeeId = "362865",
                StayFit = new StayFit()
                {
                    Balance = 400,
                    Credit = 200,
                    Reimbursement = 200,
                    TotalAmount = 800
                },
                ESPP = new ESPP()
                {
                    ESPPContibutionPercent = 15,
                    NextESPPEnrollmentDate = DateTime.Now.AddMonths(3).Date
                },
                HealthInsurance = new HealthInsurance()
                {
                    CoinsuranceMax = 1500,
                    CoinsuranceUsed = 200,
                    DeductableMax = 1000,
                    DeductibleUsed = 100,
                    OutOfPocketLeft = 4000,
                    OutOfPocketMax = 5000,
                    PlanDescription = "Premera"
                }
            });
            benefitsData.Add(new BenefitsResponse()
            {
                EmployeeId = "825678",
                StayFit = new StayFit()
                {
                    Balance = 800,
                    Credit = 0,
                    Reimbursement = 0,
                    TotalAmount = 800
                },
                ESPP = new ESPP()
                {
                    ESPPContibutionPercent = 10,
                    NextESPPEnrollmentDate = DateTime.Now.AddMonths(3).Date
                },
                HealthInsurance = new HealthInsurance()
                {
                    CoinsuranceMax = 1400,
                    CoinsuranceUsed = 100,
                    DeductableMax = 1100,
                    DeductibleUsed = 200,
                    OutOfPocketLeft = 3000,
                    OutOfPocketMax = 5000,
                    PlanDescription = "GroupHealth"
                }
            });
            benefitsData.Add(new BenefitsResponse()
            {
                EmployeeId = "123456",
                StayFit = new StayFit()
                {
                    Balance = 0,
                    Credit = 100,
                    Reimbursement = 700,
                    TotalAmount = 800
                },
                ESPP = new ESPP()
                {
                    ESPPContibutionPercent = 10,
                    NextESPPEnrollmentDate = DateTime.Now.AddMonths(3).Date
                },
                HealthInsurance = new HealthInsurance()
                {
                    CoinsuranceMax = 1400,
                    CoinsuranceUsed = 100,
                    DeductableMax = 1100,
                    DeductibleUsed = 200,
                    OutOfPocketLeft = 3000,
                    OutOfPocketMax = 5000,
                    PlanDescription = "GroupHealth"
                }
            });
            benefitsData.Add(new BenefitsResponse()
            {
                EmployeeId = "324456",
                StayFit = new StayFit()
                {
                    Balance = 100,
                    Credit = 100,
                    Reimbursement = 200,
                    TotalAmount = 400
                },
                ESPP = new ESPP()
                {
                    ESPPContibutionPercent = 10,
                    NextESPPEnrollmentDate = DateTime.Now.AddMonths(3).Date
                },
                HealthInsurance = new HealthInsurance()
                {
                    CoinsuranceMax = 1400,
                    CoinsuranceUsed = 100,
                    DeductableMax = 1100,
                    DeductibleUsed = 200,
                    OutOfPocketLeft = 3000,
                    OutOfPocketMax = 5000,
                    PlanDescription = "GroupHealth"
                }
            });
            benefitsData.Add(new BenefitsResponse()
            {
                EmployeeId = "982233",
                StayFit = new StayFit()
                {
                    Balance = 500,
                    Credit = 100,
                    Reimbursement = 300,
                    TotalAmount = 800
                },
                ESPP = new ESPP()
                {
                    ESPPContibutionPercent = 10,
                    NextESPPEnrollmentDate = DateTime.Now.AddMonths(3).Date
                },
                HealthInsurance = new HealthInsurance()
                {
                    CoinsuranceMax = 1400,
                    CoinsuranceUsed = 100,
                    DeductableMax = 1100,
                    DeductibleUsed = 200,
                    OutOfPocketLeft = 3000,
                    OutOfPocketMax = 5000,
                    PlanDescription = "GroupHealth"
                }
            });

            return benefitsData[new Random().Next(5)];
        }

      
    }
}
