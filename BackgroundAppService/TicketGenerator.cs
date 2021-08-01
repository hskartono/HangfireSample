using System;

namespace BackgroundAppService
{
    public class TicketGenerator
    {
        public bool RegisterCreateNewTicket(string filePath)
        {
            var result = Hangfire.BackgroundJob.Enqueue(() => GenerateNewTicket(filePath));
            return true;
        }

        public bool GenerateNewTicket(string fileLocation)
        {
            string newTicketName = Guid.NewGuid().ToString();
            string ticketContent = $"Some random content for ticket {newTicketName}";
            string fileName = System.IO.Path.Combine(fileLocation, newTicketName + ".txt");
            System.IO.File.WriteAllText(fileName, ticketContent);
            return true;
        }
    }
}
