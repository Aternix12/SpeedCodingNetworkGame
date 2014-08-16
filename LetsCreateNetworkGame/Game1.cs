﻿//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
#region Using Statements

using LetsCreateNetworkGame.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace LetsCreateNetworkGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private ManagerNetwork _managerNetwork;
        private ManagerInput _managerInput; 
        private Color _color; //For test
        private Texture2D _texture; //For test
        private SpriteFont _font; //For test

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _managerNetwork = new ManagerNetwork();
            _managerInput = new ManagerInput(_managerNetwork);
            _color = Color.CornflowerBlue;
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

            _managerNetwork.Start();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("white_background");
            _font = Content.Load<SpriteFont>("font");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _managerNetwork.Update();
            _managerInput.Update(gameTime.ElapsedGameTime.Milliseconds);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_managerNetwork.Active ? Color.Green : Color.Red);

            spriteBatch.Begin();
            if (_managerNetwork.Active)
            {
                foreach (var player in _managerNetwork.Players)
                {
                    spriteBatch.Draw(_texture,new Rectangle(player.XPosition,player.YPosition,100,50),Color.BlueViolet);
                    if (player.Username != _managerNetwork.Username)
                    {
                        spriteBatch.DrawString(_font, string.Format("<{0}>",player.Username), new Vector2(player.XPosition + 20, player.YPosition + 20), Color.Black);
                    }
                }            
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
