using DO;

namespace DalApi
{
    public interface IDal
    {
        double GetTemparture(int day);
        WindDirection GetWindDirection(int day);

        object GetLock();
        void Shutdown();
    }
}
