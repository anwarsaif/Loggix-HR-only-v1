using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.HR;
using Logix.Domain.HR;

namespace Logix.Application.Services
{
    public class HrServiceManager : IHrServiceManager
    {
        private readonly IHrEmployeeService hrEmployeeService;
        private readonly IHrAttDayService hrAttDayService;
        private readonly IHrAbsenceService hrAbsenceService;
        private readonly IHrVacationsService hrVacationsService;
        private readonly IHrDelayService hrDelayService;
        private readonly IInvestMonthService investMonthService;
        private readonly IHrOverTimeDService hrOverTimeDService;
        private readonly IHrPayrollTypeService hrPayrollTypeService;
        private readonly IHrKpiTemplatesCompetenceService hrKpiTemplatesCompetenceService;
        private readonly IHrAttendanceService hrAttendanceService;
        private readonly IHrAttShiftEmployeeService hrAttShiftEmployeeService;
        private readonly IHrMandateService hrMandateService;
        private readonly IHrAttTimeTableService hrAttTimeTableService;
        private readonly IHrCompetenceService hrCompetenceService;
        private readonly IHrCompetencesCatagoryService hrCompetencesCatagoryService;
        private readonly IHrKpiTypeService hrKpiTypeService;
        private readonly IHrKpiTemplateService hrKpiTemplateService;
        private readonly IHrEvaluationAnnualIncreaseConfigService hrEvaluationAnnualIncreaseConfigService;
        private readonly IHrDisciplinaryCaseActionService hrDisciplinaryCaseActionService;
        private readonly IHrDisciplinaryCaseService hrDisciplinaryCaseService;
        private readonly IHrDisciplinaryRuleService hrDisciplinaryRuleService;
        private readonly IHrSettingService hrSettingService;
        private readonly IHrVacationsCatagoryService hrVacationsCatagoryService;
        private readonly IHrRateTypeService hrRateTypeService;
        private readonly IHrVacationsTypeService hrVacationsTypeService;
        private readonly IHrAllowanceDeductionService hrAllowanceDeductionService;
        private readonly IHrLoanService hrLoanService;
        private readonly IHrPayrollDService hrPayrollDService;
        private readonly IHrArchivesFilesService hrArchivesFilesService;
        private readonly IHrLicenseService hrLicenseService;
        private readonly IHrTransferService hrTransferService;
        private readonly IHrOverTimeMService hrOverTimeMService;
        private readonly IHrOhadDetailService hrOhadDetailService;
        private readonly IHrEmpWarnService hrEmpWarnService;
        private readonly IHrVacationBalanceService hrVacationBalanceService;
        private readonly IHrDependentService hrDependentService;
        private readonly IHrDirectJobService hrDirectJobService;
        private readonly IHrIncrementService hrIncrementService;
        private readonly IHrLeaveService hrLeaveService;
        private readonly IHrKpiService hrKpiService;
        private readonly IHrKpiDetaileService hrKpiDetaileService;
        private readonly IHrEmpWorkTimeService hrEmpWorkTimeService;
        private readonly IHrSalaryGroupService hrSalaryGroupService;
        private readonly IHrSalaryGroupRefranceService hrSalaryGroupRefranceService;
        private readonly IHrSalaryGroupAccountService hrSalaryGroupAccountService;
        private readonly IHrSalaryGroupDeductionVwService hrSalaryGroupDeductionVwService;
        private readonly IHrSalaryGroupAllowanceVwService hrSalaryGroupAllowanceVwService;
        private readonly IHrCardTemplateService hrCardTemplateService;
        private readonly IHrTrainingBagService hrTrainingBagService;
        private readonly IHrNotificationsTypeService hrNotificationsTypeService;
        private readonly IHrNotificationsSettingService hrNotificationsSettingService;
        private readonly IHrPolicyService hrPolicyService;
        private readonly IHrPoliciesTypeService hrPoliciesTypeService;
        private readonly IHrAttLocationService hrAttLocationService;
        private readonly IHrAttTimeTableDayService hrAttTimeTableDayService;
        private readonly IHrDisciplinaryActionTypeService hrDisciplinaryActionTypeService;
        private readonly IHrDeductionVwService hrDeductionVwService;
        private readonly IHrAllowanceVwService hrAllowanceVwService;
        private readonly IHrAllowanceDeductionMService hrAllowanceDeductionMService;
        private readonly IHrAllowanceDeductionTempOrFixService hrAllowanceDeductionTempOrFixService;
        private readonly IHrAssignmenService hrAssignmenService;
        private readonly IHrAttLocationEmployeeService hrAttLocationEmployeeService;
        private readonly IHrAttShiftCloseDService hrAttShiftCloseDService;
        private readonly IHrClearanceService hrClearanceService;
        private readonly IHrClearanceTypeService hrClearanceTypeService;
        private readonly IHrArchiveFilesDetailService hrArchiveFilesDetailService;
        private readonly IHrAttActionService hrAttActionService;
        private readonly IHrAttShiftCloseService hrAttShiftCloseService;
        private readonly IHrAuthorizationService hrAuthorizationService;
        private readonly IHrAttendanceBioTimeService hrAttendanceBioTimeService;
        private readonly IHrCheckInOutService hrCheckInOutService;
        private readonly IHrCompensatoryVacationService hrCompensatoryVacationService;
        private readonly IHrContracteService hrContracteService;
        private readonly IHrClearanceMonthService hrClearanceMonthService;
        private readonly IHrCostTypeService hrCostTypeService;
        private readonly IHrCustodyService hrCustodyService;
        private readonly IHrCustodyItemService hrCustodyItemService;
        private readonly IHrCustodyItemsPropertyService hrCustodyItemsPropertyService;
        private readonly IHrCustodyRefranceTypeService hrCustodyRefranceTypeService;
        private readonly IHrCustodyTypeService hrCustodyTypeService;
        private readonly IHrDecisionService hrDecisionService;
        private readonly IHrDecisionsEmployeeService hrDecisionsEmployeeService;
        private readonly IHrOhadService hrOhadService;
        private readonly IHrRequestDetailsService hrRequestDetailsService;
        private readonly IHrRequestService hrRequestService;
        private readonly IHrRequestTypeService hrRequestTypeService;
        private readonly IHrNoteService hrNoteService;
        private readonly IHrIncrementsAllowanceDeductionService hrIncrementsAllowanceDeductionService;

        private readonly IHrHolidayService hrHolidayService;
        private readonly IHrPermissionService hrPermissionService;
        private readonly IHrAttShiftService hrAttShiftService;
        private readonly IHrAttShiftTimeTableService hrAttShiftTimeTableService;
        private readonly IHrEmployeeCostService hrEmployeeCostService;

        private readonly IHrInsurancePolicyService hrInsurancePolicyService;
        private readonly IHrInsuranceService hrInsuranceService;
        private readonly IHrInsuranceEmpService hrInsuranceEmpService;

        private readonly IHrJobService hrJobService;
        private readonly IHrJobDescriptionService hrJobDescriptionService;
        private readonly IHrJobEmployeeVwService hrJobEmployeeVwService;
        private readonly IHrJobLevelService hrJobLevelService;
        private readonly IHrRecruitmentApplicationService hrRecruitmentApplicationService;
        private readonly IHrRecruitmentVacancyService hrRecruitmentVacancyService;
        private readonly IHrRecruitmentCandidateService hrRecruitmentCandidateService;
        private readonly IHrJobGradeService hrJobGradeService;

        private readonly IHrRecruitmentCandidateKpiService hrRecruitmentCandidateKpiService;
        private readonly IHrRecruitmentCandidateKpiDService hrRecruitmentCandidateKpiDService;
        private readonly IHrPayrollService hrPayrollService;
        private readonly IHrTicketService hrTicketService;
        private readonly IHrVisaService hrVisaService;
        private readonly IHrFixingEmployeeSalaryService hrFixingEmployeeSalaryService;
        private readonly IHrLeaveTypeService hrLeaveTypeService;
        private readonly IHrPayrollAllowanceDeductionService hrPayrollAllowanceDeductionService;

        private readonly IHrLoanInstallmentPaymentService hrLoanInstallmentPaymentService;

        private readonly IHrLoanInstallmentService hrLoanInstallmentService;
        private readonly IHrPayrollNoteService hrPayrollNoteService;
        private readonly IHrDecisionsTypeService hrDecisionsTypeService;
        private readonly IHrDecisionsTypeEmployeeService hrDecisionsTypeEmployeeService;
        private readonly IHrNotificationService hrNotificationService;
        private readonly IHrEmployeeLocationVwService hrEmployeeLocationVwService;
        private readonly IHrAttShiftEmployeeMVwService hrAttShiftEmployeeMVwService;
        private readonly IHrAttendanceReport3Service hrAttendanceReport3Service;
        private readonly IHrOpeningBalanceService hrOpeningBalanceService;
        private readonly IHrOpeningBalanceTypeService hrOpeningBalanceTypeService;
        private readonly IHrPsAllowanceDeductionService hrPsAllowanceDeductionService;
        private readonly IHrPreparationSalaryService hrPreparationSalaryService;
        private readonly IHrPayrollDeductionAccountsVwService hrPayrollDeductionAccountsVwService;
        private readonly IHrPayrollCostcenterService hrPayrollCostcenterService;
        private readonly IHrNotificationsReplyService hrNotificationsReplyService;
        private readonly IHrLoanPaymentService hrLoanPaymentService;
        private readonly IHrPermissionTypeVwService hrPermissionTypeVwService;
        private readonly IHrPermissionReasonVwService hrPermissionReasonVwService;
        private readonly IHrEmpStatusHistoryService hrEmpStatusHistoryService;

        private readonly IHrLanguageService hrLanguageService;
        private readonly IHrFileService hrFileService;
        private readonly IHrSkillService hrSkillService;
        private readonly IHrEducationService hrEducationService;
        private readonly IHrWorkExperienceService hrWorkExperienceService;
        private readonly IHrGosiEmployeeService hrGosiEmployeeService;
        private readonly IHrGosiService hrGosiService;
        private readonly IHrVacationsDayTypeService hrVacationsDayTypeService;
        private readonly IHrIncrementTypeService hrIncrementTypeService;
        private readonly IHrPerformanceService hrPerformanceService;
        private readonly IHrKpiTemplatesJobService hrKpiTemplatesJobService;
        private readonly IHrEmpGoalIndicatorService hrEmpGoalIndicatorService;
        private readonly IHrEmpGoalIndicatorsEmployeeService hrEmpGoalIndicatorsEmployeeService;
        private readonly IHrEmpGoalIndicatorsCompetenceService hrEmpGoalIndicatorsCompetenceService;
        private readonly IHrDefinitionSalaryEmpService hrDefinitionSalaryEmpService;
        private readonly IHrActualAttendanceService hrActualAttendanceService;
        private readonly IHrFlexibleWorkingMasterService hrFlexibleWorkingMasterService;
        private readonly IHrFlexibleWorkingService hrFlexibleWorkingService;
        private readonly IHrMandateLocationDetaileService hrMandateLocationDetaileService;
        private readonly IHrMandateLocationMasterService hrMandateLocationMasterService;
        private readonly IHrMandateRequestsMasterService hrMandateRequestsMasterService;
        private readonly IHrMandateRequestsDetaileService hrMandateRequestsDetaileService;
        private readonly IHrExpensesTypeService hrExpensesTypeService;
        private readonly IHrJobOfferService hrJobOfferService;
        private readonly IHrExpenseService hrExpenseService;
        private readonly IHrExpensesEmployeeService hrExpensesEmployeeService;
        private readonly IHrJobOfferAdvantageService hrJobOfferAdvantageService;
        private readonly IHrProvisionService hrProvisionService;
        private readonly IHrProvisionsEmployeeService hrProvisionsEmployeeService;
        private readonly IHrRequestGoalsEmployeeDetailService hrRequestGoalsEmployeeDetailService;
        private readonly IHrExpensesPaymentService hrExpensesPaymentService;
        private readonly IHrExpensesScheduleService hrExpensesScheduleService;
        private readonly IHrIncomeTaxService hrIncomeTaxService;
        private readonly IHrIncomeTaxPeriodService hrIncomeTaxPeriodService;
        private readonly IHrIncomeTaxSlideService hrIncomeTaxSlideService;
        private readonly IHrPayrollTransactionTypeService hrPayrollTransactionTypeService;
        private readonly IHrPayrollTransactionTypeValueService hrPayrollTransactionTypeValueService;
        private readonly IHrStructureService hrStructureService;

        private readonly IHrVisitScheduleLocationService hrVisitScheduleLocationService;
        private readonly IHrPsAllowanceVwService hrPsAllowanceVwService;
        private readonly IHrPsDeductionVwService hrPsDeductionVwService;
        private readonly IHrJobGroupsService hrJobGroupsService;
        private readonly IHrJobCategoryService hrJobCategoryService;
        private readonly IHrIncrementsAllowanceVwService hrIncrementsAllowanceVwService;
        private readonly IHrIncrementsDeductionVwService hrIncrementsDeductionVwService;
        private readonly IHrJobAllowanceDeductionService hrJobAllowanceDeductionService;
        private readonly IHrJobLevelsAllowanceDeductionService hrJobLevelsAllowanceDeductionService;
        private readonly IHrAllReportsService hrAllReportsService;
        private readonly IHrLeaveAllowanceDeductionService hrLeaveAllowanceDeductionService;
        private readonly IHrProvisionsMedicalInsuranceService hrProvisionsMedicalInsuranceService;
        private readonly IHrProvisionsMedicalInsuranceEmployeeService hrProvisionsMedicalInsuranceEmployeeService;
        private readonly IHrClearanceAllowanceDeductionService hrClearanceAllowanceDeductionService;
        private readonly IHrContractsDeductionVwService hrContractsDeductionVwService;
        private readonly IHrContractsAllowanceVwService hrContractsAllowanceVwService;

        public HrServiceManager(
            IHrDeductionVwService hrDeductionVwService,
            IHrAllowanceVwService hrAllowanceVwService,
            IHrEmployeeService hrEmployeeService,
            IHrAttDayService hrAttDayService,
            IHrAbsenceService hrAbsenceService,
            IHrVacationsService hrVacationsService,
            IHrDelayService hrDelayService,
            IInvestMonthService investMonthService,
            IHrOverTimeDService hrOverTimeDService,
            IHrPayrollTypeService hrPayrollTypeService,
            IHrKpiTemplatesCompetenceService hrKpiTemplatesCompetenceService,
            IHrAttendanceService hrAttendanceService,
            IHrAttShiftEmployeeService hrAttShiftEmployeeService,
            IHrMandateService hrMandateService,
            IHrAttTimeTableService hrAttTimeTableService,
            IHrCompetenceService hrCompetenceService,
            IHrCompetencesCatagoryService hrCompetencesCatagoryService,
            IHrKpiTypeService hrKpiTypeService,
            IHrKpiTemplateService hrKpiTemplateService,
            IHrEvaluationAnnualIncreaseConfigService hrEvaluationAnnualIncreaseConfigService,
            IHrDisciplinaryCaseActionService hrDisciplinaryCaseActionService,
            IHrDisciplinaryCaseService hrDisciplinaryCaseService,
            IHrDisciplinaryRuleService hrDisciplinaryRuleService,
            IHrSettingService hrSettingService,
            IHrVacationsCatagoryService hrVacationsCatagoryService,
            IHrRateTypeService hrRateTypeService,
            IHrVacationsTypeService hrVacationsTypeService,
            IHrAllowanceDeductionService hrAllowanceDeductionService,
            IHrLoanService hrLoanService,
            IHrPayrollDService hrPayrollDService,
            IHrArchivesFilesService hrArchivesFilesService,
            IHrLicenseService hrLicenseService,
            IHrTransferService hrTransferService,
            IHrOverTimeMService hrOverTimeMService,
            IHrOhadDetailService hrOhadDetailService,
            IHrEmpWarnService hrEmpWarnService,
            IHrVacationBalanceService hrVacationBalanceService,
            IHrDependentService hrDependentService,
            IHrDirectJobService hrDirectJobService,
            IHrIncrementService hrIncrementService,
            IHrLeaveService hrLeaveService,
            IHrKpiService hrKpiService,
            IHrKpiDetaileService hrKpiDetaileService,
            IHrEmpWorkTimeService hrEmpWorkTimeService,
            IHrSalaryGroupService hrSalaryGroupService,
            IHrSalaryGroupRefranceService hrSalaryGroupRefranceService,
            IHrSalaryGroupAccountService hrSalaryGroupAccountService,
            IHrSalaryGroupDeductionVwService hrSalaryGroupDeductionVwService,
            IHrSalaryGroupAllowanceVwService hrSalaryGroupAllowanceVwService,
            IHrCardTemplateService hrCardTemplateService,
            IHrTrainingBagService hrTrainingBagService,
            IHrNotificationsTypeService hrNotificationsTypeService,
            IHrNotificationsSettingService hrNotificationsSettingService,
            IHrPolicyService hrPolicyService,
            IHrPoliciesTypeService hrPoliciesTypeService,
            IHrAttLocationService hrAttLocationService,
            IHrAttTimeTableDayService hrAttTimeTableDayService,
            IHrDisciplinaryActionTypeService hrDisciplinaryActionTypeService,
            IHrAttShiftCloseService hrAttShiftCloseService,
            IHrAllowanceDeductionMService hrAllowanceDeductionMService,
            IHrAllowanceDeductionTempOrFixService hrAllowanceDeductionTempOrFixService,
            IHrArchiveFilesDetailService hrArchiveFilesDetailService,
            IHrAssignmenService hrAssignmenService,
            IHrAttActionService hrAttActionService,
             IHrAttLocationEmployeeService hrAttLocationEmployeeService,
             IHrAttShiftCloseDService hrAttShiftCloseDService,
              IHrAuthorizationService hrAuthorizationService,
              IHrClearanceService hrClearanceService,
              IHrClearanceTypeService hrClearanceTypeService,
              IHrContracteService hrContracteService,
              IHrClearanceMonthService hrClearanceMonthService,
              IHrRequestDetailsService hrRequestDetailsService,
              IHrRequestService hrRequestService,
              IHrRequestTypeService hrRequestTypeService

             , IHrCostTypeService hrCostTypeService
            , IHrCustodyService hrCustodyService
            , IHrCustodyItemService hrCustodyItemService
            , IHrCustodyItemsPropertyService hrCustodyItemsPropertyService
            , IHrCustodyRefranceTypeService hrCustodyRefranceTypeService
            , IHrCustodyTypeService hrCustodyTypeService
            , IHrDecisionService hrDecisionService
            , IHrDecisionsEmployeeService hrDecisionsEmployeeService,
              IHrNoteService hrNoteService,
              IHrIncrementsAllowanceDeductionService hrIncrementsAllowanceDeductionService,
              IHrHolidayService hrHolidayService,
              IHrPermissionService hrPermissionService,
              IHrAttendanceBioTimeService hrAttendanceBioTimeService,
              IHrCheckInOutService hrCheckInOutService,
              IHrCompensatoryVacationService hrCompensatoryVacationService,
              IHrOhadService hrOhadService,
              IHrAttShiftService hrAttShiftService,
              IHrAttShiftTimeTableService hrAttShiftTimeTableService,
              IHrEmployeeCostService hrEmployeeCostService,
              IHrInsurancePolicyService hrInsurancePolicyService,
              IHrInsuranceService hrInsuranceService,
              IHrInsuranceEmpService hrInsuranceEmpService,

              IHrJobService hrJobService,
              IHrJobDescriptionService hrJobDescriptionService,
              IHrJobEmployeeVwService hrJobEmployeeVwService,
              IHrJobLevelService hrJobLevelService,
              IHrRecruitmentApplicationService hrRecruitmentApplicationService,
              IHrRecruitmentVacancyService hrRecruitmentVacancyService,
              IHrRecruitmentCandidateService hrRecruitmentCandidateService,
              IHrJobGradeService hrJobGradeService,

            IHrRecruitmentCandidateKpiService hrRecruitmentCandidateKpiService,
            IHrRecruitmentCandidateKpiDService hrRecruitmentCandidateKpiDService,
            IHrPayrollService hrPayrollService,
            IHrTicketService hrTicketService,
            IHrVisaService hrVisaService,
            IHrFixingEmployeeSalaryService hrFixingEmployeeSalaryService,
             IHrLeaveTypeService hrLeaveTypeService,
             IHrPayrollAllowanceDeductionService hrPayrollAllowanceDeductionService,
             IHrLoanInstallmentPaymentService hrLoanInstallmentPaymentService,
             IHrLoanInstallmentService hrLoanInstallmentService,
             IHrPayrollNoteService hrPayrollNoteService,
             IHrDecisionsTypeService hrDecisionsTypeService,
             IHrDecisionsTypeEmployeeService hrDecisionsTypeEmployeeService,
             IHrNotificationService hrNotificationService,
             IHrEmployeeLocationVwService hrEmployeeLocationVwService,
             IHrAttShiftEmployeeMVwService hrAttShiftEmployeeMVwService,
             IHrAttendanceReport3Service hrAttendanceReport3Service,
             IHrOpeningBalanceService hrOpeningBalanceService,
             IHrOpeningBalanceTypeService hrOpeningBalanceTypeService,
             IHrPsAllowanceDeductionService hrPsAllowanceDeductionService,
             IHrPreparationSalaryService hrPreparationSalaryService,
             IHrPayrollDeductionAccountsVwService hrPayrollDeductionAccountsVwService,
             IHrPayrollCostcenterService hrPayrollCostcenterService,
             IHrNotificationsReplyService hrNotificationsReplyService,
             IHrLoanPaymentService hrLoanPaymentService,
             IHrPermissionTypeVwService hrPermissionTypeVwService,
             IHrPermissionReasonVwService hrPermissionReasonVwService,
             IHrEmpStatusHistoryService hrEmpStatusHistoryService,

             IHrLanguageService hrLanguageService,
             IHrFileService hrFileService,
             IHrSkillService hrSkillService,
             IHrEducationService hrEducationService,
             IHrWorkExperienceService hrWorkExperienceService,
             IHrGosiEmployeeService hrGosiEmployeeService,
             IHrGosiService hrGosiService,
             IHrVacationsDayTypeService hrVacationsDayTypeService,
             IHrIncrementTypeService hrIncrementTypeService,
             IHrPerformanceService hrPerformanceService,
             IHrKpiTemplatesJobService hrKpiTemplatesJobService,
             IHrEmpGoalIndicatorService hrEmpGoalIndicatorService,
             IHrEmpGoalIndicatorsEmployeeService hrEmpGoalIndicatorsEmployeeService,
             IHrEmpGoalIndicatorsCompetenceService hrEmpGoalIndicatorsCompetenceService,
             IHrDefinitionSalaryEmpService hrDefinitionSalaryEmpService,
             IHrActualAttendanceService hrActualAttendanceService,
             IHrFlexibleWorkingService hrFlexibleWorkingService,
             IHrFlexibleWorkingMasterService hrFlexibleWorkingMasterService,
             IHrMandateLocationMasterService hrMandateLocationMasterService,
             IHrMandateLocationDetaileService hrMandateLocationDetaileService,
             IHrMandateRequestsMasterService hrMandateRequestsMasterService,
             IHrMandateRequestsDetaileService hrMandateRequestsDetaileService,
             IHrExpensesTypeService hrExpensesTypeService,
             IHrJobOfferService hrJobOfferService,
             IHrExpenseService hrExpenseService,
             IHrExpensesEmployeeService hrExpensesEmployeeService,
             IHrJobOfferAdvantageService hrJobOfferAdvantageService,
             IHrProvisionService hrProvisionService,
             IHrProvisionsEmployeeService hrProvisionsEmployeeService,
             IHrRequestGoalsEmployeeDetailService hrRequestGoalsEmployeeDetailService,
             IHrExpensesPaymentService hrExpensesPaymentService,
             IHrExpensesScheduleService hrExpensesScheduleService,
             IHrIncomeTaxService hrIncomeTaxService,
             IHrIncomeTaxPeriodService hrIncomeTaxPeriodService,
             IHrIncomeTaxSlideService hrIncomeTaxSlideService,
             IHrPayrollTransactionTypeService hrPayrollTransactionTypeService,
             IHrPayrollTransactionTypeValueService hrPayrollTransactionTypeValueService,
             IHrStructureService hrStructureService,
             IHrVisitScheduleLocationService hrVisitScheduleLocationService,
             IHrPsAllowanceVwService hrPsAllowanceVwService,
             IHrPsDeductionVwService hrPsDeductionVwService,
             IHrJobGroupsService hrJobGroupsService,
             IHrJobCategoryService hrJobCategoryService,
             IHrIncrementsAllowanceVwService hrIncrementsAllowanceVwService,
             IHrIncrementsDeductionVwService hrIncrementsDeductionVwService,
             IHrJobAllowanceDeductionService hrJobAllowanceDeductionService,
             IHrJobLevelsAllowanceDeductionService hrJobLevelsAllowanceDeductionService,
             IHrAllReportsService hrAllReportsService,
            IHrLeaveAllowanceDeductionService hrLeaveAllowanceDeductionService,
            IHrProvisionsMedicalInsuranceService hrProvisionsMedicalInsuranceService,
            IHrProvisionsMedicalInsuranceEmployeeService hrProvisionsMedicalInsuranceEmployeeService,
            IHrClearanceAllowanceDeductionService hrClearanceAllowanceDeductionService,
            IHrContractsDeductionVwService hrContractsDeductionVwService,
            IHrContractsAllowanceVwService hrContractsAllowanceVwService
            )
        {
            this.hrAllowanceVwService = hrAllowanceVwService;
            this.hrDeductionVwService = hrDeductionVwService;
            this.hrEmployeeService = hrEmployeeService;
            this.hrAttDayService = hrAttDayService;
            this.hrAbsenceService = hrAbsenceService;
            this.hrVacationsService = hrVacationsService;
            this.hrDelayService = hrDelayService;
            this.investMonthService = investMonthService;
            this.hrOverTimeDService = hrOverTimeDService;
            this.hrPayrollTypeService = hrPayrollTypeService;
            this.hrKpiTemplatesCompetenceService = hrKpiTemplatesCompetenceService;
            this.hrAttendanceService = hrAttendanceService;
            this.hrAttTimeTableService = hrAttTimeTableService;
            this.hrAttShiftEmployeeService = hrAttShiftEmployeeService;
            this.hrMandateService = hrMandateService;
            this.hrCompetenceService = hrCompetenceService;
            this.hrCompetencesCatagoryService = hrCompetencesCatagoryService;
            this.hrKpiTypeService = hrKpiTypeService;
            this.hrKpiTemplateService = hrKpiTemplateService;
            this.hrEvaluationAnnualIncreaseConfigService = hrEvaluationAnnualIncreaseConfigService;
            this.hrDisciplinaryCaseActionService = hrDisciplinaryCaseActionService;
            this.hrDisciplinaryCaseService = hrDisciplinaryCaseService;
            this.hrDisciplinaryRuleService = hrDisciplinaryRuleService;
            this.hrSettingService = hrSettingService;
            this.hrRateTypeService = hrRateTypeService;
            this.hrVacationsCatagoryService = hrVacationsCatagoryService;
            this.hrVacationsTypeService = hrVacationsTypeService;
            this.hrAllowanceDeductionService = hrAllowanceDeductionService;
            this.hrLoanService = hrLoanService;
            this.hrPayrollDService = hrPayrollDService;
            this.hrArchivesFilesService = hrArchivesFilesService;
            this.hrLicenseService = hrLicenseService;
            this.hrTransferService = hrTransferService;
            this.hrOverTimeMService = hrOverTimeMService;
            this.hrOhadDetailService = hrOhadDetailService;
            this.hrEmpWarnService = hrEmpWarnService;
            this.hrVacationBalanceService = hrVacationBalanceService;
            this.hrDependentService = hrDependentService;
            this.hrDirectJobService = hrDirectJobService;
            this.hrIncrementService = hrIncrementService;
            this.hrLeaveService = hrLeaveService;
            this.hrKpiService = hrKpiService;
            this.hrKpiDetaileService = hrKpiDetaileService;
            this.hrEmpWorkTimeService = hrEmpWorkTimeService;
            this.hrSalaryGroupService = hrSalaryGroupService;
            this.hrSalaryGroupRefranceService = hrSalaryGroupRefranceService;
            this.hrSalaryGroupAccountService = hrSalaryGroupAccountService;
            this.hrSalaryGroupAllowanceVwService = hrSalaryGroupAllowanceVwService;
            this.hrSalaryGroupDeductionVwService = hrSalaryGroupDeductionVwService;
            this.hrCardTemplateService = hrCardTemplateService;
            this.hrTrainingBagService = hrTrainingBagService;
            this.hrNotificationsTypeService = hrNotificationsTypeService;
            this.hrNotificationsSettingService = hrNotificationsSettingService;
            this.hrPoliciesTypeService = hrPoliciesTypeService;
            this.hrPolicyService = hrPolicyService;
            this.hrAttLocationService = hrAttLocationService;
            this.hrAttTimeTableDayService = hrAttTimeTableDayService;

            this.hrDisciplinaryActionTypeService = hrDisciplinaryActionTypeService;

            this.hrAllowanceDeductionMService = hrAllowanceDeductionMService;
            this.hrAllowanceDeductionTempOrFixService = hrAllowanceDeductionTempOrFixService;
            this.hrArchiveFilesDetailService = hrArchiveFilesDetailService;
            this.hrAttActionService = hrAttActionService;
            this.hrAssignmenService = hrAssignmenService;
            //this.hrAttActionService = hrAttActionService;
            this.hrAttLocationEmployeeService = hrAttLocationEmployeeService;
            this.hrAttShiftCloseDService = hrAttShiftCloseDService;
            this.hrAttShiftCloseService = hrAttShiftCloseService;

            this.hrAuthorizationService = hrAuthorizationService;
            this.hrAttendanceBioTimeService = hrAttendanceBioTimeService;
            this.hrCheckInOutService = hrCheckInOutService;
            this.hrClearanceService = hrClearanceService;
            this.hrClearanceTypeService = hrClearanceTypeService;
            this.hrCompensatoryVacationService = hrCompensatoryVacationService;
            this.hrContracteService = hrContracteService;
            this.hrClearanceMonthService = hrClearanceMonthService;

            this.hrRequestDetailsService = hrRequestDetailsService;
            this.hrRequestService = hrRequestService;
            this.hrRequestTypeService = hrRequestTypeService;
            this.hrCostTypeService = hrCostTypeService;
            this.hrCustodyService = hrCustodyService;
            this.hrCustodyItemService = hrCustodyItemService;
            this.hrCustodyItemsPropertyService = hrCustodyItemsPropertyService;
            this.hrCustodyRefranceTypeService = hrCustodyRefranceTypeService;
            this.hrCustodyTypeService = hrCustodyTypeService;
            this.hrDecisionService = hrDecisionService;
            this.hrDecisionsEmployeeService = hrDecisionsEmployeeService;
            this.hrOhadService = hrOhadService;
            this.hrNoteService = hrNoteService;
            this.hrIncrementsAllowanceDeductionService = hrIncrementsAllowanceDeductionService;
            this.hrHolidayService = hrHolidayService;
            this.hrPermissionService = hrPermissionService;
            this.hrAttShiftService = hrAttShiftService;
            this.hrAttShiftTimeTableService = hrAttShiftTimeTableService;
            this.hrEmployeeCostService = hrEmployeeCostService;

            this.hrInsurancePolicyService = hrInsurancePolicyService;
            this.hrInsuranceService = hrInsuranceService;
            this.hrInsuranceEmpService = hrInsuranceEmpService;
            this.hrJobService = hrJobService;
            this.hrJobDescriptionService = hrJobDescriptionService;
            this.hrJobEmployeeVwService = hrJobEmployeeVwService;
            this.hrJobLevelService = hrJobLevelService;
            this.hrRecruitmentVacancyService = hrRecruitmentVacancyService;
            this.hrRecruitmentApplicationService = hrRecruitmentApplicationService;
            this.hrRecruitmentCandidateService = hrRecruitmentCandidateService;
            this.hrJobGradeService = hrJobGradeService;

            this.hrRecruitmentCandidateKpiDService = hrRecruitmentCandidateKpiDService;
            this.hrRecruitmentCandidateKpiService = hrRecruitmentCandidateKpiService;
            this.hrPayrollService = hrPayrollService;
            this.hrTicketService = hrTicketService;
            this.hrVisaService = hrVisaService;
            this.hrFixingEmployeeSalaryService = hrFixingEmployeeSalaryService;
            this.hrLeaveTypeService = hrLeaveTypeService;
            this.hrPayrollAllowanceDeductionService = hrPayrollAllowanceDeductionService;
            this.hrLoanInstallmentPaymentService = hrLoanInstallmentPaymentService;
            this.hrLoanInstallmentService = hrLoanInstallmentService;
            this.hrPayrollNoteService = hrPayrollNoteService;
            this.hrDecisionsTypeService = hrDecisionsTypeService;
            this.hrDecisionsTypeEmployeeService = hrDecisionsTypeEmployeeService;
            this.hrNotificationService = hrNotificationService;
            this.hrEmployeeLocationVwService = hrEmployeeLocationVwService;
            this.hrAttShiftEmployeeMVwService = hrAttShiftEmployeeMVwService;
            this.hrAttendanceReport3Service = hrAttendanceReport3Service;
            this.hrOpeningBalanceService = hrOpeningBalanceService;
            this.hrOpeningBalanceTypeService = hrOpeningBalanceTypeService;
            this.hrPsAllowanceDeductionService = hrPsAllowanceDeductionService;
            this.hrPreparationSalaryService = hrPreparationSalaryService;
            this.hrPayrollDeductionAccountsVwService = hrPayrollDeductionAccountsVwService;
            this.hrPayrollCostcenterService = hrPayrollCostcenterService;
            this.hrNotificationsReplyService = hrNotificationsReplyService;
            this.hrLoanPaymentService = hrLoanPaymentService;
            this.hrPermissionTypeVwService = hrPermissionTypeVwService;
            this.hrPermissionReasonVwService = hrPermissionReasonVwService;
            this.hrEmpStatusHistoryService = hrEmpStatusHistoryService;

            this.hrLanguageService = hrLanguageService;
            this.hrFileService = hrFileService;
            this.hrSkillService = hrSkillService;
            this.hrEducationService = hrEducationService;
            this.hrWorkExperienceService = hrWorkExperienceService;
            this.hrGosiService = hrGosiService;
            this.hrGosiEmployeeService = hrGosiEmployeeService;
            this.hrVacationsDayTypeService = hrVacationsDayTypeService;
            this.hrIncrementTypeService = hrIncrementTypeService;
            this.hrPerformanceService = hrPerformanceService;
            this.hrKpiTemplatesJobService = hrKpiTemplatesJobService;
            this.hrEmpGoalIndicatorService = hrEmpGoalIndicatorService;
            this.hrEmpGoalIndicatorsEmployeeService = hrEmpGoalIndicatorsEmployeeService;
            this.hrEmpGoalIndicatorsCompetenceService = hrEmpGoalIndicatorsCompetenceService;
            this.hrDefinitionSalaryEmpService = hrDefinitionSalaryEmpService;
            this.hrActualAttendanceService = hrActualAttendanceService;
            this.hrFlexibleWorkingService = hrFlexibleWorkingService;
            this.hrFlexibleWorkingMasterService = hrFlexibleWorkingMasterService;
            this.hrMandateLocationMasterService = hrMandateLocationMasterService;
            this.hrMandateLocationDetaileService = hrMandateLocationDetaileService;
            this.hrMandateRequestsMasterService = hrMandateRequestsMasterService;
            this.hrMandateRequestsDetaileService = hrMandateRequestsDetaileService;
            this.hrExpensesTypeService = hrExpensesTypeService;
            this.hrJobOfferService = hrJobOfferService;
            this.hrExpensesEmployeeService = hrExpensesEmployeeService;
            this.hrExpenseService = hrExpenseService;
            this.hrJobOfferAdvantageService = hrJobOfferAdvantageService;
            this.hrProvisionService = hrProvisionService;
            this.hrProvisionsEmployeeService = hrProvisionsEmployeeService;
            this.hrRequestGoalsEmployeeDetailService = hrRequestGoalsEmployeeDetailService;
            this.hrExpensesPaymentService = hrExpensesPaymentService;
            this.hrExpensesScheduleService = hrExpensesScheduleService;
            this.hrIncomeTaxService = hrIncomeTaxService;
            this.hrIncomeTaxPeriodService = hrIncomeTaxPeriodService;
            this.hrIncomeTaxSlideService = hrIncomeTaxSlideService;
            this.hrPayrollTransactionTypeService = hrPayrollTransactionTypeService;
            this.hrPayrollTransactionTypeValueService = hrPayrollTransactionTypeValueService;
            this.hrStructureService = hrStructureService;

            this.hrVisitScheduleLocationService = hrVisitScheduleLocationService;
            this.hrPsAllowanceVwService = hrPsAllowanceVwService;
            this.hrPsDeductionVwService = hrPsDeductionVwService;
            this.hrJobGroupsService = hrJobGroupsService;
            this.hrJobCategoryService = hrJobCategoryService;
            this.hrIncrementsAllowanceVwService = hrIncrementsAllowanceVwService;
            this.hrIncrementsDeductionVwService = hrIncrementsDeductionVwService;
            this.hrJobAllowanceDeductionService = hrJobAllowanceDeductionService;
            this.hrJobLevelsAllowanceDeductionService = hrJobLevelsAllowanceDeductionService;
            this.hrAllReportsService = hrAllReportsService;
            this.hrLeaveAllowanceDeductionService = hrLeaveAllowanceDeductionService;
            this.hrProvisionsMedicalInsuranceService = hrProvisionsMedicalInsuranceService;
            this.hrProvisionsMedicalInsuranceEmployeeService = hrProvisionsMedicalInsuranceEmployeeService;
            this.hrClearanceAllowanceDeductionService = hrClearanceAllowanceDeductionService;
            this.hrContractsDeductionVwService = hrContractsDeductionVwService;
            this.hrContractsAllowanceVwService = hrContractsAllowanceVwService;
        }

        public IHrEmployeeService HrEmployeeService => hrEmployeeService;
        public IHrAttDayService HrAttDayService => hrAttDayService;
        public IHrAbsenceService HrAbsenceService => hrAbsenceService;
        public IHrVacationsService HrVacationsService => hrVacationsService;
        public IHrDelayService HrDelayService => hrDelayService;
        public IHrOverTimeDService HrOverTimeDService => hrOverTimeDService;
        public IInvestMonthService InvestMonthService => investMonthService;
        public IHrPayrollTypeService HrPayrollTypeService => hrPayrollTypeService;
        public IHrKpiTemplatesCompetenceService HrKpiTemplatesCompetenceService => hrKpiTemplatesCompetenceService;

        public IHrAttShiftEmployeeService HrAttShiftEmployeeService => hrAttShiftEmployeeService;

        public IHrMandateService HrMandateService => hrMandateService;
        public IHrAttendanceService HrAttendanceService => hrAttendanceService;

        public IHrAttTimeTableService HrAttTimeTableService => hrAttTimeTableService;
        public IHrCompetenceService HrCompetenceService => hrCompetenceService;
        public IHrCompetencesCatagoryService HrCompetencesCatagoryService => hrCompetencesCatagoryService;
        public IHrKpiTypeService HrKpiTypeService => hrKpiTypeService;
        public IHrKpiTemplateService HrKpiTemplateService => hrKpiTemplateService;
        public IHrEvaluationAnnualIncreaseConfigService HrEvaluationAnnualIncreaseConfigService => hrEvaluationAnnualIncreaseConfigService;

        public IHrDisciplinaryCaseActionService HrDisciplinaryCaseActionService => hrDisciplinaryCaseActionService;

        public IHrDisciplinaryCaseService HrDisciplinaryCaseService => hrDisciplinaryCaseService;

        public IHrDisciplinaryRuleService HrDisciplinaryRuleService => hrDisciplinaryRuleService;

        public IHrSettingService HrSettingService => hrSettingService;

        public IHrVacationsCatagoryService HrVacationsCatagoryService => hrVacationsCatagoryService;

        public IHrRateTypeService HrRateTypeService => hrRateTypeService;

        public IHrVacationsTypeService HrVacationsTypeService => hrVacationsTypeService;

        public IHrAllowanceDeductionService HrAllowanceDeductionService => hrAllowanceDeductionService;

        public IHrLoanService HrLoanService => hrLoanService;

        public IHrPayrollDService HrPayrollDService => hrPayrollDService;

        public IHrArchivesFilesService HrArchivesFilesService => hrArchivesFilesService;

        public IHrLicenseService HrLicenseService => hrLicenseService;

        public IHrTransferService HrTransferService => hrTransferService;

        public IHrOverTimeMService HrOverTimeMService => hrOverTimeMService;

        public IHrOhadDetailService HrOhadDetailService => hrOhadDetailService;

        public IHrEmpWarnService HrEmpWarnService => hrEmpWarnService;

        public IHrVacationBalanceService HrVacationBalanceService => hrVacationBalanceService;

        public IHrDependentService HrDependentService => hrDependentService;

        public IHrDirectJobService HrDirectJobService => hrDirectJobService;

        public IHrIncrementService HrIncrementService => hrIncrementService;

        public IHrLeaveService HrLeaveService => hrLeaveService;

        public IHrKpiService HrKpiService => hrKpiService;

        public IHrKpiDetaileService HrKpiDetaileService => hrKpiDetaileService;

        public IHrEmpWorkTimeService HrEmpWorkTimeService => hrEmpWorkTimeService;

        public IHrSalaryGroupService HrSalaryGroupService => hrSalaryGroupService;

        public IHrSalaryGroupAccountService HrSalaryGroupAccountService => hrSalaryGroupAccountService;

        public IHrSalaryGroupRefranceService HrSalaryGroupRefranceService => hrSalaryGroupRefranceService;

        public IHrSalaryGroupAllowanceVwService HrSalaryGroupAllowanceVwService => hrSalaryGroupAllowanceVwService;

        public IHrSalaryGroupDeductionVwService HrSalaryGroupDeductionVwService => hrSalaryGroupDeductionVwService;

        public IHrCardTemplateService HrCardTemplateService => hrCardTemplateService;

        public IHrTrainingBagService HrTrainingBagService => hrTrainingBagService;

        public IHrNotificationsTypeService HrNotificationsTypeService => hrNotificationsTypeService;

        public IHrNotificationsSettingService HrNotificationsSettingService => hrNotificationsSettingService;

        public IHrPoliciesTypeService HrPoliciesTypeService => hrPoliciesTypeService;

        public IHrPolicyService HrPolicyService => hrPolicyService;

        public IHrAttLocationService HrAttLocationService => hrAttLocationService;

        public IHrAttTimeTableDayService HrAttTimeTableDayService => hrAttTimeTableDayService;


        public IHrDisciplinaryActionTypeService HrDisciplinaryActionTypeService => hrDisciplinaryActionTypeService;

        public IHrAllowanceVwService HrAllowanceVwService => hrAllowanceVwService;

        public IHrDeductionVwService HrDeductionVwService => hrDeductionVwService;

        public IHrAllowanceDeductionMService HrAllowanceDeductionMService => hrAllowanceDeductionMService;
        public IHrAllowanceDeductionTempOrFixService HrAllowanceDeductionTempOrFixService => hrAllowanceDeductionTempOrFixService;
        public IHrAssignmenService HrAssignmenService => hrAssignmenService;

        public IHrAttLocationEmployeeService HrAttLocationEmployeeService => hrAttLocationEmployeeService;
        public IHrAttShiftCloseDService HrAttShiftCloseDService => hrAttShiftCloseDService;
        public IHrArchiveFilesDetailService HrArchiveFilesDetailService => hrArchiveFilesDetailService;
        public IHrAttActionService HrAttActionService => hrAttActionService;
        public IHrAttShiftCloseService HrAttShiftCloseService => hrAttShiftCloseService;

        public IHrAuthorizationService HrAuthorizationService => hrAuthorizationService;
        public IHrAttendanceBioTimeService HrAttendanceBioTimeService => hrAttendanceBioTimeService;
        public IHrCheckInOutService HrCheckInOutService => hrCheckInOutService;
        public IHrClearanceService HrClearanceService => hrClearanceService;
        public IHrClearanceTypeService HrClearanceTypeService => hrClearanceTypeService;
        public IHrCompensatoryVacationService HrCompensatoryVacationService => hrCompensatoryVacationService;
        public IHrContracteService HrContracteService => hrContracteService;
        public IHrClearanceMonthService HrClearanceMonthService => hrClearanceMonthService;
        public IHrCostTypeService HrCostTypeService => hrCostTypeService;

        public IHrCustodyService HrCustodyService => hrCustodyService;


        public IHrCustodyItemService HrCustodyItemService => hrCustodyItemService;

        public IHrCustodyItemsPropertyService HrCustodyItemsPropertyService => hrCustodyItemsPropertyService;


        public IHrCustodyRefranceTypeService HrCustodyRefranceTypeService => hrCustodyRefranceTypeService;

        public IHrCustodyTypeService HrCustodyTypeService => hrCustodyTypeService;

        public IHrDecisionService HrDecisionService => hrDecisionService;
        public IHrDecisionsEmployeeService HrDecisionsEmployeeService => hrDecisionsEmployeeService;
        public IHrOhadService HrOhadService => hrOhadService;
        public IHrRequestTypeService HrRequestTypeService => hrRequestTypeService;
        public IHrRequestService HrRequestService => hrRequestService;

        public IHrRequestDetailsService HrRequestDetailsService => hrRequestDetailsService;
        public IHrNoteService HrNoteService => hrNoteService;

        public IHrIncrementsAllowanceDeductionService HrIncrementsAllowanceDeductionService => hrIncrementsAllowanceDeductionService;

        public IHrHolidayService HrHolidayService => hrHolidayService;

        public IHrPermissionService HrPermissionService => hrPermissionService;

        public IHrAttShiftService HrAttShiftService => hrAttShiftService;

        public IHrAttShiftTimeTableService HrAttShiftTimeTableService => hrAttShiftTimeTableService;
        public IHrEmployeeCostService HrEmployeeCostService => hrEmployeeCostService;

        public IHrInsurancePolicyService HrInsurancePolicyService => hrInsurancePolicyService;
        public IHrInsuranceService HrInsuranceService => hrInsuranceService;
        public IHrInsuranceEmpService HrInsuranceEmpService => hrInsuranceEmpService;
        public IHrJobService HrJobService => hrJobService;

        public IHrJobDescriptionService HrJobDescriptionService => hrJobDescriptionService;

        public IHrJobEmployeeVwService HrJobEmployeeVwService => hrJobEmployeeVwService;
        public IHrJobLevelService HrJobLevelService => hrJobLevelService;

        public IHrRecruitmentVacancyService HrRecruitmentVacancyService => hrRecruitmentVacancyService;

        public IHrRecruitmentApplicationService HrRecruitmentApplicationService => hrRecruitmentApplicationService;

        public IHrRecruitmentCandidateService HrRecruitmentCandidateService => hrRecruitmentCandidateService;

        public IHrJobGradeService HrJobGradeService => hrJobGradeService;

        public IHrRecruitmentCandidateKpiDService HrRecruitmentCandidateKpiDService => hrRecruitmentCandidateKpiDService;

        public IHrRecruitmentCandidateKpiService HrRecruitmentCandidateKpiService => hrRecruitmentCandidateKpiService;

        public IHrPayrollService HrPayrollService => hrPayrollService;

        public IHrTicketService HrTicketService => hrTicketService;

        public IHrVisaService HrVisaService => hrVisaService;

        public IHrFixingEmployeeSalaryService HrFixingEmployeeSalaryService => hrFixingEmployeeSalaryService;

        public IHrLeaveTypeService HrLeaveTypeService => hrLeaveTypeService;

        public IHrPayrollAllowanceDeductionService HrPayrollAllowanceDeductionService => hrPayrollAllowanceDeductionService;

        public IHrLoanInstallmentPaymentService HrLoanInstallmentPaymentService => hrLoanInstallmentPaymentService;

        public IHrLoanInstallmentService HrLoanInstallmentService => hrLoanInstallmentService;

        public IHrPayrollNoteService HrPayrollNoteService => hrPayrollNoteService;

        public IHrDecisionsTypeService HrDecisionsTypeService => hrDecisionsTypeService;

        public IHrDecisionsTypeEmployeeService HrDecisionsTypeEmployeeService => hrDecisionsTypeEmployeeService;

        public IHrNotificationService HrNotificationService => hrNotificationService;

        public IHrEmployeeLocationVwService HrEmployeeLocationVwService => hrEmployeeLocationVwService;

        public IHrAttShiftEmployeeMVwService HrAttShiftEmployeeMVwService => hrAttShiftEmployeeMVwService;

        public IHrAttendanceReport3Service HrAttendanceReport3Service => hrAttendanceReport3Service;

        public IHrOpeningBalanceService HrOpeningBalanceService => hrOpeningBalanceService;
        public IHrOpeningBalanceTypeService HrOpeningBalanceTypeService => hrOpeningBalanceTypeService;
        public IHrPsAllowanceDeductionService HrPsAllowanceDeductionService => hrPsAllowanceDeductionService;
        public IHrPreparationSalaryService HrPreparationSalaryService => hrPreparationSalaryService;
        public IHrPayrollDeductionAccountsVwService HrPayrollDeductionAccountsVwService => hrPayrollDeductionAccountsVwService;

        public IHrPayrollCostcenterService HrPayrollCostcenterService => hrPayrollCostcenterService;

        public IHrNotificationsReplyService HrNotificationsReplyService => hrNotificationsReplyService;

        public IHrLoanPaymentService HrLoanPaymentService => hrLoanPaymentService;

        public IHrPermissionReasonVwService HrPermissionReasonVwService => hrPermissionReasonVwService;

        public IHrPermissionTypeVwService HrPermissionTypeVwService => hrPermissionTypeVwService;
        public IHrEmpStatusHistoryService HrEmpStatusHistoryService => hrEmpStatusHistoryService;

        public IHrLanguageService HrLanguageService => hrLanguageService;

        public IHrFileService HrFileService => hrFileService;

        public IHrSkillService HrSkillService => hrSkillService;

        public IHrEducationService HrEducationService => hrEducationService;

        public IHrWorkExperienceService HrWorkExperienceService => hrWorkExperienceService;

        public IHrGosiEmployeeService HrGosiEmployeeService => hrGosiEmployeeService;

        public IHrGosiService HrGosiService => hrGosiService;

        public IHrVacationsDayTypeService HrVacationsDayTypeService => hrVacationsDayTypeService;

        public IHrIncrementTypeService HrIncrementTypeService => hrIncrementTypeService;

        public IHrPerformanceService HrPerformanceService => hrPerformanceService;
        public IHrKpiTemplatesJobService HrKpiTemplatesJobService => hrKpiTemplatesJobService;

        public IHrEmpGoalIndicatorService HrEmpGoalIndicatorService => hrEmpGoalIndicatorService;

        public IHrEmpGoalIndicatorsEmployeeService HrEmpGoalIndicatorsEmployeeService => hrEmpGoalIndicatorsEmployeeService;

        public IHrEmpGoalIndicatorsCompetenceService HrEmpGoalIndicatorsCompetenceService => hrEmpGoalIndicatorsCompetenceService;

        public IHrDefinitionSalaryEmpService HrDefinitionSalaryEmpService => hrDefinitionSalaryEmpService;
        public IHrActualAttendanceService HrActualAttendanceService => hrActualAttendanceService;

        public IHrFlexibleWorkingMasterService HrFlexibleWorkingMasterService => hrFlexibleWorkingMasterService;

        public IHrFlexibleWorkingService HrFlexibleWorkingService => hrFlexibleWorkingService;

        public IHrMandateLocationDetaileService HrMandateLocationDetaileService => hrMandateLocationDetaileService;

        public IHrMandateLocationMasterService HrMandateLocationMasterService => hrMandateLocationMasterService;

        public IHrMandateRequestsMasterService HrMandateRequestsMasterService => hrMandateRequestsMasterService;
        public IHrMandateRequestsDetaileService HrMandateRequestsDetaileService => hrMandateRequestsDetaileService;
        public IHrExpensesTypeService HrExpensesTypeService => hrExpensesTypeService;
        public IHrJobOfferService HrJobOfferService => hrJobOfferService;

        public IHrExpenseService HrExpenseService => hrExpenseService;

        public IHrExpensesEmployeeService HrExpensesEmployeeService => hrExpensesEmployeeService;
        public IHrJobOfferAdvantageService HrJobOfferAdvantageService => hrJobOfferAdvantageService;

        public IHrProvisionService HrProvisionService => hrProvisionService;
        public IHrProvisionsEmployeeService HrProvisionsEmployeeService => hrProvisionsEmployeeService;
        public IHrRequestGoalsEmployeeDetailService HrRequestGoalsEmployeeDetailService => hrRequestGoalsEmployeeDetailService;

        public IHrExpensesScheduleService HrExpensesScheduleService => hrExpensesScheduleService;

        public IHrExpensesPaymentService HrExpensesPaymentService => hrExpensesPaymentService;
        public IHrIncomeTaxService HrIncomeTaxService => hrIncomeTaxService;
        public IHrIncomeTaxPeriodService HrIncomeTaxPeriodService => hrIncomeTaxPeriodService;
        public IHrIncomeTaxSlideService HrIncomeTaxSlideService => hrIncomeTaxSlideService;
        public IHrPayrollTransactionTypeService HrPayrollTransactionTypeService => hrPayrollTransactionTypeService;
        public IHrPayrollTransactionTypeValueService HrPayrollTransactionTypeValueService => hrPayrollTransactionTypeValueService;
        public IHrStructureService HrStructureService => hrStructureService;

        public IHrVisitScheduleLocationService HrVisitScheduleLocationService => hrVisitScheduleLocationService;
        public IHrPsAllowanceVwService HrPsAllowanceVwService => hrPsAllowanceVwService;
        public IHrPsDeductionVwService HrPsDeductionVwService => hrPsDeductionVwService;
        public IHrJobGroupsService HrJobGroupsService => hrJobGroupsService;
        public IHrJobCategoryService HrJobCategoryService => hrJobCategoryService;
        public IHrIncrementsAllowanceVwService HrIncrementsAllowanceVwService => hrIncrementsAllowanceVwService;
        public IHrIncrementsDeductionVwService HrIncrementsDeductionVwService => hrIncrementsDeductionVwService;
        public IHrJobAllowanceDeductionService HrJobAllowanceDeductionService => hrJobAllowanceDeductionService;
        public IHrJobLevelsAllowanceDeductionService HrJobLevelsAllowanceDeductionService => hrJobLevelsAllowanceDeductionService;
        public IHrAllReportsService HrAllReportsService => hrAllReportsService;
        public IHrLeaveAllowanceDeductionService HrLeaveAllowanceDeductionService => hrLeaveAllowanceDeductionService;
        public IHrProvisionsMedicalInsuranceService HrProvisionsMedicalInsuranceService => hrProvisionsMedicalInsuranceService;
        public IHrProvisionsMedicalInsuranceEmployeeService HrProvisionsMedicalInsuranceEmployeeService => hrProvisionsMedicalInsuranceEmployeeService;
        public IHrClearanceAllowanceDeductionService HrClearanceAllowanceDeductionService => hrClearanceAllowanceDeductionService;
        public IHrContractsDeductionVwService HrContractsDeductionVwService => hrContractsDeductionVwService;
        public IHrContractsAllowanceVwService HrContractsAllowanceVwService => hrContractsAllowanceVwService;
    }
}
