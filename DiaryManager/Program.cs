namespace DiaryManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DailyDiary.Interface();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
