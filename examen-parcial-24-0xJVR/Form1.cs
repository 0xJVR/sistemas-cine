using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace examen_parcial_24_0xJVR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void requestButton_Click(object sender, EventArgs e)
        {
            int seatsRequested = (int)seatsNumericUpDown.Value;
            string xmlRequest = CreateXmlRequest(seatsRequested);

            string xmlResponse = SendRequest(xmlRequest);
            if (!string.IsNullOrEmpty(xmlResponse))
            {
                ProcessResponse(xmlResponse);
            }
        }

        private string CreateXmlRequest(int seats)
        {
            XElement request = new XElement("RequestTicket",
                new XElement("Seats", seats)
            );
            return request.ToString();
        }

        private string SendRequest(string xmlRequest)
        {
            try
            {
                TcpClient client = new TcpClient("localhost", 5000);
                NetworkStream stream = client.GetStream();

                byte[] requestBytes = Encoding.UTF8.GetBytes(xmlRequest);
                stream.Write(requestBytes, 0, requestBytes.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string xmlResponse = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                client.Close();
                return xmlResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Conexion rechazada: {ex.Message}", "Error de conexion, comprueba que el servidor esta listo.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void ProcessResponse(string xmlResponse)
        {
            try
            {
                XElement response = XElement.Parse(xmlResponse);
                if (response.Name == "ResponseTicket")
                {
                    string ticketNumber = response.Element("TicketNumber").Value;
                    int seatsAllocated = int.Parse(response.Element("SeatsAllocated").Value);
                    MessageBox.Show($"Ticket #{ticketNumber} con {seatsAllocated} plaza(s).", "Ticket Creado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (response.Name == "ErrorExpedicionTicket")
                {
                    string errorMessage = response.Element("Message").Value;
                    MessageBox.Show($"Error: {errorMessage}", "Ticket Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Respuesta invalida del servidor.", "Respuesta desconocida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error procesando peticion: {ex.Message}", "Error de peticion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}