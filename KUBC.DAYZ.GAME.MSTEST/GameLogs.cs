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
            var fLog = new FileInfo("TestFiles\\GameLogs\\LOG.ADM");
            var tLog = new LogFiles.ADM.Log(fLog);
            Console.WriteLine($"�� ����� {fLog.Name} ��������� {tLog.Read()} �����");
            if (tLog.Errors.Count> 0 )
            {
                Console.WriteLine($"��� ������ ���� ���� {tLog.Errors} ������");
                foreach(var e in tLog.Errors ) 
                {
                    Console.WriteLine("==========================================================");
                    Console.WriteLine($"�������� ������� [{e.SourceLine}]");
                    Console.WriteLine($"������:{e.Exception?.Message}");
                }
            }
            else
            {
                Console.WriteLine("��� ������ �� ���� ��������");
            }
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
            tLog.CloseFile();
        }

        
        
    }
}