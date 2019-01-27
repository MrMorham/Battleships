using System;
using System.Collections.Generic;

namespace Battleships
{
    class Program
    {
        static void Main (string[] args)
        {
            //Title
            Console.Title = "BATTLESHIP COMMANDER";
            //Set Console Size for Game
            Console.SetWindowSize(150, 31);
            Console.SetBufferSize(150, 31);
            Console.CursorSize = 100;
            //Win Condition
            int Open = 1;
            int Game = 0;
            //Player Name
            string PlayerName = "DEFAULT";
            Player PlayerShip = new Player();
            //Allows creation and destruction of Enemies
            EnemyManager Manager = new EnemyManager();

            while (Open == 1)
            {

                switch (Game)
                {
                    //TITLE SCREEN
                    case 0:
                        while (Game == 0)
                        {
                            Console.Clear();
                            Console.CursorVisible = true;

                            void Centre (int length)
                            {
                                Console.CursorLeft = (Console.WindowWidth / 2) - length;
                            }

                            string TitleLine1 = "##### ##### ##### ##### ##    ##### ##### ## ## ##### #####";
                            string TitleLine2 = "##  # ## ## ##### ##### ##    ##    ##    ## ##  ###  ##  #";
                            string TitleLine3 = "##### #####  ###   ###  ##    ##### ##### #####  ###  #####";
                            string TitleLine4 = "##  # ## ##  ###   ###  ##    ##       ## ## ##  ###  ##   ";
                            string TitleLine5 = "##### ## ##  ###   ###  ##### ##### ##### ## ## ##### ##   ";
                            string TitleLine6 = "### ### ## ## ## ## ### ##  # ##  ### ###";
                            string TitleLine7 = "#   # # # # # # # # # # # # # # # ##  ## ";
                            string TitleLine8 = "### ### #   # #   # # # #  ## ##  ### # #";
                            string TitleLine9 = "<< PLEASE ENTER YOUR NAME COMMANDER >>";
                            string TitleLine10 = "COMMANDER ";

                            Centre(TitleLine1.Length / 2);
                            Console.WriteLine(TitleLine1); Centre(TitleLine2.Length / 2);
                            Console.WriteLine(TitleLine2); Centre(TitleLine3.Length / 2);
                            Console.WriteLine(TitleLine3); Centre(TitleLine4.Length / 2);
                            Console.WriteLine(TitleLine4); Centre(TitleLine5.Length / 2);
                            Console.WriteLine(TitleLine5 + "\n"); Centre(TitleLine6.Length / 2);
                            Console.WriteLine(TitleLine6); Centre(TitleLine7.Length / 2);
                            Console.WriteLine(TitleLine7); Centre(TitleLine8.Length / 2);
                            Console.WriteLine(TitleLine8 + "\n"); Centre(TitleLine9.Length / 2);
                            Console.WriteLine(TitleLine9); Centre((TitleLine10.Length / 2));
                            Console.Write(TitleLine10);
                            PlayerName = Console.ReadLine();
                            PlayerName = PlayerName.ToUpper();
                            Game = 1;
                        }
                        break;

                    //MAIN GAME LOOP
                    case 1:

                        for (int i = 0; i < 10; i++)
                        {
                            Manager.NewEnemy();
                        }

                        while (Game == 1)
                        {
                            Manager.CollectDead();
                            PlayerShip.PlayerShield.RechargeShield();
                            PlayerShip.PlayerShield.ReduceShield(20);

                            Console.Clear();
                            Console.WriteLine("{0,0} {1,-10}{2,-10}  {3,-10}\t{4,-10}{5,-10}{6,-10}", "       ", "SIGN", "CLASS", "STATUS", "BEARING", "DISTANCE", "VELOCITY");

                            RadarCallOut();
                            ShipReadOut();

                            Console.SetCursorPosition(0, 27);
                            Console.WriteLine(new string('-', Console.BufferWidth));
                            Console.Write($"YOUR ORDERS COMMANDER {PlayerName} >> ");
                            string inputAlpha = Console.ReadLine();
                            Console.Beep();

                            InterpretOrder(inputAlpha);

                            Manager.DestroyDead();
                            Manager.Move();
                            Manager.NewEnemy();
                        }
                        break;
                }
            }

            void RadarCallOut()
            {
                foreach (KeyValuePair<string, Enemy> instance in Manager.Enemies)
                {
                    Enemy Temp = instance.Value;
                    switch (Temp.Status)
                    {
                        case "DESTROYED":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("CONTACT {0,-10}{1,-10}  {2,-10}\t{3,-10}{4,-10}{5,-10}", instance.Key, Temp.Classification, Temp.Status, Temp.Bearing + " deg", Temp.Distance + " km", Temp.Velocity + " km/min");
                            break;
                        case "CRITICAL":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("CONTACT {0,-10}{1,-10}  {2,-10}\t{3,-10}{4,-10}{5,-10}", instance.Key, Temp.Classification, Temp.Status, Temp.Bearing + " deg", Temp.Distance + " km", Temp.Velocity + " km/min");
                            break;
                        case "DAMAGED":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("CONTACT {0,-10}{1,-10}  {2,-10}\t{3,-10}{4,-10}{5,-10}", instance.Key, Temp.Classification, Temp.Status, Temp.Bearing + " deg", Temp.Distance + " km", Temp.Velocity + " km/min");
                            break;
                        case "OPERATIONAL":
                            Console.ResetColor();
                            Console.WriteLine("CONTACT {0,-10}{1,-10}  {2,-10}\t{3,-10}{4,-10}{5,-10}", instance.Key, Temp.Classification, Temp.Status, Temp.Bearing + " deg", Temp.Distance + " km", Temp.Velocity + " km/min"); 
                            break;
                    }
                    Console.ResetColor();
                }
            }

            void ShipReadOut ()
            {
                string HealthTicks = new String('|', PlayerShip.Health / 25);
                string HealthBuffer = new String(' ', (1000 - PlayerShip.Health) / 25);
                string HealthBar = "[" + HealthTicks + HealthBuffer + "]";
                string ShieldTicks = new string('|', PlayerShip.PlayerShield.CurShield / 25);
                string ShieldBuffer = new string(' ', (PlayerShip.PlayerShield.MaxShield - PlayerShip.PlayerShield.CurShield) / 25);
                string ShieldBar = "[" + ShieldTicks + ShieldBuffer + "]";
                Console.SetCursorPosition(100, 0);
                Console.Write("SHIP REPORT");
                Console.SetCursorPosition(100,1);
                if(PlayerShip.Health <= 1000 && PlayerShip.Health >= 750)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(HealthBar);
                    Console.ResetColor();
                } else if (PlayerShip.Health < 750 && PlayerShip.Health >= 500)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(HealthBar);
                    Console.ResetColor();
                } else if (PlayerShip.Health < 500 && PlayerShip.Health >= 250)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(HealthBar);
                    Console.ResetColor();
                } else if (PlayerShip.Health < 250 && PlayerShip.Health >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(HealthBar);
                    Console.ResetColor();
                }
                Console.SetCursorPosition(100, 2);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(ShieldBar);
                Console.ResetColor();
                Console.SetCursorPosition(100, 3);
                Console.Write($"ALPHA TURRET: {PlayerShip.AlphaTurret.TurretBearing} ({PlayerShip. AlphaTurret.MinTurretDamage} ~ {PlayerShip.AlphaTurret.MaxTurretDamage}) [{PlayerShip.AlphaTurret.AmmoCount}] <{PlayerShip.AlphaTurret.LastDamageDone}>");
                Console.SetCursorPosition(100, 4);
                Console.Write($"BRAVO TURRET: {PlayerShip.BravoTurret.TurretBearing} ({PlayerShip.BravoTurret.MinTurretDamage} ~ {PlayerShip.BravoTurret.MaxTurretDamage}) [{PlayerShip.BravoTurret.AmmoCount}] <{PlayerShip.BravoTurret.LastDamageDone}>");

            }

            void InterpretOrder (string order)
            {
                var ToInterpret = order.Split(' ');

                switch (ToInterpret[0].ToUpper())
                {
                    case "ALPHA":
                        switch (ToInterpret[1].ToUpper())
                        {
                            case "BEARING":
                                if (ToInterpret.Length < 3 || ToInterpret.Length > 3)
                                {
                                    break;
                                }
                                else
                                {
                                    PlayerShip.AlphaTurret.TurretBearing = Int32.Parse(ToInterpret[2]);
                                }
                                break;
                            case "FIRE":
                                foreach(KeyValuePair<string, Enemy> instance in Manager.Enemies)
                                {
                                    if(instance.Value.Bearing == PlayerShip.AlphaTurret.TurretBearing)
                                    {
                                        instance.Value.ChangeHealth(PlayerShip.AlphaTurret.TurretDamage());
                                    }
                                }
                                PlayerShip.AlphaTurret.Shoot();
                                break;
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    case "BRAVO":
                        switch (ToInterpret[1].ToUpper())
                        {
                            case "BEARING":
                                if (ToInterpret.Length < 3 || ToInterpret.Length > 3)
                                {
                                    break;
                                }
                                else
                                {
                                    PlayerShip.BravoTurret.TurretBearing = Int32.Parse(ToInterpret[2]);
                                }
                                break;
                            case "FIRE":
                                foreach (KeyValuePair<string, Enemy> instance in Manager.Enemies)
                                {
                                    if (instance.Value.Bearing == PlayerShip.BravoTurret.TurretBearing)
                                    {
                                        instance.Value.ChangeHealth(PlayerShip.BravoTurret.TurretDamage());
                                    }
                                }
                                PlayerShip.BravoTurret.Shoot();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "FIRE":
                        foreach(KeyValuePair<string, Enemy> instance in Manager.Enemies)
                        {
                            if(instance.Value.Bearing == PlayerShip.AlphaTurret.TurretBearing)
                            {
                                instance.Value.ChangeHealth(PlayerShip.AlphaTurret.TurretDamage());
                            }
                            if(instance.Value.Bearing == PlayerShip.BravoTurret.TurretBearing)
                            {
                                instance.Value.ChangeHealth(PlayerShip.BravoTurret.TurretDamage());
                            }
                        }
                        PlayerShip.AlphaTurret.Shoot();
                        PlayerShip.BravoTurret.Shoot();
                        break;
                    case "RELOAD":
                        PlayerShip.AlphaTurret.Reload();
                        PlayerShip.BravoTurret.Reload();
                        break;
                    case "HELP":
                        Console.SetCursorPosition(100, 22);
                        Console.Write("COMMANDS");
                        Console.SetCursorPosition(100, 23);
                        Console.Write("<TURRET> BEARING <###>");
                        Console.SetCursorPosition(100, 24);
                        Console.Write("<TURRET> FIRE");
                        Console.SetCursorPosition(100, 25);
                        Console.Write("FIRE");
                        Console.Read();
                        break;
                    default:
                        break;
                }
            }
            
        }
    }
}