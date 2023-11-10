namespace KUBC.DAYZ.GAME.MSTEST
{
    /// <summary>
    /// ����� ������� ������� �������
    /// </summary>
    [TestClass]
    public class GameLogs
    {
        /// <summary>
        /// ��������� ��� ADM ����
        /// </summary>
        [TestMethod]
        public void AdminLog()
        {
            var fLog = new FileInfo("TestFiles\\GameLogs\\DayZServer_x64_2023_08_30_215815690.ADM");
            var tLog = new LogFiles.ADM.Log(fLog);
            Console.WriteLine($"�� ����� {fLog.Name} ��������� {tLog.Read()} �����");
            Console.WriteLine("���� ������� �������:");
            Console.WriteLine($"������� ������ �� ������ ����� {tLog.BledOuts.Count} �������");
            Console.WriteLine($"������� ������������� �������� {tLog.Builts.Count} �������");
            Console.WriteLine($"������� ��������� �������� {tLog.Dismantleds.Count} �������");
            Console.WriteLine($"������ �� ������� ������� {tLog.Errors.Count} �������");
            Console.WriteLine($"������ ������� ������� {tLog.LogPlayers.Count} �������");
            Console.WriteLine($"������� ���������� ��������� �������� {tLog.Placeds.Count} �������");
            Console.WriteLine($"��������� � ������� ��� {tLog.PlayerChat.Count} �������");
            Console.WriteLine($"������� ����������� ������� {tLog.PlayerConnects.Count} �������");
            Console.WriteLine($"������� ����� �������� ������� {tLog.PlayerDamages.Count} �������");
            Console.WriteLine($"������� ������� {tLog.PlayerDieds.Count} �������");
            Console.WriteLine($"������� ���������� ������� {tLog.PlayerDisconnects.Count} �������");
            Console.WriteLine($"������� ������� ������ ������ ������� {tLog.PlayerKilleds.Count} �������");
            Console.WriteLine($"������� ����� ����� ������� ����� ������ �������� {tLog.PlayerRegained.Count} �������");
            Console.WriteLine($"��������� ������������� ������� �� ������� {tLog.PlayersReport.Count} �������");
            Console.WriteLine($"������� ����� ����� ������� �������� {tLog.PlayerUnconscious.Count} �������");
            Console.WriteLine($"������������ ������� {tLog.Suicides.Count} �������");
        }
    }
}