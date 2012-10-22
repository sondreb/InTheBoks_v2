namespace InTheBoks.Command
{
    using System.Collections.Generic;
    using System.Linq;

    public class CommandResults : ICommandResults
    {
        private readonly List<ICommandResult> results = new List<ICommandResult>();

        public ICommandResult[] Results
        {
            get
            {
                return this.results.ToArray();
            }
        }

        public bool Success
        {
            get
            {
                return this.results.All<ICommandResult>(result => result.Success);
            }
        }

        public void AddResult(ICommandResult result)
        {
            this.results.Add(result);
        }
    }
}