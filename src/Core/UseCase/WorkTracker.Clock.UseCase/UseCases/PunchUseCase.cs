using WorkTracker.Clock.UseCase.Ports;
using WorkTracker.Clock.Domain.Repositories;
using WorkTracker.Clock.Domain.Ports;
using WorkTracker.Clock.UseCase.OutputViewModels;
using WorkTracker.Domain.Core;
using WorkTracker.Clock.Domain.Models;
using WorkTracker.Clock.Domain.Models.Enums;

namespace WorkTracker.Clock.UseCase.UseCases
{
	public class PunchUseCases : IPunchUseCases
	{
		private readonly IPunchesRepository _punchRepository;
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IPunchService _punchService;
		private readonly IEmployeeService _employeeService;

        public PunchUseCases(IPunchesRepository punchRepository,
			IEmployeeRepository employeeRepository,
			IPunchService punchService,
			IEmployeeService employeeService)
		{
			_punchRepository = punchRepository;
			_employeeRepository = employeeRepository;
			_punchService = punchService;
			_employeeService = employeeService;
        }

		public async Task<DailyPunchesViewModel> GetPunches(string rm)
		{
			var employeeHash = _employeeService.GenerateEmployeeHash(rm);
			var employee = await _employeeRepository.GetEmployee(employeeHash) ?? throw new DomainException("Invalid rm.");

			var punches = await _punchRepository.GetPunches(employeeHash);
			if (punches is null || !punches.Any())
			{
				throw new DomainException("No punches found.");
			}
			return new DailyPunchesViewModel
			{
				TotalWorkedHours = _punchService.CalculateTotalWorkedHours(punches),
				Punches = punches.Select(p => new OutputPunchViewModel
				{
					PunchType = p.Type.ToString(),
					Timestamp = p.GetTimestamp(),
					IsApproved = p.IsApproved
				})
			};
		}

		public async Task<MonthlyPunchesViewModel> GetMonthlyPunches(string rm)
		{
			var employeeHash = _employeeService.GenerateEmployeeHash(rm);
			var employee = await _employeeRepository.GetEmployee(employeeHash) ?? throw new DomainException("Invalid rm.");

			var punches = await _punchRepository.GetMonthlyPunches(employeeHash);
			
			if (punches is null || !punches.Any())
			{
				throw new DomainException("No punches found.");
			}

			var dailyPunches = punches.GroupBy(p => p.GetTimestamp().Date)
				.Select(g => new DailyPunchesViewModel
				{
				  TotalWorkedHours = _punchService.CalculateTotalWorkedHours(g.ToList()),
				  Punches = g.OrderBy(p => p.GetTimestamp()).Select(p => new OutputPunchViewModel
				  {
					  PunchType = p.Type.ToString(),
					  Timestamp = p.GetTimestamp(),
					  IsApproved = p.IsApproved
				  })
				})
				.ToList();

			return new MonthlyPunchesViewModel
			{
				DailyPunches = dailyPunches
			};
		}

		public async Task<OutputPunchViewModel> Punch(string rm)
		{
			var employeeHash = _employeeService.GenerateEmployeeHash(rm);
			var employee = await _employeeRepository.GetEmployee(employeeHash) ?? throw new DomainException("Invalid rm.");

            var punches = await _punchRepository.GetPunches(employeeHash);
			var punchType = punches.Count() % 2 == 0 ? PunchType.In : PunchType.Out;

			var punch = new Punch(punchType, employee.Hash);

			_punchService.ValidatePunch(punch);

			var success = await _punchRepository.Create(punch);
			
			if (!success)
			{
				throw new Exception("An error occurred while punching.");
			}

			return new OutputPunchViewModel
			{
				PunchType = punch.Type.ToString(),
				Timestamp = punch.GetTimestamp(),
				IsApproved = punch.IsApproved
			};
		}
	}
}