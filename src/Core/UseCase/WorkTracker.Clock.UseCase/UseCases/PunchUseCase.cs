using WorkTracker.Clock.UseCase.Ports;
using WorkTracker.Clock.Domain.Repositories;
using WorkTracker.Clock.Domain.Ports;
using WorkTracker.Clock.UseCase.OutputViewModels;
using WorkTracker.Domain.Core;
using WorkTracker.Clock.Domain.Models;
using WorkTracker.Clock.Domain.Models.Enums;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WorkTracker.Clock.UseCase.UseCases
{
	public class PunchUseCases : IPunchUseCases
	{
		private readonly IPunchesRepository _punchRepository;
		private readonly IPunchService _punchService;
		private readonly IUtilsService _utilsService;
		private readonly IEmailNotificationService _emailNotificationService;

        public PunchUseCases(IPunchesRepository punchRepository,
			IPunchService punchService,
			IUtilsService utilsService,
			IEmailNotificationService emailNotificationService)
		{
			_punchRepository = punchRepository;
			_punchService = punchService;
			_utilsService = utilsService;
			_emailNotificationService = emailNotificationService;
        }

		public async Task<DailyPunchesViewModel> GetPunches(string rm)
		{
			var employeeHash = _utilsService.GenerateHash(rm);

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

		public async Task<MonthlyPunchesViewModel> GetMonthlyPunches(string rm, string email)
		{
			var employeeHash = _utilsService.GenerateHash(rm);

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

			_emailNotificationService.SendMessageAsync(email, "Monthly Report", JsonSerializer.Serialize(dailyPunches));

			return new MonthlyPunchesViewModel
			{
				DailyPunches = dailyPunches
			};
		}

		public async Task<OutputPunchViewModel> Punch(string rm)
		{
			var employeeHash = _utilsService.GenerateHash(rm);

			var punches = await _punchRepository.GetPunches(employeeHash);
			var punchType = punches.Count() % 2 == 0 ? PunchType.In : PunchType.Out;

			var punch = new Punch(punchType, employeeHash);

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