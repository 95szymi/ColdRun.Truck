using ColdRun.Truck.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ColdRun.Truck.ViewModel
{
    public class EditTruckViewModel
    {
        public EditTruckViewModel()
        {
        }

        public EditTruckViewModel(Domain.Truck truck)
        {
            this.Id = truck.Id;
            this.Name = truck.Name;
            this.Description = truck.Description;
            this.TruckStatus = truck.TruckStatus;
            this.TruckStatusList = GetAvailableTruckStatuses(truck.TruckStatus)
               .Select(t => new SelectListItem
               {
                   Text = t.ToString(),
                   Value = ((int)t).ToString()
               }).ToList();
        }

        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public TruckStatus TruckStatus { get; set; }

        public List<SelectListItem> TruckStatusList { get; set; } = new List<SelectListItem>();

        private IEnumerable<TruckStatus> GetAvailableTruckStatuses(TruckStatus currentStatus)
        {
            switch (currentStatus)
            {
                case TruckStatus.OutOfService:
                    return (IEnumerable<TruckStatus>)Enum.GetValues(typeof(TruckStatus));
                case TruckStatus.Loading:
                    return new List<TruckStatus> { TruckStatus.Loading, TruckStatus.OutOfService, TruckStatus.ToJob };
                case TruckStatus.ToJob:
                    return new List<TruckStatus> { TruckStatus.ToJob, TruckStatus.OutOfService, TruckStatus.AtJob };
                case TruckStatus.AtJob:
                    return new List<TruckStatus> { TruckStatus.AtJob, TruckStatus.OutOfService, TruckStatus.Returning };
                case TruckStatus.Returning:
                    return new List<TruckStatus> { TruckStatus.Returning, TruckStatus.OutOfService, TruckStatus.Loading };
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentStatus), currentStatus, null);
            }
        }
    }
}
