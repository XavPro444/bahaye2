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


namespace AtelierXNA
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MenuJeu : DrawableGameComponent
    {
        const float INTERVALLE_MAJ_STANDARD = 1f / 60f;
        const int DIMENSION_TERRAIN = 256;
        const int NB_ZONES_DIALOGUE = 3; //Cette constante doit valoir 3 au minimum
        string NomImageFond { get; set; }
        Rectangle RectangleDialogue { get; set; }
        BoutonDeCommande BtnMode { get; set; }
        BoutonDeCommande BtnPause { get; set; }
        BoutonDeCommande BtnExit { get; set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
        public InputManager GestionInput { get; private set; }
        public bool EstActif { get; set; }
        protected SpriteBatch GestionSprites { get; private set; }
        string NomImage { get; set; }
        Rectangle ZoneAffichage { get; set; }
        protected Texture2D ImageDeFond { get; private set; }
        Jeu LeJeu { get; set; }
        public MenuJeu(Game jeu, string nomImageFond, Rectangle rectangleDialogue, bool estActif)
            : base(jeu)
        {
            RectangleDialogue = rectangleDialogue;
            EstActif = estActif;
            NomImage = nomImageFond;
        }

        public override void Initialize()
        {
            int hauteurBouton = RectangleDialogue.Height / (NB_ZONES_DIALOGUE + 1);


            Vector2 PositionBouton = new Vector2(RectangleDialogue.X + RectangleDialogue.Width / 2f,
                                                 RectangleDialogue.Y + (NB_ZONES_DIALOGUE - 2) * hauteurBouton);
            BtnMode = new BoutonDeCommande(Game, "Mode God", "", "BoutonRouge", "BoutonBleu", PositionBouton, true, EstActif, GérerModeCaméra);

            PositionBouton = new Vector2(RectangleDialogue.X + RectangleDialogue.Width / 2f,
                                                 RectangleDialogue.Y + (NB_ZONES_DIALOGUE - 1) * hauteurBouton);
            BtnPause = new BoutonDeCommande(Game,  "Pause", "", "BoutonRouge", "BoutonBleu", PositionBouton, true, EstActif, GérerPause);

            PositionBouton = new Vector2(RectangleDialogue.X + RectangleDialogue.Width / 2f,
                                                 RectangleDialogue.Y + NB_ZONES_DIALOGUE * hauteurBouton);
            BtnExit = new BoutonDeCommande(Game, "Quitter", "", "BoutonRouge", "BoutonBleu", PositionBouton,true, EstActif, Quitter);

            Game.Components.Add(BtnMode);
            Game.Components.Add(BtnPause);
            Game.Components.Add(BtnExit);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
            GestionSprites = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            ImageDeFond = GestionnaireDeTextures.Find(NomImage);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (GestionInput.EstNouvelleTouche(Keys.Tab))
            {
                EstActif = !EstActif;
                foreach (Caméra caméra in Game.Components.Where(x => x is Caméra))
                {
                    caméra.Enabled = !caméra.Enabled;
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if(EstActif)
            {
                GestionSprites.Begin();
                GestionSprites.Draw(ImageDeFond, ZoneAffichage, Color.White);
                GestionSprites.End();
            }

        }

        public void GérerModeCaméra()
        {

        }
        public void GérerPause()
        {

        }

        public void Quitter()
        {
            Game.Exit();
        }
    }
}
