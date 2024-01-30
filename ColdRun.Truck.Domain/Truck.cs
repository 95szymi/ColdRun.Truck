namespace ColdRun.Truck.Domain
{
    public class Truck : IBaseEntity
    {
        public Truck(string name, string? description = null)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Description = description;
            this.TruckStatus = TruckStatus.OutOfService;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; } = null;

        public TruckStatus TruckStatus { get; private set; }

        public void UpdateTruckStatus(TruckStatus newStatus)
        {
            if (newStatus == TruckStatus.OutOfService || this.TruckStatus == TruckStatus.OutOfService)
            {
                this.TruckStatus = newStatus;
            }
            else
            {
                int currentIndex = Array.IndexOf(statusOrder, this.TruckStatus);
                int newIndex = Array.IndexOf(statusOrder, newStatus);

                if (newIndex == 0 && currentIndex == statusOrder.Length - 1) // Going from Returning back to Loading
                {
                    this.TruckStatus = newStatus;
                }
                else if (newIndex == currentIndex + 1) // Moving to the next status in the order
                {
                    this.TruckStatus = newStatus;
                }
                else
                {
                    throw new InvalidOperationException("Invalid status transition");
                }
            }
        }

        private static readonly TruckStatus[] statusOrder = new TruckStatus[]
        {
           TruckStatus.Loading,
           TruckStatus.ToJob,
           TruckStatus.AtJob,
           TruckStatus.Returning
        };
    }
}