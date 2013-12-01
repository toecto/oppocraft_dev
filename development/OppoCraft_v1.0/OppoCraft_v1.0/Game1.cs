using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using testClient;
using System.Diagnostics;

namespace OppoCraft
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {        
        public RenderSystem render;
        public GraphContentManager graphContent;

        //Cells, Map, and Coordinate Properties
        public WorldCoords cellSize;
        public WorldCoords worldMapSize;
        public Grid theGrid;
        public PathFinder pathFinder;

        //debug test
        public Debugger debugger;

        //Mouse Movement testing
        public MouseState mouseState;
        public MouseState prevMouseState;
        int scrollValue = 0;

   

        private NetworkModule network;
        public GameMap map;
        public MessageHandler messageHandler;

        public int cid;
        public int enemyCid;
        int UIDCnt = 0;
        public bool running=false;

        public string loadMap;


        //Testing properties
        public int myFirstUnit;
        
        public Database db;
        public UserInputSystem userInput;

        

        public Game1(NetworkModule net, int cid, int enemyCid,string map)
        {
            this.debugger = new Debugger(this);
            this.loadMap = map;
            this.cid = cid;
            this.enemyCid = enemyCid;
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.network = net;
            this.messageHandler = new MessageHandler(this,this.network);
            this.db = new Database("Data Source=OppoClient.sdf");
            //this.OnExiting+=
            //Mouse Scrolling testing
            this.mouseState = Mouse.GetState();
            this.prevMouseState = mouseState;

            this.cellSize = new WorldCoords(40, 40);
            this.worldMapSize = new WorldCoords(4600, 4600); // set back to 10240/10240

            this.userInput = new UserInputSystem(this);
            this.render = new RenderSystem(this);
            this.graphContent = new GraphContentManager(this);
            this.theGrid = new Grid(this);
            this.pathFinder = new PathFinder(this.theGrid);
            this.map = new GameMap(this);

            
            //unit.task.Add(new _Movement(unit, new WorldCoords(500, 500)));

            //Testing setting up obstacles
            //this.theGrid.fillRectValues(new GridCoords(1, 3), new Coordinates(10, 1), -1);
            //this.theGrid.fillRectValues(new GridCoords(10, 5), new Coordinates(10, 1), -1);
            //this.theGrid.fillRectValues(new GridCoords(1, 7), new Coordinates(10, 1), -1);
            //Testing the Path Finder Algorithm     
            //Path finding test
 
		/*
            for (int x = 0; x < this.theGrid.gridValues.GetLength(0); x++)
            {
                for (int y = 0; y < this.theGrid.gridValues.GetLength(1); y++)
                {
                    this.debugger.AddMessage("(" + x + ", " + y + "): " + this.theGrid.gridValues[x, y].ToString());
                }
            }
            /**/

        }

        public int CreateUID()
        {
            this.UIDCnt++;
            return int.Parse(this.cid + "" + this.UIDCnt);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {                       
            this.render.LoadContent();
            OppoMessage msg = new OppoMessage(OppoMessageType.CreateUnit);
            this.map.Add(new PathFinderTest(this.CreateUID()));
            this.map.Add(new Background(this.CreateUID()));
            this.map.Add(new MiniMap(this.CreateUID()));

            if(this.loadMap!=null)
            {
                this.LoadMap();
                this.AddCommand(new OppoMessage(OppoMessageType.StartGame));
            }
            
        }


        public void LoadMap()
        {
        //Testing setting up obstacles
            //this.theGrid.fillRectValues(new GridCoords(1, 3), new Coordinates(10, 1), -1);
            //this.theGrid.fillRectValues(new GridCoords(10, 5), new Coordinates(10, 1), -1);
            //this.theGrid.fillRectValues(new GridCoords(1, 7), new Coordinates(10, 1), -1);
            
            

            for (int i = 1; i < 40; i++)
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.CreateUnit);
                msg["uid"] = this.myFirstUnit = this.CreateUID();
                msg["ownercid"] = this.cid;
                msg["x"] = 70 * i + 50;
                msg["y"] = 50 * i + 100;
                msg.Text["type"] = "Knight";
                this.AddCommand(msg);
            }
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                OppoMessage msg1 = new OppoMessage(OppoMessageType.CreateUnit);
                msg1["uid"] = this.myFirstUnit = this.CreateUID();
                msg1["ownercid"] = 0;
                msg1["x"] = 40 * rnd.Next(1,this.theGrid.gridSize.X-2);
                msg1["y"] = 40 * rnd.Next(1, this.theGrid.gridSize.Y-2);
                msg1.Text["type"] = "Tree";
                this.AddCommand(msg1);
            }
            for (int i = 1; i < 40; i++)
            {
                OppoMessage msg = new OppoMessage(OppoMessageType.CreateUnit);
                msg["uid"] = this.myFirstUnit = this.CreateUID();
                msg["ownercid"] = this.enemyCid;
                msg["x"] = 70 * i + 300;
                msg["y"] = 50 * i + 100;
                msg.Text["type"] = "Knight";
                this.AddCommand(msg);
            }


        }

        


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            this.network.Stop();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Keys[] keys =this.userInput.keyboard.GetPressedKeys();
            //debugger.AddMessage(String.Join("",keys));

            if (this.userInput.keyboard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }


            if (this.userInput.isKeyPressed(Keys.F10))
            {
                this.render.ToggleFullScreen();
            }

            // TODO: Add your update logic here

            this.mouseState = Mouse.GetState();
            this.scrollValue += (this.prevMouseState.ScrollWheelValue - this.mouseState.ScrollWheelValue) / 12;
            this.prevMouseState = this.mouseState;
            this.debugger.scrollRow = scrollValue;


            this.userInput.Tick();
            messageHandler.Tick();
            if(this.running)
                this.map.Tick();

            if (!this.network.Flush())
                this.debugger.AddMessage("Lost connection to server");
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {         
            this.render.Render(gameTime);
            base.Draw(gameTime);
        }


        public void AddCommand(OppoMessage msg)
        {
            //msg["cid"] = this.cid;
            this.network.Send(msg);
        }


    }
}
