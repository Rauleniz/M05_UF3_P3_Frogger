using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace M05_UF3_P3_Frogger
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Comienza el juego! Pulsa una tecla!");

            // llamamos al Player
            Player player = new Player();
            

            // llamamos a DynamicElement
            DynamicElement dynamicElement = new DynamicElement(new Vector2Int(1, 1), new Vector2Int(10, 10), '*', ConsoleColor.Yellow);

            //llamamos Lane                        
            //creamos una lista para guardar los carriles. Añadiremos carriles a la lista con el .add
            //pasamos los parámetros definidos en el constructor de la clase Lane:
            //Lane(int posY, bool speedPlayer, ConsoleColor background, bool damageElements, bool damageBackground, float elementsPercent, char elementsChar, List<ConsoleColor> colorsElements)

            List<Lane> lanes = new List<Lane>();
            lanes.Add(new Lane(0, false, ConsoleColor.Green, false, false, 0.0f,' ', new List<ConsoleColor> { ConsoleColor.White }));
            lanes.Add(new Lane(1, false, ConsoleColor.Blue, true, false, 0.3f, '=', new List<ConsoleColor> { ConsoleColor.DarkYellow, ConsoleColor.White }));
            lanes.Add(new Lane(2, true, ConsoleColor.Blue, true, false, 0.5f, '=', new List<ConsoleColor> { ConsoleColor.DarkYellow, ConsoleColor.White }));
            lanes.Add(new Lane(3, false, ConsoleColor.Blue, true, false, 0.7f, '=', new List<ConsoleColor> { ConsoleColor.DarkYellow, ConsoleColor.White }));
            lanes.Add(new Lane(4, false, ConsoleColor.Blue, true, false, 0.6f, '=', new List<ConsoleColor> { ConsoleColor.DarkYellow, ConsoleColor.White }));
            lanes.Add(new Lane(5, true, ConsoleColor.Blue, true, false, 0.7f, '=', new List<ConsoleColor> { ConsoleColor.DarkYellow, ConsoleColor.White }));
            lanes.Add(new Lane(6, false, ConsoleColor.Green, false, false, 0.0f, ' ', new List<ConsoleColor> { ConsoleColor.White }));
            lanes.Add(new Lane(7, false, ConsoleColor.Black, true, false, 0.4f, '╫', new List<ConsoleColor> { ConsoleColor.Cyan, ConsoleColor.Magenta }));
            lanes.Add(new Lane(8, true, ConsoleColor.Black, true, false, 0.3f, '╫', new List<ConsoleColor> { ConsoleColor.DarkYellow, ConsoleColor.Magenta }));
            lanes.Add(new Lane(9, false, ConsoleColor.Black, true, false, 0.3f, '╫', new List<ConsoleColor> { ConsoleColor.DarkGray, ConsoleColor.Magenta }));
            lanes.Add(new Lane(10, false, ConsoleColor.Black, true, false, 0.0f, '╫', new List<ConsoleColor> { ConsoleColor.Yellow, ConsoleColor.Magenta }));
            lanes.Add(new Lane(11, true, ConsoleColor.Black, true, false, 0.2f, '╫', new List<ConsoleColor> { ConsoleColor.DarkYellow, ConsoleColor.Magenta }));
            lanes.Add(new Lane(12, false, ConsoleColor.Green, false, false, 0.0f, ' ', new List<ConsoleColor> { ConsoleColor.White }));


            Utils.GAME_STATE gameState = Utils.GAME_STATE.RUNNING;



            while (gameState == Utils.GAME_STATE.RUNNING)
            {
                //Inputs
                TimeManager.NextFrame();
                dynamicElement.Update();


                // Movimientos del Player(x, y)  
                // ReadKey para que el player sólo se mueva cuando el usuario pulsa las teclas
                ConsoleKeyInfo keyPress = Console.ReadKey(true);
                Vector2Int direction = new Vector2Int(0, 0);

                // llamamos a utils charcars para darle un movimiento diferente al del player
                Vector2Int charCars = new Vector2Int();
                Vector2Int charLogs = new Vector2Int();

                //charCars.x = Utils.rnd.Next(); 
                //charCars.y = Utils.rnd.Next();
                Vector2Int.ReferenceEquals(charCars, direction);
                //Vector2Int position;
                direction.x = Utils.rnd.Next(Utils.MAP_WIDTH);

                

                         
                
                                
                
                // definir la posición del player para que quede siempre dentro del mapa
                if (player.pos.x < 0)
                {
                    player.pos.x = 0;
                }
                else if (player.pos.x >= Utils.MAP_WIDTH)
                {
                    player.pos.x = Utils.MAP_WIDTH - 1;
                }

                if (player.pos.y < 0)
                {
                    player.pos.y = 0;
                }
                else if (player.pos.y >= Utils.MAP_HEIGHT)
                {
                    player.pos.y = Utils.MAP_HEIGHT - 1;
                }



                direction = Utils.Input();
                //Vector2Int direction = Utils.Input();
                //player.pos += direction;

                switch (keyPress.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        direction.y = -1;
                        // player.character = Player.characterForward;                        
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        direction.y = 1;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        direction.x = 1;
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        direction.x = -1;
                        break;
                }

                player.Update(direction);

                
                //Lane damage = new Lane ();


                // Dibujado

                foreach (Lane lane in lanes)
                {
                    lane.Draw();
                    foreach (Element element in lane.elements)
                    {
                        element.Update();
                    }
                    lane.Draw();
                }
                player.Draw();

                // ralentizar el movimiento de los objetos en milisegundos
                Thread.Sleep(100);


            }

            // mensaje de si hemos ganado o perdido:
         

            if (player.pos.y == Utils.MAP_HEIGHT - 1)
            {
                Console.WriteLine("Has ganado!");
                
            }

            //foreach (Lane car in cars) { 
            
            //    if (player.pos == car.position)
            //    {
            //        gameState = Utils.GAME_STATE.LOOSE;
            //        Console.WriteLine("Has perdido");
            //        break;
            //    }
            //}


            // Esperamos a que el usuario presione cualquier tecla para salir
            Console.ReadKey();


        }
    }
}
