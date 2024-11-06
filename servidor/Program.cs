using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;

namespace Servidor
{
    class Program
    {
        static int maxSeats;
        static int availableSeats;
        static int ticketCounter = 1;

        static void Main(string[] args)
        {
            Console.Write("Enter the maximum number of seats available: ");
            maxSeats = availableSeats = int.Parse(Console.ReadLine());

            StartServer();
        }

        static void StartServer()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            Console.WriteLine("Server started and waiting for connections...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected.");
                var clientThread = new System.Threading.Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string xmlRequest = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            string xmlResponse = ProcessRequest(xmlRequest);

            byte[] responseBytes = Encoding.UTF8.GetBytes(xmlResponse);
            stream.Write(responseBytes, 0, responseBytes.Length);

            client.Close();
        }

        static string ProcessRequest(string xmlRequest)
        {
            try
            {
                XElement request = XElement.Parse(xmlRequest);
                if (request.Name == "RequestTicket")
                {
                    int requestedSeats = int.Parse(request.Element("Seats").Value);

                    if (requestedSeats <= availableSeats)
                    {
                        availableSeats -= requestedSeats;
                        string ticketNumber = ticketCounter.ToString("D4");
                        ticketCounter++;

                        XElement response = new XElement("ResponseTicket",
                            new XElement("TicketNumber", ticketNumber),
                            new XElement("SeatsAllocated", requestedSeats)
                        );

                        Console.WriteLine($"Ticket {ticketNumber} issued for {requestedSeats} seats.");
                        return response.ToString();
                    }
                    else
                    {
                        XElement error = new XElement("ErrorExpedicionTicket",
                            new XElement("Message", "Not enough seats available.")
                        );
                        Console.WriteLine("Error: Not enough seats available.");
                        return error.ToString();
                    }
                }
                else
                {
                    XElement error = new XElement("ErrorExpedicionTicket",
                        new XElement("Message", "Invalid request.")
                    );
                    Console.WriteLine("Error: Invalid request.");
                    return error.ToString();
                }
            }
            catch (Exception ex)
            {
                XElement error = new XElement("ErrorExpedicionTicket",
                    new XElement("Message", ex.Message)
                );
                Console.WriteLine($"Error: {ex.Message}");
                return error.ToString();
            }
        }
    }
}
