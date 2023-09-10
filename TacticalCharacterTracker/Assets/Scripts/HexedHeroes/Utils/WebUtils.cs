using System.Net.NetworkInformation;

public static class WebUtils
{
    public static bool IsInternetAvailable()
    {
        try
        {
            using Ping ping = new Ping();
            PingReply reply = ping.Send("8.8.8.8");
            return reply.Status == IPStatus.Success;
        }
        catch (PingException)
        {
            return false;
        }
    }
}
