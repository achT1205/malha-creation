namespace Ordering.Application.Orders.Helpers;
public class OrderCodeGenerator
{
    public static string GenerateOrderCode(Guid customerId)
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmss");

        string guidPrefix = customerId.ToString().Substring(0, 8);

        string randomString = GenerateRandomString(6);

        // 4. Concatenate the parts to form the final OrderCode
        string orderCode = $"{timestamp}-{guidPrefix}-{randomString}";

        return orderCode;
    }

    // Method to generate a random alphanumeric string of a given length
    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        char[] result = new char[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }

        return new string(result);
    }
}

