using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class EmailFactory : MonoBehaviour
{
    public InputField recipientEmailField, nameField, ageField;
    public Dropdown genderDropdown; 

    private string emailBody, gender;

    [SerializeField] UI_Controller uiController;
    [SerializeField] GameObject errorPanel, successPanel;
    [SerializeField] Text errorText;

    private void Start() {
        errorPanel.SetActive(false);
        successPanel.SetActive(false);
    }

    string GetGender(){
        switch (genderDropdown.value)
        {
            case 1: return "Masculino";

            case 2: return "Femenino";

            case 3: return "Otro";

            default: return "";
        }
    }

    bool IsValidEmail(string address){
        try
        {
            MailAddress m = new MailAddress(address);

            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public void SendEmail()
    {
        emailBody = uiController.resultsTotal + "\n" + uiController.resultsDetail;
        gender = GetGender();
        
        if (recipientEmailField.text != "" && nameField.text != "" && ageField.text != "" && gender != ""){
            if (IsValidEmail(recipientEmailField.text)){
                if (System.Convert.ToInt32(ageField.text) > 0){
                    try
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("mail.smtp2go.com", 2525);
                        SmtpServer.Timeout = 10000;
                        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                        SmtpServer.UseDefaultCredentials = false;

                        mail.From = new MailAddress("mentalzhapp@gmail.com", "Equipo MentAlzh");
                        mail.To.Add(new MailAddress(recipientEmailField.text));
                        mail.Bcc.Add(new MailAddress("mentalzhapp@gmail.com"));

                        
                        mail.Subject = "Reporte de resultados MentAlzh";
                        mail.Body = "¡Gracias por usar MentAlzh! A continuación se muestran sus resultados \n \n"
                                    + "Información personal:"
                                    + "\nNombre: " + nameField.text
                                    + "\nEdad: " + ageField.text
                                    + "\nSexo: " + gender
                                    + "\n\n" + emailBody;
                        

                        SmtpServer.Credentials = new System.Net.NetworkCredential("mentalzh", "proyecto_mentalzh") as ICredentialsByHost; SmtpServer.EnableSsl = true;
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };

                        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                        SmtpServer.Send(mail);
                        successPanel.SetActive(true);
                        recipientEmailField.text = ""; 
                        nameField.text = "";
                    }
                    catch (System.Exception ex)
                    {
                        errorText.text = "Ha ocurrido un error: \n" + ex.Message;
                        errorPanel.SetActive(true);
                    }
                }
                else{
                    errorText.text = "Por favor ingrese una edad válida";
                    errorPanel.SetActive(true);
                }
                
            }
            else{
                errorText.text = "Por favor ingrese una dirección de correo electrónico válida.";
                errorPanel.SetActive(true);
            }
        }
        else{
            errorText.text = "Por favor llene los campos obligatorios para continuar.";
            errorPanel.SetActive(true);
        }
        
    }
}