namespace DemandManagement.MessageContracts.Consts
{
    public class RabbitMqConsts
    {
        public static string RabbitMqUri = "rabbitmq://localhost/demand/";
        public static string UserName = "guest";
        public static string Password = "123456";
        public static string RegisterDemandServiceQueue = "registerdemand.service";
        public static string NotificationServiceQueue = "notification.service";
        public static string ThirdPartyServiceQueue = "thirdparty.service";

    }
}
