using Logix.Application.Interfaces.IServices.HR;

namespace Logix.Application.Interfaces.IServices
{
    public interface IHrServiceManager
    {
        IHrEmployeeService HrEmployeeService { get; }
        IHrAbsenceService HrAbsenceService { get; }
        IHrVacationsService HrVacationsService { get; }
        IHrDelayService HrDelayService { get; }
        IInvestMonthService InvestMonthService { get; }
        IHrAttDayService HrAttDayService { get; }
        IHrPayrollTypeService HrPayrollTypeService { get; }
        IHrOverTimeDService HrOverTimeDService { get; }
        IHrKpiTemplatesCompetenceService HrKpiTemplatesCompetenceService { get; }
        IHrAttendanceService HrAttendanceService { get; }
        IHrAttShiftEmployeeService HrAttShiftEmployeeService { get; }
        IHrMandateService HrMandateService { get; }
        IHrAttTimeTableService HrAttTimeTableService { get; }
        IHrCompetenceService HrCompetenceService { get; }
        IHrCompetencesCatagoryService HrCompetencesCatagoryService { get; }
        IHrKpiTypeService HrKpiTypeService { get; }
        IHrKpiTemplateService HrKpiTemplateService { get; }
        IHrEvaluationAnnualIncreaseConfigService HrEvaluationAnnualIncreaseConfigService { get; }
        IHrDisciplinaryCaseActionService HrDisciplinaryCaseActionService { get; }
        IHrDisciplinaryCaseService HrDisciplinaryCaseService { get; }
        IHrDisciplinaryRuleService HrDisciplinaryRuleService { get; }
        IHrSettingService HrSettingService { get; }
        IHrVacationsCatagoryService HrVacationsCatagoryService { get; }
        IHrRateTypeService HrRateTypeService { get; }
        IHrVacationsTypeService HrVacationsTypeService { get; }
        IHrAllowanceDeductionService HrAllowanceDeductionService { get; }
        IHrLoanService HrLoanService { get; }
        IHrPayrollDService HrPayrollDService { get; }

        IHrArchivesFilesService HrArchivesFilesService { get; }
        IHrLicenseService HrLicenseService { get; }
        IHrTransferService HrTransferService { get; }
        IHrOverTimeMService HrOverTimeMService { get; }
        IHrOhadDetailService HrOhadDetailService { get; }
        IHrEmpWarnService HrEmpWarnService { get; }
        IHrVacationBalanceService HrVacationBalanceService { get; }
        IHrDependentService HrDependentService { get; }
        IHrDirectJobService HrDirectJobService { get; }
        IHrIncrementService HrIncrementService { get; }
        IHrLeaveService HrLeaveService { get; }
        IHrKpiService HrKpiService { get; }
        IHrKpiDetaileService HrKpiDetaileService { get; }
        IHrEmpWorkTimeService HrEmpWorkTimeService { get; }
        IHrSalaryGroupService HrSalaryGroupService { get; }
        IHrSalaryGroupAccountService HrSalaryGroupAccountService { get; }
        IHrSalaryGroupRefranceService HrSalaryGroupRefranceService { get; }
        IHrSalaryGroupAllowanceVwService HrSalaryGroupAllowanceVwService { get; }
        IHrSalaryGroupDeductionVwService HrSalaryGroupDeductionVwService { get; }
        IHrCardTemplateService HrCardTemplateService { get; }
        IHrTrainingBagService HrTrainingBagService { get; }
        IHrNotificationsTypeService HrNotificationsTypeService { get; }
        IHrNotificationsSettingService HrNotificationsSettingService { get; }
        IHrPoliciesTypeService HrPoliciesTypeService { get; }
        IHrPolicyService HrPolicyService { get; }
        IHrAttLocationService HrAttLocationService { get; }

        IHrAttTimeTableDayService HrAttTimeTableDayService { get; }
        IHrDisciplinaryActionTypeService HrDisciplinaryActionTypeService { get; }
        IHrAllowanceVwService HrAllowanceVwService { get; }
        IHrDeductionVwService HrDeductionVwService { get; }


        IHrAttActionService HrAttActionService { get; }

        IHrAllowanceDeductionMService HrAllowanceDeductionMService { get; }
        IHrAllowanceDeductionTempOrFixService HrAllowanceDeductionTempOrFixService { get; }
        IHrArchiveFilesDetailService HrArchiveFilesDetailService { get; }
        IHrAssignmenService HrAssignmenService { get; }
        //IHRAttActionService HRAttActionService { get; }


        IHrAttLocationEmployeeService HrAttLocationEmployeeService { get; }
        IHrAttShiftCloseDService HrAttShiftCloseDService { get; }
        IHrAttShiftCloseService HrAttShiftCloseService { get; }
        IHrAuthorizationService HrAuthorizationService { get; }
        IHrAttendanceBioTimeService HrAttendanceBioTimeService { get; }
        IHrCheckInOutService HrCheckInOutService { get; }
        IHrClearanceService HrClearanceService { get; }
        IHrClearanceTypeService HrClearanceTypeService { get; }
        IHrCompensatoryVacationService HrCompensatoryVacationService { get; }
        IHrContracteService HrContracteService { get; }
        IHrClearanceMonthService HrClearanceMonthService { get; }
        IHrCostTypeService HrCostTypeService { get; }
        IHrCustodyService HrCustodyService { get; }
        IHrCustodyItemService HrCustodyItemService { get; }
        IHrCustodyItemsPropertyService HrCustodyItemsPropertyService { get; }
        IHrCustodyRefranceTypeService HrCustodyRefranceTypeService { get; }
        IHrCustodyTypeService HrCustodyTypeService { get; }
        IHrDecisionService HrDecisionService { get; }
        IHrDecisionsEmployeeService HrDecisionsEmployeeService { get; }
        IHrOhadService HrOhadService { get; }
        IHrRequestTypeService HrRequestTypeService { get; }
        IHrRequestService HrRequestService { get; }
        IHrRequestDetailsService HrRequestDetailsService { get; }

        IHrNoteService HrNoteService { get; }
        IHrIncrementsAllowanceDeductionService HrIncrementsAllowanceDeductionService { get; }
        IHrHolidayService HrHolidayService { get; }
        IHrPermissionService HrPermissionService { get; }
        IHrAttShiftService HrAttShiftService { get; }
        IHrAttShiftTimeTableService HrAttShiftTimeTableService { get; }
        IHrEmployeeCostService HrEmployeeCostService { get; }

        IHrInsurancePolicyService HrInsurancePolicyService { get; }
        IHrInsuranceService HrInsuranceService { get; }
        IHrInsuranceEmpService HrInsuranceEmpService { get; }

        IHrJobService HrJobService { get; }
        IHrJobDescriptionService HrJobDescriptionService { get; }
        IHrJobEmployeeVwService HrJobEmployeeVwService { get; }
        IHrJobLevelService HrJobLevelService { get; }
        IHrRecruitmentVacancyService HrRecruitmentVacancyService { get; }
        IHrRecruitmentApplicationService HrRecruitmentApplicationService { get; }
        IHrRecruitmentCandidateService HrRecruitmentCandidateService { get; }

        IHrJobGradeService HrJobGradeService { get; }
        IHrRecruitmentCandidateKpiDService HrRecruitmentCandidateKpiDService { get; }
        IHrRecruitmentCandidateKpiService HrRecruitmentCandidateKpiService { get; }
        IHrPayrollService HrPayrollService { get; }
        IHrTicketService HrTicketService { get; }
        IHrVisaService HrVisaService { get; }
        IHrFixingEmployeeSalaryService HrFixingEmployeeSalaryService { get; }
        IHrLeaveTypeService HrLeaveTypeService { get; }
        IHrPayrollAllowanceDeductionService HrPayrollAllowanceDeductionService { get; }
        IHrLoanInstallmentPaymentService HrLoanInstallmentPaymentService { get; }
        IHrLoanInstallmentService HrLoanInstallmentService { get; }
        IHrPayrollNoteService HrPayrollNoteService { get; }
        IHrDecisionsTypeService HrDecisionsTypeService { get; }
        IHrDecisionsTypeEmployeeService HrDecisionsTypeEmployeeService { get; }
        IHrNotificationService HrNotificationService { get; }

        IHrEmployeeLocationVwService HrEmployeeLocationVwService { get; }
        IHrAttShiftEmployeeMVwService HrAttShiftEmployeeMVwService { get; }
        IHrAttendanceReport3Service HrAttendanceReport3Service { get; }
        IHrOpeningBalanceService HrOpeningBalanceService { get; }
        IHrOpeningBalanceTypeService HrOpeningBalanceTypeService { get; }
        IHrPsAllowanceDeductionService HrPsAllowanceDeductionService { get; }
        IHrPreparationSalaryService HrPreparationSalaryService { get; }
        IHrPayrollDeductionAccountsVwService HrPayrollDeductionAccountsVwService { get; }
        IHrPayrollCostcenterService HrPayrollCostcenterService { get; }
        IHrNotificationsReplyService HrNotificationsReplyService { get; }
        IHrLoanPaymentService HrLoanPaymentService { get; }
        IHrPermissionReasonVwService HrPermissionReasonVwService { get; }
        IHrPermissionTypeVwService HrPermissionTypeVwService { get; }
        IHrEmpStatusHistoryService HrEmpStatusHistoryService { get; }



        IHrLanguageService HrLanguageService { get; }
        IHrFileService HrFileService { get; }
        IHrSkillService HrSkillService { get; }
        IHrEducationService HrEducationService { get; }
        IHrWorkExperienceService HrWorkExperienceService { get; }
        IHrGosiEmployeeService HrGosiEmployeeService { get; }
        IHrGosiService HrGosiService { get; }
        IHrVacationsDayTypeService HrVacationsDayTypeService { get; }
        IHrIncrementTypeService HrIncrementTypeService { get; }
        IHrPerformanceService HrPerformanceService { get; }
        IHrKpiTemplatesJobService HrKpiTemplatesJobService { get; }
        IHrEmpGoalIndicatorService HrEmpGoalIndicatorService { get; }
        IHrEmpGoalIndicatorsEmployeeService HrEmpGoalIndicatorsEmployeeService { get; }
        IHrEmpGoalIndicatorsCompetenceService HrEmpGoalIndicatorsCompetenceService { get; }
        IHrDefinitionSalaryEmpService HrDefinitionSalaryEmpService { get; }
        IHrActualAttendanceService HrActualAttendanceService { get; }
        IHrFlexibleWorkingMasterService HrFlexibleWorkingMasterService { get; }
        IHrFlexibleWorkingService HrFlexibleWorkingService { get; }
        IHrMandateLocationDetaileService HrMandateLocationDetaileService { get; }
        IHrMandateLocationMasterService HrMandateLocationMasterService { get; }
        IHrMandateRequestsMasterService HrMandateRequestsMasterService { get; }
        IHrMandateRequestsDetaileService HrMandateRequestsDetaileService { get; }

        IHrExpensesTypeService HrExpensesTypeService { get; }
        IHrJobOfferService HrJobOfferService { get; }
        IHrExpenseService HrExpenseService { get; }
        IHrExpensesEmployeeService HrExpensesEmployeeService { get; }
        IHrJobOfferAdvantageService HrJobOfferAdvantageService { get; }
        IHrProvisionService HrProvisionService { get; }
        IHrProvisionsEmployeeService HrProvisionsEmployeeService { get; }

        IHrRequestGoalsEmployeeDetailService HrRequestGoalsEmployeeDetailService { get; }
        IHrExpensesScheduleService HrExpensesScheduleService { get; }
        IHrExpensesPaymentService HrExpensesPaymentService { get; }
        IHrIncomeTaxService HrIncomeTaxService { get; }
        IHrIncomeTaxPeriodService HrIncomeTaxPeriodService { get; }
        IHrIncomeTaxSlideService HrIncomeTaxSlideService { get; }
        IHrPayrollTransactionTypeValueService HrPayrollTransactionTypeValueService { get; }
        IHrPayrollTransactionTypeService HrPayrollTransactionTypeService { get; }
        IHrStructureService HrStructureService { get; }

        IHrVisitScheduleLocationService HrVisitScheduleLocationService { get; }
        IHrPsAllowanceVwService HrPsAllowanceVwService { get; }
        IHrPsDeductionVwService HrPsDeductionVwService { get; }
        IHrJobGroupsService HrJobGroupsService { get; }
        IHrJobCategoryService HrJobCategoryService { get; }
        IHrIncrementsAllowanceVwService HrIncrementsAllowanceVwService { get; }
        IHrIncrementsDeductionVwService HrIncrementsDeductionVwService { get; }
        IHrJobAllowanceDeductionService HrJobAllowanceDeductionService { get; }
        IHrJobLevelsAllowanceDeductionService HrJobLevelsAllowanceDeductionService { get; }
        IHrAllReportsService HrAllReportsService { get; }
        IHrLeaveAllowanceDeductionService HrLeaveAllowanceDeductionService { get; }
        IHrProvisionsMedicalInsuranceService HrProvisionsMedicalInsuranceService { get; }
        IHrProvisionsMedicalInsuranceEmployeeService HrProvisionsMedicalInsuranceEmployeeService { get; }
        IHrClearanceAllowanceDeductionService HrClearanceAllowanceDeductionService { get; }
        IHrContractsDeductionVwService HrContractsDeductionVwService { get; }
        IHrContractsAllowanceVwService HrContractsAllowanceVwService { get; }
    }
}
