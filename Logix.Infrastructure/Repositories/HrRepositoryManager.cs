using Logix.Application.Common;
using Logix.Application.Interfaces.IRepositories;
using Logix.Application.Interfaces.IRepositories.HR;

namespace Logix.Infrastructure.Repositories
{
    public class HrRepositoryManager : IHrRepositoryManager
    {
        private readonly IHrEmployeeRepository hrEmployeeRepository;
        private readonly IHrAbsenceRepository hrAbsenceRepository;
        private readonly IHrVacationsRepository hrVacationsRepository;
        private readonly IHrDelayRepository hrDelayRepository;
        private readonly IHrOverTimeDRepository hrOverTimeDRepository;
        private readonly IHrKpiTemplatesCompetenceRepository hrKpiTemplatesCompetenceRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHrAttendanceRepository hrAttendanceRepository;
        private readonly IHrAttShiftEmployeeRepository hrAttShiftEmployeeRepository;
        private readonly IHrAttTimeTableRepository hrAttTimeTableRepository;
        private readonly IHrMandateRepository hrMandateRepository;
        private readonly IHrCompetenceRepository hrCompetenceRepository;
        private readonly IHrCompetencesCatagoryRepository hrCompetencesCatagoryRepository;
        private readonly IHrKpiTypeRepository hrKpiTypeRepository;
        private readonly IHrKpiTemplateRepository hrKpiTemplateRepository;
        private readonly IHrEvaluationAnnualIncreaseConfigRepository hrEvaluationAnnualIncreaseConfigRepository;
        private readonly IHrDisciplinaryRuleRepository hrDisciplinaryRuleRepository;
        private readonly IHrDisciplinaryCaseRepository hrDisciplinaryCaseRepository;
        private readonly IHrDisciplinaryCaseActionRepository hrDisciplinaryCaseActionRepository;
        private readonly IHrSettingRepository hrSettingRepository;
        private readonly IHrVacationsCatagoryRepository hrVacationsCatagoryRepository;
        private readonly IHrRateTypeRepository hrRateTypeRepository;
        private readonly IHrVacationsTypeRepository hrVacationsTypeRepository;
        private readonly IHrAllowanceDeductionRepository hrAllowanceDeductionRepository;
        private readonly IHrLoanRepository hrLoanRepository;
        private readonly IHrPayrollDRepository hrPayrollDRepository;
        private readonly IHrArchivesFileRepository hrArchivesFileRepository;
        private readonly IHrLicenseRepository hrLicenseRepository;
        private readonly IHrTransferRepository hrTransferRepository;
        private readonly IHrOverTimeMRepository hrOverTimeMRepository;
        private readonly IHrOhadDetailRepository hrOhadDetailRepository;
        private readonly IHrEmpWarnRepository hrEmpWarnRepository;
        private readonly IHrVacationBalanceRepository hrVacationBalanceRepository;
        private readonly IHrDependentRepository hrDependentRepository;
        private readonly IHrDirectJobRepository hrDirectJobRepository;
        private readonly IHrIncrementRepository hrIncrementRepository;
        private readonly IHrLeaveRepository hrLeaveRepository;
        private readonly IHrKpiRepository hrKpiRepository;
        private readonly IHrKpiDetaileRepository hrKpiDetaileRepository;
        private readonly IHrEmpWorkTimeRepository hrEmpWorkTimeRepository;
        private readonly IHrSalaryGroupRepository hrSalaryGroupRepository;
        private readonly IHrSalaryGroupAccountRepository hrSalaryGroupAccountRepository;
        private readonly IHrSalaryGroupRefranceRepository hrSalaryGroupRefranceRepository;
        private readonly IHrCardTemplateRepository hrCardTemplateRepository;
        private readonly IHrTrainingBagRepository hrTrainingBagRepository;
        private readonly IHrNotificationsTypeRepository hrNotificationsTypeRepository;
        private readonly IHrNotificationsSettingRepository hrNotificationsSettingRepository;
        private readonly IHrPoliciesTypeRepository hrPoliciesTypeRepository;
        private readonly IHrPolicyRepository hrPolicyRepository;
        private readonly IHrAttLocationRepository hrAttLocationRepository;
        private readonly IHrAttTimeTableDayRepository hrAttTimeTableDayRepository;

        private readonly IHrDisciplinaryActionTypeRepository hrDisciplinaryActionTypeRepository;
        private readonly IHrAllowanceVwRepository hrAllowanceVwRepository;
        private readonly IHrDeductionVwRepository hrDeductionVwRepository;
        private readonly IHrAllowanceDeductionMRepository hrAllowanceDeductionMRepository;
        private readonly IHrAllowanceDeductionTempOrFixRepository hrAllowanceDeductionTempOrFixRepository;
        private readonly IHrArchiveFilesDetailRepository hrArchiveFilesDetailRepository;
        private readonly IHrAssignmenRepository hrAssignmenRepository;
        private readonly IHrAttActionRepository hrAttActionRepository;
        private readonly IHrAttLocationEmployeeRepository hrAttLocationEmployeeRepository;
        private readonly IHrAttShiftCloseDRepository hrAttShiftCloseDRepository;
        private readonly IHrAttShiftCloseRepository hrAttShiftCloseRepository;

        private readonly IHrAuthorizationRepository hrAuthorizationRepository;
        private readonly IHrAttendanceBioTimeRepository hrAttendanceBioTimeRepository;
        private readonly IHrCheckInOutRepository hrCheckInOutRepository;
        private readonly IHrClearanceRepository hrClearanceRepository;
        private readonly IHrClearanceTypeRepository hrClearanceTypeRepository;
        private readonly IHrCompensatoryVacationRepository hrCompensatoryVacationRepository;
        private readonly IHrContracteRepository hrContracteRepository;
        private readonly IHrClearanceMonthRepository hrClearanceMonthRepository;
        private readonly IHrCostTypeRepository hrCostTypeRepository;
        private readonly IHrCustodyRepository hrCustodyRepository;
        private readonly IHrCustodyItemRepository hrCustodyItemRepository;
        private readonly IHrCustodyItemsPropertyRepository hrCustodyItemsPropertyRepository;
        private readonly IHrCustodyRefranceTypeRepository hrCustodyRefranceTypeRepository;
        private readonly IHrCustodyTypeRepository hrCustodyTypeRepository;
        private readonly IHrDecisionRepository hrDecisionRepository;
        private readonly IHrDecisionsEmployeeRepository hrDecisionsEmployeeRepository;
        private readonly IHrOhadRepository hrOhadRepository;
        private readonly IHrNoteRepository hrNoteRepository;
        private readonly IHrRequestTypeRepository hrRequestTypeRepository;
        private readonly IHrRequestDetaileRepository hrRequestDetaileRepository;
        private readonly IHrRequestRepository hrRequestRepository;
        private readonly IHrIncrementsAllowanceDeductionRepository hrIncrementsAllowanceDeductionRepository;
        private readonly IHrHolidayRepository hrHolidayRepository;
        private readonly IHrPermissionRepository hrPermissionRepository;
        private readonly IHrAttShiftRepository hrAttShiftRepository;
        private readonly IHrAttShiftTimeTableRepository hrAttShiftTimeTableRepository;
        private readonly IHrEmployeeCostRepository hrEmployeeCostRepository;
        private readonly IHrOhadDetailsVwRepository hrOhadDetailsVwRepository;
        private readonly IHrInsurancePolicyRepository hrInsurancePolicyRepository;
        private readonly IHrInsuranceRepository hrInsuranceRepository;
        private readonly IHrInsuranceEmpRepository hrInsuranceEmpRepository;
        private readonly IHrJobRepository hrJobRepository;
        private readonly IHrJobDescriptionRepository hrJobDescriptionRepository;
        private readonly IHrJobEmployeeVwRepository hrJobEmployeeVwRepository;
        private readonly IHrJobLevelRepository hrJobLevelRepository;
        private readonly IHrRecruitmentVacancyRepository hrRecruitmentVacancyRepository;
        private readonly IHrRecruitmentApplicationRepository hrRecruitmentApplicationRepository;
        private readonly IHrRecruitmentCandidateRepository hrRecruitmentCandidateRepository;
        private readonly IHrJobGradeRepository hrJobGradeRepository;

        private readonly IHrRecruitmentCandidateKpiRepository hrRecruitmentCandidateKpiRepository;
        private readonly IHrRecruitmentCandidateKpiDRepository hrRecruitmentCandidateKpiDRepository;

        private readonly IHrPayrollRepository hrPayrollRepository;
        private readonly IHrTicketRepository hrTicketRepository;
        private readonly IHrVisaRepository hrVisaRepository;
        private readonly IHrFixingEmployeeSalaryRepository hrFixingEmployeeSalaryRepository;
        private readonly IHrLeaveTypeRepository hrLeaveTypeRepository;
        private readonly IHrPayrollAllowanceDeductionRepository hrPayrollAllowanceDeductionRepository;
        private readonly IHrLoanInstallmentPaymentRepository hrLoanInstallmentPaymentRepository;
        private readonly IHrLoanInstallmentRepository hrLoanInstallmentRepository;
        private readonly IHrPayrollNoteRepository hrPayrollNoteRepository;
        private readonly IHrDecisionsTypeRepository hrDecisionsTypeRepository;
        private readonly IHrDecisionsTypeEmployeeRepository hrDecisionsTypeEmployeeRepository;
        private readonly IHrNotificationRepository hrNotificationRepository;
        private readonly IHrEmployeeLocationVwRepository hrEmployeeLocationVwRepository;
        private readonly IHrAttShiftEmployeeMVwRepository hrAttShiftEmployeeMVwRepository;
        private readonly IHrAttCheckShiftEmployeeVwRepository hrAttCheckShiftEmployeeVwRepository;
        private readonly IHrOpeningBalanceRepository hrOpeningBalanceRepository;
        private readonly IHrOpeningBalanceTypeRepository hrOpeningBalanceTypeRepository;
        private readonly IHrPayrollAllowanceVwRepository hrPayrollAllowanceVwRepository;
        private readonly IHrPsAllowanceDeductionRepository hrPsAllowanceDeductionRepository;
        private readonly IHrPreparationSalaryRepository hrPreparationSalaryRepository;
        private readonly IHrPayrollDeductionAccountsVwRepository hrPayrollDeductionAccountsVwRepository;
        private readonly IHrPayrollCostcenterRepository hrPayrollCostcenterRepository;
        private readonly IHrPayrollAllowanceAccountsVwRepository hrPayrollAllowanceAccountsVwRepository;
        private readonly IHrNotificationsReplyRepository hrNotificationsReplyRepository;
        private readonly IHrLoanPaymentRepository hrLoanPaymentRepository;
        private readonly IHrPermissionReasonVwRepository hrPermissionReasonVwRepository;
        private readonly IHrPermissionTypeVwRepository hrPermissionTypeVwRepository;
        private readonly IHrEmpStatusHistoryRepository hrEmpStatusHistoryRepository;


        private readonly IHrLanguageRepository hrLanguageRepository;
        private readonly IHrFileRepository hrFileRepository;
        private readonly IHrSkillRepository hrSkillRepository;
        private readonly IHrEducationRepository hrEducationRepository;
        private readonly IHrWorkExperienceRepository hrWorkExperienceRepository;
        private readonly IHrGosiEmployeeRepository hrGosiEmployeeRepository;
        private readonly IHrGosiRepository hrGosiRepository;
        private readonly IHrGosiEmployeeAccVwRepository hrGosiEmployeeAccVwRepository;
        private readonly IHrVacationsDayTypeRepository hrVacationsDayTypeRepository;
        private readonly IHrIncrementTypeRepository hrIncrementTypeRepository;
        private readonly IHrPerformanceRepository hrPerformanceRepository;
        private readonly IHrKpiTemplatesJobRepository hrKpiTemplatesJobRepository;
        private readonly IHrEmpGoalIndicatorRepository hrEmpGoalIndicatorRepository;
        private readonly IHrEmpGoalIndicatorsEmployeeRepository hrEmpGoalIndicatorsEmployeeRepository;
        private readonly IHrEmpGoalIndicatorsCompetenceRepository hrEmpGoalIndicatorsCompetenceRepository;
        private readonly IHrDefinitionSalaryEmpRepository hrDefinitionSalaryEmpRepository;
        private readonly IHrActualAttendanceRepository hrActualAttendanceRepository;
        private readonly IHrPayrollDeductionVwRepository hrPayrollDeductionVwRepository;
        private readonly IHrGosiTypeVwRepository hrGosiTypeVwRepository;
        private readonly IHrFlexibleWorkingMasterRepository hrFlexibleWorkingMasterRepository;
        private readonly IHrFlexibleWorkingRepository hrFlexibleWorkingRepository;
        private readonly IHrMandateLocationMasterRepository hrMandateLocationMasterRepository;
        private readonly IHrMandateLocationDetaileRepository hrMandateLocationDetaileRepository;
        private readonly IHrMandateRequestsMasterRepository hrMandateRequestsMasterRepository;
        private readonly IHrMandateRequestsDetaileRepository hrMandateRequestsDetaileRepository;
        private readonly IHrExpensesTypeRepository hrExpensesTypeRepository;
        private readonly IHrJobOfferRepository hrJobOfferRepository;
        private readonly IHrExpenseRepository hrExpenseRepository;
        private readonly IHrExpensesEmployeeRepository hrExpensesEmployeeRepository;
        private readonly IHrJobOfferAdvantageRepository hrJobOfferAdvantageRepository;
        private readonly IHrProvisionRepository hrProvisionRepository;
        private readonly IHrProvisionsEmployeeRepository hrProvisionsEmployeeRepository;
        private readonly IHrProvisionsEmployeeAccVwRepository hrProvisionsEmployeeAccVwRepository;
        private readonly IHrTimeZoneRepository hrTimeZoneRepository;
        private readonly IHrRequestGoalsEmployeeDetailRepository hrRequestGoalsEmployeeDetailRepository;
        private readonly IHrExpensesScheduleRepository hrExpensesScheduleRepository;
        private readonly IHrExpensesPaymentRepository hrExpensesPaymentRepository;
        private readonly IHrIncomeTaxRepository hrIncomeTaxRepository;
        private readonly IHrIncomeTaxPeriodRepository hrIncomeTaxPeriodRepository;
        private readonly IHrIncomeTaxSlideRepository hrIncomeTaxSlideRepository;
        private readonly IHrPayrollTransactionTypeRepository hrPayrollTransactionTypeRepository;
        private readonly IHrPayrollTransactionTypeValueRepository hrPayrollTransactionTypeValueRepository;
        private readonly IHrStructureRepository hrStructureRepository;

        private readonly IHrVisitScheduleLocationRepository hrVisitScheduleLocationRepository;
        private readonly IHrVisitStepRepository hrVisitStepRepository;
        private readonly IHrPsAllowanceVwRepository hrPsAllowanceVwRepository;
        private readonly IHrPsDeductionVwRepository hrPsDeductionVwRepository;
        private readonly IHrJobGroupsRepository hrJobGroupsRepository;
        private readonly IHrJobCategoryRepository hrJobCategoryRepository;
        private readonly IHrJobAllowanceDeductionRepository hrJobAllowanceDeductionRepository;
        private readonly IHrJobLevelsAllowanceDeductionRepository hrJobLevelsAllowanceDeductionRepository;
        private readonly IHrLeaveAllowanceDeductionRepository hrLeaveAllowanceDeductionRepository;
        private readonly IHrProvisionsMedicalInsuranceRepository hrProvisionsMedicalInsuranceRepository;
        private readonly IHrProvisionsMedicalInsuranceEmployeeAccVwRepository hrProvisionsMedicalInsuranceEmployeeAccVwRepository;
        private readonly IHrProvisionsMedicalInsuranceEmployeeRepository hrProvisionsMedicalInsuranceEmployeeRepository;

        private readonly IHrInsuranceEmpVwRepository hrInsuranceEmpVwRepository;

        private readonly IHrContractsAllowanceDeductionRepository hrContractsAllowanceDeductionRepository;
        private readonly IHrClearanceAllowanceDeductionRepository hrClearanceAllowanceDeductionRepository;
        private readonly IHrContractsDeductionVwRepository hrContractsDeductionVwRepository;
        private readonly IHrContractsAllowanceVwRepository hrContractsAllowanceVwRepository;

        public HrRepositoryManager(
            IHrEmployeeRepository hrEmployeeRepository,
            IHrAbsenceRepository hrAbsenceRepository,
            IHrVacationsRepository hrVacationsRepository,
            IHrDelayRepository hrDelayRepository,
            IHrOverTimeDRepository hrOverTimeDRepository,
            IHrKpiTemplatesCompetenceRepository hrKpiTemplatesCompetenceRepository,
            IHrAttendanceRepository hrAttendanceRepository,
            IHrAttShiftEmployeeRepository hrAttShiftEmployeeRepository,
            IHrAttTimeTableRepository hrAttTimeTableRepository,
            IHrMandateRepository hrMandateRepository,
            IHrCompetenceRepository hrCompetenceRepository,
            IHrCompetencesCatagoryRepository hrCompetencesCatagoryRepository,
            IHrKpiTypeRepository hrKpiTypeRepository,
            IHrKpiTemplateRepository hrKpiTemplateRepository,
            IHrEvaluationAnnualIncreaseConfigRepository hrEvaluationAnnualIncreaseConfigRepository,
            IHrDisciplinaryCaseActionRepository hrDisciplinaryCaseActionRepository,
            IHrDisciplinaryCaseRepository hrDisciplinaryCaseRepository,
            IHrDisciplinaryRuleRepository hrDisciplinaryRuleRepository,
            IHrSettingRepository hrSettingRepository,
            IHrVacationsCatagoryRepository hrVacationsCatagoryRepository,
             IHrRateTypeRepository hrRateTypeRepository,
             IHrVacationsTypeRepository hrVacationsTypeRepository,
             IHrAllowanceDeductionRepository hrAllowanceDeductionRepository,
             IHrLoanRepository hrLoanRepository,
             IHrPayrollDRepository hrPayrollDRepository,
             IHrArchivesFileRepository hrArchivesFileRepository,
             IHrLicenseRepository hrLicenseRepository,
             IHrTransferRepository hrTransferRepository,
             IHrOverTimeMRepository hrOverTimeMRepository,
             IHrOhadDetailRepository hrOhadDetailRepository,
             IHrEmpWarnRepository hrEmpWarnRepository,
             IHrVacationBalanceRepository hrVacationBalanceRepository,
             IHrDependentRepository hrDependentRepository,
             IHrDirectJobRepository hrDirectJobRepository,
             IHrIncrementRepository hrIncrementRepository,
             IHrLeaveRepository hrLeaveRepository,
             IHrKpiRepository hrKpiRepository,
             IHrKpiDetaileRepository hrKpiDetaileRepository,
             IHrEmpWorkTimeRepository hrEmpWorkTimeRepository,
             IHrSalaryGroupRepository hrSalaryGroupRepository,
             IHrSalaryGroupRefranceRepository hrSalaryGroupRefranceRepository,
             IHrSalaryGroupAccountRepository hrSalaryGroupAccountRepository,
             IHrCardTemplateRepository hrCardTemplateRepository,
             IHrTrainingBagRepository hrTrainingBagRepository,
             IHrNotificationsTypeRepository hrNotificationsTypeRepository,
             IHrNotificationsSettingRepository hrNotificationsSettingRepository,
             IHrPoliciesTypeRepository hrPoliciesTypeRepository,
             IHrPolicyRepository hrPolicyRepository,
             IHrAttLocationRepository hrAttLocationRepository,
             IHrAttTimeTableDayRepository hrAttTimeTableDayRepository,
             IHrDisciplinaryActionTypeRepository hrDisciplinaryActionTypeRepository,
             IHrAllowanceVwRepository hrAllowanceVwRepository,
             IHrDeductionVwRepository hrDeductionVwRepository,
             IUnitOfWork unitOfWork
             , IHrAllowanceDeductionMRepository hrAllowanceDeductionMRepository
            , IHrAllowanceDeductionTempOrFixRepository hrAllowanceDeductionTempOrFixRepository
            , IHrArchiveFilesDetailRepository hrArchiveFilesDetailRepository
            , IHrAssignmenRepository hrAssignmenRepository
            , IHrAttActionRepository hrAttActionRepository
            , IHrAttLocationEmployeeRepository hrAttLocationEmployeeRepository
            , IHrAttShiftCloseDRepository hrAttShiftCloseDRepository
            , IHrAuthorizationRepository hrAuthorizationRepository
            , IHrAttendanceBioTimeRepository hrAttendanceBioTimeRepository
            , IHrCheckInOutRepository hrCheckInOutRepository
            , IHrClearanceRepository hrClearanceRepository
            , IHrClearanceTypeRepository hrClearanceTypeRepository
            , IHrCompensatoryVacationRepository hrCompensatoryVacationRepository
            , IHrContracteRepository hrContracteRepository,
            IHrAttShiftCloseRepository hrAttShiftCloseRepository,
            IHrClearanceMonthRepository hrClearanceMonthRepository,
            IHrRequestTypeRepository hrRequestTypeRepository,
            IHrRequestDetaileRepository hrRequestDetaileRepository,
            IHrRequestRepository hrRequestRepository,
            IHrOhadRepository hrOhadRepository

            , IHrCostTypeRepository hrCostTypeRepository
            , IHrCustodyRepository hrCustodyRepository
            , IHrCustodyItemRepository hrCustodyItemRepository
            , IHrCustodyItemsPropertyRepository hrCustodyItemsPropertyRepository
            , IHrCustodyRefranceTypeRepository hrCustodyRefranceTypeRepository
            , IHrCustodyTypeRepository hrCustodyTypeRepository
            , IHrDecisionRepository hrDecisionRepository
            , IHrDecisionsEmployeeRepository hrDecisionsEmployeeRepository,
            IHrNoteRepository hrNoteRepository,
            IHrIncrementsAllowanceDeductionRepository hrIncrementsAllowanceDeductionRepository,
            IHrHolidayRepository hrHolidayRepository,
            IHrPermissionRepository hrPermissionRepository,
            IHrAttShiftRepository hrAttShiftRepository,
            IHrAttShiftTimeTableRepository hrAttShiftTimeTableRepository,
            IHrEmployeeCostRepository hrEmployeeCostRepository,
            IHrOhadDetailsVwRepository hrOhadDetailsVwRepository,
            IHrInsurancePolicyRepository hrInsurancePolicyRepository,
            IHrInsuranceRepository hrInsuranceRepository,
            IHrInsuranceEmpRepository hrInsuranceEmpRepository,
            IHrJobRepository hrJobRepository,
            IHrJobDescriptionRepository hrJobDescriptionRepository,
            IHrJobEmployeeVwRepository hrJobEmployeeVwRepository,
            IHrJobLevelRepository hrJobLevelRepository,
            IHrRecruitmentVacancyRepository hrRecruitmentVacancyRepository,
            IHrRecruitmentApplicationRepository hrRecruitmentApplicationRepository,
            IHrRecruitmentCandidateRepository hrRecruitmentCandidateRepository,
            IHrJobGradeRepository hrJobGradeRepository,
            IHrRecruitmentCandidateKpiRepository hrRecruitmentCandidateKpiRepository,
             IHrRecruitmentCandidateKpiDRepository hrRecruitmentCandidateKpiDRepository,
             IHrPayrollRepository hrPayrollRepository,
             IHrTicketRepository hrTicketRepository,
             IHrVisaRepository hrVisaRepository,
             IHrFixingEmployeeSalaryRepository hrFixingEmployeeSalaryRepository,
             IHrLeaveTypeRepository hrLeaveTypeRepository,
             IHrPayrollAllowanceDeductionRepository hrPayrollAllowanceDeductionRepository,
             IHrLoanInstallmentRepository hrLoanInstallmentRepository,
             IHrLoanInstallmentPaymentRepository hrLoanInstallmentPaymentRepository,
             IHrPayrollNoteRepository hrPayrollNoteRepository,
             IHrDecisionsTypeRepository hrDecisionsTypeRepository,
             IHrDecisionsTypeEmployeeRepository hrDecisionsTypeEmployeeRepository,
             IHrNotificationRepository hrNotificationRepository,
             IHrEmployeeLocationVwRepository hrEmployeeLocationVwRepository,
             IHrAttShiftEmployeeMVwRepository hrAttShiftEmployeeMVwRepository,
             IHrAttCheckShiftEmployeeVwRepository hrAttCheckShiftEmployeeVwRepository,
             IHrOpeningBalanceRepository hrOpeningBalanceRepository,
              IHrOpeningBalanceTypeRepository hrOpeningBalanceTypeRepository,
              IHrPayrollAllowanceVwRepository hrPayrollAllowanceVwRepository,
              IHrPsAllowanceDeductionRepository hrPsAllowanceDeductionRepository,
              IHrPreparationSalaryRepository hrPreparationSalaryRepository,
              IHrPayrollDeductionAccountsVwRepository hrPayrollDeductionAccountsVwRepository,
              IHrPayrollCostcenterRepository hrPayrollCostcenterRepository,
              IHrPayrollAllowanceAccountsVwRepository hrPayrollAllowanceAccountsVwRepository,
              IHrNotificationsReplyRepository hrNotificationsReplyRepository,
              IHrLoanPaymentRepository hrLoanPaymentRepository,
              IHrPermissionReasonVwRepository hrPermissionReasonVwRepository,
              IHrPermissionTypeVwRepository hrPermissionTypeVwRepository,
              IHrEmpStatusHistoryRepository hrEmpStatusHistoryRepository,
              IHrLanguageRepository hrLanguageRepository,
              IHrFileRepository hrFileRepository,
              IHrSkillRepository hrSkillRepository,
              IHrEducationRepository hrEducationRepository,
              IHrWorkExperienceRepository hrWorkExperienceRepository,
              IHrGosiEmployeeRepository hrGosiEmployeeRepository,
              IHrGosiRepository hrGosiRepository,
              IHrGosiEmployeeAccVwRepository hrGosiEmployeeAccVwRepository,
              IHrVacationsDayTypeRepository hrVacationsDayTypeRepository,
              IHrIncrementTypeRepository hrIncrementTypeRepository,
              IHrPerformanceRepository hrPerformanceRepository,
              IHrKpiTemplatesJobRepository hrKpiTemplatesJobRepository,
              IHrEmpGoalIndicatorRepository hrEmpGoalIndicatorRepository,
              IHrEmpGoalIndicatorsEmployeeRepository hrEmpGoalIndicatorsEmployeeRepository,
              IHrEmpGoalIndicatorsCompetenceRepository hrEmpGoalIndicatorsCompetenceRepository,
              IHrDefinitionSalaryEmpRepository hrDefinitionSalaryEmpRepository,
              IHrActualAttendanceRepository hrActualAttendanceRepository,
              IHrPayrollDeductionVwRepository hrPayrollDeductionVwRepository,
              IHrGosiTypeVwRepository hrGosiTypeVwRepository,
              IHrFlexibleWorkingMasterRepository hrFlexibleWorkingMasterRepository,
              IHrFlexibleWorkingRepository hrFlexibleWorkingRepository,
              IHrMandateLocationMasterRepository hrMandateLocationMasterRepository,
              IHrMandateLocationDetaileRepository hrMandateLocationDetaileRepository,
              IHrMandateRequestsMasterRepository hrMandateRequestsMasterRepository,
              IHrMandateRequestsDetaileRepository hrMandateRequestsDetaileRepository,
              IHrExpensesTypeRepository hrExpensesTypeRepository,
              IHrJobOfferRepository hrJobOfferRepository,
              IHrExpenseRepository hrExpenseRepository,
              IHrExpensesEmployeeRepository hrExpensesEmployeeRepository,
              IHrJobOfferAdvantageRepository hrJobOfferAdvantageRepository,
              IHrProvisionRepository hrProvisionRepository,
              IHrProvisionsEmployeeRepository hrProvisionsEmployeeRepository,
              IHrProvisionsEmployeeAccVwRepository hrProvisionsEmployeeAccVwRepository,
              IHrTimeZoneRepository hrTimeZoneRepository,
              IHrRequestGoalsEmployeeDetailRepository hrRequestGoalsEmployeeDetailRepository,
              IHrExpensesScheduleRepository hrExpensesScheduleRepository,
              IHrExpensesPaymentRepository hrExpensesPaymentRepository,
              IHrIncomeTaxRepository hrIncomeTaxRepository,
              IHrIncomeTaxPeriodRepository hrIncomeTaxPeriodRepository,
              IHrIncomeTaxSlideRepository hrIncomeTaxSlideRepository,
              IHrPayrollTransactionTypeRepository hrPayrollTransactionTypeRepository,
              IHrPayrollTransactionTypeValueRepository hrPayrollTransactionTypeValueRepository,
              IHrStructureRepository hrStructureRepository,

              IHrVisitScheduleLocationRepository hrVisitScheduleLocationRepository,
              IHrVisitStepRepository hrVisitStepRepository,
              IHrPsAllowanceVwRepository hrPsAllowanceVwRepository,
              IHrPsDeductionVwRepository hrPsDeductionVwRepository,
              IHrJobGroupsRepository hrJobGroupsRepository,
              IHrJobCategoryRepository hrJobCategoryRepository,
              IHrJobAllowanceDeductionRepository hrJobAllowanceDeductionRepository,
              IHrJobLevelsAllowanceDeductionRepository hrJobLevelsAllowanceDeductionRepository,
              IHrLeaveAllowanceDeductionRepository hrLeaveAllowanceDeductionRepository,
              IHrProvisionsMedicalInsuranceRepository hrProvisionsMedicalInsuranceRepository,
              IHrProvisionsMedicalInsuranceEmployeeAccVwRepository hrProvisionsMedicalInsuranceEmployeeAccVwRepository,
              IHrProvisionsMedicalInsuranceEmployeeRepository hrProvisionsMedicalInsuranceEmployeeRepository,
              IHrInsuranceEmpVwRepository hrInsuranceEmpVwRepository,
              IHrContractsAllowanceDeductionRepository hrContractsAllowanceDeductionRepository,
              IHrClearanceAllowanceDeductionRepository hrClearanceAllowanceDeductionRepository,
              IHrContractsDeductionVwRepository hrContractsDeductionVwRepository,
              IHrContractsAllowanceVwRepository hrContractsAllowanceVwRepository
            )
        {
            this.hrDeductionVwRepository = hrDeductionVwRepository;
            this.hrAllowanceVwRepository = hrAllowanceVwRepository;
            this.hrEmployeeRepository = hrEmployeeRepository;
            this.hrAbsenceRepository = hrAbsenceRepository;
            this.hrVacationsRepository = hrVacationsRepository;
            this.hrDelayRepository = hrDelayRepository;
            this.hrOverTimeDRepository = hrOverTimeDRepository;
            this.hrKpiTemplatesCompetenceRepository = hrKpiTemplatesCompetenceRepository;
            this.hrAttendanceRepository = hrAttendanceRepository;
            this.hrAttShiftEmployeeRepository = hrAttShiftEmployeeRepository;
            this.hrAttTimeTableRepository = hrAttTimeTableRepository;
            this.hrMandateRepository = hrMandateRepository;
            this.hrCompetenceRepository = hrCompetenceRepository;
            this.hrCompetencesCatagoryRepository = hrCompetencesCatagoryRepository;
            this.hrKpiTypeRepository = hrKpiTypeRepository;
            this.hrKpiTemplateRepository = hrKpiTemplateRepository;
            this.hrEvaluationAnnualIncreaseConfigRepository = hrEvaluationAnnualIncreaseConfigRepository;
            this.hrDisciplinaryCaseActionRepository = hrDisciplinaryCaseActionRepository;
            this.hrDisciplinaryCaseRepository = hrDisciplinaryCaseRepository;
            this.hrDisciplinaryRuleRepository = hrDisciplinaryRuleRepository;
            this.hrSettingRepository = hrSettingRepository;
            this.hrVacationsCatagoryRepository = hrVacationsCatagoryRepository;
            this.hrRateTypeRepository = hrRateTypeRepository;
            this.hrVacationsTypeRepository = hrVacationsTypeRepository;
            this.hrAllowanceDeductionRepository = hrAllowanceDeductionRepository;
            this.hrLoanRepository = hrLoanRepository;
            this.hrPayrollDRepository = hrPayrollDRepository;
            this.hrArchivesFileRepository = hrArchivesFileRepository;
            this.hrLicenseRepository = hrLicenseRepository;
            this.hrTransferRepository = hrTransferRepository;
            this.hrOverTimeMRepository = hrOverTimeMRepository;
            this.hrOhadDetailRepository = hrOhadDetailRepository;
            this.hrEmpWarnRepository = hrEmpWarnRepository;
            this.hrVacationBalanceRepository = hrVacationBalanceRepository;
            this.hrDependentRepository = hrDependentRepository;
            this.hrDirectJobRepository = hrDirectJobRepository;
            this.hrIncrementRepository = hrIncrementRepository;
            this.hrLeaveRepository = hrLeaveRepository;
            this.hrKpiRepository = hrKpiRepository;
            this.hrKpiDetaileRepository = hrKpiDetaileRepository;
            this.hrEmpWorkTimeRepository = hrEmpWorkTimeRepository;
            this.hrSalaryGroupRepository = hrSalaryGroupRepository;
            this.hrSalaryGroupAccountRepository = hrSalaryGroupAccountRepository;
            this.hrSalaryGroupRefranceRepository = hrSalaryGroupRefranceRepository;
            this.hrCardTemplateRepository = hrCardTemplateRepository;
            this.hrTrainingBagRepository = hrTrainingBagRepository;
            this.hrNotificationsTypeRepository = hrNotificationsTypeRepository;
            this.hrNotificationsSettingRepository = hrNotificationsSettingRepository;
            this.hrPoliciesTypeRepository = hrPoliciesTypeRepository;
            this.hrPolicyRepository = hrPolicyRepository;
            this.hrAttLocationRepository = hrAttLocationRepository;
            this.hrAttTimeTableDayRepository = hrAttTimeTableDayRepository;
            this.hrDisciplinaryActionTypeRepository = hrDisciplinaryActionTypeRepository;
            this.unitOfWork = unitOfWork;
            this.hrAllowanceDeductionMRepository = hrAllowanceDeductionMRepository;
            this.hrAllowanceDeductionTempOrFixRepository = hrAllowanceDeductionTempOrFixRepository;
            this.hrArchiveFilesDetailRepository = hrArchiveFilesDetailRepository;
            this.hrAssignmenRepository = hrAssignmenRepository;
            this.hrAttActionRepository = hrAttActionRepository;
            this.hrAttLocationEmployeeRepository = hrAttLocationEmployeeRepository;
            this.hrAttShiftCloseRepository = hrAttShiftCloseRepository;
            this.hrAuthorizationRepository = hrAuthorizationRepository;
            this.hrAttendanceBioTimeRepository = hrAttendanceBioTimeRepository;
            this.hrCheckInOutRepository = hrCheckInOutRepository;
            this.hrClearanceRepository = hrClearanceRepository;
            this.hrClearanceTypeRepository = hrClearanceTypeRepository;
            this.hrCompensatoryVacationRepository = hrCompensatoryVacationRepository;
            this.hrContracteRepository = hrContracteRepository;
            this.hrClearanceMonthRepository = hrClearanceMonthRepository;

            this.hrRequestRepository = hrRequestRepository;
            this.hrRequestTypeRepository = hrRequestTypeRepository;
            this.hrRequestDetaileRepository = hrRequestDetaileRepository;
            this.hrAttShiftCloseDRepository = hrAttShiftCloseDRepository;
            this.hrCostTypeRepository = hrCostTypeRepository;
            this.hrCustodyRepository = hrCustodyRepository;
            this.hrCustodyItemRepository = hrCustodyItemRepository;
            this.hrCustodyItemsPropertyRepository = hrCustodyItemsPropertyRepository;
            this.hrCustodyRefranceTypeRepository = hrCustodyRefranceTypeRepository;
            this.hrCustodyTypeRepository = hrCustodyTypeRepository;
            this.hrDecisionRepository = hrDecisionRepository;
            this.hrDecisionsEmployeeRepository = hrDecisionsEmployeeRepository;
            this.hrAuthorizationRepository = hrAuthorizationRepository;
            this.hrOhadRepository = hrOhadRepository;
            this.hrNoteRepository = hrNoteRepository;
            this.hrIncrementsAllowanceDeductionRepository = hrIncrementsAllowanceDeductionRepository;
            this.hrHolidayRepository = hrHolidayRepository;
            this.hrPermissionRepository = hrPermissionRepository;
            this.hrAttShiftRepository = hrAttShiftRepository;
            this.hrAttShiftTimeTableRepository = hrAttShiftTimeTableRepository;
            this.hrEmployeeCostRepository = hrEmployeeCostRepository;
            this.hrOhadDetailsVwRepository = hrOhadDetailsVwRepository;
            this.hrInsurancePolicyRepository = hrInsurancePolicyRepository;
            this.hrInsuranceRepository = hrInsuranceRepository;
            this.hrInsuranceEmpRepository = hrInsuranceEmpRepository;
            this.hrJobRepository = hrJobRepository;
            this.hrJobDescriptionRepository = hrJobDescriptionRepository;
            this.hrJobEmployeeVwRepository = hrJobEmployeeVwRepository;
            this.hrJobLevelRepository = hrJobLevelRepository;
            this.hrRecruitmentVacancyRepository = hrRecruitmentVacancyRepository;
            this.hrRecruitmentApplicationRepository = hrRecruitmentApplicationRepository;
            this.hrRecruitmentCandidateRepository = hrRecruitmentCandidateRepository;
            this.hrJobGradeRepository = hrJobGradeRepository;
            this.hrRecruitmentCandidateKpiRepository = hrRecruitmentCandidateKpiRepository;
            this.hrRecruitmentCandidateKpiDRepository = hrRecruitmentCandidateKpiDRepository;
            this.hrPayrollRepository = hrPayrollRepository;
            this.hrTicketRepository = hrTicketRepository;
            this.hrVisaRepository = hrVisaRepository;
            this.hrFixingEmployeeSalaryRepository = hrFixingEmployeeSalaryRepository;
            this.hrLeaveTypeRepository = hrLeaveTypeRepository;
            this.hrPayrollAllowanceDeductionRepository = hrPayrollAllowanceDeductionRepository;
            this.hrLoanInstallmentPaymentRepository = hrLoanInstallmentPaymentRepository;
            this.hrLoanInstallmentRepository = hrLoanInstallmentRepository;
            this.hrPayrollNoteRepository = hrPayrollNoteRepository;
            this.hrDecisionsTypeRepository = hrDecisionsTypeRepository;
            this.hrDecisionsTypeEmployeeRepository = hrDecisionsTypeEmployeeRepository;
            this.hrNotificationRepository = hrNotificationRepository;
            this.hrEmployeeLocationVwRepository = hrEmployeeLocationVwRepository;
            this.hrAttShiftEmployeeMVwRepository = hrAttShiftEmployeeMVwRepository;
            this.hrAttCheckShiftEmployeeVwRepository = hrAttCheckShiftEmployeeVwRepository;
            this.hrOpeningBalanceRepository = hrOpeningBalanceRepository;
            this.hrOpeningBalanceTypeRepository = hrOpeningBalanceTypeRepository;
            this.hrPayrollAllowanceVwRepository = hrPayrollAllowanceVwRepository;
            this.hrPsAllowanceDeductionRepository = hrPsAllowanceDeductionRepository;
            this.hrPreparationSalaryRepository = hrPreparationSalaryRepository;
            this.hrPayrollDeductionAccountsVwRepository = hrPayrollDeductionAccountsVwRepository;
            this.hrPayrollCostcenterRepository = hrPayrollCostcenterRepository;
            this.hrPayrollAllowanceAccountsVwRepository = hrPayrollAllowanceAccountsVwRepository;
            this.hrNotificationsReplyRepository = hrNotificationsReplyRepository;
            this.hrLoanPaymentRepository = hrLoanPaymentRepository;
            this.hrPermissionReasonVwRepository = hrPermissionReasonVwRepository;
            this.hrPermissionTypeVwRepository = hrPermissionTypeVwRepository;
            this.hrEmpStatusHistoryRepository = hrEmpStatusHistoryRepository;
            this.hrLanguageRepository = hrLanguageRepository;
            this.hrFileRepository = hrFileRepository;
            this.hrSkillRepository = hrSkillRepository;
            this.hrEducationRepository = hrEducationRepository;
            this.hrWorkExperienceRepository = hrWorkExperienceRepository;
            this.hrGosiEmployeeRepository = hrGosiEmployeeRepository;
            this.hrGosiRepository = hrGosiRepository;
            this.hrGosiEmployeeAccVwRepository = hrGosiEmployeeAccVwRepository;
            this.hrVacationsDayTypeRepository = hrVacationsDayTypeRepository;
            this.hrIncrementTypeRepository = hrIncrementTypeRepository;
            this.hrPerformanceRepository = hrPerformanceRepository;
            this.hrKpiTemplatesJobRepository = hrKpiTemplatesJobRepository;
            this.hrEmpGoalIndicatorRepository = hrEmpGoalIndicatorRepository;
            this.hrEmpGoalIndicatorsCompetenceRepository = hrEmpGoalIndicatorsCompetenceRepository;
            this.hrEmpGoalIndicatorsEmployeeRepository = hrEmpGoalIndicatorsEmployeeRepository;
            this.hrDefinitionSalaryEmpRepository = hrDefinitionSalaryEmpRepository;
            this.hrActualAttendanceRepository = hrActualAttendanceRepository;
            this.hrPayrollDeductionVwRepository = hrPayrollDeductionVwRepository;
            this.hrGosiTypeVwRepository = hrGosiTypeVwRepository;
            this.hrFlexibleWorkingMasterRepository = hrFlexibleWorkingMasterRepository;
            this.hrFlexibleWorkingRepository = hrFlexibleWorkingRepository;
            this.hrMandateLocationMasterRepository = hrMandateLocationMasterRepository;
            this.hrMandateLocationDetaileRepository = hrMandateLocationDetaileRepository;
            this.hrMandateRequestsMasterRepository = hrMandateRequestsMasterRepository;
            this.hrMandateRequestsDetaileRepository = hrMandateRequestsDetaileRepository;
            this.hrExpensesTypeRepository = hrExpensesTypeRepository;
            this.hrJobOfferRepository = hrJobOfferRepository;
            this.hrExpensesEmployeeRepository = hrExpensesEmployeeRepository;
            this.hrExpenseRepository = hrExpenseRepository;
            this.hrJobOfferAdvantageRepository = hrJobOfferAdvantageRepository;
            this.hrProvisionRepository = hrProvisionRepository;
            this.hrProvisionsEmployeeRepository = hrProvisionsEmployeeRepository;
            this.hrProvisionsEmployeeAccVwRepository = hrProvisionsEmployeeAccVwRepository;
            this.hrTimeZoneRepository = hrTimeZoneRepository;
            this.hrRequestGoalsEmployeeDetailRepository = hrRequestGoalsEmployeeDetailRepository;
            this.hrExpensesScheduleRepository = hrExpensesScheduleRepository;
            this.hrExpensesPaymentRepository = hrExpensesPaymentRepository;
            this.hrIncomeTaxRepository = hrIncomeTaxRepository;
            this.hrIncomeTaxPeriodRepository = hrIncomeTaxPeriodRepository;
            this.hrIncomeTaxSlideRepository = hrIncomeTaxSlideRepository;
            this.hrPayrollTransactionTypeRepository = hrPayrollTransactionTypeRepository;
            this.hrPayrollTransactionTypeValueRepository = hrPayrollTransactionTypeValueRepository;
            this.hrStructureRepository = hrStructureRepository;

            this.hrVisitScheduleLocationRepository = hrVisitScheduleLocationRepository;
            this.hrVisitStepRepository = hrVisitStepRepository;
            this.hrPsAllowanceVwRepository = hrPsAllowanceVwRepository;
            this.hrPsDeductionVwRepository = hrPsDeductionVwRepository;
            this.hrJobGroupsRepository = hrJobGroupsRepository;
            this.hrJobCategoryRepository = hrJobCategoryRepository;
            this.hrJobAllowanceDeductionRepository = hrJobAllowanceDeductionRepository;
            this.hrJobLevelsAllowanceDeductionRepository = hrJobLevelsAllowanceDeductionRepository;
            this.hrLeaveAllowanceDeductionRepository = hrLeaveAllowanceDeductionRepository;
            this.hrProvisionsMedicalInsuranceRepository = hrProvisionsMedicalInsuranceRepository;
            this.hrProvisionsMedicalInsuranceEmployeeAccVwRepository = hrProvisionsMedicalInsuranceEmployeeAccVwRepository;
            this.hrProvisionsMedicalInsuranceEmployeeRepository = hrProvisionsMedicalInsuranceEmployeeRepository;

            this.hrInsuranceEmpVwRepository = hrInsuranceEmpVwRepository;

            this.hrContractsAllowanceDeductionRepository = hrContractsAllowanceDeductionRepository;
            this.hrClearanceAllowanceDeductionRepository = hrClearanceAllowanceDeductionRepository;
            this.hrContractsDeductionVwRepository = hrContractsDeductionVwRepository;
            this.hrContractsAllowanceVwRepository = hrContractsAllowanceVwRepository;
        }

        public IHrEmployeeRepository HrEmployeeRepository => hrEmployeeRepository;
        public IHrAbsenceRepository HrAbsenceRepository => hrAbsenceRepository;
        public IHrVacationsRepository HrVacationsRepository => hrVacationsRepository;
        public IHrDelayRepository HrDelayRepository => hrDelayRepository;
        public IHrOverTimeDRepository HrOverTimeDRepository => hrOverTimeDRepository;
        public IHrKpiTemplatesCompetenceRepository HrKpiTemplatesCompetenceRepository => hrKpiTemplatesCompetenceRepository;
        public IUnitOfWork UnitOfWork => unitOfWork;

        public IHrAttendanceRepository HrAttendanceRepository => hrAttendanceRepository;

        public IHrAttTimeTableRepository HrAttTimeTableRepository => hrAttTimeTableRepository;

        public IHrAttShiftEmployeeRepository HrAttShiftEmployeeRepository => hrAttShiftEmployeeRepository;

        public IHrMandateRepository HrMandateRepository => hrMandateRepository;
        public IHrCompetenceRepository HrCompetenceRepository => hrCompetenceRepository;
        public IHrKpiTypeRepository HrKpiTypeRepository => hrKpiTypeRepository;
        public IHrCompetencesCatagoryRepository HrCompetencesCatagoryRepository => hrCompetencesCatagoryRepository;

        public IHrKpiTemplateRepository HrKpiTemplateRepository => hrKpiTemplateRepository;
        public IHrEvaluationAnnualIncreaseConfigRepository HrEvaluationAnnualIncreaseConfigRepository => hrEvaluationAnnualIncreaseConfigRepository;

        public IHrDisciplinaryCaseActionRepository HrDisciplinaryCaseActionRepository => hrDisciplinaryCaseActionRepository;

        public IHrDisciplinaryCaseRepository HrDisciplinaryCaseRepository => hrDisciplinaryCaseRepository;

        public IHrDisciplinaryRuleRepository HrDisciplinaryRuleRepository => hrDisciplinaryRuleRepository;

        public IHrSettingRepository HrSettingRepository => hrSettingRepository;

        public IHrVacationsCatagoryRepository HrVacationsCatagoryRepository => hrVacationsCatagoryRepository;

        public IHrRateTypeRepository HrRateTypeRepository => hrRateTypeRepository;

        public IHrVacationsTypeRepository HrVacationsTypeRepository => hrVacationsTypeRepository;

        public IHrAllowanceDeductionRepository HrAllowanceDeductionRepository => hrAllowanceDeductionRepository;

        public IHrLoanRepository HrLoanRepository => hrLoanRepository;

        public IHrPayrollDRepository HrPayrollDRepository => hrPayrollDRepository;

        public IHrArchivesFileRepository HrArchivesFileRepository => hrArchivesFileRepository;

        public IHrLicenseRepository HrLicenseRepository => hrLicenseRepository;

        public IHrTransferRepository HrTransferRepository => hrTransferRepository;

        public IHrOverTimeMRepository HrOverTimeMRepository => hrOverTimeMRepository;

        public IHrOhadDetailRepository HrOhadDetailRepository => hrOhadDetailRepository;

        public IHrEmpWarnRepository HrEmpWarnRepository => hrEmpWarnRepository;

        public IHrVacationBalanceRepository HrVacationBalanceRepository => hrVacationBalanceRepository;

        public IHrDependentRepository HrDependentRepository => hrDependentRepository;

        public IHrDirectJobRepository HrDirectJobRepository => hrDirectJobRepository;

        public IHrIncrementRepository HrIncrementRepository => hrIncrementRepository;

        public IHrLeaveRepository HrLeaveRepository => hrLeaveRepository;

        public IHrKpiRepository HrKpiRepository => hrKpiRepository;

        public IHrKpiDetaileRepository HrKpiDetaileRepository => hrKpiDetaileRepository;

        public IHrEmpWorkTimeRepository HrEmpWorkTimeRepository => hrEmpWorkTimeRepository;

        public IHrSalaryGroupRepository HrSalaryGroupRepository => hrSalaryGroupRepository;

        public IHrSalaryGroupAccountRepository HrSalaryGroupAccountRepository => hrSalaryGroupAccountRepository;

        public IHrSalaryGroupRefranceRepository HrSalaryGroupRefranceRepository => hrSalaryGroupRefranceRepository;

        public IHrCardTemplateRepository HrCardTemplateRepository => hrCardTemplateRepository;

        public IHrTrainingBagRepository HrTrainingBagRepository => hrTrainingBagRepository;

        public IHrNotificationsTypeRepository HrNotificationsTypeRepository => hrNotificationsTypeRepository;

        public IHrNotificationsSettingRepository HrNotificationsSettingRepository => hrNotificationsSettingRepository;

        public IHrPolicyRepository HrPolicyRepository => hrPolicyRepository;

        public IHrPoliciesTypeRepository HrPoliciesTypeRepository => hrPoliciesTypeRepository;

        public IHrAttLocationRepository HrAttLocationRepository => hrAttLocationRepository;

        public IHrAttTimeTableDayRepository HrAttTimeTableDayRepository => hrAttTimeTableDayRepository;


        public IHrDisciplinaryActionTypeRepository HrDisciplinaryActionTypeRepository => hrDisciplinaryActionTypeRepository;

        public IHrDeductionVwRepository HrDeductionVwRepository => hrDeductionVwRepository;

        public IHrAllowanceVwRepository HrAllowanceVwRepository => hrAllowanceVwRepository;

        public IHrAllowanceDeductionMRepository HrAllowanceDeductionMRepository => hrAllowanceDeductionMRepository;
        public IHrAllowanceDeductionTempOrFixRepository HrAllowanceDeductionTempOrFixRepository => hrAllowanceDeductionTempOrFixRepository;
        public IHrArchiveFilesDetailRepository HrArchiveFilesDetailRepository => hrArchiveFilesDetailRepository;
        public IHrAttActionRepository HrAttActionRepository => hrAttActionRepository;
        public IHrAttLocationEmployeeRepository HrAttLocationEmployeeRepository => hrAttLocationEmployeeRepository;
        public IHrAttShiftCloseDRepository HrAttShiftCloseDRepository => hrAttShiftCloseDRepository;
        public IHrAttShiftCloseRepository HrAttShiftCloseRepository => hrAttShiftCloseRepository;


        public IHrOhadRepository HrOhadRepository => hrOhadRepository;
        public IHrAssignmenRepository HrAssignmenRepository => hrAssignmenRepository;
        public IHrAttActionRepository HRAttActionRepository => hrAttActionRepository;
        public IHrAttLocationEmployeeRepository HRAttLocationEmployeeRepository => hrAttLocationEmployeeRepository;
        public IHrAttShiftCloseDRepository HRAttShiftCloseDRepository => hrAttShiftCloseDRepository;

        public IHrAuthorizationRepository HRAuthorizationRepository => hrAuthorizationRepository;

        public IHrAuthorizationRepository HrAuthorizationRepository => hrAuthorizationRepository;

        public IHrAttendanceBioTimeRepository HRAttendanceBioTimeRepository => hrAttendanceBioTimeRepository;

        public IHrAttendanceBioTimeRepository HrAttendanceBioTimeRepository => hrAttendanceBioTimeRepository;

        public IHrCheckInOutRepository HrCheckInOutRepository => hrCheckInOutRepository;

        public IHrClearanceRepository HrClearanceRepository => hrClearanceRepository;

        public IHrClearanceTypeRepository HrClearanceTypeRepository => hrClearanceTypeRepository;


        public IHrCompensatoryVacationRepository HrCompensatoryVacationRepository => hrCompensatoryVacationRepository;

        public IHrContracteRepository HrContracteRepository => hrContracteRepository;

        public IHrClearanceMonthRepository HrClearanceMonthRepository => hrClearanceMonthRepository;


        public IHrCostTypeRepository HrCostTypeRepository => hrCostTypeRepository;

        public IHrCustodyRepository HrCustodyRepository => hrCustodyRepository;

        public IHrCustodyItemRepository HrCustodyItemRepository => hrCustodyItemRepository;

        public IHrCustodyItemsPropertyRepository HrCustodyItemsPropertyRepository => hrCustodyItemsPropertyRepository;

        public IHrCustodyRefranceTypeRepository HrCustodyRefranceTypeRepository => hrCustodyRefranceTypeRepository;


        public IHrCustodyTypeRepository HrCustodyTypeRepository => hrCustodyTypeRepository;

        public IHrDecisionRepository HrDecisionRepository => hrDecisionRepository;
        public IHrDecisionsEmployeeRepository HrDecisionsEmployeeRepository => hrDecisionsEmployeeRepository;

        public IHrRequestRepository HrRequestRepository => hrRequestRepository;

        public IHrRequestDetaileRepository HrRequestDetaileRepository => hrRequestDetaileRepository;

        public IHrRequestTypeRepository HrRequestTypeRepository => hrRequestTypeRepository;

        public IHrNoteRepository HrNoteRepository => hrNoteRepository;

        public IHrIncrementsAllowanceDeductionRepository HrIncrementsAllowanceDeductionRepository => hrIncrementsAllowanceDeductionRepository;
        public IHrHolidayRepository HrHolidayRepository => hrHolidayRepository;

        public IHrPermissionRepository HrPermissionRepository => hrPermissionRepository;

        public IHrAttShiftRepository HrAttShiftRepository => hrAttShiftRepository;
        public IHrAttShiftTimeTableRepository HrAttShiftTimeTableRepository => hrAttShiftTimeTableRepository;
        public IHrEmployeeCostRepository HrEmployeeCostRepository => hrEmployeeCostRepository;
        public IHrOhadDetailsVwRepository HrOhadDetailsVwRepository => hrOhadDetailsVwRepository;

        public IHrInsurancePolicyRepository HrInsurancePolicyRepository => hrInsurancePolicyRepository;
        public IHrInsuranceRepository HrInsuranceRepository => hrInsuranceRepository;
        public IHrInsuranceEmpRepository HrInsuranceEmpRepository => hrInsuranceEmpRepository;

        public IHrJobRepository HrJobRepository => hrJobRepository;

        public IHrJobDescriptionRepository HrJobDescriptionRepository => hrJobDescriptionRepository;

        public IHrJobEmployeeVwRepository HrJobEmployeeVwRepository => hrJobEmployeeVwRepository;

        public IHrJobLevelRepository HrJobLevelRepository => hrJobLevelRepository;

        public IHrRecruitmentApplicationRepository HrRecruitmentApplicationRepository => hrRecruitmentApplicationRepository;

        public IHrRecruitmentVacancyRepository HrRecruitmentVacancyRepository => hrRecruitmentVacancyRepository;

        public IHrRecruitmentCandidateRepository HrRecruitmentCandidateRepository => hrRecruitmentCandidateRepository;

        public IHrJobGradeRepository HrJobGradeRepository => hrJobGradeRepository;

        public IHrRecruitmentCandidateKpiDRepository HrRecruitmentCandidateKpiDRepository => hrRecruitmentCandidateKpiDRepository;

        public IHrRecruitmentCandidateKpiRepository HrRecruitmentCandidateKpiRepository => hrRecruitmentCandidateKpiRepository;

        public IHrPayrollRepository HrPayrollRepository => hrPayrollRepository;

        public IHrTicketRepository HrTicketRepository => hrTicketRepository;

        public IHrVisaRepository HrVisaRepository => hrVisaRepository;

        public IHrFixingEmployeeSalaryRepository HrFixingEmployeeSalaryRepository => hrFixingEmployeeSalaryRepository;

        public IHrLeaveTypeRepository HrLeaveTypeRepository => hrLeaveTypeRepository;

        public IHrPayrollAllowanceDeductionRepository HrPayrollAllowanceDeductionRepository => hrPayrollAllowanceDeductionRepository;

        public IHrLoanInstallmentPaymentRepository HrLoanInstallmentPaymentRepository => hrLoanInstallmentPaymentRepository;

        public IHrLoanInstallmentRepository HrLoanInstallmentRepository => hrLoanInstallmentRepository;

        public IHrPayrollNoteRepository HrPayrollNoteRepository => hrPayrollNoteRepository;

        public IHrDecisionsTypeRepository HrDecisionsTypeRepository => hrDecisionsTypeRepository;

        public IHrDecisionsTypeEmployeeRepository HrDecisionsTypeEmployeeRepository => hrDecisionsTypeEmployeeRepository;

        public IHrNotificationRepository HrNotificationRepository => hrNotificationRepository;

        public IHrEmployeeLocationVwRepository HrEmployeeLocationVwRepository => hrEmployeeLocationVwRepository;

        public IHrAttShiftEmployeeMVwRepository HrAttShiftEmployeeMVwRepository => hrAttShiftEmployeeMVwRepository;

        public IHrAttCheckShiftEmployeeVwRepository HrAttCheckShiftEmployeeVwRepository => hrAttCheckShiftEmployeeVwRepository;

        public IHrOpeningBalanceRepository HrOpeningBalanceRepository => hrOpeningBalanceRepository;

        public IHrOpeningBalanceTypeRepository HrOpeningBalanceTypeRepository => hrOpeningBalanceTypeRepository;
        public IHrPayrollAllowanceVwRepository HrPayrollAllowanceVwRepository => hrPayrollAllowanceVwRepository;

        public IHrPsAllowanceDeductionRepository HrPsAllowanceDeductionRepository => hrPsAllowanceDeductionRepository;
        public IHrPreparationSalaryRepository HrPreparationSalaryRepository => hrPreparationSalaryRepository;
        public IHrPayrollDeductionAccountsVwRepository HrPayrollDeductionAccountsVwRepository => hrPayrollDeductionAccountsVwRepository;


        public IHrPayrollCostcenterRepository HrPayrollCostcenterRepository => hrPayrollCostcenterRepository;

        public IHrPayrollAllowanceAccountsVwRepository HrPayrollAllowanceAccountsVwRepository => hrPayrollAllowanceAccountsVwRepository;

        public IHrNotificationsReplyRepository HrNotificationsReplyRepository => hrNotificationsReplyRepository;

        public IHrLoanPaymentRepository HrLoanPaymentRepository => hrLoanPaymentRepository;

        public IHrPermissionTypeVwRepository HrPermissionTypeVwRepository => hrPermissionTypeVwRepository;

        public IHrPermissionReasonVwRepository HrPermissionReasonVwRepository => hrPermissionReasonVwRepository;
        public IHrEmpStatusHistoryRepository HrEmpStatusHistoryRepository => hrEmpStatusHistoryRepository;

        public IHrLanguageRepository HrLanguageRepository => hrLanguageRepository;

        public IHrFileRepository HrFileRepository => hrFileRepository;

        public IHrSkillRepository HrSkillRepository => hrSkillRepository;

        public IHrEducationRepository HrEducationRepository => hrEducationRepository;

        public IHrWorkExperienceRepository HrWorkExperienceRepository => hrWorkExperienceRepository;

        public IHrGosiEmployeeRepository HrGosiEmployeeRepository => hrGosiEmployeeRepository;

        public IHrGosiRepository HrGosiRepository => hrGosiRepository;

        public IHrGosiEmployeeAccVwRepository HrGosiEmployeeAccVwRepository => hrGosiEmployeeAccVwRepository;

        public IHrVacationsDayTypeRepository HrVacationsDayTypeRepository => hrVacationsDayTypeRepository;

        public IHrIncrementTypeRepository HrIncrementTypeRepository => hrIncrementTypeRepository;

        public IHrPerformanceRepository HrPerformanceRepository => hrPerformanceRepository;

        public IHrKpiTemplatesJobRepository HrKpiTemplatesJobRepository => hrKpiTemplatesJobRepository;

        public IHrEmpGoalIndicatorRepository HrEmpGoalIndicatorRepository => hrEmpGoalIndicatorRepository;

        public IHrEmpGoalIndicatorsCompetenceRepository HrEmpGoalIndicatorsCompetenceRepository => hrEmpGoalIndicatorsCompetenceRepository;

        public IHrEmpGoalIndicatorsEmployeeRepository HrEmpGoalIndicatorsEmployeeRepository => hrEmpGoalIndicatorsEmployeeRepository;

        public IHrDefinitionSalaryEmpRepository HrDefinitionSalaryEmpRepository => hrDefinitionSalaryEmpRepository;
        public IHrActualAttendanceRepository HrActualAttendanceRepository => hrActualAttendanceRepository;

        public IHrPayrollDeductionVwRepository HrPayrollDeductionVwRepository => hrPayrollDeductionVwRepository;

        public IHrGosiTypeVwRepository HrGosiTypeVwRepository => hrGosiTypeVwRepository;

        public IHrFlexibleWorkingMasterRepository HrFlexibleWorkingMasterRepository => hrFlexibleWorkingMasterRepository;

        public IHrFlexibleWorkingRepository HrFlexibleWorkingRepository => hrFlexibleWorkingRepository;

        public IHrMandateLocationMasterRepository HrMandateLocationMasterRepository => hrMandateLocationMasterRepository;

        public IHrMandateLocationDetaileRepository HrMandateLocationDetaileRepository => hrMandateLocationDetaileRepository;

        public IHrMandateRequestsMasterRepository HrMandateRequestsMasterRepository => hrMandateRequestsMasterRepository;
        public IHrMandateRequestsDetaileRepository HrMandateRequestsDetaileRepository => hrMandateRequestsDetaileRepository;
        public IHrExpensesTypeRepository HrExpensesTypeRepository => hrExpensesTypeRepository;

        public IHrJobOfferRepository HrJobOfferRepository => hrJobOfferRepository;

        public IHrExpensesEmployeeRepository HrExpensesEmployeeRepository => hrExpensesEmployeeRepository;

        public IHrExpenseRepository HrExpenseRepository => hrExpenseRepository;
        public IHrJobOfferAdvantageRepository HrJobOfferAdvantageRepository => hrJobOfferAdvantageRepository;

        public IHrProvisionRepository HrProvisionRepository => hrProvisionRepository;
        public IHrProvisionsEmployeeRepository HrProvisionsEmployeeRepository => hrProvisionsEmployeeRepository;
        public IHrProvisionsEmployeeAccVwRepository HrProvisionsEmployeeAccVwRepository => hrProvisionsEmployeeAccVwRepository;

        public IHrTimeZoneRepository HrTimeZoneRepository => hrTimeZoneRepository;
        public IHrRequestGoalsEmployeeDetailRepository HrRequestGoalsEmployeeDetailRepository => hrRequestGoalsEmployeeDetailRepository;

        public IHrExpensesPaymentRepository HrExpensesPaymentRepository => hrExpensesPaymentRepository;

        public IHrExpensesScheduleRepository HrExpensesScheduleRepository => hrExpensesScheduleRepository;
        public IHrIncomeTaxRepository HrIncomeTaxRepository => hrIncomeTaxRepository;
        public IHrIncomeTaxPeriodRepository HrIncomeTaxPeriodRepository => hrIncomeTaxPeriodRepository;
        public IHrIncomeTaxSlideRepository HrIncomeTaxSlideRepository => hrIncomeTaxSlideRepository;
        public IHrPayrollTransactionTypeRepository HrPayrollTransactionTypeRepository => hrPayrollTransactionTypeRepository;
        public IHrPayrollTransactionTypeValueRepository HrPayrollTransactionTypeValueRepository => hrPayrollTransactionTypeValueRepository;
        public IHrStructureRepository HrStructureRepository => hrStructureRepository;

        public IHrVisitScheduleLocationRepository HrVisitScheduleLocationRepository => hrVisitScheduleLocationRepository;
        public IHrVisitStepRepository HrVisitStepRepository => hrVisitStepRepository;
        public IHrPsAllowanceVwRepository HrPsAllowanceVwRepository => hrPsAllowanceVwRepository;
        public IHrPsDeductionVwRepository HrPsDeductionVwRepository => hrPsDeductionVwRepository;
        public IHrJobGroupsRepository HrJobGroupsRepository => hrJobGroupsRepository;
        public IHrJobCategoryRepository HrJobCategoryRepository => hrJobCategoryRepository;
        public IHrJobAllowanceDeductionRepository HrJobAllowanceDeductionRepository => hrJobAllowanceDeductionRepository;
        public IHrJobLevelsAllowanceDeductionRepository HrJobLevelsAllowanceDeductionRepository => hrJobLevelsAllowanceDeductionRepository;
        public IHrLeaveAllowanceDeductionRepository HrLeaveAllowanceDeductionRepository => hrLeaveAllowanceDeductionRepository;
        public IHrProvisionsMedicalInsuranceRepository HrProvisionsMedicalInsuranceRepository => hrProvisionsMedicalInsuranceRepository;

        public IHrProvisionsMedicalInsuranceEmployeeRepository HrProvisionsMedicalInsuranceEmployeeRepository => hrProvisionsMedicalInsuranceEmployeeRepository;

        public IHrProvisionsMedicalInsuranceEmployeeAccVwRepository HrProvisionsMedicalInsuranceEmployeeAccVwRepository => hrProvisionsMedicalInsuranceEmployeeAccVwRepository;

        public IHrInsuranceEmpVwRepository HrInsuranceEmpVwRepository => hrInsuranceEmpVwRepository;

        public IHrContractsAllowanceDeductionRepository HrContractsAllowanceDeductionRepository => hrContractsAllowanceDeductionRepository;
        public IHrClearanceAllowanceDeductionRepository HrClearanceAllowanceDeductionRepository => hrClearanceAllowanceDeductionRepository;
        public IHrContractsDeductionVwRepository HrContractsDeductionVwRepository => hrContractsDeductionVwRepository;
        public IHrContractsAllowanceVwRepository HrContractsAllowanceVwRepository => hrContractsAllowanceVwRepository;

    }
}
