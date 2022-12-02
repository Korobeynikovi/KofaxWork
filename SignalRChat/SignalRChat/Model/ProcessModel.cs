namespace SignalRChat.Model
{
	public class ProcessModel
	{
		public ProcessModel(string? name, int id, long memory, DateTime? startTime)
		{
			Name = name;
			Id = id;
			Memory = memory;
			StartTime = startTime;
		}

		public string? Name { get; set; }
		public int Id { get; set; }
		public long Memory { get; set; }
		public DateTime? StartTime { get; set; }
	}
}
