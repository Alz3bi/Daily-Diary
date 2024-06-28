using DiaryManager;
using System.Xml.Schema;

namespace DiaryManagerTests
{
    public class UnitTest1
    {
        [Fact]
        public void ReadTextCase()
        {
            // Arrange
            string expected = File.ReadAllText("../../../mydiary.txt");
            // Act
            string resault = DailyDiary.ReadEntries();

            // Assert
            Assert.Equal(expected, resault);
        }
        [Fact]
        public void AddEntryTestCase()
        {
            // Arrange
            int totalEntries = DailyDiary.GetTotalEntries();
            Entry entry = new Entry();
            entry.Date = "2021-09-01";
            entry.Content = "This is a test";
            // Act
            int resault = DailyDiary.AddEntryToText(entry);

            // Assert
            Assert.NotEqual(totalEntries, resault);
        }
    }
}